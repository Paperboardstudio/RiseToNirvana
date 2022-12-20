using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
	public GameObject stepPrefab; // the prefab for a single step
	public int numSteps = 10; // the number of steps in the staircase
	public float stepHeight = 1f; // the height of each step
	public float characterSize = 0.5f; // the size of the characters
	public int charactersPerStep = 1; // the number of characters to display on each step
	private List<char> characters; // the list of characters to choose from
	public List<Step> steps; // the list of instantiated steps
	public int currentStep = 0; // the index of the current step

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
			int j = Random.Range(i, characters.Count);
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
			step.transform.position = new Vector3(0, i * stepHeight, 0);
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
	public void DestroyCurrentStep()
	{
		steps[currentStep].gameObject.SetActive(false);
		currentStep++;
	}
	public string GetStep() {
		Step step = steps[currentStep];
		string character = step.GetCharacter();

		return character;
	}
	// regenerates the characters for all the steps and updates the text meshes
	public void RegenerateCharacters()
	{
		// shuffle the list of characters
		for (int i = 0; i < characters.Count; i++)
		{
			int j = Random.Range(i, characters.Count);
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
