using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _endMenu;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private GameObject _tree1;
    [SerializeField] private GameObject _tree2;
    [SerializeField] private GameObject _currentCam;
    [SerializeField] private GameObject[] _colliders;
    [SerializeField] private Text _endText;

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
        PlayerLogic.CanMove = false;
        foreach (GameObject go in _colliders) go.SetActive(true);
        _currentCam.transform.position = _startPosition.position;
        _currentCam.transform.rotation = _startPosition.rotation;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _startMenu.SetActive(true);

        //  Application.targetFrameRate = 15;
    }

    private IEnumerator MoveCam(Transform endTransform, Transform startTransform)
	{
        if (_startMenu.activeSelf) _startMenu.SetActive(false);
        if (endTransform == _endPosition) PlayerLogic.CanMove = false;

        float counter = 0;
        while (counter <= 5)
		{
            _currentCam.transform.position = Vector3.Lerp(startTransform.position, endTransform.position, 1f*Time.deltaTime);
            _currentCam.transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, 1f*Time.deltaTime);
            counter += Time.deltaTime;
            yield return null;
        }
        _currentCam.transform.position = endTransform.transform.position;
        _currentCam.transform.rotation = endTransform.transform.rotation;


        if (endTransform == _playerPosition)
        {
            PlayerLogic.CanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (endTransform == _endPosition) 
        { 
            _endMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
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
            go.transform.localScale *= 1 - Time.deltaTime/5;
            yield return null;
		}
        foreach (GameObject go2 in _colliders) go2.SetActive(false);
	} 

    public void EndGame()
	{
        StartCoroutine(MoveCam(_endPosition, _currentCam.transform));
        _endText.text = "You are done!\nThere are " + Negociation.Score + " clients happy";
    }

    public void StartGame()
	{
        StartCoroutine(MoveCam(_playerPosition, _currentCam.transform));
    }

    public void ExitGame()
	{
        Application.Quit();
	}

    public void RestartGame()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void ChangeCam(GameObject camera)
	{
        _currentCam.SetActive(false);
        camera.SetActive(true);
        _currentCam = camera;
    }
}
