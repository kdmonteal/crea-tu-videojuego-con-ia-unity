// Lista de los 50 ejercicios del libro, con la carpeta de su escena (si la tiene)
// y las instrucciones que aparecen en pantalla al jugar.
public class Ejercicio
{
    public int numero;
    public string titulo, carpeta, instrucciones, scripts;
    public Ejercicio(int n, string t, string c, string i, string s)
    { numero = n; titulo = t; carpeta = c; instrucciones = i; scripts = s; }
    public bool TieneEscena => !string.IsNullOrEmpty(carpeta);
    public int Parte => (numero - 1) / 10 + 1;
}

public static class CatalogoEjercicios
{
    public const string Raiz = "Assets/CreaTuVideojuego";

    public static readonly string[] Partes =
    {
        "Parte 1 · Tu estudio con IA", "Parte 2 · Mecánicas que se sienten bien",
        "Parte 3 · Arte, sonido y juice", "Parte 4 · Tu juego completo", "Parte 5 · Publica y monetiza"
    };

    public static readonly Ejercicio[] Todos =
    {
        new Ejercicio(1, "Instala Unity y crea tu primer proyecto", "", "Se hace en Unity Hub: instala Unity 6 y crea un proyecto 2D.", ""),
        new Ejercicio(2, "Tu primer prompt: un cuadrado que gira", "Parte1/Ej02_CuadradoQueGira", "Dale Play: el cuadrado gira solo. Con el juego corriendo, selecciona «Cuadrado» en la Hierarchy y cambia Velocidad en el Inspector. Prueba también un número negativo.", "Girador.cs"),
        new Ejercicio(3, "Recorre el editor como un pro", "", "Se hace recorriendo el editor: Scene, Game, Hierarchy, Project, Inspector y Console.", ""),
        new Ejercicio(4, "GameObjects y componentes", "Parte1/Ej04_GameObjectsYComponentes", "La fruta tiene un Rigidbody 2D: la gravedad la hace caer y su Circle Collider 2D la detiene sobre el piso. Selecciónala y revisa sus componentes en el Inspector. Desmarca el Rigidbody 2D y dale Play otra vez: ya no cae.", ""),
        new Ejercicio(5, "C# mínimo para no depender de la IA", "Parte1/Ej05_CSharpMinimo", "Presiona Espacio: cada vez suma 10 puntos y lo escribe en la Consola. Al llegar a 50, la arepa se pone verde y el contador se detiene.", "Contador.cs"),
        new Ejercicio(6, "Lee y entiende el código que te da la IA", "", "Practica con cualquier script de este proyecto (por ejemplo, el del ejercicio 5).", ""),
        new Ejercicio(7, "Depura con la IA", "", "Practica con los errores que aparezcan en tu Consola.", ""),
        new Ejercicio(8, "El prompt perfecto para Unity", "", "Se hace en tu asistente de IA.", ""),
        new Ejercicio(9, "Prefabs que se crean solos", "Parte1/Ej09_PrefabsQueSeCreanSolos", "El generador crea una fruta cada segundo en una posición al azar. Las que salen por abajo se destruyen solas: mira cómo aparecen y desaparecen en la Hierarchy.", "GeneradorFrutas.cs, FrutaLimite.cs"),
        new Ejercicio(10, "Mini reto: atrapa la fruta", "Parte1/Ej10_AtrapaLaFruta", "Mantén presionado el clic (o el dedo) y muévete a los lados: la canasta sigue al puntero. Cada fruta atrapada suma un punto en la Consola.", "Canasta.cs (y GeneradorFrutas.cs)"),
        new Ejercicio(11, "Mueve un personaje con el dedo", "Parte2/Ej11_MueveConElDedo", "Arrastra en cualquier parte de la pantalla: la canasta se mueve lo mismo que tu dedo, sin saltar a donde tocas. Compárala con la canasta del ejercicio 10.", "ArrastreRelativo.cs"),
        new Ejercicio(12, "Desliza y lanza", "Parte2/Ej12_DeslizaYLanza", "Desliza rápido hacia arriba y suelta para lanzar el tejo contra las mechas. Un toque corto no lo lanza. Presiona R para volver a intentarlo.", "Lanzador.cs"),
        new Ejercicio(13, "Física 2D sin miedo", "Parte2/Ej13_Fisica2D", "Toca la pantalla o presiona Espacio para saltar. Todos los saltos tienen la misma altura porque el script reinicia la velocidad vertical antes de cada salto.", "Salto.cs"),
        new Ejercicio(14, "Colisiones y triggers", "Parte2/Ej14_ColisionesYTriggers", "Toca para lanzar la pelota hacia arriba. Al caer, la Consola muestra la fuerza del choque y avisa si fue un golpe fuerte. Al pasar por la manzana (un trigger) aparece otro mensaje.", "Impacto.cs (y Salto.cs)"),
        new Ejercicio(15, "Sistema de puntaje", "Parte2/Ej15_SistemaDePuntaje", "Toca la pantalla o presiona Espacio para sumar 10 puntos. El texto se actualiza solo porque escucha el evento AlCambiar de GestorPuntaje.", "GestorPuntaje.cs, TextoPuntaje.cs"),
        new Ejercicio(16, "Temporizador y cuenta regresiva", "Parte2/Ej16_Temporizador", "Primero una cuenta regresiva de 3 segundos y luego el reloj de la partida (20 segundos). Al llegar a cero se dispara el evento AlTerminar. Cambia Duracion Partida en el Inspector.", "Reloj.cs"),
        new Ejercicio(17, "Voltea en el punto justo", "Parte2/Ej17_VolteaEnElPuntoJusto", "La arepa se cocina: pasa de cruda a dorada y luego se quema. Toca la pantalla o presiona Espacio para voltearla en el punto justo (a los 3 segundos). La calificación aparece en la Consola.", "ArepaCoccion.cs"),
        new Ejercicio(18, "Generador de obstáculos", "Parte2/Ej18_GeneradorDeObstaculos", "Caen tres frutas con pesos distintos (70, 25 y 5): la dorada casi nunca sale y nunca hay más de tres iguales seguidas. Con el tiempo caen más rápido según la curva Intervalo Por Tiempo.", "GeneradorPonderado.cs"),
        new Ejercicio(19, "Vidas y game over", "Parte2/Ej19_VidasYGameOver", "Usa los botones para recorrer los estados: empieza, pierde vidas, pausa y reanuda. Con 0 vidas llega el Game Over. Fíjate en timeScale: vale 0 durante la pausa.", "GestorJuego.cs"),
        new Ejercicio(20, "Mini reto: ordena por color", "Parte2/Ej20_OrdenaPorColor", "Arrastra cada fruta a su canasto: manzanas al rojo, bananos al amarillo y patillas al verde. Si te equivocas, la fruta vuelve a su sitio. Cada acierto suma un punto.", "FrutaArrastrable.cs, Canasto.cs"),
        new Ejercicio(21, "Define el estilo visual con la IA", "", "Se hace con tu asistente de IA y una guía de estilo.", ""),
        new Ejercicio(22, "Sprites generados con IA y cómo limpiarlos", "", "Se hace con una IA de imágenes y un editor de imágenes.", ""),
        new Ejercicio(23, "Modelos 3D con IA para tu juego", "", "Se hace con una IA de modelos 3D y Blender.", ""),
        new Ejercicio(24, "Animación 2D", "Parte3/Ej24_Animacion2D", "Toca la pantalla o presiona Espacio para reproducir el volteo. Con la arepa seleccionada, abre Window > Animation > Animator para ver los estados y el Trigger «Voltear».", "AnimadorArepa.cs"),
        new Ejercicio(25, "Juice: squash and stretch", "Parte3/Ej25_SquashAndStretch", "Toca la pantalla o presiona Espacio: la arepa se aplasta y se estira. Cambia Intensidad y Duracion en el Inspector, o edita la curva Forma, y compara la sensación.", "Rebote.cs"),
        new Ejercicio(26, "Partículas que celebran", "Parte3/Ej26_Particulas", "Toca la pantalla o presiona Espacio para lanzar chispas. Cada explosión es un prefab que se destruye solo al terminar (Stop Action = Destroy): obsérvalo en la Hierarchy.", "Efectos.cs"),
        new Ejercicio(27, "Sacudida de cámara y cámara lenta", "Parte3/Ej27_SacudidaYCamaraLenta", "Compara los dos botones. El golpe perfecto agrega una pausa brevísima (hit stop) antes de la sacudida: esa pausa es la que hace que un golpe se sienta fuerte.", "Impactos.cs"),
        new Ejercicio(28, "Música y efectos de sonido", "Parte3/Ej28_MusicaYEfectos", "El botón reproduce un efecto con un tono un poco distinto cada vez. Para los volúmenes de música y efectos, crea un Audio Mixer como en el ejercicio 28 y asígnalo en GestorAudio.", "GestorAudio.cs"),
        new Ejercicio(29, "Fuentes y UI que se ve profesional", "Parte3/Ej29_ZonaSegura", "El panel azul se ajusta al área segura. Abre Window > General > Device Simulator, elige un celular con muesca y gíralo: el panel evita la cámara y las esquinas redondeadas.", "ZonaSegura.cs"),
        new Ejercicio(30, "Mini reto: pule tu mini juego", "", "Aplica el juice de los ejercicios 24 a 29 a tu propio mini juego.", ""),
        new Ejercicio(31, "Documento de diseño de una página con IA", "", "Se hace con tu asistente de IA.", ""),
        new Ejercicio(32, "Prototipa el core loop en una hora", "", "Usa como base las escenas de los ejercicios 10, 17 o 20.", ""),
        new Ejercicio(33, "Curva de dificultad", "", "Se hace en una hoja de cálculo.", ""),
        new Ejercicio(34, "Niveles con ScriptableObjects", "Parte4/Ej34_NivelesConScriptableObjects", "Cada nivel es un archivo (ScriptableObject) en la carpeta Niveles de este ejercicio. Avanza de nivel y mira cómo cambian los datos. Selecciona un nivel en la ventana Project para editarlo.", "DatosNivel.cs, GestorNiveles.cs"),
        new Ejercicio(35, "Menú principal y pausa", "Parte4/Ej35_MenuYPausa", "Pulsa Jugar: la pantalla se funde a negro, carga la escena Juego y vuelve a aparecer. Navegacion sobrevive al cambio de escena: búscala en la Hierarchy dentro de DontDestroyOnLoad.", "Navegacion.cs"),
        new Ejercicio(36, "Guarda el progreso", "Parte4/Ej36_GuardaElProgreso", "Cambia las monedas o el récord, pulsa Guardar y sal del modo Play. Al darle Play otra vez, los datos siguen ahí: se leen del archivo partida.json.", "GestorGuardado.cs"),
        new Ejercicio(37, "Monedas y tienda simple", "Parte4/Ej37_MonedasYTienda", "Consigue monedas con el truco de prueba, compra arepas y equípalas: el color cambia. Las compras se guardan, así que siguen ahí la próxima vez que le des Play.", "Tienda.cs, ItemTienda.cs"),
        new Ejercicio(38, "Logros y récords", "Parte4/Ej38_LogrosYRecords", "Pulsa «Arepa perfecta» tres veces seguidas para desbloquear el primer logro. Una arepa fallida reinicia la racha. Cada logro se desbloquea una sola vez y queda guardado.", "GestorLogros.cs"),
        new Ejercicio(39, "Playtesting con la IA como analista", "", "Se hace con personas que prueben tu juego.", ""),
        new Ejercicio(40, "Tu juego completo", "", "Junta las piezas de los ejercicios 31 a 39 en tu juego.", ""),
        new Ejercicio(41, "Optimiza para móvil", "Parte5/Ej41_OptimizaConPool", "Las frutas no se destruyen: al salir de la pantalla vuelven al pool y se reutilizan. Fíjate en que el total de frutas creadas deja de crecer.", "GeneradorConPool.cs, FrutaPool.cs"),
        new Ejercicio(42, "Build para Android", "", "Se hace en File > Build Profiles con tu celular Android conectado.", ""),
        new Ejercicio(43, "Anuncios con AdMob", "", "Necesita el SDK de AdMob: los scripts están en Parte5/Ej43_44_Anuncios.", ""),
        new Ejercicio(44, "Anuncios sin arruinar la experiencia", "", "Ver Parte5/Ej43_44_Anuncios (ControlAnuncios.cs).", ""),
        new Ejercicio(45, "Analítica básica", "Parte5/Ej45_Analitica", "Cada botón envía un evento de analítica. En el editor los eventos se escriben en la Consola; en el celular irían al servicio que elijas.", "Analitica.cs"),
        new Ejercicio(46, "Ícono y capturas con IA", "", "Se hace con una IA de imágenes.", ""),
        new Ejercicio(47, "Ficha de Play Store que convierte", "", "Se hace en Google Play Console.", ""),
        new Ejercicio(48, "Política de privacidad y clasificación", "", "Se hace fuera de Unity.", ""),
        new Ejercicio(49, "Prueba cerrada y lanzamiento", "", "Se hace en Google Play Console.", ""),
        new Ejercicio(50, "Después del lanzamiento", "", "Empieza tu segundo juego con los scripts de este proyecto.", ""),
    };

    // Ruta de la escena principal del ejercicio
    public static string Escena(Ejercicio e)
    {
        if (!e.TieneEscena) return null;
        string nombre = e.numero == 35 ? "Menu" : $"Ej{e.numero:00}";
        return $"{Raiz}/Ejercicios/{e.carpeta}/{nombre}.unity";
    }
}
