using UnityEngine;
using System.Collections;

public class Notice_TRYmove : MonoBehaviour {

    private float moveSpeed = 0.8f;

    void Update()
    {
        this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        Vector3 position = this.transform.localPosition;
        if (position.x <= -1000f)
        {
            this.transform.localPosition = new Vector3(position.x + 600* 2, position.y, position.z);
        }
    }
}
