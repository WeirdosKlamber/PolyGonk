using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LoLSDK;
using SimpleJSON;

namespace WeirdosKlamber.PolyGonk
    { 
    public class TotalVictory : MonoBehaviour
    {
        public TextMeshProUGUI CongratulationsTxt;
        public TextMeshProUGUI EndTxt;
        public AudioSource clickFX;
        public AudioSource applauseFX;
        public AudioSource KazooFX;

        public GameObject FadeOut;
        public GameObject LabelBGD;
        public GameObject EndButton;

        public GameObject Buttons;
        public GameObject Button5;
        public GameObject Button6;
        
        private float appearTimer = 0f;
        private float Fader = 0f;
            // Start is called before the first frame update
        void Start()
        {
            SingletonSimple.Instance.ClearText();
            if (SingletonSimple.Instance.SumScores() > 214)
            {
                CongratulationsTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Congratulations4") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations4", 7f);
            }
            else if (SingletonSimple.Instance.SumScores() > 183)
            {
                CongratulationsTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Congratulations3") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations3", 7f);
            }
            else if (SingletonSimple.Instance.SumScores() > 160)
            {
                CongratulationsTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Congratulations2") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations2", 7f);
            }
            else
            {
                CongratulationsTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Congratulations1") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations1", 7f);
            }
            EndTxt.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("End");
            applauseFX.Play();
        }

        // Update is called once per frame
        void Update()
        {
            appearTimer += Time.deltaTime;
            if (appearTimer > 4.5f && LabelBGD.activeSelf == false)
            {
                Button5.SetActive(false);
                Button6.SetActive(false); 
                LabelBGD.SetActive(true);
                KazooFX.Play();
            }
                if (appearTimer > 5.5f && EndButton.activeSelf == false) EndButton.SetActive(true);

            if (Fader > 0f)
            {
                Fader -= Time.deltaTime;
                if (Fader <= 0f) LOLSDK.Instance.CompleteGame();// Application.Quit(); //this doesn''t work for web, maybe LoL have a command
                else
                    FadeOut.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f - 2f * Fader);
            }
        }

        public void Replay()
        {
            clickFX.Play();
            gameObject.SetActive(false);
        }
        public void End()
        {
            clickFX.Play();
            Fader = 0.5f;
            Buttons.SetActive(false);
            FadeOut.SetActive(true);
        }
        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }
    }
}