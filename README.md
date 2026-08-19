# S1MA — Semana 1

**Desarrollo de Videojuegos para Móviles Avanzado** · 6.º ciclo · C26S
Laboratorio 1: *Configuración del entorno y Multiplayer Play Mode*

Hoy no vamos a hacer multijugador. Hoy vamos a **ver el problema** que nos va a
ocupar las próximas 16 semanas: dos instancias del mismo juego que no se ponen
de acuerdo en nada.

---

## Cómo abrir el proyecto

1. **Unity Hub → Add → Add project from disk**
2. Elige la carpeta `S1MA`
3. Ábrelo con **Unity 6** (6000.0.x)

La primera vez tarda un par de minutos: Unity está descargando Netcode for
GameObjects y Multiplayer Play Mode, que ya vienen declarados en
`Packages/manifest.json`. No hay que instalar nada a mano.

## Prepara la escena — un clic

En el menú superior:

**Tools → S1MA → Preparar escena Semana 1**

Eso crea `Assets/Scenes/Semana01.unity` con:

- un **Suelo**
- un **Jugador** (cubo) con el script `MovimientoLocal`
- un **NetworkManager** con `UnityTransport` ya asignado *(no se usa hoy; queda
  listo para la semana 3)*
- la cámara colocada

> Si el menú `Tools` no aparece, espera a que Unity termine de compilar. Abajo
> a la derecha verás el círculo de progreso.

## Activa Multiplayer Play Mode

1. **Window → Multiplayer → Play Mode**
2. Marca **Enable Multiplayer Play Mode**
3. **Virtual Players: 2**
4. Activa **Show Player Tags** para distinguir las ventanas

## Dale a Play y observa

Con las dos ventanas a la vista:

| Qué mirar | Qué vas a ver |
|---|---|
| El **color** del cubo en cada ventana | **Distinto**. Y es el mismo cubo. |
| Mueve con las flechas en **Player 1** | El cubo se mueve **solo ahí** |
| Mira **Player 2** | El cubo **no se enteró** |

Eso es. Dos simulaciones, dos verdades, cero acuerdo.

---

## Por qué pasa

`MovimientoLocal.cs` no tiene ni una línea de red. Cada instancia:

- sortea su propio color en `Start()`
- lee su propio teclado en `Update()`
- mueve su propio `transform`

Nadie le cuenta nada a nadie. **No existe un estado global**: existe una copia
del mundo por máquina, y hoy no hay nada que las reconcilie.

## Lo que entregas

En la tarea de Canvas de esta semana:

- **Vídeo de 60 segundos** con las dos ventanas visibles: mueve el cubo en una y
  enseña que la otra no reacciona.
- **Una frase** explicando por qué ocurre.

---

## Lo que viene

| Semana | Qué arreglamos |
|---|---|
| 3 | `MovimientoLocal` hereda de `NetworkBehaviour` → el movimiento viaja |
| 4 | El color se sincroniza con `NetworkVariable` |

## Si algo falla

| Síntoma | Causa |
|---|---|
| El menú `Tools` no aparece | Unity sigue compilando, o hay un error en la consola |
| `NullReferenceException` al arrancar Host | Falta asignar el Network Transport en el NetworkManager |
| El cubo no se mueve | El proyecto está en *Input System*. Ve a **Project Settings → Player → Active Input Handling** y elige **Both** |
| Play Mode no abre ventanas | Reinicia Unity: la primera vez crea una `Library` por jugador virtual |
