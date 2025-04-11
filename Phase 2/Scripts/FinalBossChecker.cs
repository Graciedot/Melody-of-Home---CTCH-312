using UnityEngine;
using UnityEngine.SceneManagement; //Necessary to swap between scenes

/*  FinalBossChecker.cs* (Rhythm Game)
 * 
 *  This script is super important in order for us to proceed!
 * 
 *  Here, we check to make sure that all 3 prior rhythm beatmaps
 *  are successfully passed and completed! If all passes are true,
 *  we can proceed to the Final Boss beatmap!
 * 
 */


public class FinalBossChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug in place to make sure static data is keeping track of passes
        Debug.Log("Strings: " + StaticData.StringsPassed + ", Winds: " + StaticData.WindsPassed + ", Percussion: " + StaticData.PercussionPassed);

        //If the 3 main beatmaps are all PASSED, we will load 'The Call' cutscene
        if (StaticData.StringsPassed && StaticData.WindsPassed && StaticData.PercussionPassed)
            SceneManager.LoadScene("The Call");
        
        //If the boss battle is PASSED, we will load 'Decisions' cutscene
        if (StaticData.FinalBossPassed)
            SceneManager.LoadScene("Decisions");
    }
}
