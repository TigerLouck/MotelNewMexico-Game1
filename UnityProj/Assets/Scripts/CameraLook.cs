using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
	float mouseX;
	float mouseY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		mouseX += Input.GetAxisRaw("Mouse X");
		mouseY += Input.GetAxisRaw("Mouse Y");
		Debug.Log(Input.GetAxis("Roll"));

		transform.rotation = transform.rotation * Quaternion.Euler(mouseY, mouseX, Input.GetAxis("Roll"));

	}
}
