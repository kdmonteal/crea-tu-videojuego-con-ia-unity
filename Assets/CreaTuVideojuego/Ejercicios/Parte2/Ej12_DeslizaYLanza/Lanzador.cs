using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Lanzador : MonoBehaviour
{
    [SerializeField] float multiplicador = 0.6f;
    [SerializeField] float fuerzaMaxima = 12f;
    [SerializeField] float distanciaMinima = 0.3f; // en unidades del mundo

    Rigidbody2D rb;
    Camera cam;
    Vector2 inicio;
    float tiempoInicio;
    bool lanzado;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        var p = Pointer.current;
        if (lanzado || p == null) return;

        Vector2 dedo = cam.ScreenToWorldPoint(p.position.ReadValue());

        if (p.press.wasPressedThisFrame)
        {
            inicio = dedo;
            tiempoInicio = Time.time;
        }
        else if (p.press.wasReleasedThisFrame)
        {
            Vector2 gesto = dedo - inicio;
            if (gesto.magnitude < distanciaMinima) return; // fue un toque

            float duracion = Mathf.Max(Time.time - tiempoInicio, 0.05f);
            float fuerza = gesto.magnitude / duracion * multiplicador;
            fuerza = Mathf.Min(fuerza, fuerzaMaxima);

            rb.AddForce(gesto.normalized * fuerza, ForceMode2D.Impulse);
            lanzado = true;
        }
    }
}
