using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFillComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _target;
        [SerializeField] private Origin _fillOrigin;
        [SerializeField] [Range(0, 1)] private float _fillAmount;

        public enum Origin
        {
            Bottom,
            Right,
            Top,
            Left
        }

        private void Reset()
        {
            _fillAmount = 1;
            Refresh();
        }

        public Origin FillOrigir
        {
            get => _fillOrigin;
            set
            {
                _fillOrigin = value;
                Refresh();
            }
        }

        public float FillAmount
        {
            get => _fillAmount;
            set
            {
                _fillAmount = Mathf.Clamp01(value);
                Refresh();
            }
        }

        public SpriteRenderer Target
        {
            get
            {
                if (_target == null)
                    _target = GetComponent<SpriteRenderer>();
                return _target;
            }
        }

        private void Refresh()
        {
            if (Target == null || _target.sharedMaterial == null)
                return;
        
            switch (_fillOrigin)
            {
                case Origin.Bottom:
                    {
                        Target.sharedMaterial.SetFloat("_MinY", 0);
                        Target.sharedMaterial.SetFloat("_MaxY", 1);
                        Target.sharedMaterial.SetFloat("_MinX", 0);
                        Target.sharedMaterial.SetFloat("_MaxX", 0);
                        break;
                    }
                case Origin.Top:
                    {
                        Target.sharedMaterial.SetFloat("_MinY", 1);
                        Target.sharedMaterial.SetFloat("_MaxY", 0);
                        Target.sharedMaterial.SetFloat("_MinX", 0);
                        Target.sharedMaterial.SetFloat("_MaxX", 0);
                        break;
                    }
                case Origin.Left:
                    {
                        Target.sharedMaterial.SetFloat("_MinX", 0);
                        Target.sharedMaterial.SetFloat("_MaxX", 1);
                        Target.sharedMaterial.SetFloat("_MinY", 0);
                        Target.sharedMaterial.SetFloat("_MaxY", 0);
                        break;
                    }
                case Origin.Right:
                    {
                        Target.sharedMaterial.SetFloat("_MinX", 1);
                        Target.sharedMaterial.SetFloat("_MaxX", 0);
                        Target.sharedMaterial.SetFloat("_MinY", 0);
                        Target.sharedMaterial.SetFloat("_MaxY", 0);
                        break;
                    }
            }
            Target.sharedMaterial.SetFloat("_Fill", _fillAmount);
        }
        private void OnValidate()
        {
            if (!Application.isPlaying)
                Refresh();
        }
    }
}
