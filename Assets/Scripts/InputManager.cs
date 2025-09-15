using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    
    [Header("References")]
    [SerializeField] private InputActionAsset controls; // Reference to the Input Action Asset
    
    //inputs and respective variables
    [HideInInspector] public InputAction jumpInputAction;
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
    }

    private void OnEnable()
    {
        jumpInputAction.Enable();
    }

    private void OnDisable()
    {
        jumpInputAction.Disable();
    }
    #endregion
    
    public void UpdateInputs()
    {
        if (jumpInputAction.triggered) jump = true;
    }

    public void ResetInputs()
    {
        jump = false;
    }
}
