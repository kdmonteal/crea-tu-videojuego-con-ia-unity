using UnityEngine;

// Ejercicio 15: suma puntos con un toque para ver cómo se actualiza el texto
public class DemoSumarPuntos : MonoBehaviour
{
    [SerializeField] int puntosPorToque = 10;

    void Update()
    {
        if (GuiDemo.Toco()) GestorPuntaje.Instancia.Sumar(puntosPorToque);
    }

    void OnGUI()
    {
        GuiDemo.Area(() =>
        {
            if (GuiDemo.Boton("Reiniciar puntaje")) GestorPuntaje.Instancia.Reiniciar();
        }, 0.72f);
    }
}
