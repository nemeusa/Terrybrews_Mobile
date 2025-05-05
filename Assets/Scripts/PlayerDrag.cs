using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [Header("move object")]
    [SerializeField] private float _lerpSpeed = 5f;
    [SerializeField] Transform _drinkPos;
    private Rigidbody2D _objectRb;
    private bool _isDragging = false;
    private Vector2 _velocity = Vector2.zero;
    private Vector2 _worldPosition;



    [Header("deliver drink")]
    private bool _isOverClient = false;
    private GameObject _clienteActual = null;
    public Client _client;


    private void Awake()
    {
        _objectRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _objectRb.freezeRotation = true;
        ResetDrinkPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseScreenPosition = new Vector3(
        eventData.position.x,
        eventData.position.y,
        Mathf.Abs(Camera.main.transform.position.z - transform.position.z));

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        _worldPosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
        Debug.Log("Dragging");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 mouseScreenPosition = new Vector3(eventData.position.x, eventData.position.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z));

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        _worldPosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        _isDragging = true;
        //_objectRb.gravityScale = 0;
        //_objectRb.isKinematic = true;
        Debug.Log("Click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetDrink();
        ResetDrinkPosition();
        _isDragging = false;
        //_objectRb.gravityScale = 1;
        //_objectRb.isKinematic = false;
        Debug.Log("Soltar Click");
    }

    private void FixedUpdate()
    {
        if (_isDragging)
        {
            Vector2 newPosition = Vector2.SmoothDamp(transform.position, _worldPosition, ref _velocity, _lerpSpeed);
            _objectRb.MovePosition(newPosition);
        }
    }

    private void ResetDrinkPosition()
    {
        transform.position = _drinkPos.position;
        _objectRb.velocity = Vector2.zero;
    }

    void GetDrink()
    {
        if (_clienteActual != null)
        {
            _client.ReceiveDrink();
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
