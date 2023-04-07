using UnityEngine;
using System.Collections;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange = 0.5f;

    [SerializeField] private Collider WeaponCollider;

    [Header("Variaveis de Acesso")]
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private Animator animator;

    [Header("Roll")]    
    [SerializeField] private float rollForce;
    public bool isAttacking = false;
    public bool isRolling = false;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
        playerMovementController = GetComponent<PlayerMovementController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            lastPosition = transform.position;
            StartAttack();
        }    

        if (Input.GetButtonDown("Fire3") && !isAttacking && !isRolling && playerMovementController.isGrounded)
        {
            Roll();
        }
        else if (Input.GetButtonDown("Fire3") && isRolling)
        {
            Debug.Log("<color=orange>You are already rolling!</color>");
        }       
    }

    private void StartAttack()
    {
        if(!isAttacking && !isRolling && playerMovementController.isGrounded)
        {
            WeaponCollider.enabled  = true;
            isAttacking = true;
            animator.SetTrigger("isAttacking");
        }
        else
        {
            return;
        }
    }

   

    public void EndAttack()
    {
         isAttacking = false;
         WeaponCollider.enabled  = false;
    }

    public void Roll()
    {
        if (!isRolling) 
        {
            animator.SetTrigger("Roll");
            playerMovementController.rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);
            isRolling = true;
        }
    }

    public void EndRoll()
    {
        isRolling = false;
        print("rolling == false");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponTransform.position, attackRange);
    }
}
