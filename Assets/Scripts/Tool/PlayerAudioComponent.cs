using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioComponent : MonoBehaviour
{
	private AudioManager _audioManager;
	public float Range;

	private void Start()
	{
		_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
		Range = 10f;
	}

	//TODO: Add functionality to automatically enable inputs (in the new input system for now)
	public void Interact()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
		{
			if (!hit.transform.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(hit.transform.gameObject, AudioContents.StartType.OnClick);
			}
		}
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject != gameObject)
		{
			if (col.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(col.gameObject, AudioContents.StartType.OnCollision);
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject != gameObject)
		{
			if (col.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(col.gameObject, AudioContents.StartType.OnTriggerVolume);
			}
		}
	}
}
