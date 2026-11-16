using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextoPuntaje : MonoBehaviour
{
    TextMeshProUGUI texto;

    void Awake() { texto = GetComponent<TextMeshProUGUI>(); }

    void Start()
    {
        GestorPuntaje.Instancia.AlCambiar += Mostrar;
        Mostrar(GestorPuntaje.Instancia.Puntos);
    }

    void OnDestroy()
    {
        if (GestorPuntaje.Instancia != null)
            GestorPuntaje.Instancia.AlCambiar -= Mostrar;
    }

    void Mostrar(int puntos) { texto.text = puntos.ToString(); }
}
