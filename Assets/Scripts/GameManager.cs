using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject tree1;
    GameObject tree2;
    Collider startPoint;

    void RotateTrees()
    {
        StartCoroutine(RotateTree(tree1, 70));
        StartCoroutine(RotateTree(tree2, 70));
        startPoint.enabled = false;
    }

    IEnumerator RotateTree(GameObject go, float angles)
	{
        while (go.transform.rotation.z > angles)
		{
            go.transform.Rotate(0, 0, Time.deltaTime);
            yield return null;
		}
	}

    void EndGame()
	{
        //TODO: Put in proper scene or transition to end the game
        SceneManager.LoadSceneAsync(2);
	}
}
