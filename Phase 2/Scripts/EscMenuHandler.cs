using UnityEngine;
using UnityEngine.UI; //Necessary to access buttons

/*  EscMenuHandler.cs* (Player UI)
 *  <><><><><><><><><><><><><><><><><>
 * 
 *  This script creates the menu that allows us to pause the game.
 * 
 *  From here, we can decide to either resume, or exit the game entirely.
 *  It displays a UI element that open and close when the keycode ESC.
 *  is pressed.
 * 
 */ 

public class EscMenuHandler : MonoBehaviour
{
    //The actual Menu itself
    public GameObject MenuContainer;

    //Button variables for the ESC Menu
    public Button resumeButton;
    public Button quitButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Listeners to piece together event conditions
        resumeButton.onClick.AddListener(CloseMenu); //Resume button ---> Resumes the game after pause
        quitButton.onClick.AddListener(QuitGame); //Quit button ---> Hard closes the game
    }

    // Update is called once per frame
    void Update()
    {
        //If we press the ESC. key...
        if (Input.GetKeyDown(KeyCode.Escape))
        { //Another Undertale moment of nesting 'if' statements in another 'if' statement....
            if (!MenuContainer.activeSelf)
            { 
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    //Opens the menu: called when the ESC. key is pressed
    private void OpenMenu()
    {
        //STOP TIME: Should freeze the entire game ---> struggling with the audio...
        Time.timeScale = 0;

        //Ensures cursor is unlocked and visible so we can interact with the ui!
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        MenuContainer.SetActive(true);
    }
    //Closes the menu: called when the RESUME button is pressed ---> ON THE UI
    private void CloseMenu()
    {
        
        //Set the menu container to INACTIVE so it actually goes away...
        MenuContainer.SetActive(false);

        //Re-capture the mouse again!
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Resume time
        Time.timeScale = 1;
    }
    //Closes the ENTIRE GAME ---> called when QUIT button is pressed ---> ON THE UI
    private void QuitGame()
    {
        Application.Quit();
    }
}
