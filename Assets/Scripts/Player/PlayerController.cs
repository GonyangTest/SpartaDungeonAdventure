using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private Rigidbody rb;

    public event Action<int, int> OnHealthChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void Move() { }
    private void Jump() { }
    public void TakeDamage(int damage) { }
    public void Heal(int amount) { }
    public void ApplyForce(Vector3 force, ForceMode mode) { }
    public void UseItem(Item item) { }
} 