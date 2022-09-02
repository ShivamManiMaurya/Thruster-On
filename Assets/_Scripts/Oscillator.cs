using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;

    private Vector3 startingPosition;
    private float movementFactor;

    void Start()
    {   
        startingPosition = transform.position;
    }

    
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        float cycle = Time.time / period;           // It increases with time
        const float TOU = Mathf.PI * 2f;            // Contant value of 6.283
        float rawSinWave = Mathf.Sin(cycle * TOU);  // going from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f;    // recalculating so it goes from 0 to 1 so its more cleaner

        Vector3 offSet = movementVector * movementFactor;
        transform.position = startingPosition + offSet;
    }
}
