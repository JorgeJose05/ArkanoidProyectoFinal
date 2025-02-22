using UnityEngine;

public class BloquePurple : MonoBehaviour
{
    private void OnDestroy()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
        {
            spawner.GenerarBooster(transform.position);
        }
    }
}
