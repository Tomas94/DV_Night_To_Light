using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
	public CharacterController playerHeight;
	public float normalHeight, crouchHeight;
	//public Transform body;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			playerHeight.height = crouchHeight;
			//body.transform.localScale = new Vector3(body.transform.localScale.x, body.transform.localScale.y * 0.5f, body.transform.localScale.z);
		}
		if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			playerHeight.height = normalHeight;
			//body.transform.localScale = new Vector3(body.transform.localScale.x, body.transform.localScale.y * 2f, body.transform.localScale.z);
		}
	}
}