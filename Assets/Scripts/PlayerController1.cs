using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    //References
    private Rigidbody rb;
    [SerializeField] private BoxCollider grindHitbox; //necessary for the gizmo to work
    private InputManager inputManager;
    
    [Header("Player Settings")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHight;
    [SerializeField] private float coyoteTime; //not implemented yet

    [Header("GrindHitbox")]
    [SerializeField] private float hitBoxHeight;
    [SerializeField] private LayerMask grindLayerMask;
    
    //private stuff
    private bool grounded;
    private bool onRail;
    private bool underRail;

    #region Unity Methods
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
        PhysicsChecks();
        
        ProcessJumps();
        ProcessGrinds();
        
        ResetVariables(grounded);
    }
    #endregion
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

    private void ProcessJumps()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerSpeed); //setting horizontal velocity to be constant
        
        if (inputManager.jump && (grounded || onRail)) //jumping / trick
        {
            rb.AddForce(transform.up * jumpHight, ForceMode.Impulse);
            DoTrick();
        }
    }
    private void ProcessGrinds()
    {
        if (!grounded && onRail && inputManager.trick != TrickDirection.None && !grindHitbox.enabled) //grinding
        {
            grindHitbox.enabled = true;
            Debug.Log($"grindHitbox: {grindHitbox.enabled}");
        }
        if (!grounded && inputManager.trick != TrickDirection.None &&  underRail) grindHitbox.enabled = false;
        
        if (onRail && grindHitbox.enabled && rb.linearVelocity.y <= 1) DoGrind(); // THIS IS ALSO TRIGGERED WHEN PASSING THE RAIL FROM BELOW. Ez fix is checking if rb.y speed is 0 or lower so skims get skipped
    }

    private void PhysicsChecks()
    {
        LayerMask groundLayerMask = LayerMask.GetMask("Ground");
        grounded = Physics.Raycast(transform.position, -transform.up, transform.localScale.y + 0.1f, groundLayerMask);
        
        Vector3 hitBoxSize = new Vector3(grindHitbox.size.x, hitBoxHeight, grindHitbox.size.z);
        onRail = Physics.CheckBox(transform.position + grindHitbox.center - 0.5f * (grindHitbox.size.y + hitBoxHeight) * transform.up, 0.5f * hitBoxSize, Quaternion.identity, grindLayerMask);
        underRail = Physics.CheckBox(transform.position + grindHitbox.center + (0.5f * (hitBoxHeight + 0.2f) * transform.up), 0.5f * new Vector3(hitBoxSize.x, grindHitbox.size.y + hitBoxHeight, hitBoxSize.z), Quaternion.identity, grindLayerMask);
    }

    private void ResetVariables(bool grounded)
    {
        inputManager.ResetInputs();

        if ((inputManager.trick == TrickDirection.None || grounded) && grindHitbox.enabled)
        {
            grindHitbox.enabled = false;
            Debug.Log($"grindHitbox: {grindHitbox.enabled}");
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 hitBoxSize = new Vector3(grindHitbox.size.x, hitBoxHeight, grindHitbox.size.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + grindHitbox.center - 0.5f * (grindHitbox.size.y + hitBoxHeight) * transform.up, hitBoxSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + grindHitbox.center + 0.5f * (hitBoxHeight + 0.2f) * transform.up, new Vector3(hitBoxSize.x, grindHitbox.size.y + hitBoxHeight, hitBoxSize.z));
    }
}
