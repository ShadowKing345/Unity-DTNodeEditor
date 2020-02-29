using System;
using UnityEditor;

public class DTNodeEditorWindow : EditorWindow
{
    [MenuItem("Window/Editor Window")]
    public static void ShowWindow()
    {
        GetWindow<DTNodeEditorWindow>("Example");
    }

    private void OnGUI()
    {
        
    }
}
