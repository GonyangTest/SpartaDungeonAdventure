using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private Image _healthBar;

    private void Start()
    {
        GameManager.Instance.Controller.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthBar.fillAmount = (float)currentHealth / maxHealth;
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