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
	private float _xRot, _yRot;
	private Vector3 _tempVector, _targetRot;

	private void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_camera = GameObject.FindGameObjectWithTag("MainCamera");

		CanMove = true;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void Update()
	{
		if (CanMove)
		{
			_tempVector = transform.forward * _posChange.y + _posChange.x * transform.right;

			_velocity = new Vector2(_speed * Vector3.Normalize(_tempVector).x,
												_speed * Vector3.Normalize(_tempVector).z);
			_characterController.SimpleMove(new Vector3(_velocity.x, 0, _velocity.y));

			_xRot -= _rotation.y;
			_xRot = Mathf.Clamp(_xRot, -80, 80);
			_targetRot = transform.eulerAngles;
			_targetRot.x = _xRot;
			_camera.transform.eulerAngles = _targetRot;


			_yRot += _rotation.x;
			_targetRot = transform.eulerAngles;
			_targetRot.y = _yRot;

			transform.rotation = Quaternion.Euler(_targetRot);
		}
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