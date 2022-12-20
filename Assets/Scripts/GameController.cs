using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public Staircase stair;
    // Start is called before the first frame update
    void Start()
    {
        if(stair == null)
		{
			stair = FindObjectOfType<Staircase>();
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
