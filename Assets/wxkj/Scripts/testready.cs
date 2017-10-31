using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class testready : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        print("   ---- click -----   ");
        Game.SocketGame.DoREADYL(1, 0);
    }
}
