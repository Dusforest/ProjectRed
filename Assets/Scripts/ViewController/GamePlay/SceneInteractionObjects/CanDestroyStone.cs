using System.Collections;
using System.Collections.Generic;
using Elvenwood;
using UnityEngine;

public class CanDestroyStone : AbstractController
{
    public ParticleSystem destroyEffect;

    public GameObject destroyStone;

    public Collider2D collider;
    public Collider2D triggerCollider;

    public bool isBreak;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBreak)
        {
            return;
        }
        if (other.CompareTag("MagicArrow"))
        {
            // destroyEffect.gameObject.SetActive(true);
            destroyEffect.Play();
            destroyStone.SetActive(false);
            isBreak = true;
            
            triggerCollider.enabled = false;           
            if (collider)
            {
                collider.enabled = false;

            }
        }
    }
}
