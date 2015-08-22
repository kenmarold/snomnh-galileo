using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour {

	public bool windowOpen; 	// Is there a parts window open?

	public void Start ()
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		WindowManager windowManager = gameController.GetComponent<WindowManager>();    

		windowManager.SetWindowState(false);
	}

	public void SetWindowState(bool winState)
	{
		windowOpen = winState;

		GameObject windowDrop = GameObject.FindGameObjectWithTag("WindowDrop");
		windowDrop.GetComponent<CanvasGroup>().blocksRaycasts = winState;
		windowDrop.GetComponent<CanvasGroup>().interactable = winState;

		// Debug.Log ("Windows State is set to: " + winState);
	}
}
