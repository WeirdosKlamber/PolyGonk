using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WeirdosKlamber.PolyGonk;

namespace WeirdosKlamber.PolyGonk.Lesson
{
    public class LessonSlideScript : MonoBehaviour
    {
        public string lessonName;
        public string slideName;
        public GameObject LessonMain;
        public GameObject Face;
        public GameObject quiz;
        public GameObject vennDiagram;
        public GameObject treeDiagram;
        public bool hasVenn = false;
        public bool hasTree = false;
        public int showVennStep = 1;
        public int showTreeStep = 1;

        public GameObject welcomeText;
        public float welcomeTextDuration = 0f;

        public GameObject text1;
        public bool hasText1 = true;
        public float text1Duration = 0f;

        public GameObject text2;
        public bool hasText2 = true;
        public float text2Duration = 0f;

        public GameObject text3;
        public bool hasText3 = true;
        public float text3Duration = 0f;

        public GameObject ContinueButton;
        private float animTimer = 0f;
        private int stepper = 0;
        private float quickReaderMod = 0.7f;
        private float buttonTimerMaster = 0.5f;
        private float buttonTimer = 0f;


        // Start is called before the first frame update
        void Start()
        {
            if (welcomeTextDuration <= 0.1f) { welcomeTextDuration = 5f; }
            if (text1Duration <= 0.1f) { text1Duration = 5f; }
            if (text2Duration <= 0.1f) { text2Duration = 5f; }
            if (text3Duration <= 0.1f) { text3Duration = 5f; }
            if (hasVenn) { vennDiagram.SetActive(false); }
            if (hasTree) { treeDiagram.SetActive(false); }

            SingletonSimple.Instance.ClearText();
            lessonName += slideName;
            print(lessonName + "Welcome");
/*            if (WeirdosKlamber.PolyGonk.ErojiScript.IsThereText(lessonName + "Welcome"))
            {*/
                welcomeText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText(lessonName + "Welcome");

                text1.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText(lessonName + "text1");
                text2.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText(lessonName + "text2");
                text3.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText(lessonName + "text3");

                SingletonSimple.Instance.TTSAddText(lessonName + "Welcome", welcomeTextDuration);
                SingletonSimple.Instance.TTSAddText(lessonName + "text1", text1Duration);
                SingletonSimple.Instance.TTSAddText(lessonName + "text2", text2Duration);
                SingletonSimple.Instance.TTSAddText(lessonName + "text3", text3Duration);
/*            }*/
        }

        // Update is called once per frame
        void Update()
        {
            animTimer += Time.unscaledDeltaTime;
            buttonTimer+= Time.unscaledDeltaTime;
            if (stepper == 0 && animTimer > 1f)
            {
                welcomeText.SetActive(true);
                Face.SetActive(true);
                if (hasVenn && showVennStep == stepper) { vennDiagram.SetActive(true); Face.SetActive(false); };
                if (hasTree && showTreeStep == stepper) { treeDiagram.SetActive(true); Face.SetActive(false); };
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 1 && animTimer > welcomeTextDuration * quickReaderMod)
            {
                if (hasText1) { text1.SetActive(true); }
                if (hasVenn && showVennStep == stepper) { vennDiagram.SetActive(true); Face.SetActive(false); };
                if (hasTree && showTreeStep == stepper) { treeDiagram.SetActive(true); Face.SetActive(false); };
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 2 && animTimer > text1Duration * quickReaderMod)
            {
                if (hasText2) { text2.SetActive(true); }
                if (hasVenn && showVennStep == stepper) { vennDiagram.SetActive(true); Face.SetActive(false); };
                if (hasTree && showTreeStep == stepper) { treeDiagram.SetActive(true); Face.SetActive(false); };
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 3 && animTimer > text2Duration * quickReaderMod)
            {
                if (hasText3) { text3.SetActive(true); }
                if (hasVenn && showVennStep == stepper) { vennDiagram.SetActive(true); Face.SetActive(false); };
                if (hasTree && showTreeStep == stepper) { treeDiagram.SetActive(true); Face.SetActive(false); };
                stepper++;
                animTimer = 0f;
            }
            if (stepper == 4 && animTimer > text3Duration * quickReaderMod)
            {
                ContinueButton.SetActive(true);
                stepper++;
                animTimer = 0f;
            }
            if (buttonTimer>buttonTimerMaster)
            {
                if (Input.GetMouseButton(0)||Input.anyKeyDown) 
                {
                    buttonTimer = 0f;
                    animTimer = 20f;
                }
            
            }
        }

        public void Continue()
        {
            LessonMain.SendMessage("NextSlide");
        }

        void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
            quickReaderMod= 1f;
            if (hasVenn && vennDiagram.activeSelf) { vennDiagram.SendMessage("SlowDown"); }
            if (hasTree && treeDiagram.activeSelf) { treeDiagram.SendMessage("SlowDown"); }

        }
    }
}