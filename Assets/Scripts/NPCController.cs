using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
	[field: SerializeField] Dialog dialog;
	public void Interact()
	{
		StartCoroutine( DialogueMan.Instance.ShowDialog(dialog));
	}
}
