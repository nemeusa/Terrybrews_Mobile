using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Vector2 _moveDir;
    public abstract Vector2 GetMovementInput();
}
