using UnityEngine;
using UnityEngine.Pool;

public class FrutaPool : MonoBehaviour
{
    public IObjectPool<FrutaPool> Pool { get; set; }
    [SerializeField] float limiteY = -6f;
    Rigidbody2D rb;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void OnEnable() { if (rb != null) rb.linearVelocity = Vector2.zero; }

    void Update()
    {
        if (transform.position.y < limiteY) Pool.Release(this);
    }
}
