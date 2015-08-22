using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// Character class for holding and instantiating characters 

public class InstantiateChar : MonoBehaviour, IPointerClickHandler
{
	public string prefabPath;
	List<GameObject> msgSymbols = new List<GameObject>();
	GameObject currentChar;

	public GameObject encodePanel;
	public GameObject decodePanel;
	
	Vector3 symbolPos = new Vector3(0, 0, 0);
	Vector3 symbolScale = new Vector3(1.0f, 1.0f, 1.0f);

	#region IPointerClickHandler implementation
	
	public void OnPointerClick (PointerEventData eventData)
	{
		// GameSetup gameSetup = gameObject.GetComponent<GameSetup>();
	
		if (transform.CompareTag("AlphaKey"))
		{
			// Load a GameObject into the msgSymbols List and store the last character added in a variable (lastChar)
			msgSymbols.Add((GameObject)Resources.Load(prefabPath));			
			currentChar = msgSymbols.Last<GameObject>();	

		   	// Instantiate the last character (lastChar) added to msgSymbols List
			GameObject alpha = Instantiate(currentChar, symbolPos, Quaternion.identity) as GameObject;

			// Define transforms for symbol
			alpha.transform.SetParent(encodePanel.transform);
			alpha.transform.localScale = symbolScale;

			Debug.Log(alpha.transform.parent.childCount);
		}

		if (transform.CompareTag("SymbolKey"))
		{
			// Load a GameObject into the msgSymbols List nd store the last character added in a variable (lastChar)
			msgSymbols.Add((GameObject)Resources.Load(prefabPath));			
			currentChar = msgSymbols.Last<GameObject>();	
			
			// Instantiate the last character (lastChar) added to msgSymbols List
			GameObject symbol = Instantiate(currentChar, symbolPos, Quaternion.identity) as GameObject;
			
			// Define transforms for symbol
			symbol.transform.SetParent(decodePanel.transform);
			symbol.transform.localScale = symbolScale;
			
			Debug.Log(symbol.transform.parent.childCount);
		}
	}
	#endregion
}