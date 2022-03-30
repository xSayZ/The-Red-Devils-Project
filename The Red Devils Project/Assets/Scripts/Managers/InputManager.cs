// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            Referenced and sourced:
// #            Trever Mock - Youtube
// #
// #
// #
// #
// #
// ########################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour {

    public PlayerInput playerInput;

    private Vector2 movementInput = Vector2.zero;
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool pausePressed = false;
    private bool upPressed = false;
    private bool downPressed = false;
    private bool leftPressed = false;
    private bool rightPressed = false;

    private static InputManager instance;

    // Action Maps
    [SerializeField] private string actionMapPlayerControls = "Player Controls";
    [SerializeField] private string actionMapBattleControls = "Battle Controls";


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void PausePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
        }
        else if (context.canceled)
        {
            pausePressed = false;
        }
    }

    public void UpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            upPressed = true;
        }
        else if (context.canceled)
        {
            upPressed = false;
        }
    }

    public void DownPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            downPressed = true;
        }
        else if (context.canceled)
        {
            downPressed = false;
        }
    }

    public void LeftPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leftPressed = true;
        }
        else if (context.canceled)
        {
            leftPressed = false;
        }
    }

    public void RightPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rightPressed = true;
        }
        else if (context.canceled)
        {
            rightPressed = false;
        }
    }

    public Vector2 GetMoveDirection()
    {
        return movementInput;
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }
    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public bool GetPausePressed()
    {
        bool result = pausePressed;
        pausePressed = false;
        return result;
    }

    public bool GetUpPressed()
    {
        bool result = upPressed;
        upPressed = false;
        return result;
    }

    public bool GetDownPressed()
    {
        bool result = downPressed;
        downPressed = false;
        return result;
    }
    public bool GetLeftPressed()
    {
        bool result = leftPressed;
        leftPressed = false;
        return result;
    }
    public bool GetRightPressed()
    {
        bool result = rightPressed;
        rightPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }

    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
        Debug.Log("Loaded into " + actionMapPlayerControls);
    }

    public void EnableBattleControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapBattleControls);
        Debug.Log("Loaded into " + actionMapBattleControls);
    }
}