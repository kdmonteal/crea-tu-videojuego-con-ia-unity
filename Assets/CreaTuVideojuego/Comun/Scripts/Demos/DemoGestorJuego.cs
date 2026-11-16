using UnityEngine;

// Ejercicio 19: botones para probar cada estado de GestorJuego
public class DemoGestorJuego : MonoBehaviour
{
    void OnGUI()
    {
        var g = GestorJuego.Instancia;
        GuiDemo.Area(() =>
        {
            GuiDemo.Texto($"Estado: {g.Estado}    Vidas: {g.Vidas}    timeScale: {Time.timeScale}");
            switch (g.Estado)
            {
                case EstadoJuego.Inicio:
                    if (GuiDemo.Boton("Empezar")) g.Empezar();
                    break;
                case EstadoJuego.Jugando:
                    if (GuiDemo.Boton("Perder una vida")) g.PerderVida();
                    if (GuiDemo.Boton("Pausar")) g.Pausar();
                    break;
                case EstadoJuego.Pausa:
                    if (GuiDemo.Boton("Reanudar")) g.Reanudar();
                    break;
                case EstadoJuego.GameOver:
                    if (GuiDemo.Boton("Reiniciar (recarga la escena)")) g.Reiniciar();
                    break;
            }
        }, 0.5f);
    }
}
