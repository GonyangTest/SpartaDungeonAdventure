using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private GameObject _playerInventory;
    [SerializeField] private GameObject _chestInventory;
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
        _interactionUI.GetComponent<UIInteraction>().ShowInteractionUI(interactable);
    }

    public void HideInteractionUI()
    {
        _interactionUI.GetComponent<UIInteraction>().HideInteractionUI();
    }

    public void OpenChestInventory()
    {
        IInteractable chest = GameManager.Instance.Raycaster.Interactable;
        _chestInventory.GetComponent<UIChestInventory>().OpenInventory(chest as Chest);
    }

    public void CloseChestInventory()
    {
        _chestInventory.GetComponent<UIChestInventory>().CloseInventory();
    }

    public void TogglePlayerInventory()
    {
        _playerInventory.GetComponent<UIPlayerInventory>().Toggle();
    }
} 