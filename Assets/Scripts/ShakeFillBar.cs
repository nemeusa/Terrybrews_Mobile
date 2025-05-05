using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeFillBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    private float _currentFillAmount = 0;
    [SerializeField] private float _maxFillAmount = 1000f, _fillPerShake = 2.5f;

    [SerializeField, Range(1.1f, 3f)] private float _shakeForce = 2f;

    void Update()
    {
        ChargeImage();
    }

    private void ChargeImage()
    {
        if (Input.acceleration.sqrMagnitude > _shakeForce)
        {
            _currentFillAmount = Mathf.Clamp(_currentFillAmount + _fillPerShake, 0, _maxFillAmount);
        }

        _bar.fillAmount = _currentFillAmount / _maxFillAmount;
    }
}
