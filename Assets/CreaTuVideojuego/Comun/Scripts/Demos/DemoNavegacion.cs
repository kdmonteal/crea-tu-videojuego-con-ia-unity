using UnityEngine;
using UnityEngine.SceneManagement;

// Ejercicio 35: botones de la escena Menu y de la escena Juego
public class DemoNavegacion : MonoBehaviour
{
    [SerializeField] bool esMenu = true;
    bool pausado;

    void OnGUI()
    {
        GuiDemo.Area(() =>
        {
            if (esMenu)
            {
                if (GuiDemo.Boton("Jugar")) Ir("Juego");
            }
            else
            {
                if (GuiDemo.Boton(pausado ? "Reanudar" : "Pausa"))
                {
                    pausado = !pausado;
                    Time.timeScale = pausado ? 0f : 1f;
                }
                if (GuiDemo.Boton("Volver al menú")) Ir("Menu");
            }
        }, 0.68f);
    }

    void Ir(string escena)
    {
        pausado = false;
        if (Navegacion.Instancia != null)
        {
            if (escena == "Juego") Navegacion.Instancia.IrAJuego();
            else Navegacion.Instancia.IrAMenu();
        }
        else
        {
            // Si abriste la escena Juego directamente, todavía no existe Navegacion
            Time.timeScale = 1f;
            SceneManager.LoadScene(escena);
        }
    }
}
