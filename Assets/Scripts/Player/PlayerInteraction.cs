using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float checkInterval;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private UIManager uiManager;

    private void SetPromptText() { }
    private void OnInteractInput() { }
} 