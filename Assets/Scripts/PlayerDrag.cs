using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody2D _objectRb;
    [SerializeField] private float _lerpSpeed = 5f;
    private bool _isDragging = false;

    private Vector2 _worldPosition;

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
        //float z = Camera.main.WorldToScreenPoint(eventData.position).y;
        //Vector2 pointPosition = new Vector2(eventData.position.x, z);
        //_worldPosition = Camera.main.ScreenToWorldPoint(pointPosition);

        _worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0f));

        Debug.Log("Dragging");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        //_objectRb.useGravity = false;
        _objectRb.gravityScale = 0;
        _objectRb.isKinematic = true;
        Debug.Log("Click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        //_objectRb.useGravity = true;
        _objectRb.gravityScale = 1;
        _objectRb.isKinematic = false;
        Debug.Log("Soltar Click");
    }

    private void FixedUpdate()
    {
        if (_isDragging)
        {
            Vector2 newPosition = Vector2.Lerp(transform.position, _worldPosition, Time.fixedDeltaTime * _lerpSpeed);
            _objectRb.MovePosition(newPosition);
        }
    }
}
