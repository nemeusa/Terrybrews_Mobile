using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float _orderingTime;
    [SerializeField] float _orderTimer;

    void Start()
    {
        
    }

    void Update()
    {
        _orderTimer += 0.1f * Time.deltaTime;

        if (_orderTimer >= _orderingTime)
        {
            Instantiate(clientPrefab, spawnPoint);
            _orderTimer = 0;
        }
    }
}
