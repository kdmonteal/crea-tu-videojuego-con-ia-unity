# Crea tu videojuego con IA en Unity · Proyecto de los ejercicios

Proyecto de Unity que acompaña el libro **«Crea tu videojuego con IA en Unity: 50 ejercicios prácticos»** de Kelly Daniella Marín.

Aquí encuentras los scripts del libro listos para jugar: cada ejercicio con código tiene su propia escena, que se juega al darle Play y te explica en pantalla qué hacer y qué observar.

🌐 Web de la autora: [https://www.kdmonteal.studio](https://www.kdmonteal.studio)

---

## Lo que necesitas

- **Unity 6** (el proyecto se creó con la versión 6000.3). Instálalo desde Unity Hub. Si tienes otra versión de Unity 6, Unity Hub te preguntará si quieres abrir el proyecto con ella: acepta.
- Unos 2 GB libres. La primera vez, Unity tarda varios minutos en importar el proyecto.

## Cómo descargarlo

**Opción fácil (sin Git):**

1. Arriba en esta página, pulsa el botón verde **Code** y elige **Download ZIP**.
2. Descomprime el archivo en una carpeta fácil de encontrar, por ejemplo `Documentos/Unity`.

**Con Git:**

```
git clone https://github.com/kdmonteal/crea-tu-videojuego-con-ia-unity.git
```

## Cómo abrirlo

1. Abre **Unity Hub** y pulsa **Add › Add project from disk**.
2. Elige la carpeta que descomprimiste (la que contiene `Assets`, `Packages` y `ProjectSettings`).
3. Haz clic en el proyecto para abrirlo y espera a que termine de importar.

## Cómo usarlo

Al abrir el proyecto aparece la ventana **Ejercicios del libro**. Si no la ves, ábrela desde el menú **Libro › Ejercicios del libro** (o con `Cmd/Ctrl + Shift + E`).

1. Elige la parte del libro en las pestañas de arriba.
2. Busca el ejercicio y pulsa **▶ Jugar**. Se abre su escena y empieza el modo Play.
3. Mira la vista **Game**: el panel de arriba te dice qué hacer y qué observar. Los mensajes que el código escribe en la Consola también aparecen abajo, en la misma pantalla.
4. Pulsa **Script** para abrir el código del ejercicio y compararlo con el libro.
5. Para detener el juego, vuelve a pulsar el botón ▶ de la parte superior del editor.

Atajos dentro de cada escena: **R** reinicia la escena y **H** oculta o muestra el panel.

> **Consejo:** en la vista Game, elige una resolución vertical (por ejemplo, *1080x1920 Portrait*) para ver el juego como en un celular.

## Qué hay en cada carpeta

```
Assets/CreaTuVideojuego/
├── Ejercicios/
│   ├── Parte1/   Ej02_CuadradoQueGira, Ej05_CSharpMinimo, Ej10_AtrapaLaFruta…
│   ├── Parte2/   Ej11 a Ej20: toque, física, puntaje, tiempo, vidas…
│   ├── Parte3/   Ej24 a Ej29: animación, juice, partículas, audio, UI
│   ├── Parte4/   Ej34 a Ej38: niveles, menús, guardado, tienda, logros
│   └── Parte5/   Ej41 a Ej45: optimización, anuncios y analítica
└── Comun/
    ├── Sprites/  Sprites voxel usados en las escenas
    ├── Scripts/  Panel de instrucciones y botones de demostración
    └── Editor/   La ventana «Ejercicios del libro» y el constructor de escenas
```

Cada carpeta de ejercicio contiene **los scripts tal como aparecen en el libro** y su escena (`EjXX.unity`). Los scripts que empiezan por `Demo` y el `PanelEjercicio` no son parte del libro: solo agregan botones e instrucciones para que puedas probar cada ejercicio.

### Ejercicios sin escena

Algunos ejercicios no se hacen con código, sino en el editor, con tu asistente de IA o fuera de Unity (por ejemplo, el 1, el 3, el 21 o el 47). La ventana **Ejercicios del libro** te dice dónde se hace cada uno.

### Anuncios (ejercicios 43 y 44)

Los scripts `GestorAnuncios` y `ControlAnuncios` necesitan el SDK de Google Mobile Ads. Para que el proyecto compile sin él, están desactivados. Cuando instales el SDK siguiendo el ejercicio 43, agrega el símbolo `ADMOB` en *Project Settings › Player › Scripting Define Symbols*.

## Si algo no funciona

- **Errores rojos al abrir por primera vez:** espera a que Unity termine de importar y resolver los paquetes. Si siguen, cierra y vuelve a abrir el proyecto.
- **El toque no responde:** el proyecto usa el **Input System** nuevo. Revisa que *Project Settings › Player › Active Input Handling* diga *Input System Package (New)* o *Both*.
- **Rompí una escena:** usa **Libro › Mantenimiento › Reconstruir escenas** para crearlas de nuevo.
- **Quiero empezar de cero con el guardado (ejercicios 36 a 38):** en la escena del ejercicio 36, pulsa *Borrar partida*.

## Licencia

- **Código** (archivos `.cs`): licencia MIT. Úsalo, cámbialo y aprovéchalo en tus propios juegos.
- **Sprites voxel** (`Assets/CreaTuVideojuego/Comun/Sprites`): © kdmonteal.studio. Puedes usarlos para practicar con el libro; para usarlos en un juego publicado, escríbeme.

---

Hecho con cariño para mis estudiantes y lectores. Si creas algo con este proyecto, compártelo en Instagram o YouTube con **@kdmonteal.studio**.
