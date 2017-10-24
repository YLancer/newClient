using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Playflock.Log.Widget
{


    public class FpsWidget : HUDWidget
    {
        public float frequency = 0.5f;

        [UnityEngine.SerializeField()]
        private UnityEngine.UI.Text _fpsText;

        private float _accum;
        private int _frames;
        private UnityEngine.Color _color = Color.white;

        public override void Initialize()
        {
            StartCoroutine(CalculateCoroutine());
        }

        public override void Draw()
        {
            _accum += Time.timeScale / Time.deltaTime;
            ++_frames;
        }

        private IEnumerator CalculateCoroutine()
        {
            while (true)
            {
                float fps = _accum / _frames;
                _fpsText.text = fps.ToString("f" + Mathf.Clamp(1, 0, 10));
                _color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.yellow : Color.red);
                _fpsText.color = _color;
                _accum = 0f;
                _frames = 0;
                yield return new WaitForSeconds(frequency);
            }
        }
    }
}
