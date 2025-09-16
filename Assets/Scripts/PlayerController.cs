using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    //References
    private Rigidbody rb;
    private BoxCollider grindHitbox;
    private InputManager inputManager;
    
    [Header("Player Settings")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHight;
    [SerializeField] private float coyoteTime; //not implemented yet

    [Header("GrindHitbox")]
    [SerializeField] private Vector3 halfExtends;
    [SerializeField] private Vector3 center;
    [SerializeField] private LayerMask grindLayerMask;

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
        grindHitbox = gameObject.GetComponent<BoxCollider>();
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        inputManager.UpdateInputs();
    }

    private void FixedUpdate()
    {
        bool grounded = GroundedCheck();
        bool OnRail = OnGrindCheck();
        
        Debug.Log($"Grounded: {grounded}, OnRail: {OnRail}");
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerSpeed);
        if (inputManager.jump && (grounded || OnRail))
        {
            rb.AddForce(transform.up * jumpHight, ForceMode.Impulse);
            DoTrick();
        }

        if (OnRail && inputManager.trick != TrickDirection.None && rb.linearVelocity.y < 0)
        {
            grindHitbox.enabled = true;
            DoGrind();
        }
        
        ResetVariables(grounded);
    }

    private void DoTrick()
    {
        switch (inputManager.trick)
        {
            case TrickDirection.Up:
            {
                Debug.Log("UpTrick");
                break;
            }
            case TrickDirection.Down:
            {
                Debug.Log("DownTrick");
                break;
            }
            case TrickDirection.Left:
            {
                Debug.Log("LeftTrick");
                break;
            }
            case TrickDirection.Right:
            {
                Debug.Log("RightTrick");
                break;
            }
            case TrickDirection.None:
            {
                Debug.Log("Ollie");
                break;
            }
        }
    }
    private void DoGrind()
    {
        switch (inputManager.trick)
        {
            case TrickDirection.Up:
            {
                Debug.Log("UpGrind");
                break;
            }
            case TrickDirection.Down:
            {
                Debug.Log("DownGrind");
                break;
            }
            case TrickDirection.Left:
            {
                Debug.Log("LeftGrind");
                break;
            }
            case TrickDirection.Right:
            {
                Debug.Log("RightGrind");
                break;
            }
            case TrickDirection.None:
            {
                Debug.Log("No Grind");
                break;
            }
        }
    }

    private bool GroundedCheck()
    {
        LayerMask groundLayerMask = LayerMask.GetMask("Ground");
        return Physics.Raycast(transform.position, -transform.up, transform.localScale.y + 0.1f, groundLayerMask);
    }

    private bool OnGrindCheck()
    {
        return Physics.CheckBox(transform.position + center, halfExtends, Quaternion.identity, grindLayerMask);
    }

    private void ResetVariables(bool grounded)
    {
        inputManager.ResetInputs();

        if (inputManager.trick == TrickDirection.None || grounded)
        {
            grindHitbox.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + center, 2 * halfExtends);
    }
}
