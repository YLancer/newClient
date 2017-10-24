using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Playflock.Log.Widget
{
    
    
    public class MemoryWidget : HUDWidget
    {
        
        public float frequency = 0.5f;
        
        [UnityEngine.SerializeField()]
        private UnityEngine.UI.Text _memoryText;
        
        private string _memorySize;
        
        public override void Initialize()
        {
            StartCoroutine( CalculateCoroutine() );
        }
        
        public override void Draw()
        {
        }
        private IEnumerator CalculateCoroutine()
        {
            while (true)
            {
                //_memoryText.text = DeviceInfo.usedRamMemory + "Mb";
                _memoryText.text = "TODO";
                yield return new WaitForSeconds(frequency);
            }
        }
    }
}
