using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
	public CharacterController playerHeight;
	public float normalHeight, crouchHeight;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			playerHeight.height = crouchHeight;
		}
		if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			playerHeight.height = normalHeight;
		}
	}
}