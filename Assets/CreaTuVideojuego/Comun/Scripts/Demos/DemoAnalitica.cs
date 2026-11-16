using System.Collections.Generic;
using UnityEngine;

// Ejercicio 45: envía eventos de prueba y míralos en la Consola
public class DemoAnalitica : MonoBehaviour
{
    int nivel = 1;

    void OnGUI()
    {
        GuiDemo.Area(() =>
        {
            if (GuiDemo.Boton($"Empezar nivel {nivel}"))
                Analitica.Evento("nivel_iniciado", new Dictionary<string, object> { { "nivel", nivel } });
            if (GuiDemo.Boton($"Completar nivel {nivel}"))
            {
                Analitica.Evento("nivel_completado", new Dictionary<string, object> { { "nivel", nivel }, { "estrellas", 3 } });
                nivel++;
            }
            if (GuiDemo.Boton("Ver anuncio con recompensa"))
                Analitica.Evento("recompensado_visto");
        }, 0.55f);
    }
}
