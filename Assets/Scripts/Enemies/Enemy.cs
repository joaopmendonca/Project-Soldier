using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]

public class Enemy : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float movementSpeed;
    public float rotationSpeed;
    public float attackRange;
    public int damage;    
    public int armor; 
    public GameObject enemyPrefab;
    
}