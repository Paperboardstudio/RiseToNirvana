using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject stepPrefab; // the prefab for a single step
	public Transform spawnLocationPrefab; // the spawn location for the steps

	[Header("Data input")]
	[field: SerializeField] int numSteps = 10; // the number of steps in the staircase
	[field: SerializeField] float stepHeight = 1f; // the height of each step
	[field: SerializeField] float characterSize = 0.5f; // the size of the characters
	[field: SerializeField] int charactersPerStep = 1; // the number of characters to display on each step
	[field: SerializeField] public int TopFloor { get; private set; }
	[field: SerializeField] public int CurrentFloor { get; set;  }

	public event Action UIChange;

	int counter = 1;


	List<Step> steps { get; set; } // the list of instantiated steps
	int currentStep = 0; // the index of the current step
	private List<char> characters; // the list of characters to choose from

	void Start()
	{
		// initialize the list of characters
		characters = new List<char>();
		for (char c = 'a'; c <= 'z'; c++)
		{
			characters.Add(c);
		}

		// shuffle the list of characters
		for (int i = 0; i < characters.Count; i++)
		{
			int j = UnityEngine.Random.Range(i, characters.Count);
			char temp = characters[i];
			characters[i] = characters[j];
			characters[j] = temp;
		}

		// create the staircase
		steps = new List<Step>();
		for (int i = 0; i < numSteps; i++)
		{
			// create a new step
			GameObject stepObject = Instantiate(stepPrefab, transform);
			Step step = stepObject.GetComponent<Step>();
			step.transform.position = new Vector3(spawnLocationPrefab.transform.position.x, spawnLocationPrefab.transform.position.y+ i * stepHeight, spawnLocationPrefab.transform.position.z);
			steps.Add(step);

			// assign a character to the step
			char c;
			if (characters.Count == 0)
			{
				// replenish the list of characters
				for (char d = 'a'; d <= 'z'; d++)
				{
					characters.Add(d);
				}
			}
			c = characters[0];
			characters.RemoveAt(0);
			step.SetCharacter(c);

			// increase the number of characters to display on each step
			charactersPerStep++;
		}

		

	}

	/// <summary>
	/// Destroy the current step (might change to highlighting)
	/// </summary>
	public void DestroyCurrentStep()
	{
		if (currentStep <= steps.Count-1)
		{
			steps[currentStep].gameObject.SetActive(false);
			currentStep++;
			if(currentStep == steps.Count)
			{
				MoveToNewFloor();
			}
		}
		else
		{
			MoveToNewFloor();
		}
	}

	/// <summary>
	/// Reset the states of the visual steps and regenerate their characters
	/// Hardcoded events on floor X and Y, right now it's 1 and 3
	/// </summary>
	public void MoveToNewFloor()
	{
		// it gets called at the end of each floor so when we reach floor 2,  currentFloor will be 1.
		if (CurrentFloor == 1)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 3)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 5)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 7)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 9)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 11)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 13)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 15)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 17)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 19)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 21)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 23)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 25)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 27)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 29)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 31)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 33)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 35)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 37)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 39)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 41)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 43)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 45)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 47)
		{
			KeyboardInputs.i.Interact(++counter);
		}
		if (CurrentFloor == 49)
		{
			KeyboardInputs.i.Interact(++counter);
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);

		}
		if (CurrentFloor == 50)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		}
	

		currentStep = 0;
		for (int i = 0; i < steps.Count; i++)
		{
			steps[i].gameObject.SetActive(true);
		}

		CurrentFloor += 1;

		UIChange();
		RegenerateCharacters();
	}

	/// <summary>
	/// returns the character from the current step and regenerates if we reach the top of the steps
	/// </summary>
	/// <returns>returns the character from the current step</returns>
	public string GetStep()
	{
		if (currentStep <= steps.Count)
		{
			Step step = steps[currentStep];
			string character = step.GetCharacter();
			return character;
		}
		else
		{
			MoveToNewFloor();
			return GetStep();
		}
	}
	
	/// <summary>
	///	regenerates the characters for all the steps and updates the text meshes
	/// </summary>
	public void RegenerateCharacters()
	{
		// shuffle the list of characters
		for (int i = 0; i < characters.Count; i++)
		{
			int j = UnityEngine.Random.Range(i, characters.Count);
			char temp = characters[i];
			characters[i] = characters[j];
			characters[j] = temp;
		}

		// assign new characters to each step
		for (int i = 0; i < steps.Count; i++)
		{
			Step step = steps[i];

			// choose a character from the list
			char c;
			if (characters.Count == 0)
			{
				// replenish the list of characters
				for (char d = 'a'; d <= 'z'; d++)
				{
					characters.Add(d);
				}
			}
			c = characters[0];
			characters.RemoveAt(0);

			// set the character on the step and update the text mesh
			step.SetCharacter(c);
		}
	}
}
