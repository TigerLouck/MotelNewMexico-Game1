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
		thisRB.centerOfMass = new Vector3(0, -.75f, -.1f);
	}
	// Update is called once per frame
	void Update()
	{
		// Move the wheels
		
		foreach (WheelCollider wheel in wheels)
		{
			// Simulation
			if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
			{
				wheel.motorTorque = 2500;
				// get meters per second, then use as dividend over maximum speed, then fraction of max torque
				wheel.brakeTorque = 2500 * ((Mathf.Abs(wheel.rpm) * wheel.radius * Mathf.PI / 60)/speedCap);
				// At maximum speed, brake torque equals motor torque, at greater than max speed, brake torque exceeds it
			}
			else
			{
				wheel.motorTorque = 0;
				wheel.brakeTorque = 700;
			}

			//Visuals
			Vector3 outPos;
			Quaternion outRot;
			wheel.GetWorldPose(out outPos, out outRot); //fucking dumb architecture
			wheel.transform.GetChild(0).position = outPos;
			wheel.transform.GetChild(0).rotation = outRot;

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
