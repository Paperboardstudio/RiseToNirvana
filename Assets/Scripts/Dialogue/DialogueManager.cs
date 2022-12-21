using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
	[Header("Dialogue UI")]
	[SerializeField] private GameObject dialoguePanel;
	[SerializeField] private TextMeshProUGUI dialogueText;

	[Header("Choices UI")]
	[SerializeField] private GameObject[] choices;
	private TextMeshProUGUI[] choicesText;

	private Story currentStory;
	public bool dialogueIsPlaying { get; private set; }

	private static DialogueManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("Found more than one Dialogue Manager in the scene");
		}
		instance = this;
	}

	//add the code down below to freeze actions
	/*if (DialogueManager.GetInstance().dialogueIsPlaying)
	{
		return;
	}*/
	public static DialogueManager GetInstance()
	{
		return instance;
	}
	private void Start()
	{
		dialogueIsPlaying = false;
		dialoguePanel.SetActive(false);

		choicesText = new TextMeshProUGUI[choices.Length];
		int index = 0;
		foreach (GameObject choice in choices)
		{
			choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
			index++;
		}
	}
	private void Update()
	{
		if (!dialogueIsPlaying)
		{
			return;
		}
		//need a custom input to continue and for it to not skip the text immidiately
		if (Input.GetKeyDown(KeyCode.W))
		{
			ContinueStory();
		}
	}

	public void EnterDialogueMode(TextAsset inkJSON)
	{
		currentStory = new Story(inkJSON.text);
		dialogueIsPlaying = true;
		dialoguePanel.SetActive(true);

		ContinueStory();
	}
	private IEnumerator ExitDialogueMode()
	{
		yield return new WaitForSeconds(0.2f);

		dialogueIsPlaying = false;
		dialoguePanel.SetActive(false);
		dialogueText.text = "";

	}

	private void ContinueStory()
	{
		if (currentStory.canContinue)
		{
			dialogueText.text = currentStory.Continue();

			DisplayChoices();
		}
		else
		{
			StartCoroutine(ExitDialogueMode());
		}
	}

	private void DisplayChoices()
	{
		List<Choice> currentChoices = currentStory.currentChoices;

		if (currentChoices.Count > choices.Length)
		{
			Debug.LogError("More choices were given than the UI can support. Number of choices gives: " + currentChoices.Count);
		}

		int index = 0;
		foreach (Choice choice in currentChoices)
		{
			choices[index].gameObject.SetActive(true);
			choicesText[index].text = choice.text;
			index++;
		}

		for (int i = index; i < choices.Length; i++)
		{
			choices[i].gameObject.SetActive(false);
		}
		StartCoroutine(SelectFirstChoice());
	}

	private IEnumerator SelectFirstChoice()
	{
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
	}

	public void MakeChoice(int choiceIndex)
	{
		currentStory.ChooseChoiceIndex(choiceIndex);
	}
}
