using System;
using System.Collections;
using System.Collections.Generic;
using Elvenwood;
using Framework;
using UnityEngine;

public class UIManager : AbstractController
{
    public static UIManager Instance;
    
    //角色状态UI
    public GameObject playerStateUI;
    
    //游戏设置UI
    public GameObject optionUI;

    public GameObject startUI;
    //死亡UI
    public GameObject deadUI;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        //startUI.SetActive(true);
        playerStateUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void OnShowOption()
    {
        // Debug.Log("打开设置");
        optionUI.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void OnHideOption()
    {
        optionUI.SetActive(false);
        Time.timeScale = 1f;
        
    }
}
