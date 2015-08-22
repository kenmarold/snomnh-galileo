using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler 
{
	// public DragHandling dragHandling;		// access DragHandling script by creating a reference to it (dragHandling)

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(eventData.pointerDrag == null)
			return;

		DragHandling dragHandling = eventData.pointerDrag.GetComponent<DragHandling>();

		if(dragHandling != null)
			dragHandling.placeholderParent = this.transform;						// change parent based on object
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if(eventData.pointerDrag == null)
			return;

		DragHandling dragHandling = eventData.pointerDrag.GetComponent<DragHandling>();

		if(dragHandling != null && dragHandling.placeholderParent == transform)
			dragHandling.placeholderParent = dragHandling.parentToReturnTo;			// change parent based on object
	}

	public void OnDrop(PointerEventData eventData)
	{
		DragHandling dragHandling = eventData.pointerDrag.GetComponent<DragHandling>();

		if(dragHandling != null)
		{
			dragHandling.parentToReturnTo = transform;								// change parent based on the current object it's dropped on

			if (transform.gameObject == dragHandling.trashCan)
			{	
				// move back to parts panel after trashed
				dragHandling.parentToReturnTo = dragHandling.partsPanel.transform;
			}
			else if (transform.gameObject == dragHandling.windowDrop)
			{	
				// move back to correct parts panel
				dragHandling.parentToReturnTo = dragHandling.partsPanel.transform;
			} 
			else 
			{
				dragHandling.parentToReturnTo = dragHandling.buildBoard.transform;
			}
		}

		// Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
	}
}