using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [Header("move object")]
    [SerializeField] private float _lerpSpeed = 5f;
   // [SerializeField] Transform _drinkPos;
    private Rigidbody2D _objectRb;
    private bool _isDragging = false;
    private Vector2 _velocity = Vector2.zero;
    private Vector2 _worldPosition;



    //[Header("deliver drink")]
    //private bool _isOverClient = false;
    //private GameObject _clienteActual = null;
    //public Client _client;
    [SerializeField] Drink _drink;


    private void Awake()
    {
        _objectRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _objectRb.freezeRotation = true;
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
        _drink.GetDrink();
        _drink.ResetDrinkPosition();
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
}
