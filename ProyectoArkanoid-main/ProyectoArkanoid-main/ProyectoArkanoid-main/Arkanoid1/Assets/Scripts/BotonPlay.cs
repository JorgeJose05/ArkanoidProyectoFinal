using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonPlay : MonoBehaviour
{
    [SerializeField] private Button botonQuit;

    private void Start()
    {
        if (botonQuit != null)
        {
            botonQuit.onClick.AddListener(CambiarEscena);
        }
        else
        {
            Debug.LogError("Botón Jugar no asignado en el inspector.");
        }
    }

    private void CambiarEscena()
    {
        Debug.Log("Cambiando a la escena 0...");
        SceneManager.LoadScene(1);
    }
}
