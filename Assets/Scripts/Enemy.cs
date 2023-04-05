using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]

public class Enemy : ScriptableObject
{
    public string enemyName;
    public float maxHealth;
    public float movementSpeed;
    public int baseDamage;
    public GameObject enemyPrefab;
    
}