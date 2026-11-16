using System;
using UnityEngine;

public class Tienda : MonoBehaviour
{
    public event Action AlCambiar;
    DatosGuardado D => GestorGuardado.Instancia.Datos;

    public bool Tiene(ItemTienda item) => D.skins.Contains(item.id);
    public bool PuedeComprar(ItemTienda item) =>
        !Tiene(item) && D.monedas >= item.precio;

    public bool Comprar(ItemTienda item)
    {
        if (!PuedeComprar(item)) return false;
        D.monedas -= item.precio;
        D.skins.Add(item.id);
        GestorGuardado.Instancia.Guardar();
        AlCambiar?.Invoke();
        return true;
    }

    public void Equipar(ItemTienda item)
    {
        if (!Tiene(item)) return;
        D.skinActual = item.id;
        GestorGuardado.Instancia.Guardar();
        AlCambiar?.Invoke();
    }
}
