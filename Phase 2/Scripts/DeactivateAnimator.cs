using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* DeactivateAnimator.cs*
 * <><><><><><><><><><><><>
 * 
 * I can't recall if I attached this to anything
 * but I'm too scared to delete this script in case
 * it accidentally breaks something...
 * 
 */
public class DeactivateAnimator : MonoBehaviour
{
	public Animator anim;

	public void DeactivateAnimatorOnEvent()
	{
		anim.enabled = false;
	}
}
