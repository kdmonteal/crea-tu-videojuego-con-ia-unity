using UnityEngine;
using UnityEngine.InputSystem;

public class Contador : MonoBehaviour
{
    // Variables: guardan datos
    [SerializeField] int puntos = 0;          // entero
    [SerializeField] float velocidad = 2.5f;   // decimal (lleva f)
    bool juegoActivo = true;                    // verdadero o falso
    string nombre = "Arepa";                    // texto

    SpriteRenderer sr; // referencia a otro componente

    // Start: se ejecuta una vez, al empezar
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("Hola, " + nombre);
    }

    // Update: se ejecuta en cada frame
    void Update()
    {
        if (!juegoActivo) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SumarPuntos(10);
        }
    }

    // Función propia: un bloque reutilizable
    void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntos: " + puntos);

        if (puntos >= 50)
        {
            juegoActivo = false;
            if (sr != null) sr.color = Color.green;
            Debug.Log("¡Ganaste!");
        }
    }
}
