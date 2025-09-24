using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    //References
    private Rigidbody rb;
    [SerializeField] private BoxCollider grindHitbox; //necessary for the gizmo to work
    private InputManager inputManager;
    private ScoreManager scoreManager;
    
    [Header("Player Settings")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHight;
    [SerializeField] private float coyoteTime; //not implemented yet

    [Header("GrindHitbox")]
    [SerializeField] private float hitBoxHeight;
    [SerializeField] private LayerMask grindLayerMask;
    
    //private stuff
    private bool grounded;
    private bool platform;
    private bool onRail;
    private bool underRail;
    private bool mountedRail;
    private bool tricking; //used to track if player is currently tricking or not so certain processes in ResetVariables() dont occur more than once

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
        scoreManager = ScoreManager.Instance;
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
        tricking = true;
        scoreManager.AddTrickToCombo();
    }
    private void DoGrind()
    {
        tricking = true;
        scoreManager.AddGrindToCombo();
    }

    private void ProcessJumps()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, playerSpeed); //setting horizontal velocity to be constant
        
        if (inputManager.jump && (grounded || platform || onRail)) //jumping / trick
        {
            rb.AddForce(transform.up * jumpHight, ForceMode.Impulse);
            DoTrick();
            //VFXsManager.instance.CameraShake(true);
        }
    }
    private void ProcessGrinds()
    {
        if (!grounded && !platform && onRail && inputManager.trick != TrickDirection.None && !grindHitbox.enabled) //grinding
        {
            grindHitbox.enabled = true;
            Debug.Log($"grindHitbox: {grindHitbox.enabled}");
        }
        if (!grounded && !platform && inputManager.trick != TrickDirection.None &&  underRail) grindHitbox.enabled = false;
        
        if (mountedRail && grindHitbox.enabled && rb.linearVelocity.y <= 1) DoGrind();
    }

    private void PhysicsChecks()
    {
        LayerMask groundLayerMask = LayerMask.GetMask("Ground");
        LayerMask platformLayerMask = LayerMask.GetMask("Platforms");
        grounded = Physics.Raycast(transform.position, -transform.up, transform.localScale.y + 0.1f, groundLayerMask);
        platform = Physics.Raycast(transform.position, -transform.up, transform.localScale.y + 0.1f, platformLayerMask);
        
        Vector3 hitBoxSize = new Vector3(grindHitbox.size.x, hitBoxHeight, grindHitbox.size.z);
        bool railCheck = Physics.CheckBox(transform.position + grindHitbox.center - 0.5f * (grindHitbox.size.y + hitBoxHeight) * transform.up, 0.5f * hitBoxSize, Quaternion.identity, grindLayerMask);
        if (!onRail && railCheck) mountedRail = true;
        else mountedRail = false;
        onRail = railCheck;
        underRail = Physics.CheckBox(transform.position + grindHitbox.center + (0.5f * (hitBoxHeight + 0.2f) * transform.up), 0.5f * new Vector3(hitBoxSize.x, grindHitbox.size.y + hitBoxHeight, hitBoxSize.z), Quaternion.identity, grindLayerMask);
    }

    private void ResetVariables(bool grounded)
    {
        inputManager.ResetInputs();
        if (grounded && rb.linearVelocity.y < 0f && tricking)
        {
            tricking = false;
            scoreManager.CalculateNewScore();
            scoreManager.StopCombo();
        }
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
