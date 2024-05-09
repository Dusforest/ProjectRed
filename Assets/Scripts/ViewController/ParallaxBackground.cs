using UnityEngine;

namespace Elvenwood
{
    public class ParallaxBackground : AbstractController
    {
        [SerializeField] private Vector2 parallaxEffectMultiplier;
        private Transform mCameraTransform;
        private Vector3 mLastCameraPosition;
        
        void Start()
        {
            if (Camera.main != null) mCameraTransform = Camera.main.transform;
            mLastCameraPosition = mCameraTransform.position;
        }


        void Update()
        {
            var position = mCameraTransform.position;
            Vector3 deltaMovement = position - mLastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
            mLastCameraPosition = position;
        }
    }
}
