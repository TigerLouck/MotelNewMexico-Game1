using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	public Transform cameraPivot;
	public WheelCollider[] wheels;
	public WheelCollider[] steeringWheels;
	Rigidbody thisRB;
	// Start is called before the first frame update
	void Start()
	{
		thisRB = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		// Move the wheels
		
		foreach (WheelCollider wheel in wheels)
		{
			if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
			{
				wheel.motorTorque = 2000;
				wheel.brakeTorque = Mathf.Abs(wheel.rpm) / .01f;
			}
			else
			{
				wheel.motorTorque = 0;
				wheel.brakeTorque = 100000;
			}
		}

		//steer
		float steer;
		if (cameraPivot.localEulerAngles.y > 180)//stupid euler bullshit, god 
		{
			steer = Mathf.Clamp(cameraPivot.localEulerAngles.y, 310, 36000); // 310, 360
		}
		else
		{
			steer = Mathf.Clamp(cameraPivot.localEulerAngles.y, 0, 2000); // 0, 50
		}
		foreach (WheelCollider wheel in steeringWheels)
		{
			wheel.steerAngle = steer;
		}
	}
}
