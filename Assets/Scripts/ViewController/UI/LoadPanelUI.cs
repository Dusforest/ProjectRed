using System;
using System.Collections;
using UnityEngine;

namespace Elvenwood
{
    public class LoadPanelUI : AbstractController
    {
        public GameObject[] loads;

        private void OnEnable()
        {
            StartCoroutine(FloatImage());
        }
        
        IEnumerator FloatImage()
        {
            foreach (var load in loads)
            {
                UIFuncKit.Instance.DoFloatUIElementOnY(load, 50, 2f);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}