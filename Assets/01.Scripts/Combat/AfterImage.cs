using BGD.ObjPooling;
using UnityEngine;

namespace BGD.Players
{
    public class AfterImage : PoolableMono
    {
        [HideInInspector] public SpriteRenderer renderer;

        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (renderer.color.a <= 0)
                PoolingManager.Instance.Push(this);
        }

        public override void ResetItem()
        {
            
        }

    }
}
