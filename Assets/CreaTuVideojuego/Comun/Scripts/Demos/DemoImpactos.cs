using UnityEngine;

// Ejercicio 27: compara un golpe sin juice con uno con sacudida y pausa
public class DemoImpactos : MonoBehaviour
{
    [SerializeField] Rebote arepa;

    void OnGUI()
    {
        GuiDemo.Area(() =>
        {
            if (GuiDemo.Boton("Golpe suave (sacudida corta)"))
            {
                Impactos.Instancia.Sacudir(0.12f, 0.15f);
                if (arepa) arepa.Golpe();
            }
            if (GuiDemo.Boton("Golpe perfecto (sacudida + pausa)"))
            {
                Impactos.Instancia.Pausa(0.08f);
                Impactos.Instancia.Sacudir(0.35f, 0.3f);
                if (arepa) arepa.Golpe();
            }
        }, 0.62f);
    }
}
