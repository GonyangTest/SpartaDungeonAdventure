using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    public void ApplyBounceForce() {
        GameManager.Instance.Controller.Jump(bounceForce);
    }

    public void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log("JumpPad: " + other.gameObject.name);
            ApplyBounceForce();
        }
    }
} 