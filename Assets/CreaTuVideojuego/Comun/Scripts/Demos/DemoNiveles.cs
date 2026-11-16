using UnityEngine;

// Ejercicio 34: muestra los datos del nivel actual y permite avanzar
public class DemoNiveles : MonoBehaviour
{
    void OnGUI()
    {
        var n = GestorNiveles.Instancia.Actual;
        GuiDemo.Area(() =>
        {
            GuiDemo.Texto($"{n.nombre}\nPunto ideal: {n.tiempoPunto} s · Margen perfecto: ±{n.margenPerfecto} s\n" +
                          $"Se quema a los {n.tiempoQuemada} s · Meta: {n.metaPuntos} puntos");
            if (GestorNiveles.Instancia.HaySiguiente)
            {
                if (GuiDemo.Boton("Siguiente nivel")) GestorNiveles.Instancia.Avanzar();
            }
            else GuiDemo.Texto("Es el último nivel. Crea otro en la carpeta Niveles.");
        }, 0.55f);
    }
}
