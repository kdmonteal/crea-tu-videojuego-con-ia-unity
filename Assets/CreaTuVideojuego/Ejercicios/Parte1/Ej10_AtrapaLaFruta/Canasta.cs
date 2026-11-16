using UnityEngine;
using UnityEngine.InputSystem;

public class Canasta : MonoBehaviour
{
    [SerializeField] float limiteX = 2.2f;
    [SerializeField] float suavizado = 15f;
    int puntos;
    Camera cam;

    void Start() { cam = Camera.main; }

    void Update()
    {
        var p = Pointer.current;
        if (p == null || !p.press.isPressed) return;

        // Convierte la posición del dedo (píxeles) a coordenadas del mundo
        Vector2 pantalla = p.position.ReadValue();
        Vector3 mundo = cam.ScreenToWorldPoint(pantalla);

        float x = Mathf.Clamp(mundo.x, -limiteX, limiteX);
        Vector3 destino = new Vector3(x, transform.position.y, 0f);
        transform.position = Vector3.Lerp(transform.position, destino,
                                          suavizado * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Fruta"))
        {
            puntos++;
            Debug.Log("Puntos: " + puntos);
            Destroy(otro.gameObject);
        }
    }
}
