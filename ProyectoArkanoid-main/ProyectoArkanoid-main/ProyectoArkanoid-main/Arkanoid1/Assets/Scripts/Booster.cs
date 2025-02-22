using UnityEngine;

public class Booster : MonoBehaviour
{
    public float velocidadCaida = 3f; // Velocidad de caída
    public ParticleSystem particles;

    void Update()
    {
            // Simula la gravedad moviendo la bola hacia abajo
            transform.position += Vector3.down * velocidadCaida * Time.deltaTime;
        // Si el booster cae más allá de Y = -120, se desactiva
        if (transform.position.y < -120f)
        {
            gameObject.SetActive(false);
        }
        }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Raqueta")) // Si toca la raqueta
        {
        // Instanciar partículas en la posición del bloque
            Instantiate(particles, transform.position, transform.rotation);
            gameObject.SetActive(false); // Se desactiva si la raqueta lo toca

            GameManager.Instance.aumentrarMultiplicador();

        }

     
    }
}

