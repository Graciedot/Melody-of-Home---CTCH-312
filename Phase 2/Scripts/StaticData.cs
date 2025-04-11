using UnityEngine;

/*  StaticData.cs* (Data Holder)
 * 
 *  This script houses data throughout the gameplay
 *  that is necessary for game function/the continuation
 *  of gameplay.
 * 
 */ 

public class StaticData : MonoBehaviour
{
    //Variables that indicate differe beatmaps being passed (For FinalBossChecker!)
    public static bool StringsPassed;
    public static bool WindsPassed;
    public static bool PercussionPassed;

    //Variable for when the Final Boss has been passed (Scene changer purposes)
    public static bool FinalBossPassed;

    //Saved position variable ---> for when we respawn into 'SampleScene'
    //We spawn in the same spot we were in before interaction
    public static Vector3 savedPosition;


}
