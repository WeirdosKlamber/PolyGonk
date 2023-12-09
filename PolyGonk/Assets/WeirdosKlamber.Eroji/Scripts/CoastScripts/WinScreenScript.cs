using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace WeirdosKlamber.PolyGonk.Coast
{
    public class WinScreenScript : MonoBehaviour
    {
        public TextMeshProUGUI WinText;
        public TextMeshProUGUI LoseText;

        public GameObject WinInfo;
        public GameObject LoseInfo;
        public float Instructions1Delay;
        public float Instructions2Delay;

        public float ContinueButtonDelay;
        public GameObject ContinueButton;


        public GameObject CoastMain;

        public GameObject SandButton;
        public GameObject RipRapButton;
        public GameObject GabionButton;
        public GameObject GroyneButton;
        public GameObject SeaWallButton;


        private bool winloseonce = false;
        private bool buttononce = false;
        public string WhichInfo;

        public AudioSource WinSFX;
        public AudioSource LoseSFX;


        // Start is called before the first frame update
        void Start()
        {

            /*
            SandButton.SetActive(true);
            RipRapButton.SetActive(true);
            GabionButton.SetActive(true);
            GroyneButton.SetActive(true);
            SeaWallButton.SetActive(true);*/
            SandButton.GetComponent<Button>().enabled = false;
            RipRapButton.GetComponent<Button>().enabled = false;
            GabionButton.GetComponent<Button>().enabled = false;
            GroyneButton.GetComponent<Button>().enabled = false;
            SeaWallButton.GetComponent<Button>().enabled = false;


            // InfoText.text = "Change into language thing";
            ContinueButton.SetActive(false);
            Time.timeScale = 0;

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
            SingletonSimple.Instance.lastLevel = "Coast";
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
                    SingletonSimple.Instance.level3Completed = true;
                    SingletonSimple.Instance.lesson3Completed = true;
                    SingletonSimple.Instance.SaveGame();
                   // int vict = 1000;
/*                    if (SingletonSimple.Instance.coastBabyMode)
                    {
                        vict = 10;
                    }*/
                   // WinText.text = string.Format(WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastWin"), vict, stuff[1], stuff[1] * (vict/10), stuff[2] * (vict / 1000), vict + stuff[1] * (vict / 10) + stuff[2] * (vict / 1000));
                    WinSFX.Play();
                   // SingletonSimple.Instance.TTSAddText("CoastWin", 10);
                    WinInfo.SetActive(true);

                }

                else
                {
                    SingletonSimple.Instance.lesson3Completed = true;
                    SingletonSimple.Instance.SaveGame();
                   // LoseText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("CoastLose");
                    LoseSFX.Play();
                    LoseInfo.SetActive(true);
                   // SingletonSimple.Instance.TTSAddText("CoastLose", 10);
                }
            }
        }

    }

}