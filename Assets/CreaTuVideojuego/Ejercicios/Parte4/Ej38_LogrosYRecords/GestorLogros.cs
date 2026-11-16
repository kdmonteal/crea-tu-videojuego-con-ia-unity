using System;
using UnityEngine;

public class GestorLogros : MonoBehaviour
{
    public enum Tipo
    {
        PerfectasSeguidas, NivelesGanados, NivelSinPerderVidas
    }

    [Serializable]
    public class Logro
    {
        public string id, nombre, descripcion;
        public Tipo tipo;
        public int meta;
    }

    public static GestorLogros Instancia { get; private set; }
    public event Action<Logro> AlDesbloquear;
    [SerializeField] Logro[] logros;

    int racha;
    DatosGuardado D => GestorGuardado.Instancia.Datos;

    void Awake() { Instancia = this; }

    public void Perfecta()
    {
        racha++;
        Revisar(Tipo.PerfectasSeguidas, racha);
    }
    public void Fallo()    { racha = 0; }

    public void NivelGanado(bool sinPerderVidas)
    {
        Revisar(Tipo.NivelesGanados, D.nivelMaximo);
        if (sinPerderVidas) Revisar(Tipo.NivelSinPerderVidas, 1);
    }

    void Revisar(Tipo tipo, int valor)
    {
        foreach (var l in logros)
        {
            if (l.tipo != tipo || valor < l.meta) continue;
            if (D.logros.Contains(l.id)) continue;
            D.logros.Add(l.id);
            GestorGuardado.Instancia.Guardar();
            AlDesbloquear?.Invoke(l);
        }
    }
}
