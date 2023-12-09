using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LoLSDK;

namespace WeirdosKlamber.PolyGonk
    { 
    public class TotalVictory : MonoBehaviour
    {
        public TextMeshProUGUI congratulationsTxt;
        public TextMeshProUGUI endTxt;
        public AudioSource clickFX;
        public AudioSource applauseFX;
        public AudioSource kazooFX;

        public GameObject fadeOut;
        public GameObject labelBackground;
        public GameObject endButton;

        public GameObject buttons;
        public GameObject button5;
        public GameObject button6;
        
        private float appearTimer = 0f;
        private float fader = 0f;

        void Start()
        {
            SingletonSimple.Instance.ClearText();
            if (SingletonSimple.Instance.SumScores() > 214)
            {
                congratulationsTxt.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("Congratulations4") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations4", 7f);
            }
            else if (SingletonSimple.Instance.SumScores() > 183)
            {
                congratulationsTxt.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("Congratulations3") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations3", 7f);
            }
            else if (SingletonSimple.Instance.SumScores() > 160)
            {
                congratulationsTxt.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("Congratulations2") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations2", 7f);
            }
            else
            {
                congratulationsTxt.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("Congratulations1") + SingletonSimple.Instance.SumScores().ToString();
                SingletonSimple.Instance.TTSAddText("Congratulations1", 7f);
            }
            endTxt.text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("End");
            applauseFX.Play();
        }

        void Update()
        {
            appearTimer += Time.deltaTime;
            if (appearTimer > 4.5f && labelBackground.activeSelf == false)
            {
                button5.SetActive(false);
                button6.SetActive(false); 
                labelBackground.SetActive(true);
                kazooFX.Play();
            }
                if (appearTimer > 5.5f && endButton.activeSelf == false) endButton.SetActive(true);

            if (fader > 0f)
            {
                fader -= Time.deltaTime;
                if (fader <= 0f) LOLSDK.Instance.CompleteGame();
                else
                    fadeOut.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f - 2f * fader);
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
            fader = 0.5f;
            buttons.SetActive(false);
            fadeOut.SetActive(true);
        }

        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }
    }
}