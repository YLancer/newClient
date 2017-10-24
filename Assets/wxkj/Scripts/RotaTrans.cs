using UnityEngine;
using System.Collections;

public class RotaTrans : MonoBehaviour {
	public Vector3 rotaDir;
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (rotaDir);
	}
}
