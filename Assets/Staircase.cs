using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Staircase : MonoBehaviour
{
    public	 GameObject stepPrefab; // the prefab for a single step
    public int NumSteps = 10; // the number of steps in the staircase
    public float StepHeight = 1f; // the height of each step
    public float CharacterSpacing = 0.5f; // the spacing between characters on a single step
    public float CharacterSize = 0.5f; // the size of the characters
    public int CharactersPerStep = 1; // the number of characters to display on each step
    private List<char> characters; // the list of characters to choose from

    void Start()
    {
        // initialize the list of characters
        characters = new List<char>();
        for (char c = 'a'; c <= 'z'; c++)
        {
            characters.Add(c);
        }

        // create the staircase
        for (int i = 0; i < NumSteps; i++)
        {
            // create a new step
            GameObject step = Instantiate(stepPrefab, transform);
            step.transform.position = new Vector3(0, i * StepHeight, 0);
            // display the characters on the step
            for (int j = 0; j < 1; j++)
            {
                // choose a random character
                int index = Random.Range(0, characters.Count);
                char c = characters[index];
				Debug.Log(c);

				// create a text mesh to display the character
				TextMeshPro textMesh = step.AddComponent<TextMeshPro>();
				textMesh.color = Color.black;
                textMesh.text = c.ToString();
                textMesh.fontSize = (int)(CharacterSize * 10);
                textMesh.transform.position = new Vector3(0, -CharacterSpacing * (CharactersPerStep - 1) / 2 + j * CharacterSpacing, 0);
            }

            // increase the number of characters to display on each step
            CharactersPerStep++;
        }
    }
}
