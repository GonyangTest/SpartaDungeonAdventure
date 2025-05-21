using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _staminaBar;

    private void Start()
    {
        GameManager.Instance.Controller.OnHealthChanged += UpdateHealthBar;
        GameManager.Instance.Controller.OnStaminaChanged += UpdateStaminaBar;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateStaminaBar(float currentStamina, float maxStamina)
    {
        _staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void ShowInteractionUI(IInteractable interactable)
    {
        _interactionUI.GetComponent<InteractionUI>().ShowInteractionUI(interactable);
    }

    public void HideInteractionUI()
    {
        _interactionUI.GetComponent<InteractionUI>().HideInteractionUI();
    }
} 