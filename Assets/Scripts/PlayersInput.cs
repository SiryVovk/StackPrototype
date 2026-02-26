using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;



public class PlayersInput : MonoBehaviour, ITouchActions
{
    public Action OnTouch;

    private PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();
        inputActions.Touch.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();

    }

    public void OnPlayerTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnTouch?.Invoke();
        }
    }
}
