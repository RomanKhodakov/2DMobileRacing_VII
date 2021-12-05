using System.Collections;
using UnityEngine;

public sealed class AbilityView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _selfDestructionTime = 2f;

    public Rigidbody2D ObjectRigidbody2D => _rigidbody2D;

    private void Awake()
    {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(_selfDestructionTime);
        Destroy(gameObject);
    }
}