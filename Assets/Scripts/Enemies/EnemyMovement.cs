using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    private EnemyController enemyController;
    private Rigidbody rigidBody;
    private Transform target;
    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        SetDestination();
    }

  private void SetDestination()
{
    float distanceToPlayer = Vector3.Distance(transform.position, enemyController.player.position);
    float distanceToCrystal = Vector3.Distance(transform.position, enemyController.crystal.position);

    if (distanceToPlayer <= enemyController.playerDetectionRange)
    {
        target = enemyController.player;
    }
    else if (distanceToCrystal <= enemyController.crystalDetectionRange)
    {
        target = enemyController.crystal;
    }

    if (target != null)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > enemyController.attackRange)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            rigidBody.velocity = moveDirection * enemyController.movementSpeed;
            RotateTowards(target.position);
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }
    }
}

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemyController.rotationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            rigidBody.AddForce(direction * enemyController.movementSpeed, ForceMode.Impulse);
        }
    }
}