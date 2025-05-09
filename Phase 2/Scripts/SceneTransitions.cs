using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* SceneTransitions.cs* (Scene Management Script)
 * <><><><><><><><><><><><><><><><><><><><><><><><>
 * 
 * This script is to allow for scenes to transition from one to
 * another base on a timer.
 * 
 * We are able to choose a specific time for a scene to run for:
 * once it reaches less than or equal to 0, the prior scene will transition
 * to a new scene.
 */

public class SceneTransitions : MonoBehaviour
{
    //Variables for our timer and the scene that will be transitioned to
    public float changeTime;
    public string sceneName;


    // Update is called once per frame
    private void Update()
    {
        changeTime -= Time.deltaTime;
        
        //If the time reaches less than or equal to zero ---> A new scene will load
        if (changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}