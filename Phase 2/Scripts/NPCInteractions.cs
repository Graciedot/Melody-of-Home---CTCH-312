using UnityEngine;
using UnityEngine.SceneManagement;

/*  NPCInteractions.cs (Interactive Scene Script)
 *  <><><><><><><><><><><><><><><><><><><><><><><>
 * 
 *  This script houses the NPC Interactions that are
 *  registered by the PlayerInteractions through colliders
 * 
 *  These allow the 'E' to spawn above the sprites head which
 *  are coded to take the player to a new scene.
 *  
 *  The chunk that interacts with Code Monkey's code: https://www.youtube.com/watch?v=LdoImzaY6M4&t=274s
 * 
 */

public class NPCInteractions : MonoBehaviour
{
    public string sceneToChangeTo; //Variable to transition to a different scene

    //Called on interaction ---> By pressing 'E'
    public void Interact()
    {
        Debug.Log("Interact!"); //Debug to ensure the interactions are actually happening...
        SceneManager.LoadScene(sceneToChangeTo); //Upon Interaction, the current scene will change to a different scene (SampleScene to Beatmap)
    }
}
