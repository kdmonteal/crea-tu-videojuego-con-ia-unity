// Construye las escenas jugables de los ejercicios a partir del código.
// Ya vienen construidas en el repositorio: solo necesitas este menú si borras
// o rompes una escena y quieres recuperarla (Libro > Mantenimiento > Reconstruir escenas).
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class ConstructorEjercicios
{
    const string R = CatalogoEjercicios.Raiz;
    const string SPR = R + "/Comun/Sprites/";
    const string PRE = R + "/Comun/Prefabs/";
    const string MAT = R + "/Comun/Materiales/";
    static readonly Color FONDO = new Color(0.08f, 0.19f, 0.29f);
    static readonly Color CIELO = new Color(0.47f, 0.73f, 0.87f);
    static readonly Color AMARILLO = new Color(0.90f, 0.90f, 0.06f);
    static readonly Color TIERRA = new Color(0.36f, 0.25f, 0.17f);
    const int CAPA_FRUTA = 8, CAPA_CANASTO = 9;

    static Ejercicio actual;
    static Camera cam;
    static readonly List<string> escenas = new List<string>();

    [MenuItem("Libro/Mantenimiento/Reconstruir escenas", priority = 100)]
    public static void Reconstruir()
    {
        if (!EditorUtility.DisplayDialog("Reconstruir escenas",
            "Se volverán a crear todas las escenas de los ejercicios. Los cambios que hayas hecho en ellas se perderán. ¿Continuar?",
            "Reconstruir", "Cancelar")) return;
        Construir();
    }

    public static void Construir()
    {
        escenas.Clear();
        Preparar();
        Directory.CreateDirectory(PRE);
        Directory.CreateDirectory(MAT);
        AssetDatabase.Refresh();

        var pasos = new (int, Action)[]
        {
            (2, Ej02), (4, Ej04), (5, Ej05), (9, Ej09), (10, Ej10),
            (11, Ej11), (12, Ej12), (13, Ej13), (14, Ej14), (15, Ej15),
            (16, Ej16), (17, Ej17), (18, Ej18), (19, Ej19), (20, Ej20),
            (24, Ej24), (25, Ej25), (26, Ej26), (27, Ej27), (28, Ej28), (29, Ej29),
            (34, Ej34), (35, Ej35), (36, Ej36), (37, Ej37), (38, Ej38),
            (41, Ej41), (45, Ej45),
        };
        var errores = new List<string>();
        foreach (var (n, paso) in pasos)
        {
            actual = CatalogoEjercicios.Todos.First(e => e.numero == n);
            EditorUtility.DisplayProgressBar("Construyendo escenas", $"Ejercicio {n}", n / 50f);
            try { paso(); }
            catch (Exception ex) { errores.Add($"Ej{n}: {ex.Message}"); Debug.LogException(ex); }
        }
        EditorUtility.ClearProgressBar();

        EditorBuildSettings.scenes = escenas.Select(p => new EditorBuildSettingsScene(p, true)).ToArray();
        AssetDatabase.SaveAssets();
        AbrirPrimera();
        string msg = $"[Libro] {escenas.Count} escenas construidas." +
                     (errores.Count > 0 ? " Errores: " + string.Join(" | ", errores) : " Sin errores.");
        Debug.Log(msg);
        File.WriteAllText("Logs/constructor.txt", msg);
    }

    static void AbrirPrimera()
    {
        if (escenas.Count > 0) EditorSceneManager.OpenScene(escenas[0]);
    }

    // ------------------------------------------------------------ preparación
    static void Preparar()
    {
        foreach (string guid in AssetDatabase.FindAssets("t:Texture2D", new[] { R + "/Comun/Sprites" }))
        {
            var ti = (TextureImporter)AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath(guid));
            if (ti.textureType == TextureImporterType.Sprite && ti.spritePixelsPerUnit == 100) continue;
            ti.textureType = TextureImporterType.Sprite;
            ti.spriteImportMode = SpriteImportMode.Single;
            ti.spritePixelsPerUnit = 100;
            ti.alphaIsTransparency = true;
            ti.SaveAndReimport();
        }
        var tm = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        var tags = tm.FindProperty("tags");
        bool hay = false;
        for (int i = 0; i < tags.arraySize; i++) if (tags.GetArrayElementAtIndex(i).stringValue == "Fruta") hay = true;
        if (!hay)
        {
            tags.InsertArrayElementAtIndex(tags.arraySize);
            tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = "Fruta";
        }
        var capas = tm.FindProperty("layers");
        capas.GetArrayElementAtIndex(CAPA_FRUTA).stringValue = "Fruta";
        capas.GetArrayElementAtIndex(CAPA_CANASTO).stringValue = "Canasto";
        tm.ApplyModifiedPropertiesWithoutUndo();
    }

    // --------------------------------------------------------------- escenas
    static void Ej02()
    {
        Escena();
        var go = Sprite("Cuadrado", "Cuadrado", new Vector3(0, -0.5f, 0), 1.6f);
        go.GetComponent<SpriteRenderer>().color = CIELO;
        go.AddComponent<Girador>();
        Guardar(go);
    }

    static void Ej04()
    {
        Escena();
        Suelo();
        var f = Fruta("Manzana", new Vector3(0, 1.5f, 0), 0.8f, false);
        f.name = "Fruta";
        Guardar(f);
    }

    static void Ej05()
    {
        Escena();
        var a = Sprite("Arepa", "Arepa", new Vector3(0, -0.5f, 0), 0.55f);
        a.AddComponent<Contador>();
        Guardar(a);
    }

    static void Ej09()
    {
        Escena();
        var gen = new GameObject("Generador");
        gen.transform.position = new Vector3(0, 6, 0);
        var g = gen.AddComponent<GeneradorFrutas>();
        Asignar(g, "prefabFruta", PrefabFruta("Manzana"));
        Guardar(gen);
    }

    static void Ej10()
    {
        Escena();
        var gen = new GameObject("Generador");
        gen.transform.position = new Vector3(0, 6, 0);
        var g = gen.AddComponent<GeneradorFrutas>();
        Asignar(g, "prefabFruta", PrefabFruta("Manzana"));
        Flotante(g, "intervalo", 1.2f);
        var c = Sprite("Canasta", "Canasta", new Vector3(0, -3.3f, 0), 0.5f);
        var bc = c.AddComponent<BoxCollider2D>(); bc.isTrigger = true;
        c.AddComponent<Canasta>();
        Guardar(c);
    }

    static void Ej11()
    {
        Escena();
        var c = Sprite("Canasta", "Canasta", new Vector3(0, -2.5f, 0), 0.5f);
        c.AddComponent<ArrastreRelativo>();
        Guardar(c);
    }

    static void Ej12()
    {
        Escena(new Color(0.29f, 0.18f, 0.12f));
        Paredes();
        var cancha = Sprite("Cancha", "Cuadrado", new Vector3(0, 0.9f, 0), 1f);
        cancha.transform.localScale = new Vector3(2.0f, 1.4f, 1);
        cancha.GetComponent<SpriteRenderer>().color = new Color(0.55f, 0.40f, 0.26f);
        cancha.GetComponent<SpriteRenderer>().sortingOrder = -1;
        Vector3[] pos = { new(-0.9f, 1.3f), new(0.9f, 1.2f), new(0, 0.4f) };
        for (int i = 0; i < 3; i++)
        {
            var m = Sprite("Mecha " + (i + 1), "Mecha", pos[i], 0.45f);
            m.AddComponent<CircleCollider2D>();
        }
        var tejo = Sprite("Tejo", "Tejo", new Vector3(0, -3.4f, 0), 0.4f);
        var rb = tejo.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0; rb.linearDamping = 1.2f; rb.angularDamping = 2f;
        tejo.AddComponent<CircleCollider2D>();
        tejo.AddComponent<Lanzador>();
        Guardar(tejo);
    }

    static void Ej13()
    {
        Escena();
        Suelo();
        var p = Sprite("Pelota", "Pelota", new Vector3(0, -3, 0), 0.4f);
        p.AddComponent<Rigidbody2D>();
        p.AddComponent<CircleCollider2D>();
        p.AddComponent<Salto>();
        Guardar(p);
    }

    static void Ej14()
    {
        Escena();
        Suelo();
        var zona = Sprite("Manzana (trigger)", "Manzana", new Vector3(0, -1.6f, 0), 0.35f);
        var cc = zona.AddComponent<CircleCollider2D>(); cc.isTrigger = true;
        zona.tag = "Fruta";
        var p = Sprite("Pelota", "Pelota", new Vector3(0, -3.5f, 0), 0.35f);
        p.AddComponent<Rigidbody2D>();
        p.AddComponent<CircleCollider2D>();
        p.AddComponent<Salto>();
        p.AddComponent<Impacto>();
        Guardar(p);
    }

    static void Ej15()
    {
        Escena();
        Sprite("Arepa", "ArepaDorada", new Vector3(0, -1.2f, 0), 0.5f);
        var gp = new GameObject("GestorPuntaje");
        gp.AddComponent<GestorPuntaje>();
        gp.AddComponent<DemoSumarPuntos>();
        var t = Texto("0", new Vector2(0, 260), 200, Color.white);
        t.fontStyle = FontStyles.Bold;
        t.gameObject.AddComponent<TextoPuntaje>();
        Guardar(gp);
    }

    static void Ej16()
    {
        Escena();
        Sprite("Arepa", "Arepa", new Vector3(0, -2.2f, 0), 0.45f);
        var cuenta = Texto("", new Vector2(0, 40), 360, AMARILLO);
        cuenta.fontStyle = FontStyles.Bold;
        var reloj = Texto("0:20", new Vector2(0, 330), 110, Color.white);
        var go = new GameObject("Reloj");
        var r = go.AddComponent<Reloj>();
        Asignar(r, "textoCuenta", cuenta);
        Asignar(r, "textoReloj", reloj);
        Flotante(r, "duracionPartida", 20f);
        Guardar(go);
    }

    static void Ej17()
    {
        Escena();
        Sprite("Parrilla", "Parrilla", new Vector3(0, -1.6f, 0), 0.55f).GetComponent<SpriteRenderer>().sortingOrder = -1;
        var a = Sprite("Arepa", "Arepa", new Vector3(0, 0.2f, 0), 0.5f);
        a.AddComponent<ArepaCoccion>();
        Guardar(a);
    }

    static void Ej18()
    {
        Escena();
        var gen = new GameObject("Generador");
        gen.transform.position = new Vector3(0, 6, 0);
        var g = gen.AddComponent<GeneradorPonderado>();
        var so = new SerializedObject(g);
        var e = so.FindProperty("entradas");
        GameObject[] pre = { PrefabFruta("Manzana"), PrefabFruta("Naranja"), PrefabFruta("ArepaDorada") };
        int[] pesos = { 70, 25, 5 };
        e.arraySize = 3;
        for (int i = 0; i < 3; i++)
        {
            e.GetArrayElementAtIndex(i).FindPropertyRelative("prefab").objectReferenceValue = pre[i];
            e.GetArrayElementAtIndex(i).FindPropertyRelative("peso").intValue = pesos[i];
        }
        so.ApplyModifiedPropertiesWithoutUndo();
        Guardar(gen);
    }

    static void Ej19()
    {
        Escena();
        Sprite("Arepa", "ArepaDorada", new Vector3(0, 0.6f, 0), 0.4f);
        var go = new GameObject("GestorJuego");
        go.AddComponent<GestorJuego>();
        go.AddComponent<DemoGestorJuego>();
        Guardar(go);
    }

    static void Ej20()
    {
        Escena(new Color(0.33f, 0.22f, 0.15f));
        var gp = new GameObject("GestorPuntaje");
        gp.AddComponent<GestorPuntaje>();
        var t = Texto("0", new Vector2(0, 430), 110, Color.white);
        t.fontStyle = FontStyles.Bold;
        t.gameObject.AddComponent<TextoPuntaje>();
        string[] tipos = { "Manzana", "Banano", "Patilla" };
        string[] canastos = { "CanastoRojo", "CanastoAmarillo", "CanastoVerde" };
        Color[] tintes = { new Color(1f, 0.55f, 0.55f), new Color(1f, 0.95f, 0.45f), new Color(0.55f, 1f, 0.55f) };
        for (int i = 0; i < 3; i++)
        {
            var cb = Sprite("Canasto " + tipos[i], canastos[i], new Vector3(-1.8f + i * 1.8f, -3.3f, 0), 0.4f);
            cb.GetComponent<SpriteRenderer>().color = tintes[i];
            cb.layer = CAPA_CANASTO;
            cb.AddComponent<BoxCollider2D>();
            cb.AddComponent<Canasto>().tipo = tipos[i].ToLower();
        }
        var rnd = new System.Random(4);
        for (int i = 0; i < 9; i++)
        {
            var pos = new Vector3(-1.7f + (i % 3) * 1.7f + (float)rnd.NextDouble() * 0.3f, 1.0f - (i / 3) * 1.1f, 0);
            var f = Sprite("Fruta " + tipos[i % 3], tipos[i % 3], pos, 0.38f);
            f.layer = CAPA_FRUTA;
            f.GetComponent<SpriteRenderer>().sortingOrder = 2;
            f.AddComponent<CircleCollider2D>();
            var fa = f.AddComponent<FrutaArrastrable>();
            var so = new SerializedObject(fa);
            so.FindProperty("tipo").stringValue = tipos[i % 3].ToLower();
            so.FindProperty("capaFruta").intValue = 1 << CAPA_FRUTA;
            so.FindProperty("capaCanasto").intValue = 1 << CAPA_CANASTO;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
        Guardar(gp);
    }

    static void Ej24()
    {
        Escena();
        string carpeta = Carpeta();
        // Clip de volteo: la escala en Y va de 1 a -1 y vuelve a 1
        var clip = new AnimationClip { name = "Volteo" };
        var curva = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.15f, -1), new Keyframe(0.3f, 1));
        AnimationUtility.SetEditorCurve(clip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalScale.y"), curva);
        AnimationUtility.SetEditorCurve(clip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalScale.x"), AnimationCurve.Constant(0, 0.3f, 1));
        AnimationUtility.SetEditorCurve(clip, EditorCurveBinding.FloatCurve("", typeof(Transform), "m_LocalScale.z"), AnimationCurve.Constant(0, 0.3f, 1));
        AssetDatabase.DeleteAsset(carpeta + "/Volteo.anim");
        AssetDatabase.CreateAsset(clip, carpeta + "/Volteo.anim");

        AssetDatabase.DeleteAsset(carpeta + "/Arepa.controller");
        var ctrl = AnimatorController.CreateAnimatorControllerAtPath(carpeta + "/Arepa.controller");
        ctrl.AddParameter("Voltear", AnimatorControllerParameterType.Trigger);
        var sm = ctrl.layers[0].stateMachine;
        var quieta = sm.AddState("Quieta");
        var volteo = sm.AddState("Volteo");
        volteo.motion = clip;
        sm.defaultState = quieta;
        var ida = quieta.AddTransition(volteo);
        ida.AddCondition(AnimatorConditionMode.If, 0, "Voltear");
        ida.hasExitTime = false; ida.duration = 0;
        var vuelta = volteo.AddTransition(quieta);
        vuelta.hasExitTime = true; vuelta.exitTime = 1; vuelta.duration = 0;

        // La escala de la arepa va en el hijo para que la animación no la cambie
        var arepa = new GameObject("Arepa");
        arepa.transform.position = new Vector3(0, -0.5f, 0);
        var spr = Sprite("Sprite", "ArepaDorada", Vector3.zero, 0.5f);
        spr.transform.SetParent(arepa.transform, false);
        arepa.AddComponent<Animator>().runtimeAnimatorController = ctrl;
        var anim = arepa.AddComponent<AnimadorArepa>();
        var demo = arepa.AddComponent<DemoAccionAlTocar>();
        UnityEventTools.AddPersistentListener(demo.alTocar, anim.ReproducirVolteo);
        Guardar(arepa);
    }

    static void Ej25()
    {
        Escena();
        var a = Sprite("Arepa", "ArepaDorada", new Vector3(0, -0.5f, 0), 0.5f);
        var r = a.AddComponent<Rebote>();
        var demo = a.AddComponent<DemoAccionAlTocar>();
        UnityEventTools.AddPersistentListener(demo.alTocar, r.Golpe);
        Guardar(a);
    }

    static void Ej26()
    {
        Escena();
        var a = Sprite("Arepa", "ArepaDorada", new Vector3(0, -0.5f, 0), 0.5f);
        a.AddComponent<DemoParticulas>();
        var ef = new GameObject("Efectos");
        Asignar(ef.AddComponent<Efectos>(), "prefabChispas", PrefabChispas());
        Guardar(ef);
    }

    static void Ej27()
    {
        Escena();
        cam.gameObject.AddComponent<Impactos>();
        var a = Sprite("Arepa", "ArepaDorada", new Vector3(0, 0.6f, 0), 0.45f);
        var r = a.AddComponent<Rebote>();
        var demo = new GameObject("Demo");
        Asignar(demo.AddComponent<DemoImpactos>(), "arepa", r);
        Guardar(cam.gameObject);
    }

    static void Ej28()
    {
        Escena();
        var go = new GameObject("GestorAudio");
        var musica = go.AddComponent<AudioSource>(); musica.loop = true; musica.playOnAwake = false;
        var efectos = go.AddComponent<AudioSource>(); efectos.playOnAwake = false;
        var ga = go.AddComponent<GestorAudio>();
        Asignar(ga, "fuenteMusica", musica);
        Asignar(ga, "fuenteEfectos", efectos);
        go.AddComponent<DemoAudio>();
        Guardar(go);
    }

    static void Ej29()
    {
        Escena();
        var c = Lienzo();
        var zona = new GameObject("ZonaSegura", typeof(RectTransform), typeof(Image));
        zona.transform.SetParent(c.transform, false);
        zona.GetComponent<Image>().color = new Color(CIELO.r, CIELO.g, CIELO.b, 0.35f);
        zona.AddComponent<ZonaSegura>();
        var t = Texto("Área segura", Vector2.zero, 90, Color.white, zona.transform);
        t.fontStyle = FontStyles.Bold;
        Guardar(zona);
    }

    static void Ej34()
    {
        Escena();
        string carpeta = Carpeta() + "/Niveles";
        Directory.CreateDirectory(carpeta);
        AssetDatabase.Refresh();
        var niveles = new DatosNivel[3];
        float[] punto = { 3f, 2.5f, 2f }, margen = { 0.4f, 0.3f, 0.2f }, quema = { 5f, 4f, 3.2f };
        int[] meta = { 300, 500, 800 };
        for (int i = 0; i < 3; i++)
        {
            string p = $"{carpeta}/Nivel{i + 1}.asset";
            var n = AssetDatabase.LoadAssetAtPath<DatosNivel>(p);
            if (n == null) { n = ScriptableObject.CreateInstance<DatosNivel>(); AssetDatabase.CreateAsset(n, p); }
            n.nombre = "Nivel " + (i + 1); n.tiempoPunto = punto[i]; n.margenPerfecto = margen[i];
            n.margenBueno = margen[i] * 2.5f; n.tiempoQuemada = quema[i]; n.metaPuntos = meta[i];
            n.arepasSimultaneas = i == 2 ? 2 : 1;
            EditorUtility.SetDirty(n);
            niveles[i] = n;
        }
        Sprite("Arepa", "ArepaDorada", new Vector3(0, 0.4f, 0), 0.4f);
        var go = new GameObject("GestorNiveles");
        var gn = go.AddComponent<GestorNiveles>();
        var so = new SerializedObject(gn);
        var arr = so.FindProperty("niveles"); arr.arraySize = 3;
        for (int i = 0; i < 3; i++) arr.GetArrayElementAtIndex(i).objectReferenceValue = niveles[i];
        so.ApplyModifiedPropertiesWithoutUndo();
        go.AddComponent<DemoNiveles>();
        Guardar(go);
    }

    static void Ej35()
    {
        // Escena Menu: título, botón Jugar y el objeto Navegacion
        Escena(null, "Menu");
        Sprite("Parrilla", "Parrilla", new Vector3(0, -1.2f, 0), 0.45f);
        Sprite("Arepa", "ArepaDorada", new Vector3(0.2f, 0.4f, 0), 0.35f).transform.rotation = Quaternion.Euler(0, 0, -15);
        Texto("MI PRIMER JUEGO", new Vector2(0, 300), 120, AMARILLO).fontStyle = FontStyles.Bold;

        var nav = new GameObject("Navegacion", typeof(RectTransform));
        var canvas = nav.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 100;
        nav.AddComponent<CanvasGroup>();
        var negro = new GameObject("Negro", typeof(RectTransform), typeof(Image));
        negro.transform.SetParent(nav.transform, false);
        var rt = (RectTransform)negro.transform; rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
        negro.GetComponent<Image>().color = Color.black;
        nav.AddComponent<Navegacion>();
        new GameObject("Botones").AddComponent<DemoNavegacion>();
        Guardar(nav);

        // Escena Juego
        Escena(null, "Juego");
        var pj = UnityEngine.Object.FindFirstObjectByType<PanelEjercicio>();
        pj.instrucciones = "Esta es la escena Juego. Voltea la arepa, prueba la pausa y vuelve al menú. " +
                           "Para ver la transición completa, empieza desde la escena Menu.";
        Sprite("Parrilla", "Parrilla", new Vector3(0, -1.6f, 0), 0.55f).GetComponent<SpriteRenderer>().sortingOrder = -1;
        Sprite("Arepa", "Arepa", new Vector3(0, 0.2f, 0), 0.5f).AddComponent<ArepaCoccion>();
        var b = new GameObject("Botones");
        var dn = b.AddComponent<DemoNavegacion>();
        var so = new SerializedObject(dn); so.FindProperty("esMenu").boolValue = false; so.ApplyModifiedPropertiesWithoutUndo();
        Guardar(b);
    }

    static GestorGuardado Guardado()
    {
        return new GameObject("GestorGuardado").AddComponent<GestorGuardado>();
    }

    static void Ej36()
    {
        Escena();
        Sprite("Frutero", "Frutero", new Vector3(0, 0.9f, 0), 0.35f);
        var gg = Guardado();
        gg.gameObject.AddComponent<DemoGuardado>();
        Guardar(gg.gameObject);
    }

    static void Ej37()
    {
        Escena();
        string carpeta = Carpeta() + "/Items";
        Directory.CreateDirectory(carpeta);
        AssetDatabase.Refresh();
        string[] ids = { "clasica", "queso", "choclo" }, nombres = { "Clásica", "De queso", "De choclo" };
        int[] precios = { 0, 300, 600 };
        var items = new ItemTienda[3];
        for (int i = 0; i < 3; i++)
        {
            string p = $"{carpeta}/{ids[i]}.asset";
            var it = AssetDatabase.LoadAssetAtPath<ItemTienda>(p);
            if (it == null) { it = ScriptableObject.CreateInstance<ItemTienda>(); AssetDatabase.CreateAsset(it, p); }
            it.id = ids[i]; it.nombre = nombres[i]; it.precio = precios[i];
            it.sprite = AssetDatabase.LoadAssetAtPath<UnityEngine.Sprite>(SPR + "Arepa.png");
            EditorUtility.SetDirty(it);
            items[i] = it;
        }
        var arepa = Sprite("Arepa", "Arepa", new Vector3(0, 0.9f, 0), 0.4f);
        Guardado();
        var go = new GameObject("Tienda");
        var tienda = go.AddComponent<Tienda>();
        var demo = go.AddComponent<DemoTienda>();
        var so = new SerializedObject(demo);
        so.FindProperty("tienda").objectReferenceValue = tienda;
        so.FindProperty("arepa").objectReferenceValue = arepa.GetComponent<SpriteRenderer>();
        var arr = so.FindProperty("items"); arr.arraySize = 3;
        var col = so.FindProperty("colores"); col.arraySize = 3;
        Color[] colores = { Color.white, new Color(1f, 0.95f, 0.55f), new Color(0.95f, 0.8f, 0.3f) };
        for (int i = 0; i < 3; i++)
        {
            arr.GetArrayElementAtIndex(i).objectReferenceValue = items[i];
            col.GetArrayElementAtIndex(i).colorValue = colores[i];
        }
        so.ApplyModifiedPropertiesWithoutUndo();
        Guardar(go);
    }

    static void Ej38()
    {
        Escena();
        Sprite("Arepa", "ArepaDorada", new Vector3(0, 1.0f, 0), 0.35f);
        var gg = Guardado();
        var gl = gg.gameObject.AddComponent<GestorLogros>();
        var so = new SerializedObject(gl);
        var arr = so.FindProperty("logros");
        var datos = new (string id, string nombre, string desc, int tipo, int meta)[]
        {
            ("racha3", "En racha", "3 arepas perfectas seguidas", 0, 3),
            ("racha10", "Maestro parrillero", "10 arepas perfectas seguidas", 0, 10),
            ("nivel1", "Primer nivel", "Gana un nivel", 1, 1),
            ("invicto", "Invicto", "Gana un nivel sin perder vidas", 2, 1),
        };
        arr.arraySize = datos.Length;
        for (int i = 0; i < datos.Length; i++)
        {
            var el = arr.GetArrayElementAtIndex(i);
            el.FindPropertyRelative("id").stringValue = datos[i].id;
            el.FindPropertyRelative("nombre").stringValue = datos[i].nombre;
            el.FindPropertyRelative("descripcion").stringValue = datos[i].desc;
            el.FindPropertyRelative("tipo").enumValueIndex = datos[i].tipo;
            el.FindPropertyRelative("meta").intValue = datos[i].meta;
        }
        so.ApplyModifiedPropertiesWithoutUndo();
        gg.gameObject.AddComponent<DemoLogros>();
        Guardar(gg.gameObject);
    }

    static void Ej41()
    {
        Escena();
        var f = Sprite("FrutaPool", "Naranja", Vector3.zero, 0.38f);
        f.AddComponent<CircleCollider2D>();
        f.AddComponent<Rigidbody2D>();
        f.AddComponent<FrutaPool>();
        var prefab = PrefabUtility.SaveAsPrefabAsset(f, PRE + "FrutaPool.prefab");
        UnityEngine.Object.DestroyImmediate(f);
        var gen = new GameObject("GeneradorConPool");
        gen.transform.position = new Vector3(0, 6, 0);
        var g = gen.AddComponent<GeneradorConPool>();
        Asignar(g, "prefab", prefab.GetComponent<FrutaPool>());
        Flotante(g, "intervalo", 0.3f);
        gen.AddComponent<DemoPool>();
        Guardar(gen);
    }

    static void Ej45()
    {
        Escena();
        Sprite("Arepa", "ArepaDorada", new Vector3(0, 1.0f, 0), 0.35f);
        var go = new GameObject("Analitica (demo)");
        go.AddComponent<DemoAnalitica>();
        Guardar(go);
    }

    // ----------------------------------------------------------- utilidades
    static string Carpeta()
    {
        string c = $"{R}/Ejercicios/{actual.carpeta}";
        Directory.CreateDirectory(c);
        return c;
    }

    static string rutaActual;

    static void Escena(Color? fondo = null, string nombre = null)
    {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var c = new GameObject("Main Camera") { tag = "MainCamera" };
        cam = c.AddComponent<Camera>();
        cam.orthographic = true; cam.orthographicSize = 5f;
        cam.clearFlags = CameraClearFlags.SolidColor; cam.backgroundColor = fondo ?? FONDO;
        c.transform.position = new Vector3(0, 0, -10);
        c.AddComponent<AudioListener>();
        var luz = new GameObject("Global Light 2D").AddComponent<Light2D>();
        luz.lightType = Light2D.LightType.Global;

        var p = new GameObject("Panel del ejercicio").AddComponent<PanelEjercicio>();
        p.numero = actual.numero; p.titulo = actual.titulo;
        p.instrucciones = actual.instrucciones; p.scripts = actual.scripts;

        rutaActual = $"{Carpeta()}/{nombre ?? $"Ej{actual.numero:00}"}.unity";
        lienzo = null;
    }

    static void Guardar(GameObject seleccion)
    {
        var escena = SceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(escena, rutaActual);
        escenas.Add(rutaActual);
        if (seleccion != null) Selection.activeGameObject = seleccion;
    }

    static GameObject Sprite(string nombre, string sprite, Vector3 pos, float escala)
    {
        var go = new GameObject(nombre);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = AssetDatabase.LoadAssetAtPath<UnityEngine.Sprite>(SPR + sprite + ".png");
        if (sr.sprite == null) throw new Exception("Falta el sprite " + sprite);
        go.transform.position = pos;
        go.transform.localScale = Vector3.one * escala;
        return go;
    }

    static void Suelo()
    {
        var s = Sprite("Piso", "Cuadrado", new Vector3(0, -4.6f, 0), 1f);
        s.transform.localScale = new Vector3(6f, 0.4f, 1);
        s.GetComponent<SpriteRenderer>().color = TIERRA;
        s.AddComponent<BoxCollider2D>();
    }

    static void Paredes()
    {
        var p = new GameObject("Paredes");
        void Pared(Vector2 pos, Vector2 tam)
        {
            var b = p.AddComponent<BoxCollider2D>();
            b.offset = pos; b.size = tam;
        }
        Pared(new Vector2(-3.4f, 0), new Vector2(0.5f, 12));
        Pared(new Vector2(3.4f, 0), new Vector2(0.5f, 12));
        Pared(new Vector2(0, 5.3f), new Vector2(8, 0.5f));
        Pared(new Vector2(0, -5.3f), new Vector2(8, 0.5f));
    }

    static GameObject Fruta(string sprite, Vector3 pos, float escala, bool conLimite)
    {
        var f = Sprite("Fruta" + sprite, sprite, pos, escala);
        f.tag = "Fruta";
        f.AddComponent<CircleCollider2D>();
        f.AddComponent<Rigidbody2D>();
        if (conLimite) f.AddComponent<FrutaLimite>();
        return f;
    }

    static GameObject PrefabFruta(string sprite)
    {
        string ruta = PRE + "Fruta" + sprite + ".prefab";
        var existente = AssetDatabase.LoadAssetAtPath<GameObject>(ruta);
        if (existente != null) return existente;
        var f = Fruta(sprite, Vector3.zero, sprite == "ArepaDorada" ? 0.22f : 0.38f, true);
        var prefab = PrefabUtility.SaveAsPrefabAsset(f, ruta);
        UnityEngine.Object.DestroyImmediate(f);
        return prefab;
    }

    static ParticleSystem PrefabChispas()
    {
        string ruta = PRE + "Chispas.prefab";
        var mat = AssetDatabase.LoadAssetAtPath<Material>(MAT + "Chispas.mat");
        if (mat == null)
        {
            var sh = Shader.Find("Universal Render Pipeline/Particles/Unlit") ?? Shader.Find("Sprites/Default");
            mat = new Material(sh);
            if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", Color.white);
            AssetDatabase.CreateAsset(mat, MAT + "Chispas.mat");
        }
        var go = new GameObject("Chispas");
        var ps = go.AddComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var main = ps.main;
        main.loop = false; main.duration = 0.5f; main.playOnAwake = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.5f, 0.9f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(4f, 8f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.12f, 0.26f);
        main.startColor = new ParticleSystem.MinMaxGradient(AMARILLO, new Color(1f, 0.65f, 0.15f));
        main.gravityModifier = 0.6f;
        main.stopAction = ParticleSystemStopAction.Destroy;
        var em = ps.emission; em.rateOverTime = 0; em.SetBursts(new[] { new ParticleSystem.Burst(0f, 40) });
        var sh2 = ps.shape; sh2.shapeType = ParticleSystemShapeType.Circle; sh2.radius = 0.5f;
        var rend = go.GetComponent<ParticleSystemRenderer>();
        rend.sharedMaterial = mat; rend.sortingOrder = 10;
        var prefab = PrefabUtility.SaveAsPrefabAsset(go, ruta);
        UnityEngine.Object.DestroyImmediate(go);
        return prefab.GetComponent<ParticleSystem>();
    }

    static void Asignar(UnityEngine.Object obj, string campo, UnityEngine.Object valor)
    {
        var so = new SerializedObject(obj);
        var prop = so.FindProperty(campo) ?? throw new Exception($"{obj.GetType().Name} no tiene el campo {campo}");
        prop.objectReferenceValue = valor;
        so.ApplyModifiedPropertiesWithoutUndo();
    }

    static void Flotante(UnityEngine.Object obj, string campo, float valor)
    {
        var so = new SerializedObject(obj);
        var prop = so.FindProperty(campo) ?? throw new Exception($"{obj.GetType().Name} no tiene el campo {campo}");
        prop.floatValue = valor;
        so.ApplyModifiedPropertiesWithoutUndo();
    }

    static Canvas lienzo;

    static Canvas Lienzo()
    {
        if (lienzo != null) return lienzo;
        var go = new GameObject("Canvas", typeof(RectTransform));
        lienzo = go.AddComponent<Canvas>();
        lienzo.renderMode = RenderMode.ScreenSpaceOverlay;
        var cs = go.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1080, 1920); cs.matchWidthOrHeight = 0.5f;
        return lienzo;
    }

    static TextMeshProUGUI Texto(string texto, Vector2 pos, float tam, Color color, Transform padre = null)
    {
        var go = new GameObject("Texto " + texto, typeof(RectTransform));
        go.transform.SetParent(padre != null ? padre : Lienzo().transform, false);
        var rt = (RectTransform)go.transform;
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(1000, tam * 1.3f);
        rt.anchoredPosition = pos;
        var t = go.AddComponent<TextMeshProUGUI>();
        t.text = texto; t.fontSize = tam; t.color = color;
        t.alignment = TextAlignmentOptions.Center;
        t.textWrappingMode = TextWrappingModes.NoWrap;
        return t;
    }
}
