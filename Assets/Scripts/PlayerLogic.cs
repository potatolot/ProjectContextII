using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLogic : MonoBehaviour
{
	private Vector2 _posChange;
	private Vector2 _rotation;
	[SerializeField] private float _speed = 100f;
	private Vector2 _velocity;
	private CharacterController _characterController;
	private GameObject _camera;
	static public bool CanMove;

	private void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_camera = GameObject.FindGameObjectWithTag("MainCamera");

		CanMove = true;
	}

	public void Update()
	{
		Vector3 testVector = transform.forward * _posChange.y + _posChange.x * transform.right;
		if (CanMove) _velocity = new Vector2(_speed * Vector3.Normalize(testVector).x * Time.deltaTime,
												_speed * Vector3.Normalize(testVector).z * Time.deltaTime);

		if (CanMove) _characterController.SimpleMove(new Vector3(_velocity.x, 0, _velocity.y));

		//HACK Rotation not clamped. Could not edit rotation for some reason
		if (CanMove) _camera.transform.Rotate(-new Vector3(_rotation.y,
															0,
															0));


		if (CanMove) transform.Rotate(new Vector3(0,
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
}