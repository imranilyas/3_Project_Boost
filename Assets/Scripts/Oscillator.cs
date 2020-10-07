using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [Range(-1,1)][SerializeField] float movementFactor;

    [SerializeField] float period = 2f;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } //failsafe so no compiler errors when period = 0

        const float tau = Mathf.PI * 2f; //about 6.28
        float cycles = Time.time / period;
        float rawSine = Mathf.Sin(cycles * tau);

        movementFactor = rawSine; //oscillates between -1 and 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
