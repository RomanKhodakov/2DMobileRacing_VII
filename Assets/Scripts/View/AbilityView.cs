using UnityEngine;

public sealed class AbilityView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public Rigidbody2D ObjectRigidbody2D => _rigidbody2D;
}