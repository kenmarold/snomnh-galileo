using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ManageKeySets : MonoBehaviour
{
	// public GameObject messagePanelTitle;
	// public GameObject mainSceneTitle;
	public GameObject encodeButton;
	public GameObject decodeButton;
	public bool keySet;

	// Reference to encode button to access its components
	// Button theButton;
	// ColorBlock theColor;

	void Awake()
	{
		// HandleMessageTitle("");
		// HandleSceneTitle("");

		GameObject alphaGroup = GameObject.FindGameObjectWithTag("AlphaGroup");
		alphaGroup.GetComponent<CanvasGroup>().interactable = false;
		alphaGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;

		GameObject symbolGroup = GameObject.FindGameObjectWithTag("SymbolGroup");
		symbolGroup.GetComponent<CanvasGroup>().interactable = false;
		symbolGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;

		// Set Normal Color of Encode Button to Highlight Color
		// ColorBlock colorTint = encodeButton.GetComponent<Button>().colors;
		// colorTint.normalColor = new Color(0, 255, 59, 1.0f);
	}
	
	public void HandleActiveKeySet (bool keySet)
	{
		if(keySet == true)
		{
			GameObject alphaGroup = GameObject.FindGameObjectWithTag("AlphaGroup");
			alphaGroup.GetComponent<CanvasGroup>().interactable = true;
			alphaGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;

			GameObject symbolGroup = GameObject.FindGameObjectWithTag("SymbolGroup");
			symbolGroup.GetComponent<CanvasGroup>().interactable = false;
			symbolGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
			
			// HandleMessageTitle("MY ENCODED MESSAGE");
			// HandleSceneTitle("ENCODE");
		}
		else
		{
			// Set Normal Color of Encode Button back to original settings
			// ColorBlock colorTint = encodeButton.GetComponent<Button>().colors;
			// colorTint.normalColor = new Color(67, 174, 45, 1.0f);

			GameObject alphaGroup = GameObject.FindGameObjectWithTag("AlphaGroup");
			alphaGroup.GetComponent<CanvasGroup>().interactable = false;
			alphaGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;

			GameObject symbolGroup = GameObject.FindGameObjectWithTag("SymbolGroup");
			symbolGroup.GetComponent<CanvasGroup>().interactable = true;
			symbolGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
			
			// HandleMessageTitle("MY DECODED MESSAGE");
			// HandleSceneTitle("DECODE");
		}
	}

	public void allKeysActive()
	{
		GameObject alphaGroup = GameObject.FindGameObjectWithTag("AlphaGroup");
		alphaGroup.GetComponent<CanvasGroup>().interactable = true;
		alphaGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
		
		GameObject symbolGroup = GameObject.FindGameObjectWithTag("SymbolGroup");
		symbolGroup.GetComponent<CanvasGroup>().interactable = true;
		symbolGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
/*
	public void HandleMessageTitle (string messageTitle)
	{
		Text txt = messagePanelTitle.GetComponent<Text>();
		txt.text = messageTitle;
	}
	
	public void HandleSceneTitle (string sceneTitle)
	{
		Text txt = mainSceneTitle.GetComponent<Text>();
		txt.text = sceneTitle;
	}
*/
}
