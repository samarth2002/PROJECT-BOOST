using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 Startingpos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float Period = 2f;
    void Start()
    {
        Startingpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        const float tau = 2*Mathf.PI;
        if(Period<=Mathf.Epsilon){return;}
        float cycles = Time.time/Period;
        float SinWave = Mathf.Sin(cycles*tau);
        movementFactor= (SinWave + 1f)/2f;
        Vector3 offset = movementVector*movementFactor;
        transform.position = Startingpos+offset;
    }
}
