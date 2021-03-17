using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//TODO: THIS IS A WIP FILE AND IS NOT FINISHED OR IN APLHA PHASE!
public class AudioTool : EditorWindow
{
    private GameObject audioManagerGO;
    private AudioManager audioManagerScript;

    [MenuItem("Audio Tool/Audio Tool")]
    static void ShowWindow()
    {
        GetWindow<AudioTool>("Audio Tool");
    }

    private void OnGUI()
    {
        if (!audioManagerGO) audioManagerGO = GameObject.Find("AudioManager");
        else if (!audioManagerGO) audioManagerGO = GameObject.FindGameObjectWithTag("AudioManager");
        else if (!audioManagerGO) audioManagerGO = new GameObject("AudioManager");

        if(!audioManagerGO.CompareTag("AudioManager")) audioManagerGO.tag = "AudioManager";

        if (!audioManagerGO.GetComponent<AudioManager>()) audioManagerScript = audioManagerGO.AddComponent<AudioManager>();
        else audioManagerScript = audioManagerGO.GetComponent<AudioManager>();

        GUILayout.Label("Variables", EditorStyles.boldLabel);
    }
}
