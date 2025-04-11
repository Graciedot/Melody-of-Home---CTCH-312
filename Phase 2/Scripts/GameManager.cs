using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //necessary to access ui text elements
using TMPro;
using UnityEngine.SceneManagement;	//necessary to manipulate TMP elements in particular

/* 
*	GameManager.cs (Rhythm Game Script)
*	<><><><><><><><><><><><><><><><><><><>
*	
*	This script keeps track of all rhythm-related gameplay stage information, and controls the music player and all
*	things related to the results screen.
*	
*	So basically, it makes sure the entire rhythm game can actually function the way it needs to...
*/

public class GameManager : MonoBehaviour
{

	public AudioSource musicPlayer;	//Reference to the component that plays the music

	//Variables for controlling the music player
	public bool started;
	public bool startPlaying;

	public static GameManager instance;	//Only one GameManager may exist at a time and this one is assigned when the scene starts

	private int totalNotes;	//This is the counter that keeps track of the total notes in the chart (incremented when a note is hit OR missed ---> not pre-defined)

	//The number of different types of hits variables
	public int numNormalHits;
	public int numGoodHits;
	public int numPerfectHits;
	public int numMisses;

	//Combo variables
	public TextMeshProUGUI comboText;
	public int currentCombo;
	public int highestCombo;

	//Scoring variables
	public int currentScore;
	public int scorePerNote = 1;
	public int scorePerGoodNote = 2;
	public int scorePerPerfectNote = 3;

	//Results variables
	public GameObject resultsScreen;    //This is required for making results screen pop up!
	public TextMeshProUGUI normalHitsText, goodHitsText, perfectHitsText, missedHitsText, notesHitText, noteAccuracy, rankText, highestComboText;
	public bool passed;

    // Start is called before the first frame update
    void Start()
    {
		//Initializing the variables
		instance = this;
		started = false;
		startPlaying = false;

		totalNotes = 0;

		numNormalHits = 0;
		numGoodHits = 0;
		numPerfectHits = 0;
		numMisses = 0;

		currentCombo = 0;
		highestCombo = 0;

		passed = false;
    }

    // Update is called once per frame
    void Update()
    {
		//If we haven't started!
        if(!started)
		{
			//If we haven't yet started, but we're told to start ---> PLEASE start
			//---> Will be informed by the Intro Manager
			if(startPlaying)
			{ 
				started = true;
				//Play the music! Yippee!!
				musicPlayer.Play();
			}
		}
		//If we HAVE started
		else
		{
			//Check to see if the music is finished playing and the results screen isn't already showing ---> Show the results on screen <3
			if(!musicPlayer.isPlaying && !resultsScreen.activeInHierarchy)
			{
				resultsScreen.SetActive(true);

				//Fill in easy score details (number of different types of hits)
				normalHitsText.text = numNormalHits.ToString();
				goodHitsText.text = numGoodHits.ToString();
				perfectHitsText.text = numPerfectHits.ToString();
				missedHitsText.text = numMisses.ToString();

				highestComboText.text = highestCombo.ToString() + "!";

				//Calculate and display the notes that were hit
				float totalHit = numNormalHits + numGoodHits + numPerfectHits;
				notesHitText.text = totalHit.ToString() + " / " + totalNotes.ToString();

				//Calculate the ranking by first calculating a perfect score
				float perfectScore = totalNotes * scorePerPerfectNote;

				//Compare perfect score to the score we got (also display note accuracy using this number)
				float scoreComparison = (currentScore / perfectScore) * 100f;
				noteAccuracy.text = scoreComparison.ToString("F1") + "%";

				string rankValue = "err";

				//At different percentiles, give an appropriate rank
				//These percentiles may be swapped depending on how playtesters are doing with the rhythm game
				//UPDATE: Percentiles were decreased because the winning threshold was too high and people were
				//---> struggling to pass a single level...
				if (scoreComparison <= 30f)
				{
					//We failed whoopsies lol
					rankValue = "FAIL";
				}
				else if (scoreComparison > 30f && scoreComparison < 95f)
				{
					//We passed yippee!
					passed = true;
					rankValue = "PASS";
				}
				else
				{
					//We are in the top 5% of passers!
					//No one has successfully reached 'SUPER!' yet...
					passed = true;
					rankValue = "SUPER!";
				}

				//Display rank
				rankText.text = rankValue;

				if (passed)
				{ 
					//This is to make sure we can move onto the boss battle and final scenes!
					string thisScene = (SceneManager.GetActiveScene()).name;
                    
					//Having an Undertale moment...
					//Keeps track of beatmaps that were passed throughout a gameplay
					//---> First 3 needed to enter the boss fight
                    if (thisScene == "ViolinSprite")
						StaticData.StringsPassed = true;
					else if (thisScene == "WindSprite")
						StaticData.WindsPassed = true;
					else if (thisScene == "PercussionSprite")
						StaticData.PercussionPassed = true; //All 3 MUST be true in order to reach the Boss Battle
					else if (thisScene == "BossBattle" && StaticData.StringsPassed &&
						StaticData.WindsPassed && StaticData.PercussionPassed)
						StaticData.FinalBossPassed = true;
				}
			}
		}
    }

	//Called whenever a note is hit ---> regardless of type (excluding MISS, because a miss is not a hit, womp womp)
	public void NoteHit()
	{
		//Add to the combo and the number of total notes
		currentCombo++;
		totalNotes++;

		//If our current combo is our current highest, add a tick to the highest combo as well
		if(currentCombo > highestCombo)
			highestCombo++;

		//Update combo ui text element...
		comboText.text = currentCombo.ToString();
	}

	//Called by a NoteObject whenever a NORMAL hit is made
	//Adds '1' to normal hit tracker and increments score accordingly ---> then calls NoteHit()
	public void NormalHit()
	{
		//Add a normal hit to the count and score
		numNormalHits++;
		currentScore += scorePerNote;
		NoteHit();
	}

	//Called by a NoteObject whenever a GOOD hit is made
	//Adds '1' to good hits tracker and increments score accordingly ---> then calls NoteHit()
	public void GoodHit()
	{
		//Add a good hit to count and score
		numGoodHits++;
		currentScore += scorePerGoodNote;
		NoteHit();
	}

	//Called by a NoteObject whenever a PERFECT hit is made
	//Adds '1' to number of perfect hits and increments score accordingly ---> then calls NoteHit()
	public void PerfectHit()
	{
		//Add a perfect hit to count and score
		numPerfectHits++;
		currentScore += scorePerPerfectNote;
		NoteHit();
	}

	//Called by a NoteObject whenever a note is MISSed (That's tragic)
	//Adds 1 to number of misses and total notes, and resets current combo
	public void NoteMissed()
	{
		//Add to the missed count and the number of total notes
		numMisses++;
		totalNotes++;

		//Reset the combo and the combo ui text element
		currentCombo = 0;
		comboText.text = currentCombo.ToString();
	}
}
