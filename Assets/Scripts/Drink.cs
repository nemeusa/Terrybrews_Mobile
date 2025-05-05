#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
#endif
using UnityEngine;

public class Drink : MonoBehaviour
{

    Rigidbody2D _rb;
    [SerializeField] Transform _drinkPos;


    [Header("deliver drink")]
    public DrinkType drinkType;
    private bool _isOverClient = false;
    private GameObject _clienteActual = null;
    [HideInInspector]
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
        //transform.position = _drinkPos.position;
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
        _client = client;
        if (client != null)
        {
           // Debug.Log("CLIENTE DETECTADO");
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
