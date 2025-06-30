using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public bool isOcupped{ get; private set; }
    [HideInInspector]
    public Transform seatPosition;

    private void Start()
    {
        seatPosition = this.transform;
    }

    public void Ocuppy()
    {
        if (!isOcupped)
        {
            isOcupped = true;
           // Debug.Log("Ocupado");
            GetComponent<Renderer>().material.color = Color.red;
        }
    } 
    public void Free()
    {
            isOcupped = false;
           // Debug.Log("Libre");
            GetComponent<Renderer>().material.color = Color.green;
    }
}
