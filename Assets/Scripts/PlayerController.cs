using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	/// <summary>
	/// Control the state of the blend tree for walking
	/// </summary>
	/// <param name="moving">Link to blendtree, 0 = stop, 1 = walking</param>
	public void IsWalking(int moving)
	{
		animator.SetFloat("isWalking", moving);
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
