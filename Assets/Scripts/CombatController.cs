using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange = 0.5f;

    [Header("Variaveis de Acesso")]
    [SerializeField] private MovementController movementController;
    [SerializeField] private Animator animator;

    [Header("Roll")]    
    [SerializeField] private float rollForce;
    public bool isAttacking = false;
    public bool isRolling = false;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
        movementController = GetComponent<MovementController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            lastPosition = transform.position;
            StartAttack();
        }    

        if (Input.GetButtonDown("Fire3") && !isAttacking && !isRolling && movementController.isGrounded)
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
        if(!isAttacking && !isRolling && movementController.isGrounded)
        {
            isAttacking = true;
            animator.SetTrigger("isAttacking");
        }
        else
        {
            return;
        }
        
    }

    private void OnAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("<color=#90EE90>Atigiu inimigo:</color> " + enemy.name);
        }

       
    }

    public void EndAttack()
    {
         isAttacking = false;
    }

    public void Roll()
    {
        if (!isRolling) 
        {
            animator.SetTrigger("Roll");
            movementController.rb.AddForce(transform.forward * rollForce, ForceMode.Impulse);
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
