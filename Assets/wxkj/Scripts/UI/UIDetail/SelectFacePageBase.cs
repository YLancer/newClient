using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectFacePageBase : UISceneBase
{
    public SelectFacePageDetail detail;

    public override void SetAllMemberValue()
    {
        detail.CloseButton_Image = transform.Find("CloseButton").gameObject.GetComponent<Image>();
        detail.CloseButton_Button = transform.Find("CloseButton").gameObject.GetComponent<Button>();
        detail.OKButton_Image = transform.Find("OKButton").gameObject.GetComponent<Image>();
        detail.OKButton_Button = transform.Find("OKButton").gameObject.GetComponent<Button>();
        detail.Gride_GridLayoutGroup = transform.Find("Gride").gameObject.GetComponent<GridLayoutGroup>();
        detail.FaceButton_Image = transform.Find("FaceButton").gameObject.GetComponent<Image>();
        detail.FaceButton_Button = transform.Find("FaceButton").gameObject.GetComponent<Button>();
        detail.SelectFlag_UIItem = transform.Find("SelectFlag").gameObject.GetComponent<UIItem>();

    }
}
