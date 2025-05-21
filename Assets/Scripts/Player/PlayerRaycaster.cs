using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float _raycastInterval = 0.05f;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _interactableLayerMask;

    private IInteractable _interactable;
    public IInteractable Interactable => _interactable;

    private float _lastRaycastTime = 0;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        GameManager.Instance.Raycaster = this;
    }

    private void Update()
    {
        RaycastObject();
    }

    public void RaycastObject()
    {
        if (Time.time - _lastRaycastTime < _raycastInterval) return;
        _lastRaycastTime = Time.time;

        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance, _interactableLayerMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                UIManager.Instance.ShowInteractionUI(interactable);
                _interactable = interactable;
            }
            else
            {
                UIManager.Instance.HideInteractionUI();
                _interactable = null;
            }
        }
        else
        {
            UIManager.Instance.HideInteractionUI();
            _interactable = null;
        }
    }
} 