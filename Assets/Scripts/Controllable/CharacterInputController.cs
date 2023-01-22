using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController : MonoBehaviour
{
    private IControllable _controllable;
    private GameInput _gameInput;


    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        _controllable = GetComponent<IControllable>();
        if( _controllable == null )
            throw new Exception($"There is no IControllable component on the object: {gameObject.name}");
    }

    private void OnEnable()
    {
        _gameInput.Game.Push.performed += OnPushPerformed;
    }


    private void OnDisable()
    {
        _gameInput.Game.Push.performed -= OnPushPerformed;
    }
    private void OnPushPerformed(InputAction.CallbackContext obj)
    {
        _controllable.Push();
    }
}
