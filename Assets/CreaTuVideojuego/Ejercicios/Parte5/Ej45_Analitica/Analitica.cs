using System.Collections.Generic;
using UnityEngine;

public static class Analitica
{
    public static void Evento(string nombre,
                              Dictionary<string, object> datos = null)
    {
#if UNITY_EDITOR
        // En el editor solo mostramos el evento en la consola
        string detalle = "";
        if (datos != null)
            foreach (var d in datos) detalle += $" {d.Key}={d.Value}";
        Debug.Log($"[Analítica] {nombre}{detalle}");
#else
        // Aquí va la llamada al servicio que elijas, por ejemplo
        // el método para registrar eventos de su SDK.
#endif
    }
}
