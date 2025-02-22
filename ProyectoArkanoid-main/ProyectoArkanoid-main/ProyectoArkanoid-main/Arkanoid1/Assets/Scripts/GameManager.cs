using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq; // Asegúrate de incluir esto


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int multiplicador = 1;

    private static int totalBlocks;
    private int puntaje;

    [Header("UI References")]
    public TextMeshProUGUI textoPuntaje;

    private void Awake()
    {
        // Singleton: Asegurar que solo hay un GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
        // Registrar el evento para cuando una escena es cargada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Este método se llama cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar el componente TextMeshProUGUI para las vidas en la escena cargada
        textoPuntaje = GameObject.Find("numPoints")?.GetComponent<TextMeshProUGUI>();
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Vidas: " + puntaje.ToString(); // Actualizar las vidas en la UI
        }
    }

    public void ContarBloques()
    {
        try
        {
            totalBlocks = GameObject.FindGameObjectsWithTag("Brick")
                         .Count(brick => brick.name.Contains("Blue"));
            Debug.Log($"********************Bloques azules {totalBlocks}");
        }
        catch (UnityException ex)
        {
            Debug.LogError("La etiqueta 'brick' no está definida en el Tag Manager. Asegúrate de agregarla.");
            
            totalBlocks = 4;
        }
        Debug.Log($"🔢 Bloques totales: {totalBlocks}");
    }


    private void Start()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
        {
           // No actives esto que ya lo hace el spaqner en el start spawner.GenerarNivel(1);
        }

        // Verificar si estamos en la escena 0
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))  // Asegúrate de que el nombre de la escena sea correcto
        {
            puntaje = 0;  // Inicializa el puntaje a 0 si estamos en la escena inicial
        }
        else
        {
            puntaje = PlayerPrefs.GetInt("Contador", 0);  // Cargar el puntaje almacenado si no estamos en la escena 0
        }

        // Inicializa la UI con el puntaje actual
        ActualizarPuntajeUI();

        // Contar los bloques en la escena actual
        //totalBlocks = FindObjectsOfType<Brick>().Length;
        GameManager.EncontrarBricks();
    }

    public static void EncontrarBricks()
    {
        try
        {
        totalBlocks = GameObject.FindGameObjectsWithTag("Brick")
                         .Count(brick => brick.name.Contains("Blue"));
        Debug.Log($"Ya se han contado bien todos los bloques azules hhay {totalBlocks}");
        }
        catch (UnityException ex)
        {
            Debug.LogError("La etiqueta 'brick' no está definida en el Tag Manager. Asegúrate de agregarla.");
        //    bricks = new GameObject[0]; // Evita errores futuros devolviendo un array vacío
            totalBlocks = 5;
        }

    }

    public void BloqueDestruido(string nombreBloque)
    {
        string color = nombreBloque.Split('(')[0];

        Debug.Log($"Color color: {color}");
        Debug.Log(nombreBloque);
        if (nombreBloque.Contains("Blue")){
        totalBlocks--;
        }

        switch (color)
        {
            case "Yellow":
        puntaje += 1 * multiplicador; 
                break;
            case "Red":
        puntaje -= 1 * multiplicador; 
                break;
            case "Green":
        puntaje += 4 * multiplicador; 
                break;
            case "Purple":
        puntaje += 5 * multiplicador; 
                break;
            case "Grey":
        puntaje += 3 * multiplicador; 
                break;
            case "Blue":
        puntaje += 3 * multiplicador; 
                break;
            default:
        Debug.LogError($"Esto no deberia pasar es color no esta registrado {color}");

                break;
        }



        // Guardar el puntaje para la siguiente escena
        PlayerPrefs.SetInt("Contador", puntaje);
        PlayerPrefs.Save();


        ActualizarPuntajeUI();
        //Debug.Log($"Bloque destruido: {nombreBloque}");
        Debug.Log($"Bloques restantes: {totalBlocks}");
        Debug.Log($"Puntaje: {puntaje}");

        if (totalBlocks <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();

        // Obtén el índice de la escena activa
        int sceneIndex = currentScene.buildIndex;
            SceneManager.LoadScene(sceneIndex+1);
        }
    }

    public void aumentrarMultiplicador(){
        multiplicador+=1;
    }


    private void ActualizarPuntajeUI()
    {
        // Actualiza el texto de puntaje en el Canvas
        if (textoPuntaje != null)
        {
            textoPuntaje.text = puntaje.ToString();  // Actualiza el texto con el puntaje
        }
    }

}
