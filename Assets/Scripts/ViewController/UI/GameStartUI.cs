using DG.Tweening;
using Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Elvenwood
{
    public class GameStartUI : AbstractController
    {
        public Button startBtn;
        public Button continueBtn;
        public Button optionBtn;
        public Button exitBtn;
        
        public GameObject loadPanel;
        public GameObject blackTransition;

        public OptionUI optionUI;




        public void Start()
        {
            this.GetModel<IUIElementModel>().BlackTransition.Value = blackTransition;
            this.GetModel<IUIElementModel>().LoadPanel.Value = loadPanel;

            blackTransition.GetComponent<Image>().DOFade(0f, 1f);

            if (startBtn)
            {
                startBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
                startBtn.onClick.AddListener(OnStartGame);
            }
            else Debug.Log("there is no startButton");

            if (exitBtn)
            {
                exitBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
                exitBtn.onClick.AddListener(OnExitGame);
            }
            else Debug.Log("there is no exitButton");

            if (optionBtn)
            {
                optionBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
                optionBtn.onClick.AddListener(OnShowOptionUI);
            }
            
            if (continueBtn)
            {
                continueBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
                continueBtn.onClick.AddListener(OnContinueGame);
            }
            
            var temp = this.SendQuery(new AchievementCountQuery());
        }

        public void OnStartGame()
        {
            // Debug.Log("开始新关卡");
            this.SendCommand<GameStartCommand>();
        }

        public void OnExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnContinueGame()
        {
            Debug.Log("继续游戏");
        }

        public void OnShowOptionUI()
        {
            Debug.Log("打开设置");
            optionUI.gameObject.SetActive(true);
        }
        

    }
}
