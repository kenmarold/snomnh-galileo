// Scoring System

using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
	// Score calculation variables
	public List<string> totalBuildBoardParts;		// Running list of all parts on board (by Tag)
	public int numCorrectPartsOnBoard;
	public int totalPossibleCorrectParts;
	int numIncorrectPartsOnBoard;
	public float score;

	// Scoreboard variables
	public string correctAnimalParts;
	public string totalAnimalParts;
	public Text scoreBoardReadout;

	void Start()
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		totalPossibleCorrectParts = gameSetup.totalPossibleCorrectParts;

		// Debug.Log("TOTAL POSSIBLE CORRECT PARTS ARE: " + totalPossibleCorrectParts);

		ScoreBoard();
	}

	public void AddAnimalPartByTag(string tag)
	{
		// Add object tag to List
		totalBuildBoardParts.Add(tag);
		// Debug.Log ("Added an object tagged as: " + tag);

		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		if (tag == gameSetup.activeTag)
		{
			numCorrectPartsOnBoard ++;
		} else {
			numIncorrectPartsOnBoard ++;
		}

		CalculateScore();
	}

	public void RemoveAnimalPartByTag(string tag)
	{
		// Add object tag to List
		totalBuildBoardParts.Remove(tag);
		// Debug.Log ("Removed an object tagged as: " + tag);

		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		if (tag == gameSetup.activeTag)
		{
			numCorrectPartsOnBoard --;
		} else {
			numIncorrectPartsOnBoard --;
		}

		CalculateScore();
	}

	public void CalculateScore()
	{
		GameObject gameController = GameObject.FindGameObjectWithTag("gc");
		GameSetup gameSetup = gameController.GetComponent<GameSetup>();

		score =
			Mathf.Max((((float)numCorrectPartsOnBoard/totalPossibleCorrectParts)	// Correct percent is 0.15 or 15%
			           - ((float)numIncorrectPartsOnBoard/gameSetup.totalParts))	// Minus incorrect percent which is .03 or 3%
			          * 100f,                                            			// Normalized to 100 which gives us 15% - 3% = 12%
			          0f);  

		// Debug.Log ("Your current score is: " + score);

		// ScoreBoard();  TURNED OFF SCOREBOARD
	}

	public void ScoreBoard()
	{
		// Store correct and totals parts for a particular animal
		correctAnimalParts = numCorrectPartsOnBoard.ToString();
		totalAnimalParts = totalPossibleCorrectParts.ToString();

		float roundScore = Mathf.Round(score);

		scoreBoardReadout.text = roundScore + " / " + 100;

		// Debug.Log (scoreBoardReadout);
	}
}