using System;
using Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elvenwood
{
    public class OnTriggerDestroyByAni : AbstractController
    {
        private Animator mAnimator;
        private Collider2D mCollider2D;

        private void Start()
        {
            mAnimator = GetComponent<Animator>();
            mCollider2D = GetComponent<Collider2D>();
        }

        public void DestroyByAni()
        {
            gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
            this.GetSystem<ITimeSystem>().AddDelayTask(ReturnAnimationTimer(), () =>
            {
                Destroy(gameObject);
            });
        }
        
        private float ReturnAnimationTimer()
        {
            AnimationClip[] clips = mAnimator.runtimeAnimatorController.animationClips;
            return clips[0].length;
        }
    }
}