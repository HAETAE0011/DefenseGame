using UnityEditor;
using UnityEngine;

public class ChangeShader_Window : EditorWindow {
    Material mat;
    [MenuItem("Custom/Shader Changer")]
    
    public static void ShowWindow()
    {
        GetWindow<ChangeShader_Window>("Shader Changer");
        
    }

    // Window display items
    private void OnGUI()
    {
        GUILayout.Label("change the shader of the selected objects to Standard", EditorStyles.boldLabel);
        mat = new Material(Shader.Find("Standard"));
        if (GUILayout.Button("Change Shader to Standard")) {
            ShaderChanger(mat);
        }
        GUILayout.Label("change the shader of the selected objects to Unlit", EditorStyles.boldLabel);
        mat = new Material(Shader.Find("Unlit/Color"));
        if (GUILayout.Button("Change Shader to Unlit"))
        {
            ShaderChanger(mat);
        }

    }

    void ShaderChanger(Material mat) {
        foreach (GameObject g in Selection.gameObjects) {
            Color color = g.GetComponent<Renderer>().sharedMaterial.color;
            g.GetComponent<Renderer>().material = mat;
            mat.color = color;
        }
    }

    // Methods





}
