using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ButtonController.cs* (Rhythm Game Script)
 * <><><><><><><><><><><><><><><><><><>
 * 
 *This script changes the materials the materials of the buttons
 * inside of the rhythm game when hit.
 * 
 */

public class ButtonController : MonoBehaviour
{
	//Classifying parts of the button mesh
	private MeshRenderer bMeshRenderer;
	private MeshRenderer cMeshRenderer;

	// These variables are set in the Unity Inspector
	[SerializeField]public Material defaultButtonMat;
	[SerializeField]public Material defaultCenterMat;
	[SerializeField]public Material hitMat;

	[SerializeField]public KeyCode keyToPress;

	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {	//Getting the mesh renderer for the buttons (external and internal components of the button)
		bMeshRenderer = this.GetComponent<MeshRenderer>();
		cMeshRenderer = this.transform.Find("Button_Center").GetComponent<MeshRenderer>();

		anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {	//If we press a key down --> an animation the material will alter
		if (Input.GetKeyDown(keyToPress))
		{
			bMeshRenderer.material = hitMat;
			cMeshRenderer.material = hitMat;
			
			//Additional 'button pressed' animation
			anim.Play("ButtonPressAnim");
		}
		//If we release the key --> the buttons will return to their normal materials
		if (Input.GetKeyUp(keyToPress))
		{
			bMeshRenderer.material = defaultButtonMat;
			cMeshRenderer.material = defaultCenterMat;
		}
    }
}
