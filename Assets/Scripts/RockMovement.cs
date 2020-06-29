using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    [Range(0, 1)]
    [SerializeField] float movementFactor;


    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / (period);
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(tau * cycle + 3 * Mathf.PI / 2);
        movementFactor = rawSinWave / 2 + 0.5f;
        //movementFactor = Mathf.Abs(rawSinWave); This one does not give a smooth movement
        //movementFactor = Mathf.Abs(Mathf.Sin(Time.time));
        Vector3 offset = movementFactor * movementVector;


        transform.position = startingPos + offset;
        
    
}}
