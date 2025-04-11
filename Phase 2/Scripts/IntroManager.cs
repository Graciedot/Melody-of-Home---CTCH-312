using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
*	IntroManager.cs (Rhythm Game Script)
*	<><><><><><><><><><><><><><><><><><><><><>
*	
*	This script tends to the intro of the Rhythm Game mechanics
*	
*	Here, we set the time prior to the rhythm game actually starting
*	as well as starting the beat scroller so the rhythm game is actually
*	playable/moveable.
*	
*/

public class IntroManager : MonoBehaviour
{	
	//Duration we have before the first note meets the buttons
	public float introTime = 10f;

	public BeatScroller bS; //For the beatscroller within our rhythm game
	public bool startScroll = true; //Enables our scroll to move

	public Animator buttonSetupAnimator;

	// Update is called once per frame
	void Update()
    {
		//Start the beat scroller (the object that holds the notes) immediately; they will start
		//Coming closer
		if(startScroll)
			bS.hasStarted = true;

		//Start the timer
		introTime -= Time.deltaTime;

		//When the timer runs out, tell the game manager to start playing the music!
		//If everything is placed right, we should be synced up (fingers crossed)....
		if(introTime <= 0.0f)
		{
			GameManager.instance.startPlaying = true;
		}
    }
}
