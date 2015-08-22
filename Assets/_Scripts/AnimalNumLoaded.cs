using UnityEngine;
using System.Collections;

public class AnimalNumLoaded : MonoBehaviour {

	public int animalLoaded;
	
	public void IncrementAnimalNumber()
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = GetComponent<GameSetup>();

		gameSetup.arrayIndex = animalLoaded;

		animalLoaded = animalLoaded++;

		Debug.Log(animalLoaded);
	}
}
