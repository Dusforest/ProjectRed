using System;
using Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
    public class FallRock : AbstractController
    {
        private OnTriggerDestroyByAni mOnTriggerDestroyByAni;
        private Rigidbody2D mRigidbody2D;
        private Animator mAnimator;
        private GameObject mAfterShock;
        
        private void Start()
        {
            mOnTriggerDestroyByAni = transform.AddComponent<OnTriggerDestroyByAni>();
            mRigidbody2D = transform.GetComponent<Rigidbody2D>();
            mAnimator = transform.GetComponent<Animator>();
            mAfterShock = transform.Find("AfterShock").gameObject;

            mRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            
            FallDown();

        }
        

        private void FallDown()
        {
            mRigidbody2D.constraints = RigidbodyConstraints2D.None;
        }

        public void AfterShock()
        {
            mAfterShock.SetActive(true);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("MagicArrow"))
            {
                FallDown();
            }
            if (other.gameObject.CompareTag("Ground"))
            {
                mAnimator.Play("Broke");
                mOnTriggerDestroyByAni.DestroyByAni();
            }
            if (other.gameObject.CompareTag("Player"))
            {
                this.SendCommand(new HurtPlayerCommand(1));
                mAnimator.Play("Broke");
                mOnTriggerDestroyByAni.DestroyByAni();
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                //TODO HurtEnemy
                mAnimator.Play("Broke");
                mOnTriggerDestroyByAni.DestroyByAni();
            }
        }
    }
}

