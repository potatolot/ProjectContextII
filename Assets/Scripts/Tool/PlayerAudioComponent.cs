using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioComponent : MonoBehaviour
{
	private AudioManager _audioManager;
	private float _range;


	private void Start()
	{
		_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
		_range = 10f;
	}

	//TODO: Add functionality to automatically enable inputs (in the new input system for now)
	public void Interact()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
		{
			if (!hit.transform.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(hit.transform.gameObject, AudioContents.StartType.OnClick);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject != gameObject)
		{
			if (collision.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(collision.gameObject, AudioContents.StartType.OnCollision);
			}
		}
	}
}
