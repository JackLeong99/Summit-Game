using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

public class CreationEditorWindow : EditorWindow
{
    protected string scriptableName = "";

    protected string selectedPath = "Assets";
    protected System.Type selectedType = typeof(ScriptableObject);

    protected string typeName = "New Type";
    protected Rect typeButton = new Rect();

    private string createdPath = "";

    private void OnEnable()
    {
        ShowPopup();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Folder", GUILayout.MaxWidth(150)))
        {
            string basePath = EditorUtility.OpenFolderPanel("Select folder to mask path.", selectedPath, "");

            if (basePath.Contains(Application.dataPath))
            {
                basePath = basePath.Substring(basePath.LastIndexOf("Assets"));
            }

            if (AssetDatabase.IsValidFolder(basePath))
            {
                selectedPath = basePath;
            }
        }

        EditorGUILayout.LabelField(selectedPath);
        EditorGUILayout.EndHorizontal();

        if (EditorGUILayout.DropdownButton(new GUIContent(typeName), FocusType.Keyboard))
        {
            GenericMenu menu = new GenericMenu();

            var function = new GenericMenu.MenuFunction2((type) => { selectedType = (System.Type)type; typeName = type.ToString(); if (selectedType == typeof(ScriptableObject)) typeName = "New Type"; });

            menu.AddItem(new GUIContent("New Type"), OfType(typeof(ScriptableObject)), function, typeof(ScriptableObject));
            menu.AddSeparator("");

            foreach (var item in AssemblyTypes.GetAllTypes())
            {
                menu.AddItem(new GUIContent(item.ToString()), OfType(item), function, item);
            }
            menu.DropDown(typeButton);
        }
        if (Event.current.type == EventType.Repaint) typeButton = GUILayoutUtility.GetLastRect();

        scriptableName = EditorGUILayout.TextField(scriptableName);
        GUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            createdPath = Application.dataPath + "/" + selectedPath.Replace("Assets/", "") + "/" + scriptableName + ".cs";

            switch (true)
            {
                case bool _ when scriptableName.Length <= 0:
                    EditorUtility.DisplayDialog("Error: File Name Required", "The " + selectedType + " file name can not be left empty.", "Ok");
                    break;
                case bool _ when !scriptableName.All(char.IsLetterOrDigit):
                    EditorUtility.DisplayDialog("Error: File Name Required", "The " + selectedType + " file name can not contain invalid characters.", "Ok");
                    break;
                case bool _ when selectedType == typeof(ScriptableObject):
                    var template = Resources.Load<TextAsset>("TemplateScriptable");
                    string contents = template.text;
                    contents = contents.Replace("DefaultName", scriptableName);

                    StreamWriter sw = new StreamWriter(createdPath);
                    sw.Write(contents);
                    sw.Close();
                    AssetDatabase.Refresh();
                    Created(true);
                    break;
                default:
                    var type = selectedType;
                    Object newScriptable = AssemblyTypes.CreateObject(type);
                    AssetDatabase.CreateAsset(newScriptable, selectedPath + "/" + scriptableName + ".asset");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    Created(false);
                    break;
            }
        }

        if (GUILayout.Button("Close")) { Close(); }
        EditorGUILayout.EndHorizontal();
    }

    public void Created(bool newType)
    {
        switch (newType)
        {
            case true:
                switch (EditorUtility.DisplayDialog(typeName + " Created! ", typeName + " '" + scriptableName + "' has been successfully created! Would you like to open and modify it's contents?", "Yes", "No"))
                {
                    case true:
                        Application.OpenURL(createdPath);
                        Close();
                        break;
                    case false:
                        Close();
                        break;
                }
                break;
            case false:
                switch (EditorUtility.DisplayDialog(typeName + " Created! ", typeName + " '" + scriptableName + "' has been successfully created!", "Ok"))
                {
                    case true:
                        Close();
                        break;
                }
                break;
        }

    }

    protected bool OfType(System.Type type)
    {
        return selectedType == type;
    }
}
