using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	[Header("Visual Cue")]
	[SerializeField] private GameObject visualCue;

	[Header("Ink JSON")]
	[SerializeField] private TextAsset inkJSON;

	private bool playerInRange;

	private void Awake()
	{
		playerInRange = true;//default is false
		//visualCue.SetActive(true);//default is false
	}

	/*private void Update()
	{
		if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
		{
			//visualCue.SetActive(true);
			if (Input.GetKeyDown(KeyCode.C))
			{
				DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
			}
		}
		else
		{
			//visualCue.SetActive(false);
		}
	}*/
	private void Start()
	{
		if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
		{
			//visualCue.SetActive(true);
			//if (Input.GetKeyDown(KeyCode.W))
			//
				DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
			//}
		}
	}
	public void TutorialTrigger()
	{

	}

	//Need to create a custom trigger for dialogue to happen
	//Maybe the event sytem?
	/*
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerInRange = true;
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			playerInRange = false;
		}
	}*/
}
