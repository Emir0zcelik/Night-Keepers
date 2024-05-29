using UnityEngine;
using UnityEditor;

public class FindScriptInstances : EditorWindow
{
    private string scriptName = "MyScript";

    [MenuItem("Tools/Find Script Instances")]
    public static void ShowWindow()
    {
        GetWindow<FindScriptInstances>("Find Script Instances");
    }

    private void OnGUI()
    {
        GUILayout.Label("Find GameObjects with Script", EditorStyles.boldLabel);
        scriptName = EditorGUILayout.TextField("Script Name", scriptName);

        if (GUILayout.Button("Find"))
        {
            FindInstances();
        }
    }

    private void FindInstances()
    {
        MonoBehaviour[] scripts = Resources.FindObjectsOfTypeAll<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script.GetType().Name == scriptName)
            {
                Debug.Log("Script " + scriptName + " is attached to: " + script.gameObject.name, script.gameObject);
            }
        }
    }
}
