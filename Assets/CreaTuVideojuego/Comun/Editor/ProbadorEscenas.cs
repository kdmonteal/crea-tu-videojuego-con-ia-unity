// Prueba automática: abre cada escena de ejercicio, la juega unos segundos
// y anota en Logs/prueba_escenas.txt los errores que aparezcan.
// Menú: Libro > Mantenimiento > Probar todas las escenas
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class ProbadorEscenas
{
    const string Indice = "CreaTuVideojuego.Prueba.Indice";
    const string Archivo = "Logs/prueba_escenas.txt";
    const double Segundos = 3.0;
    static double fin;
    static int errores;

    static ProbadorEscenas()
    {
        EditorApplication.playModeStateChanged += AlCambiar;
    }

    static string[] Escenas() =>
        EditorBuildSettings.scenes.Select(s => s.path).ToArray();

    [MenuItem("Libro/Mantenimiento/Probar todas las escenas", priority = 101)]
    public static void Probar()
    {
        File.WriteAllText(Archivo, "Prueba de escenas\n");
        SessionState.SetInt(Indice, 0);
        Siguiente();
    }

    static void Siguiente()
    {
        int i = SessionState.GetInt(Indice, -1);
        var escenas = Escenas();
        if (i < 0) return;
        if (i >= escenas.Length)
        {
            SessionState.SetInt(Indice, -1);
            File.AppendAllText(Archivo, "FIN\n");
            Debug.Log("[Libro] Prueba terminada: " + Archivo);
            return;
        }
        EditorSceneManager.OpenScene(escenas[i]);
        EditorApplication.isPlaying = true;
    }

    static void AlCambiar(PlayModeStateChange estado)
    {
        int i = SessionState.GetInt(Indice, -1);
        if (i < 0) return;
        if (estado == PlayModeStateChange.EnteredPlayMode)
        {
            errores = 0;
            Application.logMessageReceived += AlMensaje;
            fin = EditorApplication.timeSinceStartup + Segundos;
            EditorApplication.update += Esperar;
        }
        else if (estado == PlayModeStateChange.EnteredEditMode)
        {
            File.AppendAllText(Archivo, $"{(errores == 0 ? "OK   " : "FALLA")} {Escenas()[i]}\n");
            SessionState.SetInt(Indice, i + 1);
            EditorApplication.delayCall += Siguiente;
        }
    }

    static void AlMensaje(string texto, string pila, LogType tipo)
    {
        if (tipo != LogType.Error && tipo != LogType.Exception && tipo != LogType.Assert) return;
        errores++;
        File.AppendAllText(Archivo, $"   {tipo}: {texto}\n   {pila.Split('\n').FirstOrDefault()}\n");
    }

    static void Esperar()
    {
        if (EditorApplication.timeSinceStartup < fin) return;
        EditorApplication.update -= Esperar;
        Application.logMessageReceived -= AlMensaje;
        EditorApplication.isPlaying = false;
    }
}
