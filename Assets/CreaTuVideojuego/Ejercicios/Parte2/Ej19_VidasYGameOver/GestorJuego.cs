using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EstadoJuego { Inicio, Jugando, Pausa, GameOver }

public class GestorJuego : MonoBehaviour
{
    public static GestorJuego Instancia { get; private set; }
    public event Action<EstadoJuego> AlCambiarEstado;

    [SerializeField] int vidasIniciales = 3;
    public int Vidas { get; private set; }
    public EstadoJuego Estado { get; private set; } = EstadoJuego.Inicio;

    void Awake()
    {
        Instancia = this;
        Vidas = vidasIniciales;
        Time.timeScale = 1f;
    }

    public void Empezar() => CambiarEstado(EstadoJuego.Jugando);

    public void PerderVida()
    {
        if (Estado != EstadoJuego.Jugando) return;
        Vidas--;
        if (Vidas <= 0) CambiarEstado(EstadoJuego.GameOver);
    }

    public void Pausar()
    {
        if (Estado != EstadoJuego.Jugando) return;
        Time.timeScale = 0f;
        CambiarEstado(EstadoJuego.Pausa);
    }

    public void Reanudar()
    {
        if (Estado != EstadoJuego.Pausa) return;
        Time.timeScale = 1f;
        CambiarEstado(EstadoJuego.Jugando);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CambiarEstado(EstadoJuego nuevo)
    {
        Estado = nuevo;
        AlCambiarEstado?.Invoke(nuevo);
    }
}
