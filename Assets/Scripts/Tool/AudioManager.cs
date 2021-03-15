using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject audioManager;
    public AudioContents[] audioContents;
	private Dictionary<GameObject, Components> _audioDictionary = new Dictionary<GameObject, Components>();

	private struct Components
	{
		public Components(AudioContents.StartType st, AudioClip ac, float v)
		{
			startType = st;
			audioClip = ac;
			volume = v;
		}

		public AudioContents.StartType startType;
		public AudioClip audioClip;
		public float volume;
	}

	private void Start()
	{
		foreach(AudioContents ac in audioContents)
		{
			ac.gameObject.tag = "ContainsAudio";
			_audioDictionary.Add(ac.gameObject, new Components(ac.startType, ac.audioClip, ac.volume));
		}
	}

	public void TryAudio(GameObject go, AudioContents.StartType startType)
	{
		if (_audioDictionary.ContainsKey(go))
		{
			Components component = _audioDictionary[go];
			if (component.startType == startType)
			{
				AudioSource audioSource = go.GetComponent<AudioSource>();
				audioSource.Stop();
				audioSource.clip = component.audioClip;
				audioSource.volume = component.volume / 100;
				audioSource.Play();
			}
		}
	}
}
