using System;
using System.Linq;
using Framework;
using TMPro;
using UnityEngine;

namespace Elvenwood.Test
{
    public class JsonTest : AbstractController
    {
        public TextMeshProUGUI maxHp;
        public TextMeshProUGUI curHp;
        public TextMeshProUGUI defence;
        public TextMeshProUGUI atk;
        public TextMeshProUGUI speedScale;
        public TextMeshProUGUI jumpForceScale;
        public TextMeshProUGUI typeKeys;
        

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.SendCommand<TestSaveCommand>();
            }
        
            if (Input.GetKeyDown(KeyCode.L))
            {
                TestSaveData temp = this.SendQuery(new JsonTestQuery());
                maxHp.text = temp.MaxHp.ToString();
                curHp.text = temp.CurHp.ToString();
                defence.text = temp.Defence.ToString();
                atk.text = temp.Atk.ToString();
                speedScale.text = temp.SpeedScale + "";
                jumpForceScale.text = temp.JumpForceScale + "";
                typeKeys.text = temp.TypeKeys.ToString();
            }
        }
    }

}
