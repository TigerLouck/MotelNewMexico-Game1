using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLook : MonoBehaviour
{

	public float mouseX;
	public float mouseY;


	Quaternion startOrient;

	private void Start()
	{
		startOrient = transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		mouseX += Input.GetAxisRaw("Mouse X");
		mouseY += -Input.GetAxisRaw("Mouse Y");

		transform.rotation = startOrient * Quaternion.Euler(mouseY, mouseX, 0);
	}
}
