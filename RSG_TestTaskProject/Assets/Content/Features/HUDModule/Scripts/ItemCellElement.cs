using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.HUDModule
{
    public class ItemCellElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Button _button;
        private Action _onClick;

        private void Start()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke();
        }

        public void Setup(Sprite image, int count, Action onClick)
        {
            _image.sprite = image;
            _count.text = count.ToString();
            _onClick = onClick;
        }
        public void SetCount(int count)
        {
            _count.text = count.ToString();
        }
    }
}
