using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioComponent : MonoBehaviour
{
	private AudioManager _audioManager;
	public float Range = 10f;

	private void Start()
	{
		_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	//TODO: Add functionality to automatically enable inputs (in the new input system for now)
	public void Interact()
	{
		//Shoot raycast in the middle of the player to see what is in front of the player
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, Range))
		{
			if (!hit.transform.gameObject.GetComponent<AudioSource>())
			{
				//Check if target has an audiosource
				_audioManager.PlayAudio(hit.transform.gameObject, AudioContents.StartType.OnClick);
			}
		}
	}

	/* USED WHEN ONLY USING RIGIDBODY
	//Trigger when colliding with a collider
	private void OnCollisionEnter(Collision col)
	{
		//Check if player isnt colliding with itself
		if (col.gameObject != gameObject)
		{
			//Check if target has an audiosource
			if (col.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(col.gameObject, AudioContents.StartType.OnCollision);
			}
			else Debug.LogWarning(col.gameObject.name + " has no Audio Source!");
		}
	}*/

	//Trigger when colliding with a trigger
	private void OnTriggerEnter(Collider col)
	{
		//Check if player isnt colliding with itself
		if (col.gameObject != gameObject)
		{	
			//Check if target has an audiosource
			if (col.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(col.gameObject, AudioContents.StartType.OnTriggerVolume);
			}
			else Debug.LogWarning(col.gameObject + " has no Audio Source!");
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit col)
	{
		//Check if player isnt colliding with itself
		if (col.gameObject != gameObject)
		{
			//Check if target has an audiosource
			if (col.gameObject.GetComponent<AudioSource>())
			{
				_audioManager.PlayAudio(col.gameObject, AudioContents.StartType.OnCollision);
			}
			else Debug.LogWarning(col.gameObject.name + " has no Audio Source!");
		}
	}
}
