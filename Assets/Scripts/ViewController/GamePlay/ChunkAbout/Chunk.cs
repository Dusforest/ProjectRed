using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public enum ChunkType
	{
		Standard, // 标准区块
		HorizontalScroll, // 横向卷轴区块
		VerticalScroll, // 纵向卷轴区块
		Boss // BOSS区块
	}

	public class Chunk : MonoBehaviour
	{
		public ChunkType type; // 区块的类型
		public Bounds bounds; // 区块的边界，用于确定摄像机移动限制
		public List<Chunk> adjacentChunks; // 邻近区块的列表

		// 当区块启动时获取摄像机控制器的引用
		private void Start()
		{
			var mainCamera = Camera.main;
			if (mainCamera != null)
			{
				// 检索主摄像机上的CameraController组件
				var cameraController = mainCamera.GetComponent<CameraController>();
				if (cameraController == null)
				{
					// 如果CameraController不存在，则打印警告信息
					Debug.LogWarning("CameraController component not found on main camera.");
				}
			}
			else
			{
				Debug.LogWarning("No Main Camera found in the scene.");
			}
		}

		// 检测给定的位置是否在区块的Bounds内
		public bool IsPositionInsideBounds(Vector3 position)
		{
			return bounds.Contains(position);
		}


		// 激活或失活区块
		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
			// 也激活或失活邻近区块
			foreach (var chunk in adjacentChunks)
			{
				chunk.gameObject.SetActive(active);
			}
		}


#if UNITY_EDITOR
		// 调用这个方法来设置Chunk的Bounds
		public void SetBounds()
		{
			BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
			if (boxCollider != null)
			{
				bounds = boxCollider.bounds;
			}
			else
			{
				Debug.LogError("BoxCollider2D component not found on the Chunk object.", this);
			}
		}

		// 在编辑器中调用，用于查找和存储相邻区块
		public void FindAdjacentChunks()
		{
			adjacentChunks.Clear();
			// 检测自身范围内有无其他触发器
			Collider2D[] hits = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
			foreach (var hit in hits)
			{
				Chunk hitChunk = hit.GetComponent<Chunk>();
				// 如果hitChunk不为空并且不是当前chunk本身，且adjacentChunks列表中还没有它
				if (hitChunk != null && hitChunk != this && !adjacentChunks.Contains(hitChunk))
				{
					adjacentChunks.Add(hitChunk);
				}
			}
		}
#endif
	}
}