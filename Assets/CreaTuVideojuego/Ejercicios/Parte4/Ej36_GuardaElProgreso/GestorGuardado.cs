using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class DatosGuardado
{
    public int nivelMaximo = 0;
    public int monedas = 0;
    public int record = 0;
    public float volumenMusica = 0.8f;
    public float volumenEfectos = 0.8f;
    public List<string> skins = new List<string> { "clasica" };
    public string skinActual = "clasica";
    // Campos que se agregan en el ejercicio 38 (logros y récords)
    public List<string> logros = new List<string>();
    public int perfectasSeguidasMax = 0;
}

public class GestorGuardado : MonoBehaviour
{
    public static GestorGuardado Instancia { get; private set; }
    public DatosGuardado Datos { get; private set; }

    string Ruta =>
        Path.Combine(Application.persistentDataPath, "partida.json");

    void Awake()
    {
        if (Instancia != null) { Destroy(gameObject); return; }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
        Cargar();
    }

    public void Guardar()
    {
        File.WriteAllText(Ruta, JsonUtility.ToJson(Datos));
    }

    void Cargar()
    {
        try
        {
            Datos = File.Exists(Ruta)
                ? JsonUtility.FromJson<DatosGuardado>(File.ReadAllText(Ruta))
                : new DatosGuardado();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Partida dañada, empiezo de cero: " + e.Message);
            Datos = new DatosGuardado();
        }
        if (Datos == null) Datos = new DatosGuardado();
    }

    public void Borrar()
    {
        if (File.Exists(Ruta)) File.Delete(Ruta);
        Datos = new DatosGuardado();
    }

    // En móvil, el sistema puede cerrar la app sin avisar
    void OnApplicationPause(bool pausado) { if (pausado) Guardar(); }
    void OnApplicationQuit() { Guardar(); }
}
