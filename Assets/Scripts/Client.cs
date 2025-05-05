using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _exitSpeed;
    [SerializeField] float _orderingTime;
    private float _orderTimer;
    [SerializeField] Transform _servePoint;
    //[SerializeField] Transform _enterPoint;
    //[SerializeField] GameObject pedidoTexto;
    float _intoExit;
    public bool imposter;
    bool _isEnter;
    bool _served;
    public bool _isOrdering;
    [SerializeField] bool quieto;



    Vector2 dir;
    Vector3 dir3;


    public Color nuevoColor = Color.red;
    public Color buenoColor = Color.yellow;

    private void Start()
    {
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
    }

    void Update()
    {
        if (!quieto) ClientMove();
        else
        {
            transform.position = Vector3.zero;
        }
    }

    void ClientMove()
    {
        if (_isEnter)
        {
            Debug.Log("ENTRA");
            dir = _servePoint.transform.position - transform.position;
            dir3 = new Vector3(dir.x, dir3.y, 0);
            //Debug.Log(dir.magnitude);
            transform.position += dir3 * _speed * Time.deltaTime;
        }
        if (Mathf.Abs(dir.x) < 0.1f && !_served)
        {
            _isEnter = false;
            //pedidoTexto.SetActive(true);
            _isOrdering = true;
            Debug.Log("pidiendo");
            _orderTimer += 0.1f * Time.deltaTime;
        }
        if (_orderTimer >= _orderingTime && !_isEnter)
        {
            //pedidoTexto.SetActive(false);
            Debug.Log("se va");
            _isOrdering = false;
            _served = true;
            ////transform.forward = dirEnter;
            ////transform.position += (dirEnter * _speed * Time.deltaTime);

            ////transform.forward = -dir;
            //transform.position = new Vector3(transform.position.x + _intoExit * (_exitSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            //Debug.Log(transform.position);
            ////Destroy(gameObject, 10);
            ///
            dir = _servePoint.transform.position + transform.position;
            dir3 = new Vector3(dir.x, dir3.y, 0);
            //Debug.Log(dir.magnitude);
            transform.position += dir3 * _exitSpeed * Time.deltaTime;
        }
    }

    public void ReceiveDrink()
    {
        Debug.Log("Cliente recibió la bebida.");
    }

    void entrando()
    {
        Debug.Log("entrando");
        //transform.forward = dir;
        //transform.position += (dir * _speed * Time.deltaTime);
    }


    void ColorXD()
    {
        GetComponent<Renderer>().material.color = buenoColor;
    }
}
