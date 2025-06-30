using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VIPState : State
{
    FSM<TypeFSM> _fsm;
    ClientOG _client;

    public VIPState (FSM<TypeFSM> fsm, ClientOG client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
