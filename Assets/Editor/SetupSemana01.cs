using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

/// <summary>
/// Arma la escena de la semana 1 con un clic, para no perder tiempo de clase
/// creando objetos a mano.
///
/// Menú:  Tools > S1MA > Preparar escena Semana 1
///
/// Se hace por script y no con un archivo .unity versionado a propósito: así
/// no dependemos de GUIDs ni de la versión exacta del editor, y la escena se
/// regenera igual en las 20 máquinas del laboratorio.
/// </summary>
public static class SetupSemana01
{
    private const string RutaEscena = "Assets/Scenes/Semana01.unity";

    [MenuItem("Tools/S1MA/Preparar escena Semana 1")]
    public static void PrepararEscena()
    {
        var escena = EditorSceneManager.NewScene(
            NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // --- Suelo ---------------------------------------------------------
        var suelo = GameObject.CreatePrimitive(PrimitiveType.Plane);
        suelo.name = "Suelo";
        suelo.transform.localScale = new Vector3(2f, 1f, 2f);

        // --- Jugador -------------------------------------------------------
        var jugador = GameObject.CreatePrimitive(PrimitiveType.Cube);
        jugador.name = "Jugador";
        jugador.transform.position = new Vector3(0f, 0.5f, 0f);
        jugador.AddComponent<MovimientoLocal>();

        // --- NetworkManager (aún no se usa; queda listo para la semana 3) ---
        var nmGo = new GameObject("NetworkManager");
        var nm = nmGo.AddComponent<NetworkManager>();
        var utp = nmGo.AddComponent<UnityTransport>();
        nm.NetworkConfig = new NetworkConfig { NetworkTransport = utp };

        // --- Cámara --------------------------------------------------------
        var cam = Camera.main;
        if (cam != null)
        {
            cam.transform.position = new Vector3(0f, 8f, -8f);
            cam.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
        }

        System.IO.Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(escena, RutaEscena);
        AssetDatabase.Refresh();

        Debug.Log("[S1MA] Escena lista en " + RutaEscena +
                  ". Abre Window > Multiplayer > Play Mode, pon 2 jugadores virtuales y dale a Play.");
        EditorUtility.DisplayDialog(
            "S1MA",
            "Escena de la Semana 1 preparada.\n\n" +
            "Siguiente paso:\n" +
            "Window > Multiplayer > Play Mode\n" +
            "→ Enable Multiplayer Play Mode\n" +
            "→ Virtual Players: 2\n" +
            "→ Play",
            "Vamos");
    }
}
