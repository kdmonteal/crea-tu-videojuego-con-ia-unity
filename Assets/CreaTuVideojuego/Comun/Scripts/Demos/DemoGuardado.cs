using UnityEngine;

// Ejercicio 36: cambia los datos, sal del modo Play y vuelve a entrar: siguen ahí
public class DemoGuardado : MonoBehaviour
{
    void OnGUI()
    {
        var gg = GestorGuardado.Instancia;
        var d = gg.Datos;
        GuiDemo.Area(() =>
        {
            GuiDemo.Texto($"Monedas: {d.monedas}    Récord: {d.record}    Nivel máximo: {d.nivelMaximo}");
            if (GuiDemo.Boton("+10 monedas")) d.monedas += 10;
            if (GuiDemo.Boton("Nuevo récord (+100)")) d.record += 100;
            if (GuiDemo.Boton("Guardar ahora")) { gg.Guardar(); Debug.Log("Guardado en " + Application.persistentDataPath); }
            if (GuiDemo.Boton("Borrar partida")) { gg.Borrar(); Debug.Log("Partida borrada"); }
        }, 0.45f);
    }
}
