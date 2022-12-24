using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField] private bool clockwise;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private GameObject whirlpool;

	void Update()
	{
		if (clockwise)
		{
			transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
		}
		else
		{
			transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
		}
	}
}
