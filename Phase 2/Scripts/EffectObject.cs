using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*	EffectObject.cs (Rhythm Game Script)
*	<><><><><><><><><><><><><><><><><><><>
*	This script handles individual effect object behaviour.
*	
*	The only thing it does is destroy effect objects after their a certain amount of time has passed.
*	One of these scripts is attached to the worldspace canvas (parent object) of each of the effect 
*	object prefabs.
*	
*	Everything else the effect objects do (move, fade, shoot a burst of particles) is controlled
*	by Animators on the prefabs and Animation clips, yippee!
*/

public class EffectObject : MonoBehaviour
{
	public float lifetime = 1f; //How long an effect object lasts for

	// Update is called once per frame
	void Update()
    {
		//Kill the object this script is attached to after the time has elapsed
		Destroy(gameObject, lifetime);
    }
}
