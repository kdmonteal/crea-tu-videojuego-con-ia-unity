using System.Collections;
using UnityEngine;

public class Rebote : MonoBehaviour
{
    [SerializeField] float intensidad = 0.25f;
    [SerializeField] float duracion = 0.25f;
    [SerializeField] AnimationCurve forma = new AnimationCurve(
        new Keyframe(0f, 0f), new Keyframe(0.3f, -1f),
        new Keyframe(0.7f, 0.5f), new Keyframe(1f, 0f));

    Vector3 escalaBase;
    Coroutine actual;

    void Awake() { escalaBase = transform.localScale; }

    public void Golpe()
    {
        if (actual != null) StopCoroutine(actual);
        actual = StartCoroutine(Animar());
    }

    IEnumerator Animar()
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / duracion)
        {
            float k = forma.Evaluate(t) * intensidad;
            // Si se aplasta en Y (k negativo), se ensancha en X
            transform.localScale = new Vector3(escalaBase.x * (1f - k),
                                               escalaBase.y * (1f + k),
                                               escalaBase.z);
            yield return null;
        }
        transform.localScale = escalaBase;
        actual = null;
    }
}
