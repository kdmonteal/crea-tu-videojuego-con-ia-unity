// Este script necesita el SDK de Google Mobile Ads (ver ejercicio 43).
// Para que el proyecto compile sin el SDK, el código solo se activa cuando
// agregas el símbolo ADMOB en Project Settings > Player > Scripting Define Symbols.
#if ADMOB
using UnityEngine;

public class ControlAnuncios : MonoBehaviour
{
    [SerializeField] float minutosEntreIntersticiales = 3f;
    [SerializeField] float minutosDeGracia = 5f; // al inicio de la sesión

    float ultimoIntersticial = -9999f;

    public bool PuedeMostrarIntersticial()
    {
        float ahora = Time.realtimeSinceStartup;
        bool pasoGracia = ahora > minutosDeGracia * 60f;
        float espera = minutosEntreIntersticiales * 60f;
        bool pasoEspera = ahora - ultimoIntersticial > espera;
        return pasoGracia && pasoEspera;
    }

    public void AlTerminarNivel()
    {
        if (!PuedeMostrarIntersticial()) return;
        ultimoIntersticial = Time.realtimeSinceStartup;
        GestorAnuncios.Instancia.MostrarIntersticial();
    }
}
#endif
