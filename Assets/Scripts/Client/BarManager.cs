using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    [SerializeField] GameObject clientGoodPrefab;
    GameObject _currentClientPrefab;
    public List<Chair> allChairs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnPointRight;
    [SerializeField] Transform _altureChair;
    Vector3 _spawn;

    int clientsCounter;
    private float _randomEnter;

    [SerializeField] Player _player;

    //public GameManager gameManager;

    //private string currentRequest;
    //public TMP_Text requestText;



    private void Start()
    {
        _randomEnter = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(SpawnRoutine());
        _currentClientPrefab = clientGoodPrefab;
        //NuevaPeticion();
    }

    private void Update()
    {
        if (clientsCounter == 3) _currentClientPrefab = clientPrefab;
           
    }

    public void TrySpawnClient()
    {
        Chair freeChair = allChairs.FirstOrDefault(c => !c.isOcupped);
        //Vector3 spawn = new Vector3(spawnPoint.position.x * _randomEnter, _altureChair.position.y, spawnPoint.position.z);

        //if(Random.Range(0, 100) <= 50)
        _spawn = new Vector3(spawnPoint.position.x, _altureChair.position.y, spawnPoint.position.z);
        //else
        //_spawn = new Vector3(spawnPointRight.position.x, _altureChair.position.y, spawnPointRight.position.z);

        if (freeChair != null)
        {

            clientsCounter++;
           // Debug.Log(clientsCounter);
            GameObject clientObj = Instantiate(_currentClientPrefab, _spawn, Quaternion.identity);
            ClientOG client = clientObj.GetComponent<ClientOG>();
            client.AssignChair(freeChair);
            client.player = _player;
            _player._client = client;
        }
        else
        {
            //Debug.Log("No hay sillas libres, no spawnea el cliente.");
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 4f));
            TrySpawnClient();
        }
    }

}
