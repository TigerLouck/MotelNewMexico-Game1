using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleController : MonoBehaviour
{
    ParticleSystem PSystem;
    ParticleSystem.EmissionModule emitter;
    WheelCollider Wheel;
    WheelHit hit;
    // Start is called before the first frame update
    void Start()
    {
        PSystem = GetComponent<ParticleSystem>();
        emitter = PSystem.emission;
        Wheel = transform.parent.GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Wheel.GetGroundHit(out hit))
        {
            emitter.rateOverTime = (Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip)) * 50 - 25;
        } 
        else
        {
            emitter.rateOverTime = 0;
        }
    }
}
