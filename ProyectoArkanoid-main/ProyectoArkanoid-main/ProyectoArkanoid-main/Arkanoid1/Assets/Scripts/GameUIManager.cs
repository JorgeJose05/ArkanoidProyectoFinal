using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI numPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActualizarPuntajeUI(int puntaje)
    {
        if (numPoints != null)
        {
            numPoints.text = puntaje.ToString(); 
        }
        else
        {
            Debug.LogError("El texto del puntaje no está asignado en el Inspector.");
        }
    }
}
