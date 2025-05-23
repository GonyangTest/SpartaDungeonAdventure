using UnityEngine;
using System.Collections;

public class DisappearBlock : MonoBehaviour
{
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _appearTime = 5f;

    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(_waitTime);
        _meshRenderer.enabled = false;
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(_appearTime);
        _meshRenderer.enabled = true;
        _boxCollider.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }
}