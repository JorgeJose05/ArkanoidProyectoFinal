using UnityEngine;
using UnityEngine.UI;

public class QuitButtonHandler : MonoBehaviour
{
    [SerializeField] private Button botonQuit;

    public void Awake()
    {
        // Asegúrate de que haya un botón asignado
        if (botonQuit != null)
        {
            // Añadir el listener al botón para que al hacer clic, se cierre la aplicación
            botonQuit.onClick.AddListener(() => 
            { 
                Debug.Log("QUIT!"); 
                Application.Quit(); 
            });
        }
        else
        {
            Debug.LogError("Botón Quit no asignado en el inspector.");
        }
    }
}
