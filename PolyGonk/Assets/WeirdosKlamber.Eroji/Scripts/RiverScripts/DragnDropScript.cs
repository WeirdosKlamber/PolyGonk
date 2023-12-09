using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace WeirdosKlamber.PolyGonk.River
{
    public class DragnDropScript : MonoBehaviour
    {
        public TextMeshProUGUI QuestionText;
        public TextMeshProUGUI AnswerText;
        public TextMeshProUGUI InputText;
        public TextMeshProUGUI Answer1Text;
        public TextMeshProUGUI Answer2Text;
        public TextMeshProUGUI Answer3Text;

        public GameObject ActualGameCard;

        public int QuestionNumber = 1;

        public GameObject QuizMaster;
        public GameObject AwardCardBgd;
        public GameObject AwardCardWinText;
        public GameObject AwardCardLoseText;
        public GameObject AwardCardInfoPic;
        public GameObject AwardCardCardCard;

        public GameObject InputButton;
        public GameObject Answer1Button;
        public GameObject Answer2Button;
        public GameObject Answer3Button;
        public GameObject CorrectAnswer;

        public GameObject TickObj;
        public GameObject CrossObj;
        public GameObject LectureObj;

        private GameObject LastSelected;
        private GameObject MyAnswer;

        public AudioSource Bing;
        public AudioSource Buzz;

        private bool InputEntered = false;
        private bool InputCorrect = false;
        private bool InputFalse = false;
        private bool Answer1Pressed = false;
        private bool Answer2Pressed = false;
        private bool Answer3Pressed = false;

        private Vector3 Position1;
        private Vector3 Position2;
        private Vector3 Position3;
        private Vector3 InputP;

        private float AnimTimer = 2f;
        private bool finishedAnim = false;

        [SerializeField]
        private Canvas canvas;

        public GameObject ContinueButton;
        public GameObject RetryButton;
        private bool ContinuedOnce = false;
        private bool RetryBool = false;

        public GameObject monocleFace;
        public GameObject happyFace;
        public GameObject SadFace;

        // Start is called before the first frame update
        void Start()
        {
            SingletonSimple.Instance.ClearText();
            Position1 = Answer1Button.transform.position;
            Position2 = Answer2Button.transform.position;
            Position3 = Answer3Button.transform.position;
            InputP = InputButton.transform.position;
/*
            TickObj.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Correct");
            CrossObj.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("Oh dear!");*/

            if (QuestionNumber==1)
            {
/*                QuestionText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverQuestion1");*/
/*                AnswerText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer1");
                Answer1Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer1A");
                Answer2Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer1B");
                Answer3Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer1C");
                AwardCardWinText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("AwardCardWin1");
                AwardCardLoseText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("AwardCardLose1");
                SingletonSimple.Instance.TTSAddText("RiverQuestion1",4f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer1a", 2f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer1b", 2f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer1c", 2f);*/
            }
            else if (QuestionNumber == 2)
            {
/*                QuestionText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverQuestion2");
                AnswerText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer2");
                Answer1Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer2A");
                Answer2Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer2B");
                Answer3Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer2C");
                AwardCardWinText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("AwardCardWin2");
                AwardCardLoseText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("AwardCardLose2");
                SingletonSimple.Instance.TTSAddText("RiverQuestion2", 4f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer2a", 2f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer2b", 2f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer2c", 2f);*/
            }
            else if (QuestionNumber == 3)
            {
/*                QuestionText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverQuestion3");
                AnswerText.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer3");
                Answer1Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer3A");
                Answer2Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer3B");
                Answer3Text.text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("RiverAnswer3C");
                AwardCardWinText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("AwardCardWin3");
                AwardCardLoseText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.ErojiScript.GetText("AwardCardLose3");
                SingletonSimple.Instance.TTSAddText("RiverQuestion3", 4f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer3a", 2f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer3b", 2f);
                SingletonSimple.Instance.TTSAddText("RiverAnswer3c", 2f);*/
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (!ContinuedOnce)
            {
                if (InputCorrect || InputFalse)
                {
                    if (AnimTimer > -5f)
                    {
                        AnimTimer -= Time.unscaledDeltaTime;
                        if (AnimTimer > 1.5f)
                        {
                            if (Answer1Button != MyAnswer)
                            {
                                Answer1Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, AnimTimer - 1.5f);
                                Answer1Text.color = new Color(1f, 1f, 1f, AnimTimer - 1.5f);
                            }
                            if (Answer2Button != MyAnswer)
                            {
                                Answer2Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, AnimTimer - 1.5f);
                                Answer2Text.color = new Color(1f, 1f, 1f, AnimTimer - 1.5f);
                            }
                            if (Answer3Button != MyAnswer)
                            {
                                Answer3Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, AnimTimer - 1.5f);
                                Answer3Text.color = new Color(1f, 1f, 1f, AnimTimer - 1.5f);
                            }
                        }
                        else if (AnimTimer > 1f)
                        {
                            if (InputCorrect) TickObj.SetActive(true);
                            else CrossObj.SetActive(true);
                        }
                        else if (AnimTimer > 0f)
                        {
                            LectureObj.SetActive(true); //make it impossible to lose because Reviewers say so

                        }
                        else if (AnimTimer < -2f && AnimTimer > -3.5f)
                        {
                            RetryBool= false;
                            if (InputCorrect) ContinueButton.SetActive(true);

                            
                        }
                        else if (AnimTimer < -3.5f)
                        {
                            AnimTimer= -3.5f;
                            if (InputCorrect) ContinueButton.SetActive(true);
                            if (InputFalse && RetryBool == false) RetryButton.SetActive(true);
                            else if (InputFalse && RetryBool==true)
                            {
                                RetryButton.SetActive(false);
                                CrossObj.SetActive(false);
                                InputEntered = false;
                                InputCorrect = false;
                                InputFalse = false;
                                Answer1Pressed = false;
                                Answer2Pressed = false;
                                Answer3Pressed = false;
                                AnimTimer = 2f;
                                finishedAnim = false;
                                ContinuedOnce = false;
                                Answer1Button.transform.position = Position1;
                                Answer2Button.transform.position = Position2;
                                Answer3Button.transform.position = Position3;
                                Answer1Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                                Answer2Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                                Answer3Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                                Answer1Button.GetComponent<Button>().interactable = true;
                                Answer2Button.GetComponent<Button>().interactable = true;
                                Answer3Button.GetComponent<Button>().interactable = true;
                                Answer1Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                                Answer1Text.color = new Color(1f, 1f, 1f, 1f);
                                Answer2Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                                Answer2Text.color = new Color(1f, 1f, 1f, 1f);
                                Answer3Button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                                Answer3Text.color = new Color(1f, 1f, 1f, 1f);
                                monocleFace.SetActive(true);
                                happyFace.SetActive(false);
                                SadFace.SetActive(false);
                                LectureObj.SetActive(false);
                                RetryBool = false;
                            }
                        }
                    }
                }
            }
            else //have pressed continue once
            {
                AnimTimer -= Time.unscaledDeltaTime;
                if (AnimTimer > 2.5f)
                {
                    AwardCardBgd.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1 - (AnimTimer - 2.5f) * 2f);
                }
                else if (AnimTimer > 1.5f)
                {
                    AwardCardBgd.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    if (InputCorrect) AwardCardWinText.SetActive(true);
                    else
                    {
                        AwardCardLoseText.SetActive(true);
                        //AwardCardCardCard.GetComponent<Image>().color = Color.red;
                    }
                }
                else if (AnimTimer > 1f)
                {
                    AwardCardInfoPic.SetActive(true);
                }
                else if (AnimTimer > 0.5f)
                {
                    AwardCardCardCard.SetActive(true);
                }
                else if (AnimTimer < -0.5f)
                {
                    ContinueButton.SetActive(true);
                }
            }
        }

        void AnswerPressed1() 
        {
            Answer1Pressed = true;
        }
        void AnswerPressed2()
        {
            Answer2Pressed = true;
        }
        void AnswerPressed3()
        {
            Answer3Pressed = true;
        }

        void AnswerReleased1()
        {
            print("answer1release");
            Answer1Pressed = false;

            if (InputEntered)
            {
                Answer1Button.transform.position = InputP;
                MyAnswer = Answer1Button;
                ShowAnswer();
            }
            else
            {
                Answer1Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Answer1Button.transform.position = Position1;
            }
        }
        void AnswerReleased2()
        {
            print("answer2release");
            Answer2Pressed = false;

            if (InputEntered)
            {
                Answer2Button.transform.position = InputP;
                MyAnswer = Answer2Button;
                ShowAnswer();
            }
            else
            {
                Answer2Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Answer2Button.transform.position = Position2;
            }
        }
        void AnswerReleased3()
        {
            print("answer3release");
            Answer3Pressed = false;

            if (InputEntered)
            {
                Answer3Button.transform.position = InputP;
                MyAnswer = Answer3Button;
                ShowAnswer();
            }
            else
            {
                Answer3Button.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Answer3Button.transform.position = Position3;
            }
        }

        public void DragX(BaseEventData datax)
        {
            PointerEventData pointerData = (PointerEventData)datax;
            
            Vector2 positionx;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out positionx);
            //datax.selectedObject.GetComponent<Image>().raycastTarget = false;
            datax.selectedObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            datax.selectedObject.transform.position = canvas.transform.TransformPoint(positionx);
        }



        public void InputEnter()
        {
            InputEntered = true;
            LastSelected = EventSystem.current.currentSelectedGameObject;


        }
        public void InputExit()
        {
            InputEntered = false;
        }

        public void InputPressed()
        {
            if (LastSelected == Answer1Button)
            {
                Answer1Button.transform.position = InputP;
                MyAnswer = Answer1Button;
                ShowAnswer();
            }
            else if (LastSelected == Answer2Button)
            {
                Answer2Button.transform.position = InputP;
                MyAnswer = Answer2Button;
                ShowAnswer();
            }
            else if (LastSelected == Answer3Button)
            {
                Answer3Button.transform.position = InputP;
                MyAnswer = Answer3Button;
                ShowAnswer();
            }

        }

        void ShowAnswer()
        {
            InputButton.GetComponent<Button>().interactable = false;
            Answer1Button.GetComponent<Button>().interactable = false;
            Answer2Button.GetComponent<Button>().interactable = false;
            Answer3Button.GetComponent<Button>().interactable = false;

            if (CorrectAnswer==MyAnswer)
            {
                Bing.Play();
                InputCorrect = true;
                ActualGameCard.SetActive(true);
                monocleFace.SetActive(false);
                happyFace.SetActive(true);
                SadFace.SetActive(false);
                SingletonSimple.Instance.ClearText();
                switch (QuestionNumber)
                {
                    case (1):
                        SingletonSimple.Instance.lesson4Completed = true;
                        SingletonSimple.Instance.SaveGame();
                        break;
                    case (2):
                        SingletonSimple.Instance.level4Completed = true;
                        SingletonSimple.Instance.SaveGame();
                        break;
                    case (3):
                        SingletonSimple.Instance.lesson5Completed = true;
                        SingletonSimple.Instance.SaveGame();
                        break;
                }
            }
            else
            {               
                Buzz.Play();
                InputFalse = true;
                ActualGameCard.SetActive(false);
                monocleFace.SetActive(false);
                happyFace.SetActive(false);
                SadFace.SetActive(true);
            }
            //SingletonSimple.Instance.ClearText();
            switch (QuestionNumber)
            {
                case (1):
                  //  SingletonSimple.Instance.TTSAddText("RiverAnswer1", 10f);
                    break;
                case (2):
                   // SingletonSimple.Instance.TTSAddText("RiverAnswer2", 10f);
                    break;
                case (3):
                   // SingletonSimple.Instance.TTSAddText("RiverAnswer3", 10f);
                    break;
            }

        }

        public void Continue()
        {
            if (!ContinuedOnce)
            {
                ContinuedOnce = true; 
                ContinueButton.SetActive(false);
                AnimTimer = 3f;
                AwardCardBgd.SetActive(true);
                SingletonSimple.Instance.ClearText();

                if (InputCorrect)
                {
                    switch (QuestionNumber)
                    {
                        case (1):
                          //  SingletonSimple.Instance.TTSAddText("AwardCardWin1", 12f);
                            break;
                        case (2):
                          //  SingletonSimple.Instance.TTSAddText("AwardCardWin2", 12f);
                            break;
                        case (3):
                          //  SingletonSimple.Instance.TTSAddText("AwardCardWin3", 12f);
                            break;
                    }
                }
                else
                {
                    switch (QuestionNumber)
                    {
                        case (1):
                         //   SingletonSimple.Instance.TTSAddText("AwardCardLose1", 12f);
                            break;
                        case (2):
                         //   SingletonSimple.Instance.TTSAddText("AwardCardLose2", 12f);
                            break;
                        case (3):
                          //  SingletonSimple.Instance.TTSAddText("AwardCardLose3", 12f);
                            break;
                    }
                }
            }
            else
            {
                QuizMaster.SendMessage("questionFinish", QuestionNumber);
                Destroy(gameObject);
            }
        }
        public void RetryButtonPress()
        {
            RetryBool = true;
        }
    }

}