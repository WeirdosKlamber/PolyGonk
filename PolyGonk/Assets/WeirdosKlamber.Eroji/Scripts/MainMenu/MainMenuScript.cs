
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WeirdosKlamber.PolyGonk
{
    public class MainMenuScript : MonoBehaviour
    {
        public GameObject mainTitle;

        private bool langReceived = false;
        private float wait = 2f;

        public GameObject Fader;
        public AudioSource ClickFX;
        private string LevelSelection;
        private bool LevelSelected = false;
        private float FadeTimer = 1f;
        public GameObject[] Titles;
        public GameObject TotalVictory;
        private bool StartShow = false;
        private bool ShowTVOnce = false;

        // Start is called before the first frame update
        void Start()
        {
            //doesn't always receive language in time
            if (PolyGonkScript.GetText("Test") == "-Test") langReceived = false;
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
                        if (PolyGonkScript.GetText("Test") == "-Test") langReceived = false;
                        else
                        {
                            langReceived = true;
                            SingletonSimple.Instance.isLOL = true;
                        }
                    }
                }
                if (LevelSelected)
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