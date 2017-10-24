using UnityEngine;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {
    public Animator handAnimator;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            handAnimator.SetInteger("Act", 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            handAnimator.SetInteger("Act", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handAnimator.SetInteger("Act", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            handAnimator.SetInteger("Act", 3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            handAnimator.SetInteger("Act", 4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            handAnimator.SetInteger("Act", 5);
        }

    }
}
