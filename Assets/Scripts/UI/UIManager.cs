using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text promptText;
    [SerializeField] private Text interactionText;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
} 