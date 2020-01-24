using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	public Transform cameraPivot;
	public WheelCollider[] wheels;
	public WheelCollider[] steeringWheels;
	public float speedCap;
	Rigidbody thisRB;
	void Start()
	{
		thisRB = GetComponent<Rigidbody>();
		thisRB.centerOfMass = new Vector3(0, -1, 0);
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
				// get meters per second, then use as dividend over maximum speed, then fraction of max torque
				wheel.brakeTorque = 2000 * ((Mathf.Abs(wheel.rpm) * wheel.radius * Mathf.PI / 60)/speedCap);
			}
			else
			{
				wheel.motorTorque = 0;
				wheel.brakeTorque = 1000;
			}
		}

		//steer
		float steer;
		if (cameraPivot.localEulerAngles.y > 180)//stupid euler bullshit, god 
		{
			steer = Mathf.Clamp(cameraPivot.localEulerAngles.y, 310, 360); // 310, 360
		}
		else
		{
			steer = Mathf.Clamp(cameraPivot.localEulerAngles.y, 0, 50); // 0, 50
		}
		foreach (WheelCollider wheel in steeringWheels)
		{
			wheel.steerAngle = steer;
		}
	}
}
