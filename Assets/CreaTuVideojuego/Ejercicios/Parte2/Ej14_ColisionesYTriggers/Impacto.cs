using UnityEngine;

public class Impacto : MonoBehaviour
{
    [SerializeField] float golpeFuerte = 5f;

    void OnCollisionEnter2D(Collision2D choque)
    {
        float fuerza = choque.relativeVelocity.magnitude;
        Debug.Log($"Choqué con {choque.gameObject.name}, fuerza {fuerza:F1}");

        if (fuerza > golpeFuerte)
            Debug.Log("¡Golpe fuerte!"); // luego: sonido o partículas
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Fruta"))
            Debug.Log("Entré en una fruta");
    }
}
