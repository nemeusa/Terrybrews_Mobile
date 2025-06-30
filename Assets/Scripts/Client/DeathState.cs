using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    FSM<TypeFSM> _fsm;
    ClientOG _client;

    public DeathState(FSM<TypeFSM> fsm, ClientOG client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {
        _client.GetComponent<MeshRenderer>().material.color = Color.white;
        //_client.bloodPartycles.Play();
    }

    public void OnUpdate()
    {
        var dir = _client.player.transform.position - _client.transform.position;
        _client.transform.forward = dir;
        _client.transform.position += -dir * _client.speed * Time.deltaTime;
        _client.StartCoroutine(_client.IsDestroy());
    }

    public void OnExit()
    {
    }
}
