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

	/// <summary>
	///  sets the character and updates the text mesh pro object
	/// </summary>
	/// <param name="character"> the string we want to assign to the step</param>
	public void SetCharacter(char character)
	{
		this.character = character;
		textMeshPro.text = character.ToString();
	}

	/// <summary>
	/// returns the character as a string
	/// </summary>
	/// <returns> returns the character on the current step as a string</returns>
	public string GetCharacter()
	{
		return character.ToString();
	}
	/// <summary>
	/// returns the character as a string on the indexth step as a string
	/// NEED ADJUSTMENTS
	/// </summary>
	/// <returns> returns the character as a string</returns>
	public string GetCharacter(int index)
	{
		return character.ToString();
	}
	/// <summary>
	/// INCOMPLETE
	/// </summary>
	/// <param name="step">index of the step array</param>
	/// <param name="numCharacters">how many characters this current step has</param>
	/// <returns></returns>
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
