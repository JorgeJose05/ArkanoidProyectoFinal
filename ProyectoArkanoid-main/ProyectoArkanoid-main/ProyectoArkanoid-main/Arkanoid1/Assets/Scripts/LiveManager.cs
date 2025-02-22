using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LiveManager : MonoBehaviour
{
    public static LiveManager Instance { get; private set; }

    // Número de vidas iniciales
    public int startingLives = 3;

    // Variable para llevar la cuenta actual
    private int currentLives;

    // Referencia a un elemento de UI para mostrar las vidas (puede ser Text o TextMeshProUGUI)
    [Header("UI References")]
    public TextMeshProUGUI livesText;  // Si usas UI.Text
    // public TextMeshProUGUI livesText; // Si usas TextMeshPro


    void Awake()
    {
        // Si ya existe una instancia, destrúyela
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto asegura que el LiveManager no se destruya al cargar nuevas escenas
        }


         // Si es la primera vez que se ejecuta el juego, inicializa las vidas
        if (SceneManager.GetActiveScene().buildIndex == 1)  
        {
            PlayerPrefs.SetInt("Vidas", 3);
            PlayerPrefs.Save();
        }

        // Cargar el número de vidas desde PlayerPrefs
        currentLives = PlayerPrefs.GetInt("Vidas", startingLives);

        // Registrar el evento para cuando una escena es cargada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        startingLives = 3;
        // Asegurarse de que el UI se actualiza en la primera escena
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }


// Este método se llama cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar el componente TextMeshProUGUI en la escena cargada
        livesText = GameObject.Find("vidas")?.GetComponent<TextMeshProUGUI>();
        UpdateLivesUI();
    }


    // Método para restar una vida
    public void LoseLife()
    {
        currentLives--;
        PlayerPrefs.SetInt("Vidas", currentLives);
        PlayerPrefs.Save();
        Debug.Log("Vidas restantes: " + currentLives);

        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    // Método para ganar una vida (opcional)
    public void GainLife()
    {
        currentLives++;
        PlayerPrefs.SetInt("Vidas", currentLives);
        PlayerPrefs.Save();

        UpdateLivesUI();
    }

    // Actualiza el texto en la UI
    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + currentLives;
        }
    }

    // Lógica a ejecutar cuando se agoten las vidas
    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("Scenes/GameOver");
    }
}
