using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  PlayerMovement.cs* (Player Control)
 * 
 *  This script works with the built in Character Controller in order
 *  for the player to walk around.
 * 
 *  Using the WASD keys, we enable walking that is guided through a camera
 *  But not fully dependent on the camera to move around.
 *  ---> We used the cinemachine camera in order to implement a 3rd person view
 *  
 *  For some reason Aria is strangely jittery and has not been fixed...
 * 
 */


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera; //Camera that will follow the player ---> THIRD PERSON!
    public Transform orientation; //Camera Orientation

    [SerializeField] public float walkSpeed = 40f; //Variable to set the player walkspeed ---> We can set it in the Inspector
    [SerializeField] public float gravity = 30f; //Variable for gravity so we aren't floating!

    private CharacterController characterController; //What handles all of our character movements ---> We abandoned the rigid bodies...
    public Animator characterAnimator; //Important for when we implement the animations

    private Vector3 moveDirection = Vector3.zero;


    void Start()
    {   //Get the character controller so we can move
        //Keep the cursor locked and invisible 
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //We need to save the position of our character when they return to 'SampleScene' after completeing a beatmap
        //If our position is NOT 
        if (StaticData.savedPosition.x != 0 && StaticData.savedPosition.y != 0 && StaticData.savedPosition.z != 0) 
        {
            //We disable the character controller so it can not interfere with the reposition
            characterController.enabled = false;
            //Overwrites the start position with the one that has been saved in 'StaticData'
            this.transform.position = new Vector3(StaticData.savedPosition.x, StaticData.savedPosition.y, StaticData.savedPosition.z);
            //Character Controller is re-enabled so that we can move YAY
            characterController.enabled = true;
        }
    }

    //Animation handler for Character animations
    void handleAnimation()
    {
        //If 'WASD' keys are pressed, initiate the character walking animation state
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            characterAnimator.SetBool("Walking", true);
        }
        else //Else, return to idle animation state
        {
            characterAnimator.SetBool("Walking", false);
        }
    }

    void Update()
    {
        //Get the player inputs for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        handleAnimation();

        //Set move direction based on the camera orientation and inputs
        moveDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);

        //If we are in the air, apply gravity to moveDirection.y so we fall ---> no more floating in the sky
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Move the character ---> YIPPEE
        characterController.Move(moveDirection * walkSpeed * Time.deltaTime);
    }
}