using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject enemyStatsWindow;
    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private Image enemyHpBar;

    [SerializeField] private LockOnController lockOnController;

    void Start()
    {
        lockOnController = FindObjectOfType<LockOnController>();
        if (lockOnController == null)
        {
            Debug.LogError("LockOnController não encontrado na cena.");
        }
    }

    void Update()
    {
        if (lockOnController.CurrentEnemy != null)
        {
            enemyStatsWindow.SetActive(true);
            enemyNameText.text = lockOnController.CurrentEnemy.enemyName;
            enemyHpBar.fillAmount = lockOnController.CurrentEnemy.percHealth;
        }
        else
        {
            // Adicione aqui o código para lidar com o caso em que nenhum inimigo está sendo alvejado
            enemyStatsWindow.SetActive(false);
        }
        if (Input.GetButtonDown("Menu"))
        {
            pausePanel.SetActive(true);
        }
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}