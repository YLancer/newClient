using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        for (int j = 1; j <= 9; j++)
        {
            int a = 1 * 10 + j;
            int b = Utils.CardC2S(a);
            print(a + ":" + b);
        }
        for (int j = 1; j <= 9; j++)
        {
            int a = 2 * 10 + j;
            int b = Utils.CardC2S(a);
            print(a + ":" + b);
        }
        for (int j = 1; j <= 9; j++)
        {
            int a = 4 * 10 + j;
            int b = Utils.CardC2S(a);
            print(a + ":" + b);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
