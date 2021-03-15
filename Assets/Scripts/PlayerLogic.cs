using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
	private Vector2 _posChange;
	private Vector2 _rotation;
	private float _speed;
	private float _range;
	private Vector2 _velocity;
	private CharacterController _characterController;
	private GameObject _camera;
	private AudioManager _audioManager;

	private void Start()
	{
		_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
		_characterController = GetComponent<CharacterController>();
		_camera = GameObject.FindGameObjectWithTag("MainCamera");

		_speed = 10f;
		_range = 10f;
	}

	public void Update()
	{
		Vector3 testVector = transform.forward * _posChange.y + _posChange.x * transform.right;
		_velocity = new Vector2(_speed * Vector3.Normalize(testVector).x * Time.deltaTime,
			_speed * Vector3.Normalize(testVector).z * Time.deltaTime);
		
		_characterController.Move(new Vector3(_velocity.x, 0, _velocity.y));

		//FIXME //HACK Rotation not clamped. Could not edit rotation for some reason
		_camera.transform.Rotate(-new Vector3(_rotation.y,
															0,
															0));
		
		
		transform.Rotate(new Vector3(0,
												_rotation.x,
												0));

		//Debug.Log(_camera.transform.rotation.y);
		//_camera.transform.eulerAngles = -new Vector3(Mathf.Clamp(_camera.transform.rotation.x + _rotation.y, -70, 80),
		//													_camera.transform.rotation.y,
		//													_camera.transform.rotation.z);

	}

	public void WALK(InputAction.CallbackContext context)
	{
		_posChange = context.ReadValue<Vector2>();
	}

	public void ROTATE(InputAction.CallbackContext context)
	{
		_rotation = context.ReadValue<Vector2>();
	}

	public void Interact()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
		{
			if (hit.transform.gameObject.tag == "ContainAudio")
			{
				_audioManager.TryAudio(hit.transform.gameObject, AudioContents.StartType.OnClick);
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject != gameObject)
		{
			if (collision.gameObject.transform.gameObject.tag == "ContainAudio")
			{
				_audioManager.TryAudio(collision.gameObject, AudioContents.StartType.OnCollision);
			}
		}
	}
}