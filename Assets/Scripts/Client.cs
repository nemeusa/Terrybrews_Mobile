using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] List<Sprite> listaSprites;
    private SpriteRenderer spriteRenderer;
    private static List<Sprite> spritesEnUso = new List<Sprite>();
    private Sprite spriteElegido;


    [Header("Comportamiento")]
    [SerializeField] float _speed;
    [SerializeField] float _exitSpeed;
    [SerializeField] float _orderingTime;
    private float _orderTimer;
    Transform _servePoint;
    //[SerializeField] Transform _enterPoint;
    //[SerializeField] GameObject pedidoTexto;
    float _intoExit;
    public bool imposter;
    bool _isEnter;
    bool _served;
    public bool _isOrdering;
    [SerializeField] bool quieto;
    bool _goodOrder;
    [HideInInspector]


    [Header("Drink")]
    public DrinkType wishDrink;

    Vector2 dir;
    Vector3 dir3;


    public Color water = Color.red;
    public Color coke = Color.yellow;   
    
    public Color _happy = Color.green;
    public Color _sad = Color.red;

    private void Start()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();   
        
        if (listaSprites != null && listaSprites.Count > 0)
        {
            List<Sprite> disponibles = new List<Sprite>(listaSprites);
            disponibles.RemoveAll(sprite => spritesEnUso.Contains(sprite));

            if (disponibles.Count > 0)
            {
                spriteElegido = disponibles[Random.Range(0, disponibles.Count)];
                spriteRenderer.sprite = spriteElegido;
                spritesEnUso.Add(spriteElegido);
            }
            else
            {
                Debug.LogWarning("No hay sprites disponibles para el nuevo cliente. Se destruirá.");
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            Debug.LogWarning("No hay sprites definidos para el cliente.");
        }

        _servePoint = GameObject.Find("Serve point").transform;

        if (_servePoint == null)
        {
            Debug.LogError("Serve point no encontrado en " + name);
        }

        _intoExit = Random.Range(0, 2) == 0 ? -1 : 1;
        _isEnter = true;

        //if (_intoExit == -1)
        //{
        //    imposter = true;
        //    GetComponent<Renderer>().material.color = water;
        //    // Debug.Log("Impostor");
        //}
        //else
        //{
        //    imposter = false;
        //    // Debug.Log("tipo bueno");
        //    ColorXD();
        //}
        wishDrink = RandomBeverage();
        Debug.Log("El cliente quiere: " + wishDrink);

     


    }

    void Update()
    {
        if (!quieto) ClientMove();
        else
        {
            transform.position = Vector3.zero;
        }
        Destroy(gameObject, 15);
    }

    private DrinkType RandomBeverage()
    {
        DrinkType[] tipos = (DrinkType[])System.Enum.GetValues(typeof(DrinkType));

        int indice = Random.Range(0, tipos.Length);
        return tipos[indice];
    }

    void ColorDrink()
    { 
        if(wishDrink == DrinkType.Coca) GetComponent<Renderer>().material.color = coke;

        else if(wishDrink == DrinkType.Water) GetComponent<Renderer>().material.color = water;
    
    }

    public void ReceiveDrink(Drink bebida)
    {
        if (bebida.drinkType == wishDrink)
        {
            _goodOrder = true;
            GetComponent<Renderer>().material.color = _happy;
            int puntosSuma = bebida.drinkType == DrinkType.Coca ? 100 : 50;
            AddPoints(puntosSuma);

            Debug.Log("Cliente feliz: bebida correcta. +" + puntosSuma + " puntos");
        }
        else
        {
            _goodOrder = true;
            GetComponent<Renderer>().material.color = _sad;

            RestarPuntos(50);
            RestarTiempo(5f); // restar 5 segundos

            Debug.Log("Cliente enojado: quería " + wishDrink + ". -50 puntos y -5 seg");
        }

    }

    void AddPoints(int cantidad)
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        puntos += cantidad;
        PlayerPrefs.SetInt("Points", puntos);
        PlayerPrefs.Save(); 

    }

    void RestarPuntos(int cantidad)
    {
        int puntos = PlayerPrefs.GetInt("Points", 0);
        puntos = Mathf.Max(0, puntos - cantidad);
        PlayerPrefs.SetInt("Points", puntos);
        PlayerPrefs.Save();
    }

    void RestarTiempo(float segundos)
    {
        GameTimer timer = FindObjectOfType<GameTimer>();
        if (timer != null)
        {
            timer.RestarTiempo(segundos);
        }
    }
    void ClientMove()
    {
        if (_isEnter)
        {
            dir = (_servePoint.position - transform.position).normalized;
            dir3 = new Vector3(dir.x, dir3.y, 0);
            transform.position += dir3 * _speed * Time.deltaTime;
        }
        if (Mathf.Abs(dir.x) < 0.1f && !_served && !_goodOrder)
        {
            _isEnter = false;
            ColorDrink();
            _isOrdering = true;
            _orderTimer += 0.1f * Time.deltaTime;
        }
        if (_orderTimer >= _orderingTime && !_isEnter || _goodOrder)
        {
            _isOrdering = false;
            _served = true;
            dir = _servePoint.transform.position + transform.position;
            dir3 = new Vector3(dir.x, dir3.y, 0);
            transform.position += dir3 * _exitSpeed * Time.deltaTime;
        }
    }
    private void OnDestroy()
    {
        if (spriteElegido != null && spritesEnUso.Contains(spriteElegido))
        {
            spritesEnUso.Remove(spriteElegido);
        }
    }
}
