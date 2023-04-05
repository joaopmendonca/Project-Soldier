using Cinemachine;
using UnityEngine;


public class LockOnController : MonoBehaviour
{
    public Transform lookAt;
    public float enemySearchRadius = 10f; // O raio em que procura inimigos
    public LayerMask enemyLayer;
    public string lockOnInput = "LockOn"; // O nome da entrada que ativa o movimento e rotação da câmera
    public CinemachineFreeLook freeLookCamera;


    private Vector3 velocity; // A velocidade atual da suavização da câmera

    private Transform currentEnemy;
    public EnemyController CurrentEnemy { get; private set; }

    /*private Transform currentEnemy; - Esta linha declara uma variável privada do tipo Transform chamada currentEnemy.
    Esta variável é usada para armazenar o componente Transform do inimigo atualmente alvejado pelo sistema de travamento (lock-on). 
    É privada, o que significa que só pode ser acessada dentro do script LockOnController.

    public EnemyController CurrentEnemy { get; private set; } - Esta linha declara uma propriedade pública do tipo EnemyController chamada CurrentEnemy.
    Esta propriedade é usada para armazenar uma referência ao script EnemyController do inimigo atualmente alvejado pelo sistema de travamento (lock-on).
    É pública, o que significa que pode ser acessada por outros scripts.No entanto, o acessador set é privado, o que significa que o valor da propriedade só pode ser alterado dentro do script LockOnController.

    Essas duas variáveis são usadas em conjunto para armazenar informações sobre o inimigo atualmente alvejado pelo sistema de travamento (lock-on) e permitir que outros scripts acessem essas informações.*/

    void Start()
    {
        freeLookCamera.Follow = lookAt; // Define o objeto de jogo seguido pela câmera como o jogador
    }

    void Update()
    {
        if (Input.GetButton(lockOnInput))
        {
            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                currentEnemy = closestEnemy; // Define o inimigo atualmente alvejado como o inimigo mais próximo encontrado
                freeLookCamera.LookAt = closestEnemy; 
                Vector3 targetPosition = Quaternion.LookRotation(closestEnemy.position - lookAt.position).eulerAngles; // Calcula a rotação necessária para olhar para o inimigo mais próximo
                freeLookCamera.m_XAxis.Value = Mathf.SmoothDampAngle(freeLookCamera.m_XAxis.Value, targetPosition.y, ref velocity.y, 0.3f); // Suaviza a rotação da câmera em torno do eixo Y para olhar para o inimigo mais próximo
                CurrentEnemy = closestEnemy.GetComponent<EnemyController>(); // Obtém a referência ao script EnemyController do inimigo mais próximo encontrado
            }
            else // Se nenhum inimigo foi encontrado
            {
                currentEnemy = null; 
                freeLookCamera.LookAt = lookAt;
                CurrentEnemy = null;
            }
        }
        else 
        {
            currentEnemy = null; // Limpa a variável do inimigo atualmente alvejado
            freeLookCamera.LookAt = lookAt; // Define o objeto de jogo olhado pela câmera como o jogador
            CurrentEnemy = null; // Limpa a referência ao script EnemyController do inimigo atualmente alvejado
        }

        // Desativa a entrada do usuário para o eixo X se o botão LockOn estiver pressionado (para impedir que o usuário mova a câmera horizontalmente enquanto estiver travando em um inimigo)
        if (Input.GetButton(lockOnInput) && currentEnemy != null)
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "";
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "Horizontal Right";
        }
    }

    
    Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity; 

        // Obtém todos os objetos de jogo inimigos dentro do raio de pesquisa
        Collider[] enemiesInRange = Physics.OverlapSphere(lookAt.position, enemySearchRadius, enemyLayer);

        // Percorre todos os objetos de jogo inimigos encontrados
        foreach (Collider enemyCollider in enemiesInRange)
        {
            // Calcula a distância entre o jogador e o objeto de jogo inimigo atual
            float distanceToEnemy = Vector3.Distance(lookAt.position, enemyCollider.transform.position);

            // Se a distância for menor que a distância mais próxima atual, atualiza a variável do inimigo mais próximo e a distância mais próxima
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemyCollider.transform;
            }
        }

        return closestEnemy;
    }
}