using UnityEngine;
using System.Collections;

public class InactivityTimer : MonoBehaviour 
{

	float incrementTime = 1f;
	float incrementBy = 1;
	public static float counter = 0;
	double time = 0;
	
	public void Update() {
		time += Time.deltaTime; 
		
		while(time >= incrementTime)
		{
			time -= incrementTime;
			counter += incrementBy;
		} 
		
		Debug.Log(counter);
	}
}
