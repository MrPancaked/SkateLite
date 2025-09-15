using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    //References
    private Rigidbody rb;
    private InputManager inputManager;
    
    [Header("Player Settings")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHight;

    private void Awake()
    {
        // Make sure only one ScoreManager exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        inputManager.UpdateInputs();
    }

    private void FixedUpdate()
    {
        bool grounded = GroundedCheck();
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerSpeed);
        if (inputManager.jump && grounded)
        {
            rb.AddForce(transform.up * jumpHight, ForceMode.Impulse);
        }
        
        inputManager.ResetInputs();
    }

    private bool GroundedCheck()
    {
        LayerMask layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, -transform.up, transform.localScale.y + 0.1f, layerMask))
        {
            return true;
        }
        return false;
    }
}
