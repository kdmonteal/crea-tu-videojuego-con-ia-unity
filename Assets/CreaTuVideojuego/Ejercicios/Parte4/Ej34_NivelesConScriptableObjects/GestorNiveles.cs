using UnityEngine;

public class GestorNiveles : MonoBehaviour
{
    public static GestorNiveles Instancia { get; private set; }
    [SerializeField] DatosNivel[] niveles;
    [SerializeField] int indiceActual = 0;

    public DatosNivel Actual => niveles[indiceActual];
    public bool HaySiguiente => indiceActual < niveles.Length - 1;

    void Awake() { Instancia = this; }

    public void Avanzar() { if (HaySiguiente) indiceActual++; }
}
