// Get the child count from a parent object

using UnityEngine;

public class ChildCount : MonoBehaviour 
{
	public int childCount;

	public void countChildren(string parentTag) 
	{
		GameObject parentObject = GameObject.FindGameObjectWithTag(parentTag);
		childCount = parentObject.transform.childCount;

		if (childCount == 1)
		{
			Debug.Log("There is " + childCount + " object on the board.");
		} 
		else if (childCount > 1) 
		{
			Debug.Log("There are " + childCount + " objects on the board.");
		}
	}
}