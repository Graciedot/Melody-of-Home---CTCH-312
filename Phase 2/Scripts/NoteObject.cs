using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
*	NoteObject.cs (Rhythm Game Script)
*	<><><><><><><><><><><><><><><><><><><>
*	
*	This script handles individual note behaviour.
*	
*	Notes are assigned a keycode. If that key is pressed when they enter a collider for one of the Activator
*	object (in this case, any of the coloured buttons situated along the bottom of the screen), the following
*	will occur:
*		- the individual note will be deactivated
*		- the distance between the center of the note (the z value) and 0 will be compared to a few numbers 
*		   ---> the GameManager will be informed of what type of success was had (how close it was), and the 
*		  correct effect object will be spawned at the position of the Activator object.
*/

public class NoteObject : MonoBehaviour
{
    public bool canBeHit;   //Used to keep track of whether a note can be hit or not
    public KeyCode keyToPress;  //Keycode set within the inspector for what key has to be hit to hit this note

    //These gameobjects hold prefabs for the effects that play when players successfully hit (or miss, oof) a note
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    private Transform activatorTransform;	//Used to hold transform of the activator for use when spawning effects

    // Update is called once per frame
    void Update()
    {
        //Check for key presses
        if (Input.GetKeyDown(keyToPress))
        { //Me when I nest 'if' statements inside of 'if' statements like Undertale...

            if (canBeHit)
            {
                gameObject.SetActive(false);

                //Check if the center of the note is more than 0.15 away from 0 on either side for normal hit
                if (Mathf.Abs(transform.position.z) > 0.15f)
                {
                    //Tell the GameManager we got a normal hit here and spawn HIT effect above the Activator
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, activatorTransform.position, hitEffect.transform.rotation);
                }
                //Check if center of note is more than 0.05 away from 0 on either side for good hit
                else if (Mathf.Abs(transform.position.z) > 0.05f)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, activatorTransform.position, goodEffect.transform.rotation);
                }
                //Otherwise, assume it's a perfect hit
                else
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, activatorTransform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }

    //This Triggers when a note object enters an object that has the inspector tag "Activator"
    //--> Ultimately prevents note objects from being pressed before they reach the buttons!!!
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Activator")
        {
            canBeHit = true;

            //Pass Activator's transform for the effect spawning
            activatorTransform = other.transform;
        }
    }

    //This triggers when a note object leaves an object that has the inspector tag "Activator"
    //---> Since scrolling only happens in one direction, a note that is exiting had to enter in the
    //    First place: if it leaves the collider and nothing happened, then we missed...
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Activator")
        {
            canBeHit = false;

            //Inform the GameManager of a note miss and spawn miss effect :(
            GameManager.instance.NoteMissed();
            Instantiate(missEffect, activatorTransform.position, missEffect.transform.rotation);
        }
    }
}