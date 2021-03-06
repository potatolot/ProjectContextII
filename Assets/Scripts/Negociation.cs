using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Negociation : MonoBehaviour
{
	static public float Score;

	[SerializeField] private GameObject _visual;
	[SerializeField] private GameObject _level1;
	[SerializeField] private GameObject _mainCamera;
	[SerializeField] private Text _question;
	[SerializeField] private Text[] _answers;
	[SerializeField] private Button[] _buttons;
	[SerializeField] private storyContents[] _storyContent;

    static Dictionary<GameObject, storyContents> _storyDictionary = new Dictionary<GameObject, storyContents>();
	private storyContents _currentStory;

	[System.Serializable]
	struct storyContents
	{
        public GameObject go;
		public string question;
		public answerResult[] answers;
	}

	[System.Serializable]
	struct answerResult
	{
		public string answer;
		public float result;
		public AudioClip answerclip;
	}

	private void Start()
	{
		foreach(storyContents sc in _storyContent)
		{
			_storyDictionary.Add(sc.go, sc);
		}
	}

	public IEnumerator StartNegociation(GameObject go, AudioSource ac)
	{
		if (_storyDictionary.ContainsKey(go))
		{
			PlayerLogic.CanMove = false;
			while (ac.isPlaying)
			{
				yield return null;
			}
			_currentStory = _storyDictionary[go];

			_visual.SetActive(true);
			foreach(Text t in _answers)
			{
				t.gameObject.SetActive(false);
			}
			foreach (Button b in _buttons)
			{
				b.gameObject.SetActive(false);
			}
			for (int i = 0; i < _currentStory.answers.Length; i++)
			{
				_answers[i].gameObject.SetActive(true);
				_answers[i].text = _currentStory.answers[i].answer;
				_buttons[i].gameObject.SetActive(true);
			}
			_question.text = _currentStory.question;

			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;

			_storyDictionary.Remove(go);
		}
	}

	public void answerClicked(int answer)
	{
		answer--;
		CheckAnswer(answer);
		
	}

	private void CheckAnswer(int answer)
	{
		Score += _currentStory.answers[answer].result;

		AudioSource ac = _currentStory.go.GetComponent<AudioSource>();
		ac.clip = _currentStory.answers[answer].answerclip;
		ac.Play();

		_visual.SetActive(false);
		PlayerLogic.CanMove = true;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Debug.Log(Score);

		if(_currentStory.go == _level1)
		{
			GameManager.Instance.RotateTrees();
		}
		else if(_storyDictionary.Keys.Count <= 0)
		{
			GameManager.Instance.EndGame();
			return;
		}
		GameManager.Instance.ChangeCam(_mainCamera);
	}
}
