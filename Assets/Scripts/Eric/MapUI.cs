using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapUI : MonoBehaviour
{
	public TextMeshProUGUI TopFloorText;
	public TextMeshProUGUI CurrentFloorText;

	private Staircase stair;
	public void UpdateCurrentFloorText(string currentFloor)
	{
		CurrentFloorText.text = currentFloor.ToString() + "f";
	}
	public void UpdateTopFloorText(string topFloor)
	{
		TopFloorText.text = topFloor.ToString() + "f";
	}
	public void UpdateUI(string topFloor, string currentFloor)
	{
		TopFloorText.text = topFloor.ToString() + "f";
		CurrentFloorText.text = currentFloor.ToString() + "f";
	}
	// Start is called before the first frame update

	public void Init(Staircase passedStair)
	{
		UpdateUI(	KeyboardInputs.i.stair.TopFloor.ToString(),
					KeyboardInputs.i.stair.CurrentFloor.ToString());

		this.stair = passedStair;
		stair.UIChange += OnFloorChange;
	}

	void OnFloorChange()
	{
		CurrentFloorText.text = stair.CurrentFloor.ToString();
	}

	void Destroy()
	{
		stair.UIChange -= OnFloorChange;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
