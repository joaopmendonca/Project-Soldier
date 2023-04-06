using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public Canvas mainCanvas;
    public Camera mainCamera;

    private void OnEnable()
    {
        EnemyController.OnEnemyTakeDamage += ShowDamage;
    }

    private void OnDisable()
    {
        EnemyController.OnEnemyTakeDamage -= ShowDamage;
    }

    public void ShowDamage(int damageAmount, Vector3 enemyPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(enemyPosition);
        GameObject damagePopupInstance = Instantiate(damagePopupPrefab, mainCanvas.transform);
        damagePopupInstance.transform.position = screenPosition;
        damagePopupInstance.GetComponentInChildren<TextMeshProUGUI>().text = damageAmount.ToString();
    }
}