using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {
    public int Num = 1;
    public Vector3[] rotas;
    public void SetDice(int num)
    {
        if (num < 1 || num > rotas.Length - 1) return;
        this.Num = num;
        this.transform.localRotation = Quaternion.Euler(rotas[num - 1]);
        //switch (num)
        //{
        //    case 1:
        //        this.transform.localRotation = Quaternion.Euler(90, 0, 0);
        //        break;
        //    case 2:
        //        this.transform.localRotation = Quaternion.Euler(180, 0, 0);
        //        break;
        //    case 3:
        //        this.transform.localRotation = Quaternion.Euler(0, 0, -90);
        //        break;
        //    case 4:
        //        this.transform.localRotation = Quaternion.Euler(-90,0,0);
        //        break;
        //    case 5:
        //        this.transform.localRotation = Quaternion.Euler(0, 0, 90);
        //        break;
        //    default:
        //        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //        break;
        //}
    }
}
