using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBlaster : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
		if (otherRB != null)
		{
			otherRB.AddForceAtPosition(
				(otherRB.transform.position - transform.position).normalized * 2000,
				transform.position,
				ForceMode.Acceleration
			);
		}
	}
}
