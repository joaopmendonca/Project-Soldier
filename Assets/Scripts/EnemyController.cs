using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20; // Saúde máxima do inimigo
    private int currentHealth; // Saúde atual do inimigo

// É importante armazenar a posição inicial do inimigo para que ele possa voltar para lá depois de atingir o jogador
private Vector3 startingPos;

// Velocidade de movimento do inimigo
[SerializeField] private float movementSpeed = 3f;

// Referência ao Rigidbody do inimigo
private Rigidbody rb;

private void Start()
{
    currentHealth = maxHealth; // Define a saúde inicial como a saúde máxima
    startingPos = transform.position; // Armazena a posição inicial

    rb = GetComponent<Rigidbody>(); // Obtém o Rigidbody do inimigo
}

private void Update()
{
    // Move o inimigo em direção à posição inicial, com a velocidade definida
    rb.MovePosition(Vector3.MoveTowards(transform.position, startingPos, movementSpeed * Time.deltaTime));
}

// Função que recebe a quantidade de dano causado pelo jogador
public void TakeDamage(int damage)
{
    currentHealth -= damage; // Remove a quantia de dano da saúde atual do inimigo

    // Se a saúde atual for menor ou igual a zero, o inimigo é destruído
    if (currentHealth <= 0)
    {
        Destroy(gameObject);
    }
}
}