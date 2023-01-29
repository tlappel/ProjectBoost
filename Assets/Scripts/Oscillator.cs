using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField]Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor =2;
    [SerializeField]float period = 10f;
    const float tau = Mathf.PI * 2;
    float cycles = 0;
    float rawSinWave;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        cycles = Time.time / period;
        rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor=(rawSinWave + 1f)/2f;
        Vector3 offset =  movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
