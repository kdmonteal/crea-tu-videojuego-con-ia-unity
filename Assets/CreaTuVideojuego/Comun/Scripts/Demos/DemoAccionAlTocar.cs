using UnityEngine;
using UnityEngine.Events;

// Llama a una función del ejercicio cada vez que tocas la pantalla o presionas Espacio.
// La función se elige en el Inspector, en la lista "Al tocar".
public class DemoAccionAlTocar : MonoBehaviour
{
    public UnityEvent alTocar = new UnityEvent();

    void Update()
    {
        if (GuiDemo.Toco()) alTocar.Invoke();
    }
}
