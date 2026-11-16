using UnityEngine;

// Ejercicio 26: lanza chispas donde está la arepa en cada toque
public class DemoParticulas : MonoBehaviour
{
    void Update()
    {
        if (GuiDemo.Toco()) Efectos.Instancia.Chispas(transform.position);
    }
}
