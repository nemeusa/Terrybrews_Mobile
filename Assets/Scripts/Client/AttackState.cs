using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    FSM<TypeFSM> _fsm;
    ClientOG _client;
    
    public AttackState(FSM<TypeFSM> fsm, ClientOG client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {
        _client.player._score -= 50;
       // _client.player._cordura -= 30;
    }

    public void OnUpdate()
    {
        _client.GetComponent<MeshRenderer>().material.color = Color.red;
        var dir = _client.player.transform.position - _client.transform.position;
        _client.transform.forward = dir;
        _client.transform.position += dir * _client.speed * Time.deltaTime;
        //_client.transform.Translate(Vector3.forward * _client.exitSpeed * Time.deltaTime);
        _client.StartCoroutine(_client.IsDestroy());
    }

    public void OnExit()
    {
    }
}
