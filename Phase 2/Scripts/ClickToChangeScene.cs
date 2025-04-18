using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Necessary to access buttons
using UnityEngine.SceneManagement;	//Necessary to swap between scenes

/*	ClickToChangeScene.cs* (Managing Scenes Script)
 *	<><><><><><><><><><><><><><><><><><><><><><><><><>
 * 
 *	This script helps manage scene transitions.
 * 
 *	When option available, we can click on a component
 *	that will direct us to a new scene.
 * 
 *	Actively searches for a GameManager in the scene so that
 *	we can reuse it instead of making a new one...
 *	
 */

public class ClickToChangeScene : MonoBehaviour
{
	public Button returnButton;

	//Variables pertaining to scene switching!
	public string nextSceneName;
	public string previousSceneName;
	public string defaultSceneName;

	private GameManager gameManager;

	void Start()
	{
		//Ensure cursor is unlocked and visible (from previous scenes) --> Gotta be able to move the mouse!
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

		//If it exists: find the gamemanager object and set reference to GameManager script component!
		GameObject gameManagerObj = GameObject.Find("GameManager");
		if (gameManagerObj != null)
		{
			//Debugs to see if the GameManager is actively being searched for
			Debug.Log("GameManager object found!");
			gameManager = gameManagerObj.GetComponent<GameManager>();
		}
		else
			Debug.Log("no GameManager in this scene");
		
		//Button that waits for directions to take us (FROM THE RHYTHM GAME) back to 'SampleScene'
		Button btn = returnButton.GetComponent<Button>();
		btn.onClick.AddListener(ChangeSceneToHUB);
	}

	private void ChangeSceneToHUB()
	{
		//If we DO have a GameManager in this scene
		if (gameManager != null && gameManager.passed)
			SceneManager.LoadScene(nextSceneName);  //Load next scene (cutscene)
		else
			SceneManager.LoadScene(previousSceneName);  //Reload the scene prior
	}
}
