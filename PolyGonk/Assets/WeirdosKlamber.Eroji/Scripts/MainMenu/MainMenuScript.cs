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
    public class MainMenuScript : MonoBehaviour
    {

        public GameObject mainTitle;

        private int score = 0;

        private bool langReceived = false;
        private float wait = 2f;

        public GameObject Fader;
        public AudioSource ClickFX;
        private string LevelSelection;
        private bool LevelSelected = false;
        private float FadeTimer = 1f;
        private float TitleTimer = 0.5f;
        private int TitleCounter = 0;
        public GameObject[] Titles;
        public GameObject TotalVictory;
        private bool StartShow = false;
        private bool PlayedMusic = false;
        private bool ShowTVOnce = false;




        // Start is called before the first frame update
        void Start()
        {
            score = SingletonSimple.Instance.SumScores();

            //doesn't receive language in time
            if (WeirdosKlamber.PolyGonk.ErojiScript.GetText("Test") == "-Test") langReceived = false;
            else
            {
                langReceived = true;
                SingletonSimple.Instance.isLOL = true;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (StartShow)
            {
                if (!ShowTVOnce && !PlayedMusic)
                {
                    score = SingletonSimple.Instance.SumScores();

                }

                //make happen after spring in and spin out, prevent music
                if (!ShowTVOnce && SingletonSimple.Instance.levelScores[6]>1)
                {
                    TotalVictory.SetActive(true);
                    ShowTVOnce = true;
                    mainTitle.SendMessage("TotalVictory");
                }                

                if (langReceived == false)
                {
                    wait -= Time.deltaTime;
                    if (wait < 0f)
                    {
                        wait = 2f;
                        if (WeirdosKlamber.PolyGonk.ErojiScript.GetText("Test") == "-Test") langReceived = false;
                        else
                        {
                            langReceived = true;
                            SingletonSimple.Instance.isLOL = true;
                        }
                    }
                }
                if (LevelSelected)// && FinishedLecture)
                {
                    FadeTimer -= Time.deltaTime;
                    if (FadeTimer < 0f)
                    {
                        SceneManager.LoadScene(LevelSelection);
                    }
                    else
                    {
                        Fader.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f - FadeTimer);
                    }
                }
            }
        }


        void StarttheShow()
        {
            StartShow = true;
            mainTitle.SetActive(true);            
        }

        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }

        private void LevelButtonPress(int levelX)
        {
            print("Level " + levelX + " button pressed");
            if (SingletonSimple.Instance.countCheckpoints()<(levelX*2)-1)
            {
                LevelSelection = "Lesson" + levelX.ToString();
            }
            else
            {
                LevelSelection = "PolyGonkLevel" + levelX.ToString();
            }

            LevelSelected = true;
        }
    }
}