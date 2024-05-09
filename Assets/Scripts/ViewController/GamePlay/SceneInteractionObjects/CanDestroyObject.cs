using System;
using Elvenwood;
using UnityEngine;

namespace Elvenwood
{
    public class CanDestroyObject : AbstractController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("MagicArrow"))
            {
                Destroy(gameObject);
            }
        }
    }
}