using UnityEngine;
using System.Collections;

public class MJTimer : MonoBehaviour {
    public Renderer rnd;
    private Material mat;
    private float endTime = 0;
    public Vector2 offset = new Vector2();

    private void Start()
    {
        mat = rnd.material;
        Countdown(16);
    }
    public int CursorTime
    {
        get
        {
            float time = endTime - Time.realtimeSinceStartup;
            if (time<0)
            {
                time = 0;
            }
            return Mathf.FloorToInt(time);
        }
    }

    public void Countdown(int from)
    {
        endTime = Time.realtimeSinceStartup + from;
    }

    private void Update()
    {
        int count = CursorTime;
        int row = count / 4;
        int col = count % 4;
        offset.x = (3 - col) * 0.25f;
        offset.y = (1 + row) * 0.25f;
        mat.mainTextureOffset = offset;
    }
}
