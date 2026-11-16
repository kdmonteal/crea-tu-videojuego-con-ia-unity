using UnityEngine;

// Ejercicio 37: compra y equipa los items de la tienda
public class DemoTienda : MonoBehaviour
{
    [SerializeField] Tienda tienda;
    [SerializeField] ItemTienda[] items;
    [SerializeField] SpriteRenderer arepa;
    [SerializeField] Color[] colores;

    void Start() { tienda.AlCambiar += Pintar; Pintar(); }

    void Pintar()
    {
        string actual = GestorGuardado.Instancia.Datos.skinActual;
        for (int i = 0; i < items.Length; i++)
            if (items[i].id == actual && i < colores.Length) arepa.color = colores[i];
    }

    void OnGUI()
    {
        var d = GestorGuardado.Instancia.Datos;
        GuiDemo.Area(() =>
        {
            GuiDemo.Texto($"Monedas: {d.monedas}");
            if (GuiDemo.Boton("+100 monedas (truco de prueba)")) d.monedas += 100;
            foreach (var item in items)
            {
                if (d.skinActual == item.id) GuiDemo.Texto($"{item.nombre} · equipada");
                else if (tienda.Tiene(item)) { if (GuiDemo.Boton($"Equipar {item.nombre}")) tienda.Equipar(item); }
                else
                {
                    GUI.enabled = tienda.PuedeComprar(item);
                    if (GuiDemo.Boton($"Comprar {item.nombre} · {item.precio}")) tienda.Comprar(item);
                    GUI.enabled = true;
                }
            }
        }, 0.45f);
    }
}
