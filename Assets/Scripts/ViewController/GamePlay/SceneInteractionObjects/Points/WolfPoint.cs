using System;
using Framework;
using UnityEngine;

namespace Elvenwood
{
    public class WolfPoint : AbstractController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                this.SendCommand(new ObtainKeyCommand());
            }
        }
    }
}