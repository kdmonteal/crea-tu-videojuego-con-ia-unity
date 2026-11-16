// Este script necesita el SDK de Google Mobile Ads (ver ejercicio 43).
// Para que el proyecto compile sin el SDK, el código solo se activa cuando
// agregas el símbolo ADMOB en Project Settings > Player > Scripting Define Symbols.
#if ADMOB
using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class GestorAnuncios : MonoBehaviour
{
    public static GestorAnuncios Instancia { get; private set; }

    // IDs de PRUEBA de Google para Android. Cámbialos por los tuyos
    // solo al publicar.
    const string IdIntersticial = "ca-app-pub-3940256099942544/1033173712";
    const string IdRecompensado = "ca-app-pub-3940256099942544/5224354917";

    InterstitialAd intersticial;
    RewardedAd recompensado;

    void Awake()
    {
        if (Instancia != null) { Destroy(gameObject); return; }
        Instancia = this;
        DontDestroyOnLoad(gameObject);

        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(_ =>
        {
            CargarIntersticial();
            CargarRecompensado();
        });
    }

    void CargarIntersticial()
    {
        intersticial?.Destroy();
        InterstitialAd.Load(IdIntersticial, new AdRequest(), (ad, error) =>
        {
            if (error != null) { Debug.Log(error); return; }
            intersticial = ad;
            ad.OnAdFullScreenContentClosed += CargarIntersticial;
        });
    }

    void CargarRecompensado()
    {
        recompensado?.Destroy();
        RewardedAd.Load(IdRecompensado, new AdRequest(), (ad, error) =>
        {
            if (error != null) { Debug.Log(error); return; }
            recompensado = ad;
            ad.OnAdFullScreenContentClosed += CargarRecompensado;
        });
    }

    public void MostrarIntersticial()
    {
        if (intersticial != null && intersticial.CanShowAd())
            intersticial.Show();
    }

    public bool RecompensadoListo =>
        recompensado != null && recompensado.CanShowAd();

    public void MostrarRecompensado(Action alGanar)
    {
        if (!RecompensadoListo) return;
        recompensado.Show(_ => alGanar?.Invoke());
    }
}
#endif
