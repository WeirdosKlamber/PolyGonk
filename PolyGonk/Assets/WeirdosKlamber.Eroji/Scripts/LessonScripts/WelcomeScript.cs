using UnityEngine;
using TMPro;

namespace WeirdosKlamber.PolyGonk.Intro
{
    public class WelcomeScript : MonoBehaviour
    {
        public GameObject info1;
        public GameObject[] info1Lines;
        public GameObject info2;
        public GameObject[] info2Lines;
        public GameObject info3;
        public GameObject[] info3Lines;
        public GameObject continueButton;
        public GameObject controlButtons;
        private int clickCounter = 0;
        private float timerX = 0f;
        public int level = 0;
        public GameObject main;
        public GameObject whiteDrop;

        void Start()
        {
            continueButton.SetActive(false);
            Time.timeScale = 0f;
            info1.SetActive(true);
            SingletonSimple.Instance.ClearText();
            controlButtons.SetActive(false);
            if (level == 1)
            {

                info1Lines[0].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome1a");
                info1Lines[1].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome1b");
                info1Lines[2].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome1c");

                info2Lines[0].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome2a");
                info2Lines[1].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome2b");
                info2Lines[2].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome2c");

                info3Lines[0].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome3a");
                info3Lines[1].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome3b");
                info3Lines[2].GetComponent<TextMeshProUGUI>().text = PolyGonkScript.GetText("Level1Welcome3c");

                SingletonSimple.Instance.TTSAddText("Level1Welcome1a", 3f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome1b", 4f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome1c", 8f);
            }

            else 
            {
                for(int i = 0; i < info1Lines.Length; i++)
                {
                    string line = PolyGonkScript.GetText(info1Lines[i].GetComponent<TextMeshProUGUI>().text);
                    info1Lines[i].GetComponent<TextMeshProUGUI>().text = line;
                    SingletonSimple.Instance.TTSAddText(line, 2.5f);
                }
            }
            
        }

        void Update()
        {
            timerX += Time.unscaledDeltaTime;

            if (level == 1)
            {
                switch (clickCounter)
                {
                    case (0):
                        if (timerX > 1f)
                        {
                            whiteDrop.SetActive(true);
                            clickCounter = 1;
                            info1Lines[0].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (1):
                        if (timerX > 1.5f)
                        {
                            clickCounter = 2;
                            info1Lines[1].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (2):
                        if (timerX > 4f)
                        {
                            clickCounter = 3;
                            info1Lines[2].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (3):
                        if (timerX > 3f)
                        {
                            clickCounter = 4;
                            continueButton.SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (4):
                        {
                            break;
                        }
                    case (5):
                        if (timerX > 1f)
                        {
                            clickCounter = 6;
                            info2Lines[0].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (6):
                        if (timerX > 3f)
                        {
                            clickCounter = 7;
                            info2Lines[1].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (7):
                        if (timerX > 4f)
                        {
                            clickCounter = 8;
                            info2Lines[2].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (8):
                        clickCounter = 9;
                        break;
                    case (9):
                        if (timerX > 3f)
                        {
                            continueButton.SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (10):
                        if (timerX > 1f)
                        {
                            clickCounter = 11;
                            info3Lines[0].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (11):
                        if (timerX > 2.5f)
                        {
                            clickCounter = 12;
                            info3Lines[1].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (12):
                        if (timerX > 3f)
                        {
                            clickCounter = 13;
                            info3Lines[2].SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    case (13):
                        if (timerX > 1f)
                        {
                            clickCounter = 14;
                            continueButton.SetActive(true);
                            timerX = 0f;
                        }
                        break;
                    default:
                        break;
                }
            }

            else 
            {
                if (timerX > 0.5f && whiteDrop.activeSelf == false) 
                {
                    whiteDrop.SetActive(true);
                    info1.SetActive(true);
                }
                if (timerX > 1.2f && info1Lines[0].activeSelf == false)
                {
                    info1Lines[0].SetActive(true) ;
                }
                if (timerX > 1.9f && info1Lines[1].activeSelf == false)
                {
                    info1Lines[1].SetActive(true);
                }
                if (timerX > 2.6f && info1Lines[2].activeSelf == false)
                {
                    info1Lines[2].SetActive(true);
                }
                if (timerX > 3.3f && info1Lines[3].activeSelf == false)
                {
                    info1Lines[3].SetActive(true);
                }
                if (timerX > 4f && info1Lines[4].activeSelf == false)
                {
                    info1Lines[4].SetActive(true);
                }
                if (timerX > 4.7f)
                {
                    continueButton.SetActive(true);
                }
            }
        }

        public void ContinuePressed()
        {
            print("continuepressed clickcounter: " + clickCounter + "  level:" + level);
            if (clickCounter == 4)
            {
                clickCounter = 5;
                info1.SetActive(false);
                info2.SetActive(true);
                timerX = 0f;
                continueButton.SetActive(false);
                SingletonSimple.Instance.ClearText();
                if (level == 1)
                {
                    controlButtons.SetActive(true);
                    SingletonSimple.Instance.TTSAddText("Level1Welcome2a", 3f);
                    SingletonSimple.Instance.TTSAddText("Level1Welcome2b", 8f);
                    SingletonSimple.Instance.TTSAddText("Level1Welcome2c", 4f);

                } 
            }
            else if (clickCounter == 9 && level == 1)
            {
                clickCounter = 10;
                info1.SetActive(false);
                info2.SetActive(false);
                info3.SetActive(true);
                timerX = 0f;
                continueButton.SetActive(false);
                SingletonSimple.Instance.ClearText();
                SingletonSimple.Instance.TTSAddText("Level1Welcome3a", 3f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome3b", 5f);
                SingletonSimple.Instance.TTSAddText("Level1Welcome3c", 4f);                
            }

            else
            {
                controlButtons.SetActive(true);
                main.SendMessage("endIntro");

                Destroy(gameObject);
            }
        }
        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }

        public void skipPressed()
        {
            timerX = 100f;
        }

    }
}