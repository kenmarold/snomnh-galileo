using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DeleteSymbol : MonoBehaviour, IPointerClickHandler
{
	public GameObject deleteButton;
	GameObject charToDestroy;

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		GameObject instSymScript = GameObject.Find("Game Controller");
		InstantiateChar instSym = instSymScript.GetComponent<InstantiateChar>();

		//GameObject instSym = GameObject.GetComponent<InstantiateSymbol>();
		// charToDestroy = instSym.msgSymbols[instSym.msgSymbols.Count - 1];
		// Destroy(charToDestroy);
			
	/*	instSym.msgSymbols.RemoveAt(instSym.msgSymbols.Count - 1);
			
		Debug.Log(instSym.msgSymbols.Count);*/
	}
	#endregion
}
