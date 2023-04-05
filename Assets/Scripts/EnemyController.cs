using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy enemyData;
    private float currentHealth;
    public float percHealth;
    public string enemyName;


    void Start()
    {
        enemyName = enemyData.enemyName;
        currentHealth = enemyData.maxHealth;
        percHealth = enemyData.maxHealth / currentHealth;
        print(enemyName);
    }

    void Update()
    {
        // Adicione aqui o código para controlar o comportamento do inimigo
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
        percHealth = enemyData.maxHealth / currentHealth;
    }

    private void Die()
    {
        // Adicione aqui o código para lidar com a morte do inimigo
        Destroy(gameObject);
    }
}