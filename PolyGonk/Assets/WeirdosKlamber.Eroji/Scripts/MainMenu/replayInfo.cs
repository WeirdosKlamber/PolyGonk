using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using LoLSDK;
using SimpleJSON;
using WeirdosKlamber;
namespace WeirdosKlamber.PolyGonk
{
    public class replayInfo : MonoBehaviour
    {
        public GameObject labelBGD;
        public GameObject messageObj;
        public GameObject continueButton;
        public GameObject ttsButton;

        public AudioSource audiosc;
        private float startTimer = 0f;
        private bool killSoon = false;
        private float killTimer = 0.2f;
        // Start is called before the first frame update
        void Start()
        {
            SingletonSimple.Instance.ClearText();

            if (SingletonSimple.Instance.levelScores[0] > SingletonSimple.Instance.levels1starThresholds[0])
            { 
                messageObj.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("replayInfo");
                SingletonSimple.Instance.TTSAddText("replayInfo",12f);
            }
            else 
            {
                messageObj.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("replayInfo2");
                SingletonSimple.Instance.TTSAddText("replayInfo2", 12f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            startTimer += Time.unscaledDeltaTime;
            if (startTimer > 2f && startTimer < 90f) 
            { 
                continueButton.SetActive(true);
                startTimer = 100f;
            }

            if (killSoon) 
            {
                killTimer-=Time.unscaledDeltaTime;
                if(killTimer>=0.1f)
                {
                    continueButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, (killTimer - 0.1f) * 10f);
                }
                else if (killTimer >= 0f)
                {
                    messageObj.SetActive(false);
                    labelBGD.GetComponent<Image>().color = new Color(1f, 1f, 1f, (killTimer) * 10f);
                    ttsButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, (killTimer) * 10f);
                } 
                else 
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void ContinuePressed()
        {
            audiosc.Play();
            killSoon = true;
        }

        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }
    }
}