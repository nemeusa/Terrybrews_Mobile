using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pump : MonoBehaviour
{
    //[SerializeField] Camera cam;

    //void Update()
    //{
    //    Vector3 mousePos = Input.mousePosition;

    //    mousePos = new Vector3(Screen.width / 2, Screen.height / 2, mousePos.z);


    //    Ray ray = cam.ScreenPointToRay(mousePos);

    //    Plane plane = new Plane(Vector3.up, transform.position);
    //    float dist;

    //    if (plane.Raycast(ray, out dist))
    //    {
    //        Vector3 puntoEnPlano = ray.GetPoint(dist);

    //        Vector3 direccion = puntoEnPlano - transform.position;

    //        Quaternion rotacion = Quaternion.LookRotation(direccion);

    //        float rotacionY = rotacion.eulerAngles.y;

    //        rotacionY = Mathf.Clamp(rotacionY, -25, 25);

    //        transform.rotation = Quaternion.Euler(0, rotacionY, 0);
    //    }
    //}
}
