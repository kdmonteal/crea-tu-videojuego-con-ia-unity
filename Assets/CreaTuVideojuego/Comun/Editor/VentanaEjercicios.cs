// Ventana de bienvenida con los 50 ejercicios del libro.
// Se abre sola la primera vez y desde el menú Libro > Ejercicios del libro.
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class VentanaEjercicios : EditorWindow
{
    const string ClaveMostrar = "CreaTuVideojuego.MostrarAlIniciar";
    Vector2 scroll;
    int parteAbierta = 0;
    GUIStyle sTitulo, sNum, sDesc, sInfo;

    static VentanaEjercicios()
    {
        EditorApplication.delayCall += () =>
        {
            // Si faltan las escenas (por ejemplo, se borraron), se construyen de nuevo
            if (!EditorApplication.isPlayingOrWillChangePlaymode &&
                AssetDatabase.LoadAssetAtPath<SceneAsset>(CatalogoEjercicios.Raiz +
                    "/Ejercicios/Parte1/Ej02_CuadradoQueGira/Ej02.unity") == null &&
                AssetDatabase.IsValidFolder(CatalogoEjercicios.Raiz + "/Comun/Sprites"))
            {
                ConstructorEjercicios.Construir();
            }
            if (SessionState.GetBool("CreaTuVideojuego.Vista", false)) return;
            SessionState.SetBool("CreaTuVideojuego.Vista", true);
            if (EditorPrefs.GetBool(ClaveMostrar, true)) Abrir();
        };
    }

    [MenuItem("Libro/Ejercicios del libro %#e", priority = 0)]
    public static void Abrir()
    {
        var w = GetWindow<VentanaEjercicios>("Ejercicios del libro");
        w.minSize = new Vector2(420, 480);
        w.Show();
    }

    void Estilos()
    {
        if (sTitulo != null) return;
        sTitulo = new GUIStyle(EditorStyles.boldLabel) { fontSize = 17, wordWrap = true };
        sNum = new GUIStyle(EditorStyles.boldLabel) { fontSize = 12, wordWrap = true };
        sDesc = new GUIStyle(EditorStyles.wordWrappedMiniLabel) { fontSize = 11 };
        sInfo = new GUIStyle(EditorStyles.wordWrappedLabel) { fontSize = 12, richText = true };
    }

    void OnGUI()
    {
        Estilos();
        GUILayout.Space(8);
        GUILayout.Label("Crea tu videojuego con IA en Unity", sTitulo);
        GUILayout.Label(
            "<b>Cómo usar este proyecto</b>\n" +
            "1. Elige un ejercicio y pulsa <b>Jugar</b>: se abre su escena y empieza el modo Play.\n" +
            "2. Lee el panel de arriba en la vista <b>Game</b>: te dice qué hacer y qué observar.\n" +
            "3. Pulsa <b>Script</b> para abrir el código y compararlo con el libro.\n" +
            "Para salir del modo Play, vuelve a pulsar el botón ▶ de arriba.", sInfo);
        GUILayout.Space(6);

        parteAbierta = GUILayout.Toolbar(parteAbierta, new[] { "Parte 1", "Parte 2", "Parte 3", "Parte 4", "Parte 5" });
        GUILayout.Label(CatalogoEjercicios.Partes[parteAbierta], EditorStyles.boldLabel);

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var e in CatalogoEjercicios.Todos)
        {
            if (e.Parte != parteAbierta + 1) continue;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label($"Ejercicio {e.numero} · {e.titulo}", sNum);
            if (e.TieneEscena)
            {
                GUILayout.Label(e.instrucciones, sDesc);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("▶  Jugar", GUILayout.Height(24))) Jugar(e);
                if (GUILayout.Button("Abrir escena", GUILayout.Height(24))) AbrirEscena(e);
                if (!string.IsNullOrEmpty(e.scripts) && GUILayout.Button("Script", GUILayout.Height(24))) AbrirScript(e);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("Sin escena: " + e.instrucciones, sDesc);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();

        GUILayout.Space(4);
        bool mostrar = EditorPrefs.GetBool(ClaveMostrar, true);
        bool nuevo = EditorGUILayout.ToggleLeft("Mostrar esta ventana al abrir el proyecto", mostrar);
        if (nuevo != mostrar) EditorPrefs.SetBool(ClaveMostrar, nuevo);
        GUILayout.Label("Libro de Kelly Daniella Marín · https://www.kdmonteal.studio", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Space(4);
    }

    static bool AbrirEscena(Ejercicio e)
    {
        if (EditorApplication.isPlaying)
        {
            EditorUtility.DisplayDialog("Modo Play", "Sal del modo Play (botón ▶ de arriba) y vuelve a intentarlo.", "OK");
            return false;
        }
        string ruta = CatalogoEjercicios.Escena(e);
        if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ruta) == null)
        {
            EditorUtility.DisplayDialog("Escena no encontrada", "No encontré " + ruta, "OK");
            return false;
        }
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return false;
        EditorSceneManager.OpenScene(ruta);
        return true;
    }

    static void Jugar(Ejercicio e)
    {
        if (EditorApplication.isPlaying)
        {
            // Sale del modo Play y, al terminar de salir, abre la escena nueva
            pendiente = e;
            EditorApplication.playModeStateChanged -= AlCambiarModo;
            EditorApplication.playModeStateChanged += AlCambiarModo;
            EditorApplication.isPlaying = false;
            return;
        }
        if (AbrirEscena(e)) EditorApplication.isPlaying = true;
    }

    static Ejercicio pendiente;

    static void AlCambiarModo(PlayModeStateChange estado)
    {
        if (estado != PlayModeStateChange.EnteredEditMode) return;
        EditorApplication.playModeStateChanged -= AlCambiarModo;
        var e = pendiente;
        pendiente = null;
        if (e != null) Jugar(e);
    }

    static void AbrirScript(Ejercicio e)
    {
        string primero = e.scripts.Split(',')[0].Split(' ')[0].Trim();
        string carpeta = $"{CatalogoEjercicios.Raiz}/Ejercicios/{e.carpeta}";
        foreach (string guid in AssetDatabase.FindAssets("t:MonoScript", new[] { CatalogoEjercicios.Raiz + "/Ejercicios" }))
        {
            string ruta = AssetDatabase.GUIDToAssetPath(guid);
            if (ruta.EndsWith("/" + primero))
            {
                var script = AssetDatabase.LoadAssetAtPath<MonoScript>(ruta);
                Selection.activeObject = script;
                EditorGUIUtility.PingObject(script);
                AssetDatabase.OpenAsset(script);
                return;
            }
        }
        EditorUtility.RevealInFinder(carpeta);
    }
}
