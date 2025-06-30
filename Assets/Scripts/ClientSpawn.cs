using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawn : MonoBehaviour
{
    [SerializeField] private GameObject clientePrefab;
    [SerializeField] private Transform puntoDeSpawn;
    [SerializeField] private float tiempoEntreClientes = 5f;
    [SerializeField] private Transform _chair;
    [SerializeField] private GameTimer _gameTimer;

    private float _timer;

    private void Start()
    {
        SpawnCliente();
    }
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= tiempoEntreClientes)
        {
            SpawnCliente();
            _timer = 0f;
        }
    }

    private void SpawnCliente()
    {
        GameObject clientObj = Instantiate(clientePrefab, puntoDeSpawn.position, Quaternion.identity);
        Client client = clientObj.GetComponent<Client>();
        client._servePoint = _chair;
        client.timer = _gameTimer;
    }
}
