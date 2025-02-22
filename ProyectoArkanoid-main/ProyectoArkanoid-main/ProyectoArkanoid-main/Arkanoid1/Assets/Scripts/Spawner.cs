using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> bloquesPrefabs; // Lista de prefabs asignados en el inspector
    [SerializeField] private GameObject boosterPrefab; // Prefab de la bola booster
    [SerializeField] private int columnas = 8;
    [SerializeField] private int filas = 6;
    [SerializeField] private float espacio = 1.2f; // Espacio entre bloques



    private int nivelActual = 1; // Nivel por defecto

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Obtén el índice de la escena activa
        int sceneIndex = currentScene.buildIndex;
        nivelActual = sceneIndex;
        // Verificar si la lista de prefabs está vacía
        if (bloquesPrefabs == null || bloquesPrefabs.Count == 0)
        {
            Debug.LogError("⚠ La lista de prefabs no está inicializada o está vacía.");
            return;
        }
        else
        {
            Debug.Log("La lista de prefabs esta bien.");

        }
        GenerarNivel(nivelActual);
    }

    public void GenerarNivel(int nivel)
    {
        nivelActual = nivel;
        Debug.Log($"Generando nivel {nivel}");

        switch (nivel)
        {
            case 1:
                GenerarPatronBasico();
                break;
            case 2:
                GenerarPatronEscalonado();
                break;
            case 3:
                GenerarPatronAlterno();
                break;
            default:
                Debug.LogError("⚠ Nivel no válido, generando patrón básico.");
                GenerarPatronBasico();
                break;
        }

        // Actualizar contador de bloques en el GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ContarBloques();
        }
    }

    private void GenerarPatronBasico()
    {
        Debug.Log("Generando patrón básico");


        float desplazamientoY = 0 - 90;
        for (int fila = 0; fila < filas + 4; fila++)
        {
            float desplazamientoX = 0 + 5;
            for (int columna = 0; columna < columnas; columna++)
            {
                SpawnBloque(fila + desplazamientoX, columna + desplazamientoY);
                desplazamientoX += 9.6f;
            }
            desplazamientoY += 25.6f;
        }
        GameManager.EncontrarBricks();
    }

    private void GenerarPatronEscalonado()
    {
        float desplazamientoY = 0f - 90f;
        Debug.Log("🟠 Generando patrón escalonado");

        float ultimoDesplazamientoX = 0;
        float ultimoDesplazamientoY = 0;
        float desplazamientoX = 0;
        // 🔻 PRIMER FOR: Orden descendente
        for (int fila = 0; fila < filas; fila++)
        {
            desplazamientoX = 0 + 5;
            for (int columna = 0; columna < columnas - fila; columna++)
            {
                SpawnBloque(fila + desplazamientoX, columna + desplazamientoY);
                desplazamientoX += 9.6f;

                // Guardamos la última posición usada
                ultimoDesplazamientoX = desplazamientoX;
                ultimoDesplazamientoY = desplazamientoY;
            }
            desplazamientoY += 25.6f;
        }

        // 🔺 SEGUNDO FOR: Orden ascendente (empezando en la última posición)
        desplazamientoY = ((float)(ultimoDesplazamientoY + 30)); // Posición después del último bloque
        for (int fila = filas - 1; fila >= 0; fila--) // Ahora en orden ascendente
        {
            desplazamientoX = 5;
            for (int columna = 0; columna < columnas - fila; columna++)
            {
                SpawnBloque(fila + desplazamientoX, columna + desplazamientoY);
                desplazamientoX += 9;
            }
            desplazamientoY += 25;
        }
    }
    private void GenerarPatronAlterno()
    {
        Debug.Log("🔵 Generando patrón alterno de bloques");

        float desplazamientoY = -100f; // Ajuste inicial en Y

        filas += 5;
        columnas += 5;


        for (int fila = 0; fila < filas; fila++)
        {
            float desplazamientoX = 5f; // Ajuste inicial en X

            for (int columna = 0; columna < columnas; columna++)
            {
                // Solo generamos un bloque en posiciones alternas
                if ((fila + columna) % 2 == 0)
                {
                    SpawnBloque(fila + desplazamientoX, columna + desplazamientoY);
                }

                desplazamientoX += 9.6f; // Espaciado entre bloques
            }
            desplazamientoY += 25.6f; // Espaciado entre filas
        }

    }

    private void SpawnBloque(float fila, float columna)
    {
        if (bloquesPrefabs.Count == 0)
        {
            Debug.LogError("⚠ No hay bloques en la lista de prefabs.");
            return;
        }

        int index = Random.Range(0, bloquesPrefabs.Count);
        GameObject prefabSeleccionado = bloquesPrefabs[index];

        Vector2 posicion = new Vector2(columna, fila);
        GameObject bloqueInstanciado = Instantiate(prefabSeleccionado, posicion, Quaternion.identity);

        // Si el bloque es Purple, le añadimos el script de detección

    }

    public void GenerarBooster(Vector2 posicion)
    {
        if (boosterPrefab != null)
        {
            GameObject booster = Instantiate(boosterPrefab, posicion, Quaternion.identity);
            booster.SetActive(true);
        }
    }
}
