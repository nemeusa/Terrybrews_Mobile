using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatidoraTrigger : MonoBehaviour
{
    public bool isBatiendo = false;
    private int _drinksAmount = 0, _drinksMaxAmount = 2;
    public string[] drinkNames = new string[2];

    public GameObject batidoraCanvas;

    private void Start()
    {
        batidoraCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_drinksAmount >= _drinksMaxAmount)
        {
            Debug.Log("No se pueden agregar más bebidas");
            return;
        }

        string drinkName = collision.gameObject.name;

        drinkNames[_drinksAmount] = drinkName;
        _drinksAmount++;

        Debug.Log($"Bebida agregada: {drinkName}");
        
        if(_drinksAmount == _drinksMaxAmount)
        {
            isBatiendo = true;
            _drinksAmount = 0;
        }
    }

    private void Update()
    {
        if (isBatiendo)
        {
            Debug.Log("Batiendo");
            batidoraCanvas.SetActive(true);
            isBatiendo = false;
        }
    }
}
