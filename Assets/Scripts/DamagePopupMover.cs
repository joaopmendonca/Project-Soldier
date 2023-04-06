using UnityEngine;

public class DamagePopupMover : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fallDelay = 1f;
    public float fallSpeed = 1f;

    private bool isFalling = false;
    private float fallTimer = 0f;

    private void Update()
    {
        if (!isFalling)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            fallTimer += Time.deltaTime;
            if (fallTimer >= fallDelay)
            {
                isFalling = true;
            }
        }
        else
        {
            transform.position -= Vector3.up * fallSpeed * Time.deltaTime;
        }
    }
}