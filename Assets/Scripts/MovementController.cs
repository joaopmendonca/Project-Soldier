using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;    
    [SerializeField] private LayerMask groundLayer;
    public AudioClip[] jumpClips;   

    [Header("Variaveis de Acesso")]
    [SerializeField] private AudioController audioController;
    [SerializeField] private CombatController combatController;   
    public Rigidbody rb;
    private Animator animator;
    public bool isGrounded;
    private Vector3 movement;
    private float speed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
        
    }

    private void Update()
    {
        
        float sensitivity = 1.0f;
        float verticalInput = Input.GetAxis("Vertical") * sensitivity;
        float horizontalInput = Input.GetAxis("Horizontal") * sensitivity;

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 moveDirection = cameraForward * verticalInput + Camera.main.transform.right * horizontalInput;
        speed = Mathf.Clamp01(moveDirection.magnitude) * moveSpeed;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        animator.SetFloat("Speed", speed);

        movement = moveDirection.normalized;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }


        float verticalSpeed = Vector3.Dot(rb.velocity, transform.forward.normalized);
        float horizontalSpeed = Vector3.Dot(rb.velocity, transform.right.normalized);

        animator.SetFloat("VerticalSpeed", verticalSpeed);
        animator.SetFloat("HorizontalSpeed", horizontalSpeed);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

   private void Move()
{    
    
    if (combatController.isAttacking || combatController.isRolling)
    {
        return;
    }

    else if (isGrounded)
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    else if (!isGrounded)
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime * 0.5f);
    }
}


   private void Rotate()
{    
    if (combatController.isAttacking || combatController.isRolling)
        
        return;

    if (movement != Vector3.zero)
    {
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
    }
}

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            int randJumpSound = Random.Range(0, jumpClips.Length);
            audioController.PlaySound(jumpClips[randJumpSound]);
        }
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + animator.deltaPosition);
        rb.rotation = animator.rootRotation;
    }   
    
}

    

