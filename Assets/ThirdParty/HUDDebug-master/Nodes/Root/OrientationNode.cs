using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Playflock.Log.Node
{
    
    
    public class OrientationNode : HUDNode
    {
        
        public override void OnToggle(bool isOn)
        {
            base.OnToggle( isOn );
            Screen.orientation = isOn ? ScreenOrientation.Portrait : ScreenOrientation.Landscape;
        }
    }
}
