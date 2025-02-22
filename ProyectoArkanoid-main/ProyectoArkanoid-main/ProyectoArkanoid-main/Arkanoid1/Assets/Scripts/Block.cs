using UnityEngine;

public class Block : MonoBehaviour
{
    public ParticleSystem particles;

    public AudioClip destroySound = null; 

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Avisar al GameManager que un bloque fue destruido
        GameManager.Instance.BloqueDestruido(gameObject.name.Split('_')[1]);

        // Deshabilitar el bloque visualmente
        Renderer render = GetComponent<Renderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        render.enabled = false;
        collider.enabled = false;

        // Instanciar partículas en la posición del bloque
        AudioSource.PlayClipAtPoint(destroySound, transform.position); 
        Instantiate(particles, collider.transform.position, collider.transform.rotation);

        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null && this.name.Contains("Purple") )
        {
            spawner.GenerarBooster(transform.position);
        }else{
            Debug.Log("Error no se ha creado un booster");
        }


    }
    
}
