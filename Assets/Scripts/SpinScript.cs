using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public Vector3 spin;
    public bool paused;

    private Quaternion convertedSpin;

    private void Start()
    {
        convertedSpin = Quaternion.Euler(spin);
    }

    // Update is called once per frame
    /*
    void FixedUpdate()
    {
        if (!paused)
        {
            transform.localRotation *= convertedSpin;
        }
    }
    */

    public void ManualSpin()
    {
        transform.localRotation *= (convertedSpin);
    }
}
