using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    public GameObject tutoVaso;
    public GameObject tutoClient;
    public GameObject tutoGenerator;
    public GameObject tutoBatidoraUno;
    public GameObject tutoBatidoraDos;
    public GameObject tutoFinish;

    public PlayerDrag glassCode;

    [SerializeField] Generator generator;
    [SerializeField] Generator generatorDos;
    [SerializeField] BatidoraTrigger codeBatidora;
    [SerializeField] ShakeFillBar codeBatidoraFinish;

    bool pasoTres;

    private void Start()
    {
        tutoVaso.SetActive(true);
        tutoClient.SetActive(false);
        tutoGenerator.SetActive(false);
        tutoBatidoraUno.SetActive(false);
        tutoBatidoraDos.SetActive(false);
        tutoFinish.SetActive(false);
    }

    private void Update()
    {
        if (glassCode != null)
        {
            if (glassCode._isDragging)
            {
                tutoVaso.SetActive(false);
                tutoClient.SetActive(true);
            }

            if (glassCode._drink.bebidaEntregada)
            {
                tutoClient.SetActive(false);

                generator.Broken();

                tutoGenerator.SetActive(true);

                pasoTres = true;

                Destroy(glassCode.gameObject);
            }
        }

        if (!generator.broken && pasoTres)
        {
            tutoGenerator.SetActive(false);

            generator.SpawnDrinks();
            generatorDos.SpawnDrinks();

            tutoBatidoraUno.SetActive(true);

            pasoTres = false;
        }

        if (codeBatidora.isBatiendo)
        {
            tutoBatidoraUno.SetActive(false);

            tutoBatidoraDos.SetActive(true);
        }

        if (codeBatidoraFinish.barFilled)
        {
            tutoBatidoraDos.SetActive(false);



        }



    }
}