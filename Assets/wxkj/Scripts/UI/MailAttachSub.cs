using UnityEngine;
using System.Collections;

public class MailAttachSub : MailAttachSubBase
{
    public void SetValue(int attachType,int attachNum)
    {
        bool isGold = attachType == 1;
        bool isCard = attachType == 2;
        detail.CardIcon_Image.gameObject.SetActive(isCard);
        detail.GoldIcon_Image.gameObject.SetActive(isGold);

        if (attachNum >= 10000)
        {
            detail.Num_Text.text = "X"+ Utils.GetShotName(attachNum, 1);
        }else
        {
            detail.Num_Text.text = "X" + attachNum.ToString();
        }
    }
}
