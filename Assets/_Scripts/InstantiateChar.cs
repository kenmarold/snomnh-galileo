using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// Character class for holding and instantiating characters 

public class InstantiateChar : MonoBehaviour, IPointerClickHandler
{
	// public string prefabPath;
	public GameObject prefabObject;
	public GameObject alphaObject;
	public GameObject symbolObject;

	public GameObject encodePanel;
	public GameObject decodePanel;

	List<GameObject> alphaObjects = new List<GameObject>();
	List<GameObject> symbolObjects = new List<GameObject>();
	GameObject currentChar;
	
	Vector3 symbolPos = new Vector3(0, 0, 0);
	Vector3 symbolScale = new Vector3(1.0f, 1.0f, 1.0f);

	#region IPointerClickHandler implementation
	
	public void OnPointerClick (PointerEventData eventData)
	{

		// Load a GameObject into the msgSymbols List and store the last character added in a variable (lastChar)
		symbolObjects.Add(symbolObject);	
		// msgSymbols.Add((GameObject)Resources.Load(prefabPath));			
		currentChar = symbolObjects.Last<GameObject>();	
		
		// Instantiate the last character (lastChar) added to msgSymbols List
		GameObject alpha = Instantiate(currentChar, symbolPos, Quaternion.identity) as GameObject;
		// Define transforms for symbol
		alpha.transform.SetParent(encodePanel.transform);
		alpha.transform.localScale = symbolScale;



		// Load a GameObject into the msgSymbols List nd store the last character added in a variable (lastChar)
		alphaObjects.Add(alphaObject);	
		// msgSymbols.Add((GameObject)Resources.Load(prefabPath));	
		currentChar = alphaObjects.Last<GameObject>();	
		
		// Instantiate the last character (lastChar) added to msgSymbols List
		GameObject symbol = Instantiate(currentChar, symbolPos, Quaternion.identity) as GameObject;
		// Define transforms for symbol
		symbol.transform.SetParent(decodePanel.transform);
		symbol.transform.localScale = symbolScale;


		
		/*if (transform.CompareTag("AlphaKey"))
		{
			Debug.Log ("I hit an alpha key!");
			// Load a GameObject into the msgSymbols List and store the last character added in a variable (lastChar)
			symbolObjects.Add(symbolObject);	
			// msgSymbols.Add((GameObject)Resources.Load(prefabPath));			
			currentChar = symbolObjects.Last<GameObject>();	

		   	// Instantiate the last character (lastChar) added to msgSymbols List
			GameObject alpha = Instantiate(currentChar, symbolPos, Quaternion.identity) as GameObject;
			// Define transforms for symbol
			alpha.transform.SetParent(encodePanel.transform);
			alpha.transform.localScale = symbolScale;

			// Debug.Log(alpha.transform.parent.childCount);

		}*/

		/*if (transform.CompareTag("SymbolKey"))
		{
			Debug.Log ("I hit a symbol key!");
			// Load a GameObject into the msgSymbols List nd store the last character added in a variable (lastChar)
			alphaObjects.Add(alphaObject);	
			// msgSymbols.Add((GameObject)Resources.Load(prefabPath));	
			currentChar = alphaObjects.Last<GameObject>();	
			
			// Instantiate the last character (lastChar) added to msgSymbols List
			GameObject symbol = Instantiate(currentChar, symbolPos, Quaternion.identity) as GameObject;
			// Define transforms for symbol
			symbol.transform.SetParent(decodePanel.transform);
			symbol.transform.localScale = symbolScale;
			
			// Debug.Log(symbol.transform.parent.childCount);
		}*/
	}
	#endregion
}