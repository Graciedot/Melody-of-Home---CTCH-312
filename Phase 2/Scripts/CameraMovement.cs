using UnityEngine;
using UnityEngine.EventSystems; //Necessary in order to get the camera to move around the player ---> Input access


/*  CameraMovement.cs* (Player Movement)
 * 
 *  this has nothing to do with moving the camera, but everything to do with moving the character based on
 *  the camera's look direction
 * 
 *  attached to the main camera (cinemachine) that orbits around the character
 * 
 */



public class CameraMovement : MonoBehaviour
{
    //Variables to hold different parts of the character
    public Transform character; //The transform of the character's parent object
    public Transform orientation;   //The transform of the character's orientation object
    public Transform characterObj;  //The transform of the object that holds the character model
    public Rigidbody characterRB;   //The rigidbody on the character's parent object

    [SerializeField]public float rotationSpeed; //speed at which we will rotate

    // Update is called once per frame
    void Update()
    {
        //Determines which way we are looking by taking the character's position and subtracting the camera's position
        Vector3 viewDirection = character.position - new Vector3(transform.position.x, character.position.y, transform.position.z);
        //Re-orients the character's orientation object forward property (used to determine what is "forward" relative to the character)
        //By setting it to the 'viewDirection'
        orientation.forward = viewDirection.normalized;

        //Get the player inputs for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Determine the direction of our input using the player inputs and the orientation object:
        //This will allow a 'move forward' to occur in the direction we are looking instead of relative to the direction we started
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //If we have an input direction, then we rotate the object holding the character model towards the input direction 
        if (inputDirection != Vector3.zero)
            characterObj.forward = Vector3.Slerp(characterObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
    }
}
