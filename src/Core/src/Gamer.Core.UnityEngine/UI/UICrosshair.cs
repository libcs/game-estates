using UnityEngine;
using UnityEngine.UI;

namespace Gamer.Core.UI
{
    [RequireComponent(typeof(Image))]
    public class UICrosshair : MonoBehaviour
    {
        Image _crosshair = null;

        public bool Enabled
        {
            get => _crosshair.enabled;
            set => _crosshair.enabled = value;
        }

        void Awake() => _crosshair = GetComponent<Image>();

        void Start()
        {
            var crosshairTexture = (Texture2D)null; // BaseEngine.instance.Asset.LoadTexture("target", true);
            _crosshair.sprite = UIUtils.CreateSprite(crosshairTexture);
        }

        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}
