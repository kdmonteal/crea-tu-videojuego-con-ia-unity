using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class ArepaCoccion : MonoBehaviour
{
    [Header("Tiempos (segundos)")]
    [SerializeField] float tiempoPunto = 3f;      // momento ideal
    [SerializeField] float margenPerfecto = 0.3f; // ± para "¡Perfecta!"
    [SerializeField] float margenBueno = 0.8f;    // ± para "Buena"
    [SerializeField] float tiempoQuemada = 5f;
    [SerializeField] float duracionVolteo = 0.3f;

    [Header("Colores")]
    [SerializeField] Color colorCruda = new Color(0.96f, 0.91f, 0.76f);
    [SerializeField] Color colorDorada = new Color(0.86f, 0.60f, 0.26f);
    [SerializeField] Color colorQuemada = new Color(0.25f, 0.15f, 0.10f);

    SpriteRenderer sr;
    float tiempo;      // segundos desde que empezó esta cara
    bool volteando;    // bloquea toques durante la animación

    void Awake() { sr = GetComponent<SpriteRenderer>(); }

    void Update()
    {
        if (volteando) return;

        tiempo += Time.deltaTime;              // 1. el reloj
        sr.color = ColorSegunTiempo(tiempo);   // 2. el color

        bool toco = Pointer.current != null &&
                    Pointer.current.press.wasPressedThisFrame;
        if (toco || tiempo >= tiempoQuemada)
            StartCoroutine(Voltear(Calificar(tiempo)));
    }

    Color ColorSegunTiempo(float t)
    {
        if (t <= tiempoPunto)
            return Color.Lerp(colorCruda, colorDorada, t / tiempoPunto);
        float avance = (t - tiempoPunto) / (tiempoQuemada - tiempoPunto);
        return Color.Lerp(colorDorada, colorQuemada, avance);
    }

    // 3. La calificación: distancia al punto ideal, nunca ==
    string Calificar(float t)
    {
        if (t >= tiempoQuemada) return "Quemada";
        float diferencia = Mathf.Abs(t - tiempoPunto);
        if (diferencia <= margenPerfecto) return "¡Perfecta!";
        if (diferencia <= margenBueno) return "Buena";
        return t < tiempoPunto ? "Cruda" : "Pasada";
    }

    // 4. El volteo: la escala en Y va de 1 a -1 y vuelve a 1
    IEnumerator Voltear(string resultado)
    {
        volteando = true;
        Debug.Log(resultado);

        Vector3 escala = transform.localScale;
        float t = 0f;
        while (t < duracionVolteo)
        {
            t += Time.deltaTime;
            float giro = Mathf.Cos(t / duracionVolteo * Mathf.PI * 2f);
            transform.localScale =
                new Vector3(escala.x, escala.y * giro, escala.z);
            yield return null; // espera al siguiente frame
        }
        transform.localScale = escala;

        tiempo = 0f;       // empieza la otra cara
        volteando = false;
    }
}
