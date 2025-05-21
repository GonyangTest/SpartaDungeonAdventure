using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [Header("Condition")]
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _maxStamina = 100;
    private float _currentHealth;
    private float _currentStamina;
    [SerializeField] private int _useStaminaAmount = 10;
    [SerializeField] private float _staminaRecoveryAmount = 10;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    private Rigidbody rb;

    public LayerMask groundLayer;

    private Vector2 _moveInput;
    private Vector2 _MouseDelta;

    [Header("Look")]
    [SerializeField] private Transform _cameraTransform;
    private float _cameraCurrentXRotation;
    private float _minXLook = -80f;
    private float _maxXLook = 80f;
    private float _lookSensitivity = 0.1f;

    public event Action<float, float> OnHealthChanged;
    public event Action<float, float> OnStaminaChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _currentHealth = _maxHealth;
        _currentStamina = _maxStamina;
    }
    private void Start()
    {
        GameManager.Instance.Controller = this;
    }

    private void FixedUpdate()
    {
        Move();

        if(GameManager.Instance.Raycaster.IsClimbing)
        {
            rb.AddForce(Vector3.up * rb.mass * 9.81f, ForceMode.Force);
        }
    }

    private void Update()
    {
        CameraLook();

        if (_currentStamina < _maxStamina)
        {
            _currentStamina += _staminaRecoveryAmount * Time.deltaTime;
            OnStaminaChanged?.Invoke(_currentStamina, _maxStamina);
        }
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        moveDirection *= _moveSpeed;
        moveDirection.y = rb.velocity.y;
        rb.velocity = moveDirection;
    }

    public void UseStamina(int amount)
    {
        _currentStamina = Mathf.Clamp(_currentStamina - amount, 0, _maxStamina);
        OnStaminaChanged?.Invoke(_currentStamina, _maxStamina);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IInteractable interactable = GameManager.Instance.Raycaster.Interactable;
            interactable?.Interact();
        }
    }

    void CameraLook()
    {
        _cameraCurrentXRotation += _MouseDelta.y * _lookSensitivity;
        _cameraCurrentXRotation = Mathf.Clamp(_cameraCurrentXRotation, _minXLook, _maxXLook);
        _cameraTransform.localRotation = Quaternion.Euler(-_cameraCurrentXRotation, 0, 0);

        transform.rotation *= Quaternion.Euler(0, _MouseDelta.x * _lookSensitivity, 0);
    }


    public void Jump(float jumpForce)
    {
        rb.AddForce(Vector3.up * jumpForce + transform.forward, ForceMode.Impulse);
        UseStamina(_useStaminaAmount);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded() && _currentStamina >= _useStaminaAmount)
        {
            Jump(_jumpForce);
        }
        else if (context.performed && GameManager.Instance.Raycaster.IsClimbing)
        {
            // 캐릭터가 바라보는 방향
            Vector3 forward = transform.forward.normalized;
            // rb.AddForce(forward * -10f + Vector3.up * (_jumpForce / 2), ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _MouseDelta = context.ReadValue<Vector2>();
    }

    public bool isGrounded()
    {

        Ray[] rays = new Ray[4];
        rays[0] = new Ray(transform.position + (transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down); // 앞쪽 첫번째 레이어
        rays[1] = new Ray(transform.position + (-transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down); // 앞쪽 두번째 레이어
        rays[2] = new Ray(transform.position + (transform.right * 0.2f) + transform.up * 0.01f, Vector3.down); // 오른쪽 첫번째 레이어
        rays[3] = new Ray(transform.position + (-transform.right * 0.2f) + transform.up * 0.01f, Vector3.down); // 왼쪽 첫번째 레이어

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, 0.2f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage: " + damage);
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
    public void Heal(int amount)
    {
        Debug.Log("Heal: " + amount);
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
    public void ApplyForce(Vector3 force, ForceMode mode) { }

    public void StartItemEffectCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
} 