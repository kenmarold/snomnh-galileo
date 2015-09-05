using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DeleteSymbol : MonoBehaviour, IPointerClickHandler
{
	public GameObject deleteButton;
	public GameObject encodePanel;
	public GameObject decodePanel;
	// GameObject charToDestroy;

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		// get child count
		int encodeChildren = encodePanel.transform.childCount;	
		int decodeChildren = encodePanel.transform.childCount;	
		// Debug.Log("There are " + numChildren + " children");

		// check that children exist and then destroy last child
		if (encodeChildren > 0)
		{
			Destroy(encodePanel.transform.GetChild(encodeChildren - 1).gameObject);
			Destroy(decodePanel.transform.GetChild(decodeChildren - 1).gameObject);
		}
	}
	#endregion
}