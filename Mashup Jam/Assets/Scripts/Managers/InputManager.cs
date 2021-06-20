using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

//UnityEvents with special types
[Serializable]
public class Vector2Event : UnityEvent<Vector2>{}

public class InputManager : Singleton<InputManager>
{
	UnconventionalMeans controls;
	
	//public UnityEvents to be attached from inspector or from code using the singleton
	public Vector2Event move;
	public Vector2Event mousePosition;
	public UnityEvent placePortal;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		//instantiate controls
		controls = new UnconventionalMeans();
		controls.Gameplay.Enable();
		
		//attach HandlerMethods to events in the controls
		controls.Gameplay.Movement.performed += MoveInputHandler;
		controls.Gameplay.MousePosition.performed += MousePositionInputHandler;
		controls.Gameplay.MousePosition.canceled += MousePositionInputHandler;
		controls.Gameplay.PlacePortal.performed += PlacePortalInputHandler;
	}
	
	//handler methods
	public void MoveInputHandler(InputAction.CallbackContext value)
	{
		var input = value.ReadValue<Vector2>();
		move?.Invoke(input);
	}
	
	public void MousePositionInputHandler(InputAction.CallbackContext value)
	{
		var input = value.ReadValue<Vector2>();
		mousePosition?.Invoke(input);
	}
	
	public void PlacePortalInputHandler(InputAction.CallbackContext value)
	{
		placePortal?.Invoke();
	}
}