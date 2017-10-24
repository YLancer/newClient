using UnityEngine;
using System.Collections;
using TMPro;

public class MJTable : MonoBehaviour
{
    public Animation ShuffleAnimation;
    public Animation DiceAnimation;
    public Transform table;
    public TextMeshPro roomNum;
    public TextMeshPro baseScore;
    public GameObject timer;

    public Dice dice1;
    public Dice dice2;

    public Renderer[] indicators;
    public Material onMat;
    public Material offMat;

    private int diceNum = -1;
    public int DiceNum
    {
        get
        {
            return diceNum;
        }
    }

    private Material mat;
    private float endTime = 0;
    private Vector2 offset = new Vector2();

    private void Start()
    {
        Renderer r = timer.GetComponent<Renderer>();
        mat = r.material;
        //Countdown(16);
    }
    public int CursorTime
    {
        get
        {
            float time = endTime - Time.realtimeSinceStartup;
            if (time < 0)
            {
                time = 0;
            }
            return Mathf.FloorToInt(time);
        }
    }

    public void SetDirection(int position)
    {
        if (RoomMgr.IsVip2Room())
        {
            table.rotation = Quaternion.Euler(0, 180 * position, 0);
        }
        else
        {
            table.rotation = Quaternion.Euler(0, 90 * position, 0);
        }
        //timer.SetActive(false);
    }

    public void PlayShuffle()
    {
        ShuffleAnimation.Play("Shuffle");
        Game.SoundManager.PlayShuffle();
    }
    Coroutine timerCoroutine = null;

    private void UpdateTime()
    {
        int count = CursorTime;
        int row = count / 4;
        int col = count % 4;
        offset.x = col * 0.25f;
        offset.y = 1 - row * 0.25f;
        mat.mainTextureOffset = offset;
    }

    public void ShowCountdown(int from,int position = 0)
    {
        Game.MJMgr.ActivePosition = position;
        EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);

        timer.SetActive(true);
        SetIndicator(position);
        //timer.transform.localRotation = Quaternion.Euler(0, 0, -90 * index);

        endTime = Time.realtimeSinceStartup + from;

        UpdateTime();
        Game.StopDelay(timerCoroutine);
        timerCoroutine = Game.DelayLoop(from, 1, (i) =>
        {
            UpdateTime();
        });
    }

    public void HideCountdown()
    {
        timer.SetActive(false);
    }

    public int Dice(int diceIndex1, int diceIndex2)
    {
        //int diceIndex1 = Random.Range(1, 6);
        //int diceIndex2 = Random.Range(1, 6);
        DiceAnimation.Play("Dice");
        Debug.LogFormat("Dice:[{0},{1}]", diceIndex1, diceIndex2);
        dice1.SetDice(diceIndex1);
        dice2.SetDice(diceIndex2);
        diceNum = Mathf.Min(diceIndex1, diceIndex2);

        MJCardGroup.GetStartGroup();
        return diceNum;
    }

    public void SetIndicator(int position = -1)
    {
        indicators[0].material = offMat;
        indicators[1].material = offMat;
        indicators[2].material = offMat;
        indicators[3].material = offMat;

        if(position != -1)
        {
            if (RoomMgr.IsVip2Room())
            {
                indicators[position*2].material = onMat;
            }
            else
            {
                indicators[position].material = onMat;
            }
        }
    }
}
