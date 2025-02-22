using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource audioSource; // Arrástralo desde el Inspector
    public AudioClip loopSound; // Sonido que se repetirá

    void Start()
    {
        audioSource.clip = loopSound; // Asignar el sonido
        audioSource.loop = true; // Hacer que se repita
        audioSource.Play(); // Iniciar la reproducción
    }



    // Update is called once per frame
    void Update()
    {

    }
}
