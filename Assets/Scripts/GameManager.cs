using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private GameObject _tree1;
    [SerializeField] private GameObject _tree2;
    [SerializeField] private GameObject _currentCam;
    [SerializeField] private GameObject[] _colliders;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
	{
        foreach (GameObject go in _colliders) go.SetActive(true);
    }

    public void RotateTrees()
    {
        StartCoroutine(RotateTree(_tree1, 4f));
        StartCoroutine(RotateTree(_tree2, 4f));
    }

    private IEnumerator RotateTree(GameObject go, float size)
	{
        while (go.transform.localScale.z > size )
		{
            go.transform.localScale *= 1 - Time.deltaTime/15;
            yield return null;
		}
        foreach (GameObject go2 in _colliders) go2.SetActive(false);
	} 

    public void EndGame()
	{
        //TODO: Put in proper scene or transition to end the game
        SceneManager.LoadSceneAsync(2);
	}

    public void ChangeCam(GameObject camera)
	{
        _currentCam.SetActive(false);
        camera.SetActive(true);
        _currentCam = camera;
    }
}
