using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInteraction : MonoBehaviour
{
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _promptText;
    private void Start()
    {
        HideInteractionUI();
    }
    public void ShowInteractionUI(IInteractable interactable)
    {
        gameObject.SetActive(true);
        _nameText.text = interactable.Name;
        _promptText.text = interactable.InteractionPrompt;
    }

    public void HideInteractionUI()
    {
        gameObject.SetActive(false);
    }
}

public interface IInteractable
{
    public string Name { get; }
    public string InteractionPrompt { get; set; }
    public void Interact();
}