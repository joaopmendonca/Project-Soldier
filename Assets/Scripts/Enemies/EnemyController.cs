using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Characteristics")]
    public Enemy enemyData;
    public float currentHealth;
    public float percHealth;
    public string enemyName;
    public float movementSpeed;
    public float rotationSpeed;
    public float attackRange;

    [Header("Destination Settings")]
    public Transform player;
    public Transform crystal;
    public float playerDetectionRange = 10f;
    public float crystalDetectionRange = 20f;

    private void Awake()
    {
        enemyName = enemyData.enemyName;
        currentHealth = enemyData.maxHealth;
        percHealth = enemyData.maxHealth / currentHealth;
        movementSpeed = enemyData.movementSpeed;
        rotationSpeed = enemyData.rotationSpeed;
        attackRange = enemyData.attackRange;

        // Busca o jogador pela tag "Player"
        player = GameObject.FindWithTag("Player").transform;

        // Busca o cristal pela tag "Crystal"
        crystal = GameObject.FindWithTag("Crystal").transform;

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        CalculePercHealth();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void CalculePercHealth()
    {
        percHealth = currentHealth / enemyData.maxHealth;
    }

    private void Die()
    {
        // Adicione aqui o código para lidar com a morte do inimigo
        Destroy(gameObject);
    }
}
