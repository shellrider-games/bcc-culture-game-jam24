using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ActivateWobbleOnHover : MonoBehaviour
{
    [SerializeField] private float stateChangeSpeed = 0.5f;
    
    private Material _material;
    private float _currentWobbleTarget;
    private float _currentWobble;
    private float _startWobbleValue;
    private float _timeLeft;
    
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _currentWobbleTarget = 0;
        _currentWobble = _currentWobbleTarget;
        _material.SetFloat("_Wobble", _currentWobble);
    }

    public void StartWobble()
    {
        _currentWobbleTarget = 1f;
        _startWobbleValue = _currentWobble;
        _timeLeft = (1f - _currentWobble) * stateChangeSpeed;
    }

    public void EndWobble()
    {
        _currentWobbleTarget = 0f;
        _startWobbleValue = _currentWobble;
        _timeLeft = _currentWobble * stateChangeSpeed;
    }

    private void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft = Mathf.Max(0,_timeLeft-Time.deltaTime);
            _currentWobble = Mathf.Lerp(_startWobbleValue, _currentWobbleTarget, 1 - _timeLeft / stateChangeSpeed);
            _material.SetFloat("_Wobble", _currentWobble);
        }
    }
}
