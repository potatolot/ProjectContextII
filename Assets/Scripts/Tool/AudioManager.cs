using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
	[SerializeField] private AudioContents[] _audioContents;
	private Negociation _negotiationscript;

	private Dictionary<GameObject, AudioComponents> _audioDictionary = new Dictionary<GameObject, AudioComponents>();

	//Stores all the required components for the audio
	private struct AudioComponents
	{
		public AudioContents.StartType startType;
		public AudioClip audioClip;
		public float volume;

		//Fill all variables for the audio 
		public AudioComponents(AudioContents.StartType st, AudioClip ac, float v)
		{
			startType = st;
			audioClip = ac;
			volume = v;
		}
	}

	//Toggle debug features depending on if the game is running in Unity or as a build
	private void Awake()
	{
		#if UNITY_EDITOR
			Debug.unityLogger.logEnabled = true;
		#else
			Debug.unityLogger.logEnabled = false;
		#endif
	}

	//Gather and configure all data needed for the audiomanager to work
	private void Start()
	{
		//Get player Game Object if the _player variable is empty
		if (!_player) _player = GameObject.FindGameObjectWithTag("Player");
		else if (!_player) _player = GameObject.Find("Player");
 		else if (!_player) Debug.LogError("No player recognized in the " + this.name + 
											" script on the " + gameObject.name + " Game Object!");

		//Add PlayerAudioComponent script to _player Game Object for audio at runtime
		if(!_player.GetComponent<PlayerAudioComponent>()) _player.AddComponent<PlayerAudioComponent>().Range = 10f;

		//Add all audio contents in a directory, the key will be the gameobject and the value will be all audio components needed
		if(_audioContents.Length > 0)
		{
			foreach(AudioContents ac in _audioContents)
			{
				if (!ac.gameObject.GetComponent<AudioSource>()) ac.gameObject.AddComponent<AudioSource>();
				_audioDictionary.Add(ac.gameObject, new AudioComponents(ac.startType, ac.audioClip, ac.volume));
			}
		}
		else Debug.LogError("There are no Audio Components on the " + this.name + 
							" component on the " + gameObject.name + " Game Object!");

		_negotiationscript = _player.GetComponent<Negociation>();
	}

	//Play the audio connected to the Game Object
	public void PlayAudio(GameObject go, AudioContents.StartType startType)
	{
		if (_audioDictionary.ContainsKey(go))
		{
			AudioComponents component = _audioDictionary[go];

			//Check if the start type for the audio is correct
			if (component.startType == startType)
			{
				//Get audio source and play the audio clip
				AudioSource audioSource = go.GetComponent<AudioSource>();
				audioSource.Stop();
				audioSource.clip = component.audioClip;
				audioSource.volume = component.volume / 100;
				audioSource.Play();

				StartCoroutine(_negotiationscript.StartNegociation(go, audioSource));
			}
		}
	}
}
