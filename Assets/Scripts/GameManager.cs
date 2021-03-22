using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _tree1;
    [SerializeField] private GameObject _tree2;

    public void RotateTrees()
    {
        StartCoroutine(RotateTree(_tree1, 3.5f));
        StartCoroutine(RotateTree(_tree2, 3.5f));
    }

    private IEnumerator RotateTree(GameObject go, float size)
	{
        while (go.transform.localScale.z > size )
		{
            go.transform.localScale *= 1 - Time.deltaTime/25;
            yield return null;
		}
	} 

    public void EndGame()
	{
        //TODO: Put in proper scene or transition to end the game
        SceneManager.LoadSceneAsync(2);
	}
}
