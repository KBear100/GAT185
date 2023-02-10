using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputRouter", menuName = "Game/InputRouter")]
public class InputRouter : ScriptableObject, PlayerInputActions.IPlayerActions
{
	public UnityAction fireEvent;
	public UnityAction fireStopEvent;
	public UnityAction jumpEvent;
	public UnityAction jumpStopEvent;
	public UnityAction<Vector2> moveEvent;

	private PlayerInputActions inputActions;

	private void OnEnable()
	{
		if (inputActions == null) 
		{
			inputActions = new PlayerInputActions();
			inputActions.Player.SetCallbacks(this);
			inputActions.Player.Enable();
		}
	}

	private void OnDisable()
	{
		inputActions.Player.Disable();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveEvent.Invoke(context.ReadValue<Vector2>());
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				fireEvent?.Invoke();
				break;
			case InputActionPhase.Canceled:
				fireStopEvent?.Invoke();
				break;
		}
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Performed:
				jumpEvent?.Invoke();
				break;
			case InputActionPhase.Canceled:
				jumpStopEvent?.Invoke();
				break;
		}
	}
}
