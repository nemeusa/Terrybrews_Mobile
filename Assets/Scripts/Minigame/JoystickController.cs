using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : Controller, IDragHandler, IEndDragHandler
{
    Vector2 _initialPost;
    [SerializeField, Range(75, 150)] float _maxMagnitude = 125;
    
    void Start()
    {
        _initialPost = transform.position;
    }

    public override Vector2 GetMovementInput()
    {
        Vector2 modifiedDir = new Vector2(_moveDir.x, _moveDir.y);
        //Vector2 modifiedDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        modifiedDir /= _maxMagnitude;
        return modifiedDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector2.ClampMagnitude(eventData.position - _initialPost, _maxMagnitude);
        transform.position = _initialPost + _moveDir;  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initialPost;
        _moveDir = Vector2.zero;
    }

}
