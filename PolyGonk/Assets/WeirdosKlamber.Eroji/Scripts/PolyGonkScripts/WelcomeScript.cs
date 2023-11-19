using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LoLSDK;
namespace WeirdosKlamber.PolyGonk.Intro
{
    public class WelcomeScript : MonoBehaviour
    {
        public GameObject Info1;
        public GameObject[] Info1Lines;
        public GameObject Info2;
        public GameObject[] Info2Lines;
        public GameObject Info3;
        public GameObject[] Info3Lines;
        public GameObject ContinueButton;
        public GameObject controlButtons;
        private int clickcounter = 0;
        private float Timerx = 0f;
        public int level = 0;
        public GameObject Main;
        public GameObject whiteDrop;

        // Start is called before the first frame update
        void Start()
        {
            ContinueButton.SetActive(false);
            Time.timeScale = 0f;
            Info1.SetActive(true);
            SingletonSimple.Instance.ClearText();
            controlButtons.SetActive(false);
            if (level == 1)
            {

                Info1Lines[0].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome1a");
                Info1Lines[1].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome1b");
                Info1Lines[2].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome1c");

                Info2Lines[0].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome2a");
                Info2Lines[1].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome2b");
                Info2Lines[2].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome2c");

                Info3Lines[0].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome3a");
                Info3Lines[1].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome3b");
                Info3Lines[2].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText("Level1Welcome3c");

                SingletonSimple.Instance.TTSAddText("Level1Welcome1a", 3f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome1b", 4f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome1c", 8f);
            }

            else //just cast list
            {
                Info1Lines[0].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText(Info1Lines[0].GetComponent<TextMeshProUGUI>().text);
                Info1Lines[1].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText(Info1Lines[1].GetComponent<TextMeshProUGUI>().text);
                Info1Lines[2].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText(Info1Lines[2].GetComponent<TextMeshProUGUI>().text);
                Info1Lines[3].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText(Info1Lines[3].GetComponent<TextMeshProUGUI>().text);
                Info1Lines[4].GetComponent<TextMeshProUGUI>().text = ErojiScript.GetText(Info1Lines[4].GetComponent<TextMeshProUGUI>().text);

                SingletonSimple.Instance.TTSAddText(Info1Lines[0].GetComponent<TextMeshProUGUI>().text, 2.5f);
                SingletonSimple.Instance.TTSAddText(Info1Lines[1].GetComponent<TextMeshProUGUI>().text, 2.5f);
                SingletonSimple.Instance.TTSAddText(Info1Lines[2].GetComponent<TextMeshProUGUI>().text, 2.5f);
                SingletonSimple.Instance.TTSAddText(Info1Lines[3].GetComponent<TextMeshProUGUI>().text, 2.5f);
                SingletonSimple.Instance.TTSAddText(Info1Lines[4].GetComponent<TextMeshProUGUI>().text, 2.5f);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            Timerx += Time.unscaledDeltaTime;

            if (level == 1)
            {
                switch (clickcounter)
                {
                    case (0):
                        if (Timerx > 1f)
                        {
                            whiteDrop.SetActive(true);
                            clickcounter = 1;
                            Info1Lines[0].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (1):
                        if (Timerx > 1.5f)
                        {
                            clickcounter = 2;
                            Info1Lines[1].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (2):
                        if (Timerx > 4f)
                        {
                            clickcounter = 3;
                            Info1Lines[2].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (3):
                        if (Timerx > 3f)
                        {
                            clickcounter = 4;
                            ContinueButton.SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (4):
                        {
                            break;
                        }
                    case (5):
                        if (Timerx > 1f)
                        {
                            clickcounter = 6;
                            Info2Lines[0].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (6):
                        if (Timerx > 3f)
                        {
                            clickcounter = 7;
                            Info2Lines[1].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (7):
                        if (Timerx > 4f)
                        {
                            clickcounter = 8;
                            Info2Lines[2].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (8):
                        clickcounter = 9;
                        break;
                    case (9):
                        if (Timerx > 3f)
                        {
                            ContinueButton.SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (10):
                        if (Timerx > 1f)
                        {
                            clickcounter = 11;
                            Info3Lines[0].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (11):
                        if (Timerx > 2.5f)
                        {
                            clickcounter = 12;
                            Info3Lines[1].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (12):
                        if (Timerx > 3f)
                        {
                            clickcounter = 13;
                            Info3Lines[2].SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    case (13):
                        if (Timerx > 1f)
                        {
                            clickcounter = 14;
                            ContinueButton.SetActive(true);
                            Timerx = 0f;
                        }
                        break;
                    default:
                        break;
                }
            }

            else //just cast list
            {
                if (Timerx > 0.5f && whiteDrop.activeSelf == false) 
                {
                    whiteDrop.SetActive(true);
                    Info1.SetActive(true);
                }
                if (Timerx > 1.2f && Info1Lines[0].activeSelf == false)
                {
                    Info1Lines[0].SetActive(true) ;
                }
                if (Timerx > 1.9f && Info1Lines[1].activeSelf == false)
                {
                    Info1Lines[1].SetActive(true);
                }
                if (Timerx > 2.6f && Info1Lines[2].activeSelf == false)
                {
                    Info1Lines[2].SetActive(true);
                }
                if (Timerx > 3.3f && Info1Lines[3].activeSelf == false)
                {
                    Info1Lines[3].SetActive(true);
                }
                if (Timerx > 4f && Info1Lines[4].activeSelf == false)
                {
                    Info1Lines[4].SetActive(true);
                }
                if (Timerx > 4.7f)
                {
                    ContinueButton.SetActive(true);
                }
            }
        }

        public void ContinuePressed()
        {
            print("continuepressed clickcounter: " + clickcounter + "  level:" + level);
            if (clickcounter == 4)
            {
                clickcounter = 5;
                Info1.SetActive(false);
                Info2.SetActive(true);
                Timerx = 0f;
                ContinueButton.SetActive(false);
                SingletonSimple.Instance.ClearText();
                if (level == 1)
                {
                    controlButtons.SetActive(true);

                    SingletonSimple.Instance.TTSAddText("Level1Welcome2a", 3f);
                    SingletonSimple.Instance.TTSAddText("Level1Welcome2b", 8f);
                    SingletonSimple.Instance.TTSAddText("Level1Welcome2c", 4f);

                }
                /*                else
                                {
                                    SingletonSimple.Instance.TTSAddText("CoastWelcome2", 7f);
                                    SingletonSimple.Instance.TTSAddText("CoastWelcome2a", 4f);
                                    SingletonSimple.Instance.TTSAddText("CoastWelcome2b", 5f);
                                    SingletonSimple.Instance.TTSAddText("CoastWelcome2c", 4f);
                                    SingletonSimple.Instance.TTSAddText("CoastWelcome2d", 4f);
                                }*/
            }
            else if (clickcounter == 9 && level == 1)
            {
                clickcounter = 10;
                Info1.SetActive(false);
                Info2.SetActive(false);
                Info3.SetActive(true);
                Timerx = 0f;
                ContinueButton.SetActive(false);
                SingletonSimple.Instance.ClearText();

                SingletonSimple.Instance.TTSAddText("Level1Welcome3a", 3f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome3b", 5f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome3c", 4f);
                
            }

            else
            {
                controlButtons.SetActive(true);
                Main.SendMessage("endIntro");

                Destroy(gameObject);
            }
        }
        public void TTSButtonPressed()
            {
            SingletonSimple.Instance.Speak();
            }

        public void skipPressed()
        {
            Timerx= 100f;
        }

    }
}