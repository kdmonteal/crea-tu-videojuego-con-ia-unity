using System;
using UnityEngine;

public class GestorPuntaje : MonoBehaviour
{
    public static GestorPuntaje Instancia { get; private set; }
    public event Action<int> AlCambiar;   // avisa el nuevo puntaje

    public int Puntos { get; private set; }

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
    }

    public void Sumar(int cantidad)
    {
        Puntos += cantidad;
        AlCambiar?.Invoke(Puntos);
    }

    public void Reiniciar()
    {
        Puntos = 0;
        AlCambiar?.Invoke(Puntos);
    }
}
