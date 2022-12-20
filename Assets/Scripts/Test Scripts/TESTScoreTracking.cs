using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTScoreTracking : MonoBehaviour
{
	public delegate void UpdateUI();
	public static UpdateUI updateUI;

	public int scorePoints = 0;

    // Start is called before the first frame update
    void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
