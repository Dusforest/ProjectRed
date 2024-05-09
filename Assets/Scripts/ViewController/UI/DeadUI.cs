using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Elvenwood
{
    public class DeadUI : AbstractController
    {
        public Button loadBtn;
        public Button exitBtn;
        
        public GameObject loadPanel;
        public GameObject blackTransition;
        
        public void Start()
        {
            blackTransition = this.GetModel<IUIElementModel>().BlackTransition.Value;
            loadPanel = this.GetModel<IUIElementModel>().LoadPanel.Value;

            blackTransition.GetComponent<Image>().DOFade(0f, 1f);

            if (loadBtn)
            {
                loadBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
                loadBtn.onClick.AddListener(OnLoadGame);
                
            }
            else Debug.Log("there is no loadButton");

            if (exitBtn)
            {
                exitBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
                exitBtn.onClick.AddListener(OnExitGame);
            }
            else Debug.Log("there is no exitButton");
            
            // var temp = this.SendQuery(new AchievementCountQuery());
            
            
        }

        public void OnLoadGame()
        {
            // Debug.Log("加载");
            this.SendCommand<GameLoadCommand>();
        }

        public void OnExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }

}
