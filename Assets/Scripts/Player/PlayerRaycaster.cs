using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [SerializeField] private float _raycastInterval = 0.05f;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private LayerMask _climbableLayerMask;
    [SerializeField] private Transform _head;

    private IInteractable _interactable;
    public IInteractable Interactable => _interactable;

    private float _lastRaycastTime = 0;

    private Camera _camera;

    private Rigidbody _rigidbody;

    private bool _isClimbing = false;
    public bool IsClimbing => _isClimbing;

    private Vector3 _climbableNormalPoint;
    public Vector3 ClimbableNormalPoint => _climbableNormalPoint;

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        GameManager.Instance.Raycaster = this;
    }

    private void Update()
    {
        if (Time.time - _lastRaycastTime < _raycastInterval) return;
            _lastRaycastTime = Time.time;  
             
        Debug.Log(_rigidbody.velocity);
        RaycastInteractableObject();
        RaycastClimbableObject();
    }

    public void RaycastClimbableObject()
    {
        Ray ray = new Ray(_head.position, _head.forward);
        Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.red);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 0.5f, _climbableLayerMask))
        {
            if(!GameManager.Instance.Controller.isGrounded())
            {
                if (_isClimbing)
                {
                    Vector3 velocity = _rigidbody.velocity;
                    velocity.y = 0;
                    _rigidbody.velocity = velocity; 
                }
                _isClimbing = true;
            }
        }
        else
        {
            _isClimbing = false;
        }
    }

    public void RaycastInteractableObject()
    {
        Ray ray = new Ray(_head.position, _head.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.5f, _interactableLayerMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                UIManager.Instance.ShowInteractionUI(interactable);
                _interactable = interactable;
            }
            else
            {
                UIManager.Instance.HideInteractionUI();
                //TODO: 구조 변경 필요
                if(_interactable != null && _interactable is Chest)
                {
                    ((Chest)_interactable).Close();
                }
                _interactable = null;
            }
        }
        else
        {
            UIManager.Instance.HideInteractionUI();
            //TODO: 구조 변경 필요
            if(_interactable != null && _interactable is Chest)
            {
                ((Chest)_interactable).Close();
            }
            _interactable = null;
        }
    }
} 