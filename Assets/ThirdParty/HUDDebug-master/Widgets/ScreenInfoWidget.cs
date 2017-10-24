using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Playflock.Log.Widget
{
    
    
    public class ScreenInfoWidget : HUDWidget
    {
        
        [UnityEngine.SerializeField()]
        private UnityEngine.UI.Text _horizontalText;
        
        [UnityEngine.SerializeField()]
        private UnityEngine.UI.Text _verticalText;
        
        public override void Initialize()
        {
        }
        
        public override void Draw()
        {
            _horizontalText.text = Screen.width.ToString();
            _verticalText.text = Screen.height.ToString();
        }
    }
}
