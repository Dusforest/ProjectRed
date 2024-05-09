using System;
using Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Elvenwood
{
    public class KeyedDoor : AbstractController
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (this.SendQuery(new KeyIsObtainQuery()))
                {
                    if (transform.GetComponent<Animator>())
                    {
                        transform.AddComponent<OnTriggerDestroyByAni>().DestroyByAni();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Debug.Log("你这钥匙不够啊");
                }
            }
        }
    }
}