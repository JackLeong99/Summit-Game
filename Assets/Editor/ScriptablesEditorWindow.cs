using System.Linq;
using UnityEngine;
using UnityEditor;

public class SriptablesEditorWindow : EditorWindow
{

    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected ScriptableObject[] activeObjects;
    protected string selectedPropertyPach;
    protected string selectedProperty;

    Vector2 scrollPosition = Vector2.zero;
    Vector2 itemScrollPosition = Vector2.zero;
    readonly float sidebarWidth = 250f;

    protected string activePath = "Assets";
    protected System.Type activeType = typeof(ScriptableObject);

    protected string scriptableName = "";
    protected string typeName = "Scriptable Types";

    [MenuItem("Tools/Scriptable Object Editor")]
    protected static void ShowWindow()
    {
        GetWindow<SriptablesEditorWindow>("Scriptables Editor");
    }

    private void OnGUI()
    {
        activeObjects = GetAllInstancesOfType(activePath, activeType);

        if (activeObjects.Length > 0)
            serializedObject = new SerializedObject(activeObjects[0]);
        
        HeaderNavigation();

        EditorGUILayout.BeginHorizontal();

        SelectionNavigation();
        SelectableContents();

        EditorGUILayout.EndHorizontal();

        if (activeObjects.Length > 0 && serializedObject != null)
            Apply();
    }

    private void HeaderNavigation()
    {
        EditorGUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Folder", GUILayout.MaxWidth(150)))
        {
            string basePath = EditorUtility.OpenFolderPanel("Select folder to mask path.", activePath, "");

            if (basePath.Length <= 0) { return; }

            if (basePath.Contains("Assets"))
            {
                basePath = basePath.Substring(basePath.LastIndexOf("Assets"));
            }

            if (!AssetDatabase.IsValidFolder(basePath))
            {
                EditorUtility.DisplayDialog("Error: File Path Invalid", "Please make sure the path is contained with the project's assets folder", "Ok");
            }
            else
            {
                activePath = basePath;
            }
        }
        EditorGUILayout.LabelField(activePath);

        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Scriptables Editor");
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    private void SelectionNavigation()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(sidebarWidth), GUILayout.ExpandHeight(true));

        if (EditorGUILayout.DropdownButton(new GUIContent(typeName), FocusType.Keyboard))
        {
            GenericMenu menu = new GenericMenu();

            var function = new GenericMenu.MenuFunction2((type) => { activeType = (System.Type)type; typeName = type.ToString(); });

            menu.AddItem(new GUIContent("All"), false, function, typeof(ScriptableObject));
            menu.AddSeparator("");

            foreach (var item in GetAllTypes())
            {
                menu.AddItem(new GUIContent(item.ToString()), false, function, item);
            }
            menu.ShowAsContext();
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.ExpandHeight(true));

        if (activeObjects.Length > 0)
            DrawScriptables(activeObjects);

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        scriptableName = EditorGUILayout.TextField(scriptableName);
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            if (scriptableName.Length <= 0)
            {
                EditorUtility.DisplayDialog("Error: File Name Required", "The " + activeObjects[0].GetType() + " file name can not be left empty.", "Ok");
            }
            else if (!scriptableName.All(char.IsLetterOrDigit))
            {
                EditorUtility.DisplayDialog("Error: File Name Required", "The " + activeObjects[0].GetType() + " file name can not contain invalid characters.", "Ok");
            }
            else
            {
                var type = activeObjects[0].GetType();
                UnityEngine.Object newScriptable = CreateObject(type);
                AssetDatabase.CreateAsset(newScriptable, activePath + "/" + scriptableName + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        EditorGUILayout.EndHorizontal();
            
        EditorGUILayout.EndVertical();
    }

    private void SelectableContents()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        itemScrollPosition = EditorGUILayout.BeginScrollView(itemScrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.ExpandHeight(true));
        switch (true)
        {
            case bool x when selectedProperty != null && selectedProperty != "":
                for (int i = 0; i < activeObjects.Length; i++)
                {
                    if (activeObjects[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(activeObjects[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);

                        GUILayout.Space(15);

                        var style = new GUIStyle(GUI.skin.button);
                        style.normal.textColor = Color.red;

                        if (GUILayout.Button("Delete", style))
                        {
                            string path = AssetDatabase.GetAssetPath(activeObjects[i]);
                            AssetDatabase.DeleteAsset(path);
                            serializedObject = null;
                        }
                    }
                }
                break;
            default:
                EditorGUILayout.LabelField("No item selected, make sure you've selected an item.");
                break;
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }


    public static ScriptableObject[] GetAllInstancesOfType(string activePath, System.Type activeType)
    {
        string[] guids = AssetDatabase.FindAssets("t:" + activeType.Name, new[] { activePath });
        ScriptableObject[] a = new ScriptableObject[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = (ScriptableObject)AssetDatabase.LoadAssetAtPath(path, activeType);
        }
        return a;
    }

    public static Object CreateObject(System.Type type)
    {
        return CreateInstance(type);
    }

    public static System.Type[] GetAllTypes()
    {
        System.Type[] types = AssemblyTypes.ReturnTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(ScriptableObject)) select type).ToArray();

        return possible;
    }

    protected void DrawProperties(SerializedProperty property)
    {
        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property, true);
        }
    }

    protected void DrawScriptables(ScriptableObject[] objects)
    {
        foreach (ScriptableObject item in objects)
        {
            if (GUILayout.Button(item.name))
            {
                selectedPropertyPach = item.name;
            }
        }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}