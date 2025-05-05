using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawn : MonoBehaviour
{
    [SerializeField] private GameObject clientePrefab;
    [SerializeField] private Transform puntoDeSpawn;
    [SerializeField] private float tiempoEntreClientes = 5f;

    private float _timer;

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
        Instantiate(clientePrefab, puntoDeSpawn.position, Quaternion.identity);
    }
}
