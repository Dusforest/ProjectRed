using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elvenwood
{
	public class ChunkManager : AbstractController
	{
		[Tooltip("默认初始或玩家存档点所处的Chunk")]
		public Chunk defaultChunk;//TODO 读档时需要在PlayerModel里读取该值进行对Player的初始化
		[Tooltip("用于本身的触发器的缩放值，要小于1，具体小多少视情况而定")]
		public float chunkTrigger2DScale;
		[Tooltip("用于检测别的Chunk的触发器的缩放值，要大于1，具体大多少视情况而定")]
		public float chunkCheckedTrigger2DScale;

		public Transform playerTrans;

		public List<Chunk> allChunks; // 包含所有区块的列表

		public Chunk CurrentChunk { get; private set; }
		private CameraController mCameraController;

		private void Start()
		{
			if (playerTrans == null) playerTrans = PlayerManager.Instance.playerController.transform;
			if (Camera.main != null) mCameraController = Camera.main.GetComponent<CameraController>();
		}

		private void Update()
		{
			CheckPlayerChunk();
		}

		private void CheckPlayerChunk()
		{
			foreach (Chunk chunk in allChunks)
			{
				if (chunk.IsPositionInsideBounds(playerTrans.position))
				{
					if (CurrentChunk != chunk)
					{
						// 如果玩家进入了一个新的Chunk
						SetCurrentChunkAndSetActive(chunk);

					}
					return; // 找到当前Chunk后退出循环
				}
			}
		}

		public void SetCurrentChunk(Chunk newChunk)
		{
			// 更新当前区块
			CurrentChunk = newChunk;
		}

		public void SetCurrentChunkAndSetActive(Chunk newChunk)
		{
			if (CurrentChunk != null)
			{
				// 失活当前区块及其邻近区块
				CurrentChunk.SetActive(false);
			}

			// 更新当前区块
			CurrentChunk = newChunk;
			if (CurrentChunk != null)
			{
				// 激活新区块及其邻近区块
				CurrentChunk.SetActive(true);
			}
			//更新相机位置
			mCameraController.UpdateCurrentChunk(CurrentChunk);
		}

		// 调用这个方法来查找所有的区块
		public void FindAllChunks()
		{
			allChunks.Clear(); // 清除旧的列表
			foreach (Transform child in transform)
			{
				// 确保当前子物体不是ChunkManager自身
				if (child != transform)
				{
					Chunk chunk = child.GetComponent<Chunk>();
					if (chunk != null)
					{
						allChunks.Add(chunk);
					}
				}
			}
		}

#if UNITY_EDITOR
		public void CheckPlayerChunkEditor()
		{
			foreach (Chunk chunk in allChunks)
			{
				if (chunk.IsPositionInsideBounds(playerTrans.position))
				{
					if (CurrentChunk != chunk)
					{
						SetCurrentChunk(chunk);
					}
					return;
				}
			}
		}
#endif

	}
}