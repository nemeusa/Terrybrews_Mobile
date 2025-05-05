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

    public DrinkType wishDrink;

    public DrinkType bebidaEsperada;

    Vector2 dir;
    Vector3 dir3;


    public Color nuevoColor = Color.red;
    public Color buenoColor = Color.yellow;

    private void Start()
    {
        _servePoint = GameObject.Find("Serve point").transform;

        if (_servePoint == null)
        {
            Debug.LogError("Serve point no encontrado en " + name);
        }

        _intoExit = Random.Range(0, 2) == 0 ? -1 : 1;
        _isEnter = true;

        if (_intoExit == -1)
        {
            imposter = true;
            GetComponent<Renderer>().material.color = nuevoColor;
            // Debug.Log("Impostor");
        }
        else
        {
            imposter = false;
            // Debug.Log("tipo bueno");
            ColorXD();
        }
        bebidaEsperada = ElegirBebidaAleatoria();
        Debug.Log("El cliente quiere: " + bebidaEsperada);
    }



    void Update()
    {
        if (!quieto) ClientMove();
        else
        {
            transform.position = Vector3.zero;
        }
    }

    private DrinkType ElegirBebidaAleatoria()
    {
        DrinkType[] tipos = (DrinkType[])System.Enum.GetValues(typeof(DrinkType));
        int indice = Random.Range(0, tipos.Length);
        return tipos[indice];
    }

    public void ReceiveDrink(Drink bebida)
    {
        if (bebida.drinkType == bebidaEsperada)
        {
            Debug.Log("Cliente feliz: bebida correcta");
            _goodOrder = true;
        }
        else
        {
            Debug.Log("Cliente enojado: quería " + bebidaEsperada);
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

    //public void ReceiveDrink(Drink drink)
    //{
        //if (drink.drinkType == wishDrink)
        //{
      //      Debug.Log("Cliente recibió la bebida correcta: " + drink.drinkType);
        //}
        //else
        //{
        //    Debug.Log("¡Bebida incorrecta! Esperaba: " + wishDrink);
      //  }
    //}

    void entrando()
    {
   //     Debug.Log("entrando");
        //transform.forward = dir;
        //transform.position += (dir * _speed * Time.deltaTime);
    }


    void ColorXD()
    {
        GetComponent<Renderer>().material.color = buenoColor;
    }
}
