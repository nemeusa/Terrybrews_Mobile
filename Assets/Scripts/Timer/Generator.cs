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
    public Color actualColor;

    [Header("Sprites")]
    public Sprite funciona;
    public Sprite roto;
    private SpriteRenderer spriteRenderer;

    public bool broken = false;
    public int clickCounter = 0;

    [Header("Lista de Objetos")]
    private List<GameObject> objetosGenerados = new List<GameObject>();

    [Header("Unity Cloud")]
    public bool ndGenerator = false;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = funciona;
        StartCoroutine(SpawnObjects());
        Upgrades();

        if (!(ndGenerator = false))
        {
            gameObject.SetActive(true);
        }

    }
    void Upgrades()
    {
        // Nivel de mejoras
        int levelBreak = PlayerPrefs.GetInt("Upgrade_BreakChanceLevel", 0);
        int levelMaxObjects = PlayerPrefs.GetInt("Upgrade_MaxObjectsLevel", 0);
        int levelGenTime = PlayerPrefs.GetInt("Upgrade_GenTimeLevel", 0);
        int levelRepair = PlayerPrefs.GetInt("Upgrade_RepairClickLevel", 0);

        // Aplicar reducción de chance de rotura
        breakChance = Mathf.Max(0.05f, breakChance - (levelBreak * 0.05f));
        // Aumentar límite de objetos (de 3 hasta 10)
        maxObjectsInScene = Mathf.Clamp(3 + levelMaxObjects, 3, 10);

        // Reducir tiempo de generación (de 10 a 5)
        tiempoMin = Mathf.Clamp(10 - levelGenTime, 2,3);
        tiempoMax = Mathf.Clamp(10 - levelGenTime, 2, 10);

        // Reducir clics requeridos para reparar
        clicsParaReparar = Mathf.Clamp(10 - levelRepair, 2, 10);
    }
    public void ActivarBurst()
    {
        if (broken) return;

        for (int i = 0; i < 10; i++)
        {
            GameObject prefab = (Random.value < 0.5f) ? spawnedObject : spawnedObject2;
            GameObject nuevoObjeto = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            objetosGenerados.Add(nuevoObjeto);
        }

        broken = true;
        clickCounter = 0;
        spriteRenderer.sprite = roto;
        GetComponent<Renderer>().material.color = newColor;
        Debug.Log("¡Burst activado! Generador roto.");
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
                    spriteRenderer.sprite = roto;
                    AudioManager.Instance.PlaySFXClip(0);
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
            if (clickCounter >= clicsParaReparar)
            {
                actualColor = originalColor;
                spriteRenderer.sprite = funciona;
                broken = false;
                AudioManager.Instance.PlaySFXClip(1);
                Debug.Log("¡Generador reparado!");
            }

            else
            {
                actualColor = newColor;
            }
        }
                StartCoroutine(clickRepare());
    }

    IEnumerator clickRepare()
    {
        GetComponent<Renderer>().material.color = Color.green;
        clickCounter++;

        yield return new WaitForSeconds(0.1f);

        GetComponent<Renderer>().material.color = actualColor;
    }
}
