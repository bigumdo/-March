using BGD.ObjPooling;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BGD.UI
{
    public class PopUpText : PoolableMono
    {
        [HideInInspector] public TextMesh textMesh;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _fadeTime;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            textMesh = GetComponent<TextMesh>();
            _meshRenderer.sortingLayerName = "UI";
        }

        private void OnEnable()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(_maxScale, 0.3f));
            seq.Append(transform.DOScale(0.7f, 0.3f));
            seq.Join(DOFadeTextMesh(_fadeTime));
            seq.AppendCallback(() => PoolingManager.Instance.Push(this));
        }

        private Tweener DOFadeTextMesh(float fadeDuration)
        {
            return DOTween.To(
                () => textMesh.color,
                x => textMesh.color = x,
                new Color(1, 1, 1, 0),
                fadeDuration
            );
        }

        public override void ResetItem()
        {
            transform.localScale = Vector3.one;
            textMesh.color = Vector4.one;
        }
    }
}
