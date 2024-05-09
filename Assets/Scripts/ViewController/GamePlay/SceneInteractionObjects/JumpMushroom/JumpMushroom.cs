using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elvenwood
{
    public class JumpMushroom : MonoBehaviour
    {
        public float jumpVelocity;
        private PlayerController mPlayerController;

        private void Start()
        {
            mPlayerController = PlayerManager.Instance.playerController;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                mPlayerController.SetVelocity(mPlayerController.rb.velocity.x,jumpVelocity);
            }
        }
    }

}
