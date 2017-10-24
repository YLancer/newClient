using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Playflock.Log.Widget
{


    public class HeapWidget : HUDWidget
    {
        public float frequency = 0.5f;

        [UnityEngine.SerializeField()]
        private UnityEngine.UI.Text _heapText;
        [UnityEngine.SerializeField()]
        private UnityEngine.UI.Button _bodyBtn;
        private string _heapSize;

        public override void Initialize()
        {
            _bodyBtn.onClick.AddListener( OnBodyBtnClick );
            StartCoroutine( CalculateCoroutine() );
        }

        public override void Draw()
        {
        }

        public override void OnDismiss()
        {
            base.OnDismiss();
            _bodyBtn.onClick.RemoveListener( OnBodyBtnClick );
        }

        private IEnumerator CalculateCoroutine()
        {
            while ( true )
            {
                //round to float "f" + Mathf.Clamp(3, 0, 10)
                _heapSize = ((int)(GC.GetTotalMemory( false ) / 1024f / 1024f)) + "Mb";
                _heapText.text = _heapSize;
                yield return new WaitForSeconds( frequency );
            }
        }

        private void OnBodyBtnClick()
        {
            GC.Collect();
        }
    }
}
