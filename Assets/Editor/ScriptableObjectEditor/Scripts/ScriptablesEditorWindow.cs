using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ScriptablesEditorWindow : EditorWindow
{
    protected GUISkin skin;

    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected Object selectedObject;
    protected string selectedName;
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

    protected string sortSearch = "";
    protected int stringMax = 27;

    protected Rect renameButton;

    [MenuItem("Tools/Scriptable Object Editor")]
    protected static void ShowWindow()
    {
        var window = GetWindow<ScriptablesEditorWindow>("Scriptables Editor");
        window.UpdateObjets();
    }

    private void OnEnable()
    {
        skin = (GUISkin)Resources.Load("ScriptableEditorGUI");
        UpdateObjets();
    }

    private void OnGUI()
    {
        if (activeObjects.Length > 0)
            serializedObject = new SerializedObject(activeObjects[0]);

        EditorGUILayout.BeginVertical("box");

        HeaderTitle();
        HeaderNavigation();

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();

        SelectionNavigation();
        SelectableContents();

        EditorGUILayout.EndHorizontal();

        if (activeObjects.Length > 0 && serializedObject != null)
            Apply();
    }

    private void HeaderTitle()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Scriptables Editor", skin.customStyles.ToList().Find(x => x.name == "Header"));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    #region Navigation
    #region Header
    private void HeaderNavigation()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

        if (GUILayout.Button("Folder", GUILayout.MaxWidth(150)))
        {
            string basePath = EditorUtility.OpenFolderPanel("Select folder to mask path.", activePath, "");

            if (basePath.Contains(Application.dataPath))
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
                UpdateObjets();
            }
        }
        EditorGUILayout.LabelField(activePath);
        EditorGUILayout.EndHorizontal();
    }
    #endregion

    #region Sidebar
    private void SelectionNavigation()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(sidebarWidth), GUILayout.ExpandHeight(true));

        if (EditorGUILayout.DropdownButton(new GUIContent(typeName), FocusType.Keyboard))
        {
            GenericMenu menu = new GenericMenu();

            var function = new GenericMenu.MenuFunction2((type) => { activeType = (System.Type)type; typeName = type.ToString(); if (activeType == typeof(ScriptableObject)) typeName = "All"; UpdateObjets(); });

            menu.AddItem(new GUIContent("All"), OfType(typeof(ScriptableObject)), function, typeof(ScriptableObject));
            menu.AddSeparator("");

            foreach (var item in AssemblyTypes.GetAllTypes())
            {
                menu.AddItem(new GUIContent(item.ToString()), OfType(item), function, item);
            }
            menu.ShowAsContext();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        sortSearch = EditorGUILayout.TextField(sortSearch, GUILayout.MaxWidth(240));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

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
                Object newScriptable = CreateObject(type);
                AssetDatabase.CreateAsset(newScriptable, activePath + "/" + scriptableName + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    protected bool OfType(System.Type type)
    {
        return activeType == type;
    }
    #endregion
    #endregion

    #region Display Selected Contents
    private void SelectableContents()
    {
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        itemScrollPosition = EditorGUILayout.BeginScrollView(itemScrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar, GUILayout.ExpandHeight(true));
        switch (true)
        {
            case bool _ when serializedObject != null && selectedObject != null:
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Name"));
                GUI.enabled = true;

                serializedProperty = serializedObject.GetIterator();
                serializedProperty.NextVisible(true);
                DrawProperties(serializedProperty);

                GUILayout.Space(15);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Rename"))
                {
                    PopupWindow.Show(renameButton, new NamingEditorWindow(selectedObject, renameButton));
                }
                if (Event.current.type == EventType.Repaint) renameButton = GUILayoutUtility.GetLastRect();

                var style = new GUIStyle(GUI.skin.button);
                style.normal.textColor = Color.red;

                if (GUILayout.Button("Delete", style))
                {
                    switch (EditorUtility.DisplayDialog("Delete " + selectedObject.name + "?", "Are you sure you want to delete '" + selectedObject.name + "'?", "Yes", "No"))
                    {
                        case true:
                            string path = AssetDatabase.GetAssetPath(selectedObject);
                            AssetDatabase.DeleteAsset(path);
                            serializedObject = null;
                            selectedObject = null;
                            UpdateObjets();
                            break;
                    }
                }
                EditorGUILayout.EndHorizontal();
                break;
            default:
                EditorGUILayout.LabelField("No item selected, make sure you've selected an item.");
                break;
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
    #endregion

    protected void UpdateObjets()
    {
        activeObjects = GetAllInstancesOfType(activePath, activeType);
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

    protected void DrawProperties(SerializedProperty property)
    { 
        if (property.displayName == "Script") { GUI.enabled = false; }
        EditorGUILayout.PropertyField(property, true);
        GUI.enabled = true;

        EditorGUILayout.Space(40);

        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property, true);
        }
    }

    protected void DrawScriptables(ScriptableObject[] objects)
    {
        foreach (ScriptableObject item in objects)
        {
            if (item != null && item.name.IndexOf(sortSearch, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (GUILayout.Button(ShortenString(item.name), skin.button, GUILayout.ExpandWidth(true)))
                {
                    selectedPropertyPach = item.name;

                    if (!string.IsNullOrEmpty(selectedPropertyPach))
                    {
                        selectedProperty = selectedPropertyPach;
                        selectedObject = FindObject(activeObjects);
                    }

                    UpdateObjets();
                }
            }
        }

        switch (true)
        {
            case bool _ when selectedObject != null:
                serializedObject = new SerializedObject(selectedObject);
                break;
        }
    }

    protected string ShortenString(string item)
    {
        switch (true)
        {
            case bool _ when item.Length >= stringMax:
                return item.Substring(0, stringMax) + "...";
            default:
                return item;
        }
    }

    protected ScriptableObject FindObject(ScriptableObject[] objects)
    {
        return objects.Where(x => x.name == selectedProperty).First();
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}