using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Drink : MonoBehaviour
{

    Rigidbody2D _rb;
    [SerializeField] Transform _drinkPos;


    [Header("deliver drink")]
    public DrinkType drinkType;
    private bool _isOverClient = false;
    private GameObject _clienteActual = null;
    public Client _client;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetDrinkPosition();
    }
    public void ResetDrinkPosition()
    {
        transform.position = _drinkPos.position;
        _rb.velocity = Vector2.zero;
    }

    public void GetDrink()
    {
        if (_clienteActual != null)
        {
            Drink drink = GetComponent<Drink>();
            _client.ReceiveDrink(drink);
            Debug.Log("¡Bebida entregada al cliente!");

            Destroy(gameObject);
        }
        else
        {
            ResetDrinkPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Client client = other.GetComponent<Client>();
        if (client != null)
        {
            _clienteActual = client.gameObject;
            _isOverClient = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Client>() != null && _clienteActual != null)
        {
            _isOverClient = false;
            _clienteActual = null;
        }
    }
}
