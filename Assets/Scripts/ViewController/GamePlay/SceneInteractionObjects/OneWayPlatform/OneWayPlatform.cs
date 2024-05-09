using Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
    [RequireComponent(typeof(Collider2D))]
    public class OneWayPlatform : AbstractController
    {
        private PlatformEffector2D mEffector;
        private Collider2D mPlatformCollider;

        [Tooltip("禁用碰撞的持续时间")]
        public float disableDuration = 0.3f;

        public bool isDisable;
        [SerializeField] private float currentTime;

        private void Start()
        {
            mEffector = GetComponent<PlatformEffector2D>();
            mPlatformCollider = GetComponent<Collider2D>();
            mPlatformCollider.enabled = true;

            currentTime = disableDuration;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                DisableCollision();
            }

            if (isDisable)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    mPlatformCollider.enabled = true;
                    isDisable = false;
                    currentTime = disableDuration;
                }
            }
        }

        private void DisableCollision()
        {
            mPlatformCollider.enabled = false;
            isDisable = true;
            // this.GetSystem<ITimeSystem>().AddDelayTask(0.3f,()=>
            // {
            //     mPlatformCollider.enabled = true;
            // });
        }
    }
}