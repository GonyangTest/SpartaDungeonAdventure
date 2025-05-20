using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField]private int _currentHealth;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    private Rigidbody rb;

    public LayerMask groundLayer;

    private Vector2 _moveInput;

    public event Action<int, int> OnHealthChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _currentHealth = _maxHealth;
        
    }
    private void Start()
    {
        PlayerManager.Instance.Controller = this;
        TakeDamage(70);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
        moveDirection *= _moveSpeed;
        moveDirection.y = rb.velocity.y;
        rb.velocity = moveDirection;
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
    private void Jump()
    {
        rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded())
        {
            Jump();
        }
    }

    private bool isGrounded()
    {

        Ray[] rays = new Ray[4];
        rays[0] = new Ray(transform.position + (transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down); // 앞쪽 첫번째 레이어
        rays[1] = new Ray(transform.position + (-transform.forward * 0.2f) + transform.up * 0.01f, Vector3.down); // 앞쪽 두번째 레이어
        rays[2] = new Ray(transform.position + (transform.right * 0.2f) + transform.up * 0.01f, Vector3.down); // 오른쪽 첫번째 레이어
        rays[3] = new Ray(transform.position + (-transform.right * 0.2f) + transform.up * 0.01f, Vector3.down); // 왼쪽 첫번째 레이어

        foreach (Ray ray in rays)
        {
            Debug.Log(ray.origin);
            Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red);
            if (Physics.Raycast(ray, 0.1f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
    public void Heal(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
    public void ApplyForce(Vector3 force, ForceMode mode) { }

    public void StartItemEffectCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
} 