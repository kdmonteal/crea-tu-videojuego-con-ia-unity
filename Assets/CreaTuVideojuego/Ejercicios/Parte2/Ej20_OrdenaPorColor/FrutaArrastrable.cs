using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrutaArrastrable : MonoBehaviour
{
    [SerializeField] string tipo;
    [SerializeField] LayerMask capaFruta;
    [SerializeField] LayerMask capaCanasto;

    static FrutaArrastrable arrastrando; // solo una fruta a la vez
    Vector3 origen, desfase;
    Camera cam;
    bool colocada;

    void Start() { cam = Camera.main; origen = transform.position; }

    void Update()
    {
        var p = Pointer.current;
        if (p == null || colocada) return;
        Vector2 dedo = cam.ScreenToWorldPoint(p.position.ReadValue());

        if (p.press.wasPressedThisFrame && arrastrando == null)
        {
            var tocado = Physics2D.OverlapPoint(dedo, capaFruta);
            if (tocado != null && tocado.gameObject == gameObject)
            {
                arrastrando = this;
                desfase = transform.position - (Vector3)dedo;
            }
        }
        else if (arrastrando == this && p.press.isPressed)
        {
            transform.position = (Vector3)dedo + desfase;
        }
        else if (arrastrando == this && p.press.wasReleasedThisFrame)
        {
            arrastrando = null;
            var col = Physics2D.OverlapPoint(transform.position, capaCanasto);
            var canasto = col != null ? col.GetComponent<Canasto>() : null;

            if (canasto != null && canasto.tipo == tipo)
            {
                colocada = true;
                transform.position = canasto.transform.position;
                if (GestorPuntaje.Instancia != null)
                    GestorPuntaje.Instancia.Sumar(1);
            }
            else
            {
                StartCoroutine(Volver());
            }
        }
    }

    IEnumerator Volver()
    {
        Vector3 desde = transform.position;
        for (float t = 0f; t < 1f; t += Time.deltaTime * 5f)
        {
            transform.position = Vector3.Lerp(desde, origen, t);
            yield return null;
        }
        transform.position = origen;
    }
}
