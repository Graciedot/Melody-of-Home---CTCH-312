using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/* PlayerInteractions.cs (Interactive Scene Script)
 * <><><><><><><><><><><><><><><><><><><><><><><><><>
 * 
 *  This script allows colliders to scan around the map for specific
 *  colliders that will then interact with each other in order to
 *  form an interaction.
 * 
 *  When colliders find the right collider they are looking for, an
 *  Interaction (in this case an 'E' above the sprites heads') will
 *  occur for the player.
 * 
 *  This specific code (aside from the position saved) is followed from a
 *  tutorial by CodeMonkey: https://www.youtube.com/watch?v=LdoImzaY6M4&t=274s
 * 
 */


public class PlayerInteraction : MonoBehaviour
{
    private GameObject npcInteractableGameObject;

    // Update is called once per frame
    void Update()
    {
        //If/when the 'E' key press condition is fulfilled
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Range at which Interactable appears to the player
            float interactRange = 12f;
            //Cycle through nearby colliders objects within the 12f range of the player
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                //If the collider found is an NPC Interactable
                if (collider.TryGetComponent(out NPCInteractions npcInteractable))
                {
                    //Save the player position ---> This saved position will be where we respawn after a beatmap!
                    StaticData.savedPosition = this.gameObject.transform.position;

                    npcInteractable.Interact();
                }
            }
        }
    }
    //This section is a repeat of the code above; However we are getting the NPC Interactable Object.
    //Therefore everything is relatively the same aside from the end.
    public NPCInteractions GetInteractableObject()
    {
        float interactRange = 12f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractions npcInteractable))
            {
                npcInteractableGameObject = npcInteractable.gameObject;
                return npcInteractable; //If the interactable is an NPCInteractable, it is returned to the caller and
                                        //Interaction is shown
            }
        }
        return null; //If the Interactable does not have have the NPC Interactable script, nothing will show
    }
}
