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
    }

    private void OnEnable()
    {
        jumpInputAction.Enable();
        upTrickInputAction.Enable();
        downTrickInputAction.Enable();
        leftTrickInputAction.Enable();
        rightTrickInputAction.Enable();
    }

    private void OnDisable()
    {
        jumpInputAction.Disable();
        upTrickInputAction.Disable();
        downTrickInputAction.Disable();
        leftTrickInputAction.Disable();
        rightTrickInputAction.Disable();
    }
    #endregion
    
    public void UpdateInputs()
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
