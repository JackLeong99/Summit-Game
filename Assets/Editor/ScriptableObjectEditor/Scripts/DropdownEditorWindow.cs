/*using UnityEditor;
using UnityEngine;

public class DropdownEditorWindow : PopupWindowContent
{
    protected Rect button;

    public DropdownEditorWindow(Object selected, Rect origin)
    {
        selectedObject = selected;
        button = origin;
    }

    public override void OnGUI(Rect rect)
    {
        EditorGUILayout.Space(10);
        var style = new GUIStyle(GUI.skin.label);
        style.fontSize = 12;
        style.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.LabelField("Rename " + selectedObject.name, style);
        EditorGUILayout.Space(10);

        newName = EditorGUILayout.TextField(newName);

        if (GUILayout.Button("Rename"))
        {
            var path = AssetDatabase.GetAssetPath(selectedObject);
            AssetDatabase.RenameAsset(path, newName);
            editorWindow.Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            editorWindow.Close();
        }

        GUILayout.FlexibleSpace();
    }

    public override Vector2 GetWindowSize()
    {
        return new Vector2(button.width, 125);
    }
}
*/