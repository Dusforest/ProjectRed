#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Elvenwood
{
    [CustomEditor(typeof(ChunkManager))]
    public class ChunkManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            ChunkManager chunkManager = (ChunkManager)target;

            // 查找所有区块
            if (GUILayout.Button("Init Chunks"))
            {
                chunkManager.FindAllChunks();
                
                // 给每个Chunk创建一个trigger2D用于Chunk的 OnTriggerEnter2D，在计算完边界后要设置其略小与Chunk本身，以免出现玩家未完全脱离Chunk时回头，但摄像机没移动
                List<BoxCollider2D> chunkColliders = new List<BoxCollider2D>();

                List<Vector2> chunkCollidersSize = new List<Vector2>();
                foreach (Chunk chunk in chunkManager.allChunks)
                {
                    if (chunk.GetComponent<BoxCollider2D>() == null)
                    {
                        chunkColliders.Add(chunk.gameObject.AddComponent<BoxCollider2D>());
                    }
                    else
                    {
                        chunkColliders.Add(chunk.gameObject.GetComponent<BoxCollider2D>());
                    }
                }

                foreach (var collider in chunkColliders)
                {
                    chunkCollidersSize.Add(collider.size); //记录所有chunkCollidersSize
                }
                
                for(int i = 0;i<chunkColliders.Count;i++)
                {
                    chunkColliders[i].size = chunkCollidersSize[i] * chunkManager.chunkTrigger2DScale; // 让触发器稍微小于Chunk本身
                    // 调用每个Chunk的SetBounds方法来计算Bounds
                    chunkManager.allChunks[i].SetBounds();
                    // 标记Chunk已被修改，防止丢失更改
                }

                for(int i = 0;i<chunkColliders.Count;i++)
                {
                    chunkColliders[i].size =
                        chunkCollidersSize[i] * chunkManager.chunkCheckedTrigger2DScale; // 让触发器稍微大于Chunk本身
                }
                
                foreach (Chunk chunk in chunkManager.allChunks)
                {
                    // 动态查找相邻区块
                    chunk.FindAdjacentChunks();
                }

                for(int i = 0;i<chunkColliders.Count;i++)
                {
                    DestroyImmediate(chunkColliders[i]);
                }
                
                foreach (Chunk chunk in chunkManager.allChunks)
                {
                    // 标记Chunk已被修改，防止丢失更改
                    EditorUtility.SetDirty(chunk);
                }
            }
            
            
            //失活除默认初始或玩家存档点所处的Chunk以外的所有Chunk
            if (GUILayout.Button("SetActive DefaultChunks"))
            {
                chunkManager.CheckPlayerChunkEditor();
                chunkManager.defaultChunk = chunkManager.CurrentChunk;
                foreach (var chunk in chunkManager.allChunks)
                {
                    if (chunk != chunkManager.defaultChunk)
                    {
                        chunk.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
#endif