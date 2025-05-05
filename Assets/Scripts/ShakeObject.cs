using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    public float shakeThreshold = 2.0f;   // sensibilidad
    public float moveAmount = 0.2f;       // amplitud del movimiento
    public float moveSpeed = 5.0f;        // velocidad del movimiento oscilatorio

    private Vector3 originalPosition;
    private bool isShaking = false;
    private float shakeTimer = 0f;
    private float shakeCooldown = 0.3f;   // tiempo sin agitar para detener el movimiento

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Detecta si hay agitación
        if (Input.acceleration.sqrMagnitude > shakeThreshold * shakeThreshold)
        {
            isShaking = true;
            shakeTimer = 0f; // resetea el temporizador
        }
        else if (isShaking)
        {
            // Espera un tiempo antes de detener el movimiento
            shakeTimer += Time.deltaTime;
            if (shakeTimer > shakeCooldown)
            {
                isShaking = false;
                transform.position = originalPosition;
            }
        }

        // Movimiento vertical suave
        if (isShaking)
        {
            float yOffset = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
            transform.position = originalPosition + new Vector3(0, yOffset, 0);
        }
    }
}
