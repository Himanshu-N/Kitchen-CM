using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputAction playerInputAction;
    public event EventHandler OnInteractAction;
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(obj, EventArgs.Empty);
    }

    public Vector2 GameInputVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        /*
        // Vector2 inputVector = new Vector2(0,0);
        // if (Input.GetKey(KeyCode.W))
        // {
        //     inputVector.y = +1;
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     inputVector.y = -1;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     inputVector.x = -1;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     inputVector.x = +1;
        // }
        */
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
