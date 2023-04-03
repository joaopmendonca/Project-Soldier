using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockOnController : MonoBehaviour
{
    public Transform playerTransform; // O componente Transform do jogador
    public float enemySearchRadius = 10f; // O raio em que procurar inimigos
    public LayerMask enemyLayer; // A camada dos objetos de jogo inimigos
    public string lockOnInput = "LockOn"; // O nome da entrada que ativa o movimento e rotação da câmera
    public CinemachineFreeLook freeLookCamera; // O componente da câmera FreeLook

    private Transform currentEnemy; // O inimigo atualmente alvejado
    private Vector3 velocity; // A velocidade atual da suavização da câmera

    void Start()
    {
        freeLookCamera.Follow = playerTransform;
    }

    void Update()
    {
        if (Input.GetButton(lockOnInput))
        {
            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                currentEnemy = closestEnemy;
                freeLookCamera.LookAt = closestEnemy;
                Vector3 targetPosition = Quaternion.LookRotation(closestEnemy.position - playerTransform.position).eulerAngles;
                freeLookCamera.m_XAxis.Value = Mathf.SmoothDampAngle(freeLookCamera.m_XAxis.Value, targetPosition.y, ref velocity.y, 0.3f);
            }
            else
            {
                currentEnemy = null;
                freeLookCamera.LookAt = playerTransform;
            }
        }
        else
        {
            currentEnemy = null;
            freeLookCamera.LookAt = playerTransform;
        }

        // Desativa a entrada do usuário para o eixo X se o botão LockOn estiver pressionado
        freeLookCamera.m_XAxis.m_InputAxisName = Input.GetButton(lockOnInput) ? "" : "Horizontal Right";
    }

    // Encontra o inimigo mais próximo dentro do raio de pesquisa
    Transform FindClosestEnemy()
    {
        Transform closestEnemy = null; // Inicializa a variável do inimigo mais próximo como nula
        float closestDistance = Mathf.Infinity; // Inicializa a distância mais próxima como infinita

        // Procura por todos os objetos de jogo na camada inimiga dentro do raio de pesquisa
        Collider[] enemiesInRange = Physics.OverlapSphere(playerTransform.position, enemySearchRadius, enemyLayer);
        foreach (Collider enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}