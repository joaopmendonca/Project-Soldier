using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;

    void Start()
    {
        GameObject newEnemy = Instantiate(enemy.enemyPrefab);
    }
}