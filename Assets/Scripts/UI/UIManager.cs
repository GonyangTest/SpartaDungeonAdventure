using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _promptText;
    [SerializeField] private Text _interactionText;
    [SerializeField] private Image _healthBar;

    private void Start()
    {
        GameManager.Instance.Controller.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
} 