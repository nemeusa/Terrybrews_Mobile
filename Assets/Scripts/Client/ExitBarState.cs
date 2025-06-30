
using System.Collections;
using UnityEngine;

public class ExitBarState : State
{
    FSM<TypeFSM> _fsm;
    ClientOG _client;
    Vector3 _dir;

    public ExitBarState(FSM<TypeFSM> fsm, ClientOG client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {
        _dir = (Random.Range(0, 2) == 0) ? Vector3.left : Vector3.right;
       // _client.visor.GetComponent<MeshRenderer>().material.color = Color.green;
        _client.player._score += 100;
        //_client.player._cordura += 5;
    }

    public void OnUpdate()
    {
       // Debug.Log("Exit");
        _client.GetComponent<MeshRenderer>().material.color = Color.green;
        //var dir = _client.chair.transform.position + _client.transform.position;
        _client.transform.forward = _dir;
        _client.transform.position += _dir * _client.exitSpeed * Time.deltaTime;
        _client.StartCoroutine(_client.IsDestroy());
    }

    public void OnExit()
    {
    }
}
