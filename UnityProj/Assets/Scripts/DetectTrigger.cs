﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
