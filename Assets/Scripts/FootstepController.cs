// Este código define um script FootstepController em Unity que lida com a reprodução de sons de passos.
// Ele requer um objeto AudioController e um array de AudioClips contendo os sons de passos a serem reproduzidos.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
[SerializeField] private AudioController audioController;
[SerializeField] private AudioClip[] footstepSounds;
[SerializeField] private float footstepInterval = 0.5f;
[SerializeField] private LayerMask groundLayer;
[SerializeField] private float movementThreshold = 0.1f; //Velocidade mínima de movimento para a reprodução de sons de passos

// Private fields used by the script
private float lastFootstepTime;
private bool isGrounded;
private int currentFootstep;

void Update()
{
     // Verifique se o personagem está no chão lançando um raio na camada do chão
    bool wasGrounded = isGrounded;
    isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);

    // Se o personagem não estiver no chão antes e agora estiver, reproduza um som de passo
if (!wasGrounded && isGrounded)
{
    PlayNextFootstepSound();
}
// Se o personagem estiver no chão e se movendo a uma velocidade maior que a threshold, reproduza sons de passos em um intervalo definido\n        if (isGrounded && GetComponent<Rigidbody>().velocity.magnitude > movementThreshold)\n        {\n            // Se o tempo atual for maior ou igual ao lastFootstepTime mais o intervalo de tempo, reproduza um som de passo
if (isGrounded && GetComponent<Rigidbody>().velocity.magnitude > movementThreshold)
{
    if (Time.time >= lastFootstepTime + footstepInterval)
    {
        PlayNextFootstepSound();
        lastFootstepTime = Time.time;
    }
}

}
// Método que reproduz o próximo som de passo no array e lida com o retorno ao início
private void PlayNextFootstepSound()
{
    if (footstepSounds.Length > 0 && audioController != null)
    {
        // Obtenha o som de passo atual e incremente o contador currentFootstep, voltando para 0 uma vez que atingir o final do array
        AudioClip footstepSound = footstepSounds[currentFootstep];
        currentFootstep = (currentFootstep + 1) % footstepSounds.Length;
        // Reproduza o som de passo usando o objeto audioController
        audioController.PlaySound(footstepSound);
    }
} 
}


