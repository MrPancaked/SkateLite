using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TrickDirection {Up, Down, Left, Right, None}
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [Header("References")]
    [SerializeField] private InputActionAsset controls; // Reference to the Input Action Asset
    
    //tricks
    public TrickDirection trick;
    
    //inputs and respective variables
    private InputAction jumpInputAction;
    private InputAction upTrickInputAction;
    private InputAction downTrickInputAction;
    private InputAction leftTrickInputAction;
    private InputAction rightTrickInputAction;
    private InputAction tabInputAction;
    private InputAction escapeInputAction;

    public bool tabPressed;
    public bool escapePressed;
    public bool jump;
    
    
    #region Unity Methods
    private void Awake()
    {
        // Make sure only one ScoreManager exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        jumpInputAction = controls.FindActionMap("PlayerActions").FindAction("Jump");
        upTrickInputAction = controls.FindActionMap("PlayerActions").FindAction("UpTrick");
        downTrickInputAction = controls.FindActionMap("PlayerActions").FindAction("DownTrick");
        leftTrickInputAction = controls.FindActionMap("PlayerActions").FindAction("LeftTrick");
        rightTrickInputAction = controls.FindActionMap("PlayerActions").FindAction("RightTrick");
        tabInputAction = controls.FindActionMap("UIActions").FindAction("Tab");
        escapeInputAction = controls.FindActionMap("UIActions").FindAction("Escape");
    }
    
    private void OnEnable()
    {
        jumpInputAction.Enable();
        upTrickInputAction.Enable();
        downTrickInputAction.Enable();
        leftTrickInputAction.Enable();
        rightTrickInputAction.Enable();
        tabInputAction.Enable();
        escapeInputAction.Enable();
    }

    private void OnDisable()
    {
        jumpInputAction.Disable();
        upTrickInputAction.Disable();
        downTrickInputAction.Disable();
        leftTrickInputAction.Disable();
        rightTrickInputAction.Disable();
        tabInputAction.Disable();
        escapeInputAction.Disable();
    }

    private void Update()
    {
        if (tabInputAction.triggered) tabPressed = true;
        else tabPressed = false;
        
        if (escapeInputAction.triggered) escapePressed = true;
        else escapePressed = false;
    }

    #endregion
    
    public void UpdateInputs() //most if not all of these can just be called in update since this method is called in the playercontroller update
    {
        if (jumpInputAction.triggered) jump = true; //maybe change jumpInputAction.triggered to jumpInputAction. to allow bhop?
        if (upTrickInputAction.IsPressed()) trick = TrickDirection.Up;
        else if (downTrickInputAction.IsPressed()) trick = TrickDirection.Down;
        else if (leftTrickInputAction.IsPressed()) trick = TrickDirection.Left;
        else if (rightTrickInputAction.IsPressed()) trick = TrickDirection.Right;
        else trick = TrickDirection.None;
    }

    public void ResetInputs()
    {
        jump = false;
    }
}
