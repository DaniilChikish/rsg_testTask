using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Content.Features.HUDModule
{
    public class SliderBarUIElement : MonoBehaviour, IFillable
    {
        [SerializeField] Slider _slider;
        [SerializeField] float _smoothTime;

        private float _realValue;
        private Coroutine _smoothRoutine;
        public float FillAmount
        {
            get => _realValue;
            set
            {
                _realValue = value;
                DoAnimation();
            }
        }

        private void DoAnimation()
        {
            if (_smoothTime > 0)
            {
                if (_smoothRoutine != null)
                {
                    StopCoroutine(_smoothRoutine);
                }
                _smoothRoutine = StartCoroutine(AnimationRoutine.FillAmountSinusInterpolation(_slider, _slider.value, _realValue, _smoothTime, null));
            }
            else
            {
                _slider.value = _realValue;
            }
        }
    }
}
