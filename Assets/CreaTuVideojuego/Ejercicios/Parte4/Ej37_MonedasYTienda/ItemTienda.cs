using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Mi Juego/Item de tienda")]
public class ItemTienda : ScriptableObject
{
    public string id;
    public string nombre;
    public int precio;
    public Sprite sprite;
}
