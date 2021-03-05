using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    public GameObject Player;
    public GameObject AudioManager;
    public AudioContents[] audioContents;
	private Dictionary<GameObject, _components> _audioDictionary = new Dictionary<GameObject, _components>();

	private struct _components
	{
		public _components(AudioContents.StartType st, AudioClip ac, float v)
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
			_audioDictionary.Add(ac.gameObject, new _components(ac.startType, ac.audioClip, ac.volume);
		}
	}
}
