using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueMan : MonoBehaviour
{
	[field: SerializeField] GameObject dialogBox;
	[field: SerializeField] TextMeshProUGUI dialogText;
	[field: SerializeField] int lettersPerSecond;

	public static DialogueMan Instance { get; private set; }

	public event Action OnShowDialog;
	public event Action OnCloseDialog;

	Dialog dialog;
	int currentLine = 0;
	bool isTyping;
	private void Awake()
	{
		Instance = this;
	}
	public IEnumerator ShowDialog(Dialog dialog)
	{
		yield return new WaitForEndOfFrame();
		OnShowDialog?.Invoke();

		this.dialog = dialog;
		dialogBox.SetActive(true);
		dialogText.text = dialog.Lines[0];

		if(isTyping == true)
		{
			StartCoroutine(TypeDialog(dialog.Lines[0]));
		}
	}

	public IEnumerator TypeDialog(string line)
	{
		isTyping = true;
		dialogText.text = "";
		foreach(var letter in line.ToCharArray())
		{
			dialogText.text += letter;
			yield return new WaitForSeconds(1f / lettersPerSecond);
		}

	}
	public void HandleUpdate()
	{
		if (!isTyping)
		{
			currentLine++;
			if(currentLine< dialog.Lines.Count)
			{
				StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
			}
			else
			{
				currentLine = 0;
				dialogBox.SetActive(false);
				OnCloseDialog?.Invoke();
				KeyboardInputs.i.SavedKey = true;
			}
			isTyping = false;
		}
	}
}
