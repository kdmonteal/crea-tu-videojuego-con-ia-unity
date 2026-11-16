using UnityEngine;

[CreateAssetMenu(fileName = "Nivel", menuName = "Mi Juego/Nivel")]
public class DatosNivel : ScriptableObject
{
    public string nombre = "Nivel 1";
    [Header("Cocción")]
    public float tiempoPunto = 3f;
    public float margenPerfecto = 0.3f;
    public float margenBueno = 0.8f;
    public float tiempoQuemada = 5f;
    [Header("Reto")]
    public int arepasSimultaneas = 1;
    public int metaPuntos = 500;
}
