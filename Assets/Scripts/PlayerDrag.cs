using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody2D _objectRb;
    [SerializeField] private float _lerpSpeed = 5f;
    private bool _isDragging = false;
    [SerializeField] Transform _drinkPos;

    private Vector2 _velocity = Vector2.zero;

    private Vector2 _worldPosition;

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
        _objectRb.gravityScale = 0;
        _objectRb.isKinematic = true;
        Debug.Log("Click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetDrinkPosition();
        _isDragging = false;
        _objectRb.gravityScale = 1;
        _objectRb.isKinematic = false;
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
}
