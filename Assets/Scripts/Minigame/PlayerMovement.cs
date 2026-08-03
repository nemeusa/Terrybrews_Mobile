using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    private Rigidbody2D PlayerRb;
    private Vector2 MoveInput;
    public bool DontMove = false;
    [SerializeField] Vector2 ReboundSpeed;

    [SerializeField] Controller _controller;

    [SerializeField] Animator PlayerAnimator;




    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        //PlayerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {

        //PlayerAnimator.SetFloat("Speed", _controller.GetMovementInput().sqrMagnitude);


        //if (_controller.GetMovementInput().x < 0)
        if (_controller.GetMovementInput().y < -0.05 && transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            transform.localScale = new Vector3(transform.localScale.x, -0.25f, transform.localScale.z);
        }

       //else if (_controller.GetMovementInput().x > 0)
        else if (_controller.GetMovementInput().y > 0.05 && transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.25f, transform.localScale.z);
        }

        else if (_controller.GetMovementInput().y > 0.05 && _controller.GetMovementInput().y < -0.05) transform.localScale = new Vector3(transform.localScale.x, 0.25f, transform.localScale.z);


        if (_controller.GetMovementInput().x < -0.1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        //else if (_controller.GetMovementInput().x > 0)
        else if (_controller.GetMovementInput().x > 0.1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        else transform.rotation = Quaternion.Euler(0, 0, 0);

    }


    private void FixedUpdate()
    {
        if (!DontMove)
        {
            //PlayerRb.MovePosition(PlayerRb.position + MoveInput * speed * Time.fixedDeltaTime);
            PlayerRb.MovePosition(PlayerRb.position + _controller.GetMovementInput().normalized * speed * Time.fixedDeltaTime);
        }
        
    }

    public void Reboud (Vector2 HitPoint)
    {
       PlayerRb.velocity = new Vector2(ReboundSpeed.x * HitPoint.x, ReboundSpeed.y);
    }

}
