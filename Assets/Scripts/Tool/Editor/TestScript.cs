using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{
    public GameObject[] gameObjects;
    public AudioClip[] audioClips;
    public Dictionary<GameObject,AudioClip> connectedObjects;
    public Color color;

    public struct CGO
	{
        public string ss;
        public int ff;
	};


    [MenuItem("Window/Audio Tool")]
    static void ShowWindow()
	{
        GetWindow<ExampleWindow>("Audio Tool");
	}

	private void OnGUI()
	{
        GUILayout.Label("Variables", EditorStyles.boldLabel);

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("gameObjects");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties

        ScriptableObject target2 = this;
        SerializedObject so2 = new SerializedObject(target2);
        SerializedProperty stringsProperty2 = so2.FindProperty("audioClips");

        EditorGUILayout.PropertyField(stringsProperty2, true); // True means show children
        so2.ApplyModifiedProperties(); // Remember to apply modified properties



        GUILayout.FlexibleSpace();
        //GUILayout.Label("Apply", EditorStyles.boldLabel);
        if (GUILayout.Button("Apply"))
        {
            
        }



        //color = EditorGUILayout.ColorField("colorpici", color);
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
