using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    public void ApplyBounceForce() {
        GameManager.Instance.Controller.Jump(bounceForce, false);
    }

    public void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")) {
            ApplyBounceForce();
        }
    }
} 