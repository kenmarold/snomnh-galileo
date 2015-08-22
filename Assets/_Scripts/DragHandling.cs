using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragHandling : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	public float partScale;
	
	[HideInInspector] public Transform placeholderParent = null;
	[HideInInspector] public Transform parentToReturnTo = null;
	[HideInInspector] public Transform panelOrigin;
	[HideInInspector] public GameObject trashCan; 
	[HideInInspector] public GameObject partsPanel;
	// [HideInInspector] public GameObject partsWindow;
	[HideInInspector] public GameObject buildBoard;
	[HideInInspector] public int orderIndex;

	[HideInInspector] public bool onBuildBoard;

	[HideInInspector] public GameObject windowDrop;

	// Parts Windows
	[HideInInspector] public GameObject hornsWindow;
	[HideInInspector] public GameObject headWindow;
	[HideInInspector] public GameObject earsWindow;
	[HideInInspector] public GameObject neckWindow;
	[HideInInspector] public GameObject bodyWindow;
	[HideInInspector] public GameObject legsWindow;
	[HideInInspector] public GameObject feetWindow;
	[HideInInspector] public GameObject tailWindow;
	[HideInInspector] public GameObject moreWindow;
	
	GameObject placeholder = null;
	GameObject dragLayer;
	Vector3 buildPanelScale;
	Vector3 partsPanelScale = new Vector3(1.0f, 1.0f, 1.0f);
	
	Vector3 dist;			// Distance between object center point and mouse/touch point
	float posX;				// mouse/touch point x
	float posY;				// mouse/touch point y

	void Start ()
	{
		dragLayer = GameObject.FindGameObjectWithTag("DragLayer");
		buildBoard = GameObject.FindGameObjectWithTag("Board");
		partsPanel = GameObject.FindGameObjectWithTag("Parts");
		// partsWindow = GameObject.FindGameObjectWithTag("PartsWindow");
		trashCan = GameObject.FindGameObjectWithTag("Trash");
		windowDrop = GameObject.FindGameObjectWithTag("WindowDrop");
		
		hornsWindow = GameObject.FindGameObjectWithTag("HornsWindow");
		headWindow = GameObject.FindGameObjectWithTag("HeadWindow");
		earsWindow = GameObject.FindGameObjectWithTag("EarsWindow");
		neckWindow = GameObject.FindGameObjectWithTag("NeckWindow");
		bodyWindow = GameObject.FindGameObjectWithTag("BodyWindow");
		legsWindow = GameObject.FindGameObjectWithTag("LegsWindow");
		feetWindow = GameObject.FindGameObjectWithTag("FeetWindow");
		tailWindow = GameObject.FindGameObjectWithTag("TailWindow");
		moreWindow = GameObject.FindGameObjectWithTag("MoreWindow");
		
		panelOrigin = transform.parent;									// Store the object's parts panel origin
		orderIndex = transform.GetSiblingIndex();						// Store the objects's index
	}
	
#region IPointerClickHandler implementation
	
	public void OnPointerClick (PointerEventData eventData)
	{
		if(transform.parent.gameObject == buildBoard)
			transform.SetAsLastSibling();
	}
	
#endregion
	
#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = eventData.position.x - dist.x;
		posY = eventData.position.y - dist.y;
		
		buildPanelScale = new Vector3(partScale, partScale, partScale);
		transform.localScale = buildPanelScale;
		
		// Store the part index
		if(transform.parent.gameObject == partsPanel){

			#if UNITY_EDITOR			
			// Debug.Log ("I came from Parts Panel!!");
			#endif
		}
		
		placeholder = new GameObject();
		placeholder.transform.SetParent(transform.parent);
		placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
		
		parentToReturnTo = transform.parent;							// store current parent location
		placeholderParent = parentToReturnTo;					 		// set placeholder gameobject transform
		
		GetComponent<CanvasGroup>().blocksRaycasts = false;				// turn off image raycasting when dragging image in order to see what's behind the image            

		#if UNITY_EDITOR
		// Debug.Log("My Part Index is " + orderIndex);
		// Debug.Log("My Panel Origin is: " + panelOrigin.name);
		#endif

		GameObject windowDrop = GameObject.FindGameObjectWithTag("WindowDrop");

		if(windowDrop != null)
		{
			GameObject gameController = GameObject.FindGameObjectWithTag("gc");
			WindowManager windowManager = gameController.GetComponent<WindowManager>();

			if (windowManager.windowOpen = true)
			{
				windowManager.SetWindowState(true);
			} else {
				windowManager.SetWindowState(false);
			}
		}
	}
	
#endregion
	
#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		Vector3 curPos = new Vector3(eventData.position.x - posX, eventData.position.y - posY, dist.z);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
		transform.position = worldPos;
		transform.SetParent(dragLayer.transform);                   	 // pop object to draglayer to move object out of parts Panel
	}
	
#endregion
	
#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		if(windowDrop != null)
		{
			if (windowDrop.activeSelf)
			{
				GameObject gameController = GameObject.FindGameObjectWithTag("gc");
				WindowManager windowManager = gameController.GetComponent<WindowManager>();
				
				windowManager.SetWindowState(false);
				#if UNITY_EDITOR
				// Debug.Log("Window Drop is disengaged onEndDrag");
				#endif
			}
		}

		transform.SetParent(parentToReturnTo);									// Snaps object back to orginal parent if dropped outside of a dropzone
		transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());		// Returns card back to placeholder location
		
		GetComponent<CanvasGroup>().blocksRaycasts = true;						// turn Raycast back on
		Destroy(placeholder);        
		
		if(transform.parent.gameObject == partsPanel){
			transform.localScale = partsPanelScale;
			transform.SetParent(panelOrigin);
			transform.SetSiblingIndex(orderIndex);
		}
		
		if(transform.parent.gameObject == trashCan){
			transform.SetParent(panelOrigin);
			transform.SetSiblingIndex(orderIndex);
		}

		if(transform.parent.gameObject == windowDrop){
			transform.SetParent(panelOrigin);
			transform.SetSiblingIndex(orderIndex);
		}

		if(transform.parent.gameObject == buildBoard){
			if(!onBuildBoard)
			{
				onBuildBoard = true;
				// Pass the animal part tag to Score Manager
				GameObject gameController = GameObject.FindGameObjectWithTag("gc");
				ScoreManager scoreManager = gameController.GetComponent<ScoreManager>();    
				scoreManager.AddAnimalPartByTag(transform.gameObject.tag);
			}
			transform.SetAsLastSibling();
		}
		
		if(transform.parent.gameObject != buildBoard){
			if(onBuildBoard)
			{
				onBuildBoard = false;
				// Pass the animal part tag to Score Manager
				GameObject gameController = GameObject.FindGameObjectWithTag("gc");
				ScoreManager scoreManager = gameController.GetComponent<ScoreManager>();
				scoreManager.RemoveAnimalPartByTag(transform.gameObject.tag);
			}
		}

		// Debug.Log("ParentToReturnTo is: " + panelOrigin.name);
	}
	
#endregion
	
}