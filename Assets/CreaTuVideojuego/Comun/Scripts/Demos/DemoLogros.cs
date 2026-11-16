using UnityEngine;

// Ejercicio 38: simula jugadas para desbloquear logros
public class DemoLogros : MonoBehaviour
{
    void Start()
    {
        GestorLogros.Instancia.AlDesbloquear += l => Debug.Log($"¡Logro desbloqueado! {l.nombre}: {l.descripcion}");
    }

    void OnGUI()
    {
        var gl = GestorLogros.Instancia;
        var d = GestorGuardado.Instancia.Datos;
        GuiDemo.Area(() =>
        {
            GuiDemo.Texto($"Logros desbloqueados: {d.logros.Count}");
            if (GuiDemo.Boton("Arepa perfecta")) gl.Perfecta();
            if (GuiDemo.Boton("Arepa fallida (rompe la racha)")) gl.Fallo();
            if (GuiDemo.Boton("Ganar nivel sin perder vidas")) { d.nivelMaximo++; gl.NivelGanado(true); }
            if (GuiDemo.Boton("Borrar logros")) { d.logros.Clear(); GestorGuardado.Instancia.Guardar(); }
        }, 0.45f);
    }
}
