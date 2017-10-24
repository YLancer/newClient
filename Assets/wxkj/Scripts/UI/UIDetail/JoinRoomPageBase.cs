using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoinRoomPageBase : UISceneBase
{
    public JoinRoomPageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.Text0_Text = transform.Find("InputGroup/Num0/Text0").gameObject.GetComponent<Text>();
        detail.Num0_Image = transform.Find("InputGroup/Num0").gameObject.GetComponent<Image>();
        detail.Text1_Text = transform.Find("InputGroup/Num1/Text1").gameObject.GetComponent<Text>();
        detail.Num1_Image = transform.Find("InputGroup/Num1").gameObject.GetComponent<Image>();
        detail.Text2_Text = transform.Find("InputGroup/Num2/Text2").gameObject.GetComponent<Text>();
        detail.Num2_Image = transform.Find("InputGroup/Num2").gameObject.GetComponent<Image>();
        detail.Text3_Text = transform.Find("InputGroup/Num3/Text3").gameObject.GetComponent<Text>();
        detail.Num3_Image = transform.Find("InputGroup/Num3").gameObject.GetComponent<Image>();
        detail.Text4_Text = transform.Find("InputGroup/Num4/Text4").gameObject.GetComponent<Text>();
        detail.Num4_Image = transform.Find("InputGroup/Num4").gameObject.GetComponent<Image>();
        detail.Text5_Text = transform.Find("InputGroup/Num5/Text5").gameObject.GetComponent<Text>();
        detail.Num5_Image = transform.Find("InputGroup/Num5").gameObject.GetComponent<Image>();
        detail.InputGroup_GridLayoutGroup = transform.Find("InputGroup").gameObject.GetComponent<GridLayoutGroup>();
        detail.Button1_Image = transform.Find("BtnGroup/Button1").gameObject.GetComponent<Image>();
        detail.Button1_Button = transform.Find("BtnGroup/Button1").gameObject.GetComponent<Button>();
        detail.Button2_Image = transform.Find("BtnGroup/Button2").gameObject.GetComponent<Image>();
        detail.Button2_Button = transform.Find("BtnGroup/Button2").gameObject.GetComponent<Button>();
        detail.Button3_Image = transform.Find("BtnGroup/Button3").gameObject.GetComponent<Image>();
        detail.Button3_Button = transform.Find("BtnGroup/Button3").gameObject.GetComponent<Button>();
        detail.Button4_Image = transform.Find("BtnGroup/Button4").gameObject.GetComponent<Image>();
        detail.Button4_Button = transform.Find("BtnGroup/Button4").gameObject.GetComponent<Button>();
        detail.Button5_Image = transform.Find("BtnGroup/Button5").gameObject.GetComponent<Image>();
        detail.Button5_Button = transform.Find("BtnGroup/Button5").gameObject.GetComponent<Button>();
        detail.Button6_Image = transform.Find("BtnGroup/Button6").gameObject.GetComponent<Image>();
        detail.Button6_Button = transform.Find("BtnGroup/Button6").gameObject.GetComponent<Button>();
        detail.Button7_Image = transform.Find("BtnGroup/Button7").gameObject.GetComponent<Image>();
        detail.Button7_Button = transform.Find("BtnGroup/Button7").gameObject.GetComponent<Button>();
        detail.Button8_Image = transform.Find("BtnGroup/Button8").gameObject.GetComponent<Image>();
        detail.Button8_Button = transform.Find("BtnGroup/Button8").gameObject.GetComponent<Button>();
        detail.Button9_Image = transform.Find("BtnGroup/Button9").gameObject.GetComponent<Image>();
        detail.Button9_Button = transform.Find("BtnGroup/Button9").gameObject.GetComponent<Button>();
        detail.ButtonClear_Image = transform.Find("BtnGroup/ButtonClear").gameObject.GetComponent<Image>();
        detail.ButtonClear_Button = transform.Find("BtnGroup/ButtonClear").gameObject.GetComponent<Button>();
        detail.Button0_Image = transform.Find("BtnGroup/Button0").gameObject.GetComponent<Image>();
        detail.Button0_Button = transform.Find("BtnGroup/Button0").gameObject.GetComponent<Button>();
        detail.ButtonDel_Image = transform.Find("BtnGroup/ButtonDel").gameObject.GetComponent<Image>();
        detail.ButtonDel_Button = transform.Find("BtnGroup/ButtonDel").gameObject.GetComponent<Button>();
        detail.BtnGroup_GridLayoutGroup = transform.Find("BtnGroup").gameObject.GetComponent<GridLayoutGroup>();
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.JoinButton_Image = transform.Find("JoinButton").gameObject.GetComponent<Image>();
        detail.JoinButton_Button = transform.Find("JoinButton").gameObject.GetComponent<Button>();

    }
}
