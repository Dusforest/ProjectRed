using System.Collections;
using DG.Tweening;
using Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Elvenwood
{
    public interface IUIFuncKit 
    {
        void LoadSceneAsync(string sceneName);
        void DoFloatUIElementOnY(GameObject element, float height, float onceTime);
    }
    public class UIFuncKit : Singleton<UIFuncKit>, IUIFuncKit
    {
        private UIFuncKit()
        {
            
        }
        
        public void LoadSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                if (operation.progress >= 0.9f)
                    operation.allowSceneActivation = true;
            }
        }

        public void LoadSceneAsync(string sceneName, GameObject loadPanel, GameObject blackTransition, float blackTransitionTime)
        {
            blackTransition.SetActive(true);
            blackTransition.transform.GetComponent<Image>().DOFade(255f, blackTransitionTime / 2);
            loadPanel.SetActive(true);
            blackTransition.transform.GetComponent<Image>().DOFade(0f, blackTransitionTime / 2);

            Debug.Log("11");
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            while (operation.progress < 0.9f)
            {
               
            }
            blackTransition.transform.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>
            {
                operation.allowSceneActivation = true;
            });
        }

        public void DoFloatUIElementOnY(GameObject element, float height, float onceTime)
        {
            if (!element.activeInHierarchy) return;

            var position = element.transform.GetComponent<RectTransform>().anchoredPosition;

            element.transform.GetComponent<RectTransform>().DOAnchorPosY(position.y + height, onceTime / 2)
                .OnComplete(
                    () =>
                    {
                        element.transform.GetComponent<RectTransform>().DOAnchorPosY(position.y, onceTime / 2).OnComplete(
                            () =>
                            {
                                DoFloatUIElementOnY(element, height, onceTime);
                            });
                    });
        }
        
        
        /// <summary>
        /// 好像用不到了
        /// </summary>
        /// <param name="element"></param>
        /// <param name="onceTime"></param>
        public void DoBlackTransition(GameObject element, float onceTime)
        {
            if (!element.activeInHierarchy) return;

            element.transform.GetComponent<Image>().DOFade(255f, onceTime / 2).OnComplete(() =>
            {
                element.transform.GetComponent<Image>().DOFade(0f, onceTime / 2);
            });
        }
    }

}