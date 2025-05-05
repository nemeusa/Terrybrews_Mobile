using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDrink : MonoBehaviour
{
    [SerializeField] private GameObject _drinkPref;
    [SerializeField] private ShakeFillBar _shakeFillBar;
    [SerializeField] private Transform _spawnPoint;

    private void Update()
    {
        if(_shakeFillBar.barFilled)
        {
            Debug.Log("Driiiinkkkk");
            Instantiate(_drinkPref, _spawnPoint);
            _shakeFillBar.barFilled = false;
        }
    }
}
