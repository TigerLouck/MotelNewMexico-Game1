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
    float lastGroundedElevation;
    public UnityEngine.UI.Text dedText;
    bool isSlipping;
    public AudioManager audioManager;
    public bool started;

    void Start()
    {
        thisRB = GetComponent<Rigidbody>();
        thisRB.centerOfMass = new Vector3(0, -.75f, -.1f);
        dedText.gameObject.SetActive(false);
        audioManager.PlayEngine();
        isSlipping = false;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the wheels
        foreach (WheelCollider wheel in wheels)
        {
            // Simulation
            if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && started)
            {
                wheel.motorTorque = 2500;
                // get meters per second, then use as dividend over maximum speed, then fraction of max torque
                wheel.brakeTorque = 2500 * ((Mathf.Abs(wheel.rpm) * wheel.radius * Mathf.PI / 60) / speedCap);
                // At maximum speed, brake torque equals motor torque, at greater than max speed, brake torque exceeds it

                // increase the pitch of the engine
                audioManager.IncreaseEnginePitch(wheel.rpm);

            }
            else if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftAlt))
            {
                wheel.motorTorque = -2500;
                // get meters per second, then use as dividend over maximum speed, then fraction of max torque
                wheel.brakeTorque = 2500 * ((Mathf.Abs(wheel.rpm) * wheel.radius * Mathf.PI / 60) / speedCap);
                // At maximum speed, brake torque equals motor torque, at greater than max speed, brake torque exceeds it

                // increase the pitch of the engine
                audioManager.IncreaseEnginePitch(Mathf.Abs(wheel.rpm));
            }
            else
            {
                audioManager.DecreaseEnginePitch();
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
        float camOrient = cameraPivot.localEulerAngles.y;
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftAlt))
        {
            if (camOrient > 180) camOrient -= 180;
            else camOrient += 180;
            camOrient = 360 - camOrient;
        }

        if (camOrient > 180)//stupid euler bullshit, god 
        {
            steer = Mathf.Clamp(camOrient, 310, 360); // 310, 360
        }
        else
        {
            steer = Mathf.Clamp(camOrient, 0, 50); // 0, 50
        }

        WheelHit hit = new WheelHit();
        // average of how much the tires are slipping side to side
        float avgSlip = 0.0f;
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = steer;
            if (wheel.GetGroundHit(out hit))
            {
                //Wheel grounding rider for kill floor
                lastGroundedElevation = hit.point.y;
                avgSlip += hit.sidewaysSlip;
            }
        }

        // screech audio check
        avgSlip /= 4;
        if ((avgSlip > .1f || avgSlip < -.1f) && isSlipping == false)
        {
            audioManager.PlayScreech();
            isSlipping = true;
        }
        if (avgSlip < .1f && avgSlip > -.1f)
        {
            audioManager.StopScreech();
            isSlipping = false;
        }

        //Kill Floor
        if (transform.position.y < lastGroundedElevation - 50)
        {
            dedText.gameObject.SetActive(true);
            StartCoroutine(DieAndRespawn());
        }

    }

    IEnumerator DieAndRespawn()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        GetComponentInChildren<CameraLook>().enabled = false;
    }

    private void OnEnable()
    {
        GetComponentInChildren<CameraLook>().enabled = true;
    }
}
