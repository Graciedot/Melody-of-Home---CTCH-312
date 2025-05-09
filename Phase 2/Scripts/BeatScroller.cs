using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
*	BeatScroller.cs (Rhythm Game Script)
*	<><><><><><><><><><><><><><><><><><><><>
*	
*	This script controls the beat scroller object, which contains all of our notes, making them slowly scroll
*	towards the coloured buttons located at the bottom edge of the screen.
*	
*	It uses a pre-entered song tempo (which SHOULD be the same bpm as the audio track we're using if
*	we don't want any timing issues!) to determine how quickly it should move. It then moves the object
*	it is attached to every frame by that amount accordingly.
*	
*	It was set up to be started by the IntroManager script in the event we wanted cutscenes inside the
*	rhythm game scenes; however, we ended up just giving it the 'okay' to start as soon as it loads alongside having 
*	a timer count down the seconds until the beat scroller arrives at the buttons.
*/

public class BeatScroller : MonoBehaviour
{

	[SerializeField]public float beatTempo;

	public bool hasStarted; //Variable for when the beatmap 'HAS STARTED'

	private float scrollSpeed; //Variable controls how fast the beatmap will flow

    // Start is called before the first frame update
    void Start()
    {
		//Set the scrollspeed based on beattempo (assigned in unity inspector based on song)
		scrollSpeed = beatTempo / 60f; //This is how fast the notes should move per second
    }

    // Update is called once per frame
    void Update()
    {
		//hasStarted is controlled by the IntroManager script
       if(hasStarted)
		{
			transform.position -= new Vector3(0f, 0f, scrollSpeed * Time.deltaTime);
		}
    }
}
