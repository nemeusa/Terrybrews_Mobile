using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Generator : MonoBehaviour, IPointerClickHandler
{
    [Header("Generación")]
    public GameObject spawnedObject;
    public GameObject spawnedObject2;
    public Transform spawnPoint;
    public float tiempoMin = 3f;
    public float tiempoMax = 5f;
    public int maxObjectsInScene = 10;

    [Header("Rotura y Reparación")]
    [Range(0f, 1f)] public float breakChance = 0.1f;
    public int clicsParaReparar = 10;

    public Color originalColor = Color.white;
    public Color newColor = Color.red;

    public bool broken = false;
    public int clickCounter = 0;

    [Header("Lista de Objetos")]
    private List<GameObject> objetosGenerados = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            LimpiarLista(); 

            if (!broken && objetosGenerados.Count < maxObjectsInScene)
            {
                GameObject prefab = (Random.value < 0.5f) ? spawnedObject : spawnedObject2;
                GameObject nuevoObjeto = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                objetosGenerados.Add(nuevoObjeto);

                if (Random.value < breakChance)
                {
                   
                    clickCounter = 0;
                    Debug.Log("¡El generador se rompió!");
                    GetComponent<Renderer>().material.color = newColor; 
                    broken = true;
                }
            }

            float tiempoEspera = Random.Range(tiempoMin, tiempoMax);
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
    private void LimpiarLista()
    {
        objetosGenerados.RemoveAll(obj => obj == null);
    }

    // Detectar clics
    public void OnPointerClick(PointerEventData eventData)
    {
        if (broken)
        {
            clickCounter++;

           

            if (clickCounter >= clicsParaReparar)
            {
                broken = false;
                Debug.Log("¡Generador reparado!");
                GetComponent<Renderer>().material.color = originalColor;
            }
        }
    }
}
