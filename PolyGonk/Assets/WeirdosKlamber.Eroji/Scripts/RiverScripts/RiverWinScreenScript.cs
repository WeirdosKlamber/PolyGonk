using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace WeirdosKlamber.PolyGonk.River
{
    public class RiverWinScreenScript : MonoBehaviour
    {
        public TextMeshProUGUI WinText;
        public TextMeshProUGUI LoseText;

        public GameObject WinInfo;
        public GameObject LoseInfo;
        public float Instructions1Delay;
        public float Instructions2Delay;

        public float ContinueButtonDelay;
        public GameObject ContinueButton;
        public GameObject WhiteBox;

        public GameObject RiverMain;


        private bool winloseonce = false;
        private bool buttononce = false;
        public string WhichInfo;



        // Start is called before the first frame update
        void Start()
        {

            // InfoText.text = "Change into language thing";
            ContinueButton.SetActive(false);
            //Time.timeScale = 0;

        }

        // Update is called once per frame
        void Update()
        {/*
        Instructions1Delay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
        if (Instructions1Delay < 0)
        {
            Instructions1.SetActive(true);

        }
        Instructions2Delay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
        if (Instructions2Delay < 0)
        {
            Instructions2.SetActive(true);

        }

        */

            ContinueButtonDelay -= Time.unscaledDeltaTime;  //game paused so delta won't work'
            if (ContinueButtonDelay < 0 && buttononce == false)
            {
                ContinueButton.SetActive(true);
                buttononce = true;

            }


        }

        void ContinuePressed()
        {
            Time.timeScale = 1;
            SingletonSimple.Instance.lastLevel = "River";
            SceneManager.LoadScene("Main");
        }

        void DoWinScreen(int[] winfo)
        {
            WhiteBox.SetActive(true);
            SingletonSimple.Instance.ClearText();
            if (winfo[0]==0)//lose
            {
                if (winfo[1] == 0) //normal death
                {
                   // LoseText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverLose");
                   // SingletonSimple.Instance.TTSAddText("RiverLose", 10);
                }
                else  //eaten
                {
                    SingletonSimple.Instance.lesson6Completed = true;
                    SingletonSimple.Instance.SaveGame();
                   // LoseText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverLoseCroc");
                   // SingletonSimple.Instance.TTSAddText("RiverLoseCroc", 10);
                }


                LoseInfo.SetActive(true);
            }

            else //win
            {
                SingletonSimple.Instance.lesson6Completed = true;
                SingletonSimple.Instance.level6Completed = true;
                SingletonSimple.Instance.SaveGame();
                int vict = 100;
/*                if (SingletonSimple.Instance.riverBabyMode)
                {
                    vict = 1;
                }*/
               // WinText.text = string.Format( WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverWin"),vict*10, winfo[2], winfo[3], winfo[4], winfo[4]*vict, winfo[1], winfo[5]);
               // SingletonSimple.Instance.TTSAddText("RiverWin", 10);
                WinInfo.SetActive(true);

            }

        }
        void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }

    }


}