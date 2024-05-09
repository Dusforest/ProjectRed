using System;
using UnityEngine;

namespace Elvenwood
{
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans; 
        private Chunk mCurrentChunk; 
        private Vector3 mVelocity = Vector3.zero; // 用于SmoothDamp方法的速度引用
        [SerializeField] 
        private float smoothSpeed = 0.125f; // 平滑移动的速度
        [SerializeField] 
        private Vector3 offset = new Vector3(1f, 0f, -10f); // 相机对玩家的偏移
        private float mCameraHeight;
        private float mCameraWidth;

        private void Start()
        {
            mPlayerTrans = PlayerManager.Instance.playerController.transform;
            if (Camera.main != null)
            {
                var main = Camera.main;
                mCameraHeight = 2f * main.orthographicSize;
                mCameraWidth = mCameraHeight * main.aspect;
            }
        }
        
        // TODO 如果玩家的碰撞体没有完全脱离上一个Chunk的Trigger2D的话，再回头，摄像机不会回到上一个Chunk
        public void UpdateCurrentChunk(Chunk newChunk)
        {
            Debug.Log("更新相机位置");
            mCurrentChunk = newChunk; 
        }

        public Chunk GetCurChunk()
        {
            return mCurrentChunk;
        }

        private void LateUpdate()
        {
            if (!mPlayerTrans || mCurrentChunk == null)
            {
                return; 
            }

            // 计算期望位置
            offset = Math.Abs(mPlayerTrans.rotation.y - (-180)) < 0.01f ? new Vector3(-1f, 0f, -10) : new Vector3(1f, 0f, -10);
            Vector3 desiredPosition = mPlayerTrans.position + offset;
            
            // 根据当前区块类型决定摄像机行为
            switch (mCurrentChunk.type)
            {
                case ChunkType.Standard:
                    // 标准区块，摄像机固定在区块的中心
                    desiredPosition = new Vector3(mCurrentChunk.bounds.center.x, mCurrentChunk.bounds.center.y,
                        desiredPosition.z);
                    break;
                case ChunkType.HorizontalScroll:
                    // 横向卷轴，摄像机仅水平跟随玩家，但限制在区块边界内
                    float leftBound = mCurrentChunk.bounds.min.x + mCameraWidth / 2;
                    float rightBound = mCurrentChunk.bounds.max.x - mCameraWidth / 2;
                    desiredPosition.x = Mathf.Clamp(desiredPosition.x, leftBound, rightBound);
                    desiredPosition.y = mCurrentChunk.bounds.center.y;
                    break;
                case ChunkType.VerticalScroll:
                    // 纵向卷轴，摄像机仅垂直跟随玩家，但限制在区块边界内
                    float lowerBound = mCurrentChunk.bounds.min.y + mCameraHeight / 2;
                    float upperBound = mCurrentChunk.bounds.max.y - mCameraHeight / 2;
                    desiredPosition.y = Mathf.Clamp(desiredPosition.y, lowerBound, upperBound);
                    desiredPosition.x = mCurrentChunk.bounds.center.x;
                    break;
                case ChunkType.Boss:
                    // BOSS区块，摄像机自由跟随玩家，但保持在区块边界内
                    desiredPosition.x = Mathf.Clamp(desiredPosition.x, mCurrentChunk.bounds.min.x + mCameraWidth / 2,
                        mCurrentChunk.bounds.max.x - mCameraWidth / 2);
                    desiredPosition.y = Mathf.Clamp(desiredPosition.y, mCurrentChunk.bounds.min.y + mCameraHeight / 2,
                        mCurrentChunk.bounds.max.y - mCameraHeight / 2);
                    break;
            }

            // 使用SmoothDamp函数平滑地移动摄像机到目标位置
            var smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref mVelocity, smoothSpeed);
            transform.position = new Vector3(smoothPosition.x, smoothPosition.y, offset.z); // 更新摄像机的位置，保持原始的z偏移
        }
    }
}