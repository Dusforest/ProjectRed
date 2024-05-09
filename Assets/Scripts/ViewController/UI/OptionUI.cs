using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Button backBtn;
    public Button exitBtn;

    public Slider bgmSlider;
    public Slider fxSlider;

    private void Start()
    {
        if (backBtn)
        {
            //Debug.Log("BackBtn绑定");
            backBtn.onClick.AddListener(OnHideOption);
            backBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
        }
        else
        {
            Debug.Log("No backBtn");
        }

        if (exitBtn)
        {
            exitBtn.onClick.AddListener(OnExitGame);
            exitBtn.onClick.AddListener(AudioManager.Instance.OnPlayConfirmAudio);
        }
        else
        {
            Debug.Log("No exitBtn");
        }

        if (bgmSlider)
        {
            bgmSlider.onValueChanged.AddListener(AudioManager.Instance.OnBGMVolumeChange);
        }

        if (fxSlider)
        {
            fxSlider.onValueChanged.AddListener(AudioManager.Instance.OnFXVolumeChange);
        }
        
        //初始化Slider的值
        UpdateSliderValue();
    }

    private void OnEnable()
    {
        //Debug.Log("打开设置");
        UpdateSliderValue();
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    
    public void OnHideOption()
    {

        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnShowOption()
    {

        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateSliderValue()
    {
        // bgmSlider.value = AudioManager.Instance.bgmSource.volume;
        // Debug.Log(bgmSlider.value);
        // fxSlider.value = 10f/(10-AudioManager.Instance.fxSource.volume);
    }
}
