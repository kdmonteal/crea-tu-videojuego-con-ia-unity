using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ZonaSegura : MonoBehaviour
{
    RectTransform rect;
    Rect ultimaZona;

    void Awake() { rect = GetComponent<RectTransform>(); Aplicar(); }

    void Update()
    {
        if (Screen.safeArea != ultimaZona) Aplicar(); // giro o cambio
    }

    void Aplicar()
    {
        ultimaZona = Screen.safeArea;
        Vector2 min = ultimaZona.position;
        Vector2 max = ultimaZona.position + ultimaZona.size;
        min.x /= Screen.width;  min.y /= Screen.height;
        max.x /= Screen.width;  max.y /= Screen.height;
        rect.anchorMin = min;
        rect.anchorMax = max;
    }
}
