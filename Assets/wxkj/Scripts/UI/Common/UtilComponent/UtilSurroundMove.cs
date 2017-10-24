using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class UtilSurroundMove : MonoBehaviour
{
    public enum PosType
    {
        LeftBottom = 0,
        RightBottom = 1,
        RightTop = 2,
        LeftTop = 3,
    }
    /// <summary>
    /// 是否是顺时针
    /// </summary>
    public bool isClockwise = false;
    /// <summary>
    /// 是否是循环
    /// </summary>
    public bool isLoop = false;
    /// <summary>
    /// 起点
    /// </summary>
    public PosType startPoint = PosType.LeftBottom;
    /// <summary>
    /// 需要移动的对象
    /// </summary>
    public GameObject moveGameObject;
    RectTransform rectTransform;
    Dictionary<int, Vector3> mPath;
    List<PosType> typeList = new List<PosType>()
    {
        PosType.LeftBottom,
        PosType.RightBottom,
        PosType.RightTop,
        PosType.LeftTop,
    };
    public MovePaht movePath;
    public float speed = 80f;
    public AnimationCurve animationCure = AnimationCurve.Linear(0, 0, 1.0f, 1.0f);
    public Vector3 offset = Vector3.one * 10;
    RectTransform target;
    // Use this for initialization
    void Start()
    {
        if (moveGameObject == null)
            return;

        moveGameObject.SetActive(false);
        rectTransform = transform as RectTransform;
        target = moveGameObject.transform as RectTransform;
        GetMovePointList();
    }


    void GetMovePointList()
    {
        mPath = new Dictionary<int, Vector3>();
        foreach (PosType item in typeList)
            mPath.Add((int)item, GetBoundaryPoint(item));

        CreateMoveQueue();
    }

    void CreateMoveQueue()
    {
        int index = (int)startPoint;
        movePath = new MovePaht(mPath[index]);
        MovePaht next = movePath;
        //逆时针
        if (!isClockwise)
        {
            for (int i = index + 1; i < mPath.Count; i++)
            {
                next.next = new MovePaht(mPath[i]);
                next = next.next;
            }

            for (int i = 0; i < index; i++)
            {
                next.next = new MovePaht(mPath[i]);
                next = next.next;
            }
        }
        else
        {
            for (int i = index - 1; i >= 0; i--)
            {
                next.next = new MovePaht(mPath[i]);
                next = next.next;
            }

            for (int i = mPath.Count - 1; i > index; i--)
            {
                next.next = new MovePaht(mPath[i]);
                next = next.next;
            }
        }

        if (isLoop)
            next.next = movePath;

        moveGameObject.SetActive(true);
        target.anchoredPosition = movePath.points;
        MoveByPath(movePath.next);
    }



    void MoveByPath(MovePaht path)
    {
        if (path == null)
            return;

        float duration = Mathf.Abs(Vector2.Distance(target.anchoredPosition, path.points)) / this.speed;
        Vector3 pos = path.points;
        pos.z += offset.z;
        Tween tween = moveGameObject.transform.DOLocalMove(pos, duration);
        tween.SetEase(animationCure);
        tween.OnComplete(() =>
        {
            MoveByPath(path.next);
        });
    }


    Vector3 GetBoundaryPoint(PosType type)
    {
        Vector3 point = Vector3.zero;
        float factor = 0.5f;
        float factorX = 1f;
        float factorY = 1f;
        switch (type)
        {
            case PosType.LeftTop:
                factorX *= -1;
                break;
            case PosType.LeftBottom:
                factorY *= -1;
                factorX *= -1;
                break;
            case PosType.RightTop:
                break;
            case PosType.RightBottom:
                factorY *= -1;
                break;
            default:
                break;
        }

        point = rectTransform.anchoredPosition + new Vector2(rectTransform.sizeDelta.x * factorX * factor + factorX * offset.x, rectTransform.sizeDelta.y * factorY * factor + factorY * offset.y);
        return point;
    }
}

public class MovePaht
{
    public MovePaht next;
    public Vector3 points;
    public MovePaht(Vector3 point)
    {
        this.points = point;
    }
}
