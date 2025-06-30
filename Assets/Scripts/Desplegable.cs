 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desplegable : MonoBehaviour
{
    [SerializeField] Camera _cameraPos;
    [SerializeField] Transform _followObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cameraPos.transform.position += _followObject.position;
        _cameraPos.transform.position = new Vector3(_followObject.position.x, 0, _cameraPos.transform.position.z);
    }
}
