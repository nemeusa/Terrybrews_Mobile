using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
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
    public DrinkType wishDrink;

    Vector2 dir;
    Vector3 dir3;


    public Color water = Color.red;
    public Color coke = Color.yellow;

    private void Start()
    {
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
        wishDrink = ElegirBebidaAleatoria();
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

    private DrinkType ElegirBebidaAleatoria()
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
            Debug.Log("Cliente feliz: bebida correcta");
            _goodOrder = true;
        }
        else
        {
            Debug.Log("Cliente enojado: quería " + wishDrink);
        }

    }

    void ClientMove()
    {
        if (_isEnter)
        {
           // Debug.Log("ENTRA");
            dir = (_servePoint.position - transform.position).normalized;
            dir3 = new Vector3(dir.x, dir3.y, 0);
            //Debug.Log(dir.magnitude);
            transform.position += dir3 * _speed * Time.deltaTime;
        }
        if (Mathf.Abs(dir.x) < 0.1f && !_served)
        {
            _isEnter = false;
            ColorDrink();
            //pedidoTexto.SetActive(true);
            _isOrdering = true;
           // Debug.Log("pidiendo");
            _orderTimer += 0.1f * Time.deltaTime;
        }
        if (_orderTimer >= _orderingTime && !_isEnter || _goodOrder)
        {
            //pedidoTexto.SetActive(false);
           // Debug.Log("se va");
            _isOrdering = false;
            _served = true;
            dir = _servePoint.transform.position + transform.position;
            dir3 = new Vector3(dir.x, dir3.y, 0);
            //Debug.Log(dir.magnitude);
            transform.position += dir3 * _exitSpeed * Time.deltaTime;
        }
    }
}
