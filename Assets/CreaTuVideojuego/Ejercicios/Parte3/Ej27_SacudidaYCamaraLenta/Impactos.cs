using System.Collections;
using UnityEngine;

public class Impactos : MonoBehaviour
{
    public static Impactos Instancia { get; private set; }
    Vector3 posBase;
    bool enPausa;
    Coroutine sacudida;

    void Awake() { Instancia = this; posBase = transform.localPosition; }

    public void Sacudir(float intensidad, float duracion)
    {
        // Reinicia solo la sacudida; una pausa en curso no se toca
        if (sacudida != null) StopCoroutine(sacudida);
        transform.localPosition = posBase;
        sacudida = StartCoroutine(RutinaSacudir(intensidad, duracion));
    }

    IEnumerator RutinaSacudir(float intensidad, float duracion)
    {
        for (float t = 0f; t < duracion; t += Time.unscaledDeltaTime)
        {
            float fuerza = intensidad * (1f - t / duracion); // se apaga
            Vector3 azar = Random.insideUnitCircle * fuerza;
            transform.localPosition = posBase + azar;
            yield return null;
        }
        transform.localPosition = posBase;
    }

    public void Pausa(float duracion)
    {
        if (!enPausa) StartCoroutine(RutinaPausa(duracion));
    }

    IEnumerator RutinaPausa(float duracion)
    {
        enPausa = true;
        float anterior = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duracion);
        Time.timeScale = anterior;
        enPausa = false;
    }
}
