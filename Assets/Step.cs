using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Step : MonoBehaviour
{
	public char character; // the character on the step
	public TextMeshProUGUI textMeshPro; // the text mesh pro object to display the character

	private void Start()
	{
		// initialize the text mesh pro object
		textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
		if (textMeshPro == null)
		{
			textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
		}

		// set the character and update the text mesh pro object
		SetCharacter(character);
	}

	// sets the character and updates the text mesh pro object
	public void SetCharacter(char character)
	{
		this.character = character;
		textMeshPro.text = character.ToString();
	}

	// returns the character as a string
	public string GetCharacter()
	{
		return character.ToString();
	}
	public string GetCharacter(int index)
	{
		return character.ToString();
	}
	string GetStepCharacters(int step, int numCharacters)
	{
		// Choose numCharacters random characters from the alphabet.
		string characters = "";
		for (int i = 0; i < numCharacters; i++)
		{
			char c = (char)('A' + Random.Range(0, 26));
			characters += c;
		}
		return characters;
	}
}
