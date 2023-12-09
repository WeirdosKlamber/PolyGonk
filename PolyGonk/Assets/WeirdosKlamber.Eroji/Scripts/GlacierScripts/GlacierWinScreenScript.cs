using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

namespace WeirdosKlamber.PolyGonk.Glacier.GlacierWinScreen
{
    public class GlacierWinScreenScript : MonoBehaviour
    {
        public TextMeshProUGUI WinText;
        public TextMeshProUGUI LoseText;

        public GameObject WinInfo;
        public GameObject LoseInfo;
        public float Instructions1Delay;
        public float Instructions2Delay;

        public float ContinueButtonDelay;
        public GameObject ContinueButton;


        public GameObject GlacierMain;

        public GameObject GlacierButton;

        public AudioSource WinSFX;
        public AudioSource LoseSFX;

        private bool winloseonce = false;
        private bool buttononce = false;
        public string WhichInfo;


        // Start is called before the first frame update
        void Start()
        {


            GlacierButton.GetComponent<Button>().enabled = false;

            ContinueButton.SetActive(false);
            Time.timeScale = 0;
            
        }

        // Update is called once per frame
        void Update()
        {
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
            SingletonSimple.Instance.lastLevel = "Glacier";
            SceneManager.LoadScene("Main");


        }

        void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();            
        }

        void WinLose(int[] stuff)
        {
            if (winloseonce == false)
            {
                winloseonce = true;
                SingletonSimple.Instance.ClearText();
                if (stuff[0] == 1)
                {
                    SingletonSimple.Instance.level1Completed = true;
                    SingletonSimple.Instance.lesson2Completed = true;
                    SingletonSimple.Instance.SaveGame();
                    WinText.text = string.Format(WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("GlacierWin"), 0 - (Math.Round(stuff[1] / 100d, 0) * 100), stuff[2], stuff[3], stuff[4], stuff[5], stuff[6], stuff[7]);
                    WinSFX.Play();
                    WinInfo.SetActive(true);
                    SingletonSimple.Instance.TTSAddText("GlacierWin", 10); //it can't do strings with arguments
                }

                else
                {
                    SingletonSimple.Instance.level1Completed = true;
                    SingletonSimple.Instance.SaveGame();
                    LoseText.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("GlacierLose");
                    LoseSFX.Play();
                    LoseInfo.SetActive(true);
                    SingletonSimple.Instance.TTSAddText("GlacierLose", 10);
                }
            }
        }
    }
}