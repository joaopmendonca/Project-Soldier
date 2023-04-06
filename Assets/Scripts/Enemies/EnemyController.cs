using UnityEngine;
using System;
public class EnemyController : MonoBehaviour
{
    public static event Action<int, Vector3> OnEnemyTakeDamage;

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

    private bool alreadyHit = false; // Adiciona a variável alreadyHit

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
    
     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            if (!alreadyHit) // Verifica se o inimigo já foi atingido
            {
                // Aplicar dano ou executar outra ação no inimigo
                TakeDamage(10);
                CalculePercHealth();
                alreadyHit = true; // Define alreadyHit como verdadeiro
            }
        }
    }

    private void OnTriggerExit(Collider other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Weapon"))
    {
        alreadyHit = false;
    }
}

   public void TakeDamage(int damage)
{
    Debug.Log("TakeDamage called with damage: " + damage);

    currentHealth -= damage;
    CalculePercHealth();
    OnEnemyTakeDamage?.Invoke(damage, transform.position);

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