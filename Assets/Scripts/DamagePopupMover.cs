using UnityEngine;
using TMPro;
public class DamagePopupMover : MonoBehaviour
{
public float moveSpeed = 1f;
public float fadeDelay = 1f;
public float fadeSpeed = 1f;
private float fadeTimer = 0f;
private TextMeshProUGUI textMesh;

private void Start()
{
    textMesh = GetComponent<TextMeshProUGUI>();
}

private void Update()
{
    fadeTimer += Time.deltaTime;

    if (fadeTimer >= fadeDelay)
    {
        Color color = textMesh.color;
        if (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    else
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        Color color = textMesh.color;
        color.a = 1f - (fadeTimer / fadeDelay);
        textMesh.color = color;
    }
}
}
