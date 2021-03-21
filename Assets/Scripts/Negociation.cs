using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Negociation : MonoBehaviour
{
	[SerializeField] private GameObject visual;
	[SerializeField] private Text _question;
	[SerializeField] private Text[] _answers;
	private Color white = Color.white;
	private Color gray = Color.gray;
	private Vector2 _posChange;
	static private bool _canVote;
	[SerializeField] private storyContents[] _storyContent;
    static Dictionary<GameObject, storyContents> _storyDictionary = new Dictionary<GameObject, storyContents>();
	static public float Score { get; private set; }
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
	}

	private void Start()
	{
		foreach(storyContents sc in _storyContent)
		{
			_storyDictionary.Add(sc.go, sc);
		}
		_canVote = false;
	}

	private void FixedUpdate()
	{
		if (_canVote)
		{
			if (!PlayerLogic.CanMove)
			{
				//TODO
			}
		}
	}

	public void Move(InputAction.CallbackContext context)
	{
		if (!PlayerLogic.CanMove) _posChange = context.ReadValue<Vector2>();
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

			visual.SetActive(true);
			foreach(Text t in _answers)
			{
				t.gameObject.SetActive(false);
			}
			for(int i = 0; i < _currentStory.answers.Length; i++)
			{
				_answers[i].gameObject.SetActive(true);
				_answers[i].text = _currentStory.answers[i].answer;
			}
			_question.text = _currentStory.question;

			_canVote = true;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;

			_storyDictionary.Remove(go);
		}
	}

	public void answerClicked(int answer)
	{
		CheckAnswer(answer);
	}

	private void CheckAnswer(int answer)
	{
		Score += _currentStory.answers[answer].result;
		visual.SetActive(false);
		_canVote = false;
		PlayerLogic.CanMove = true;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Debug.Log(Score);
	}
}
