using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace WeirdosKlamber.PolyGonk.River
{
    public class RiverMasterScript : MonoBehaviour
    {
        public int Checkpoint = 0;
        private int Checkpoint1nav = 10;
        private int Checkpoint2nav = 20;
        private int CurrentNav = 0;
        private int CurrentStage = 1;
        public Button UndergroundButton;
        public Button OxbowButton;
        public Button FloodButton;
        public GameObject RiverPathobj;
        public GameObject[] Hearts;
        static public int Life = 5;

        public float Stage1ScoreFloat = 0f;
        public float Stage2ScoreFloat = 0f;
        public float Stage3ScoreFloat = 0f;
        public float fruitScoreFloat = 0f;
        public float totalScoreFloat = 0f;

        public float timeTotal = 0f;
        private int timeScore = 0;

        public AudioSource VictorySFX;
        public AudioSource LoseSFX;

        public GameObject WinScreen;
        private int[] winStuff = new int[6];
        private bool VictoryBool = false;
        private float EndTimer = 0f;
        public Image FadeOut;
        private bool Ended = false;
        private int eaten = 0;
        public GameObject MainCamera;
        public Image iceCream;
        // Start is called before the first frame update
        void Start()
        {
            
/*            if (SingletonSimple.Instance.riverBabyMode)
            {
                Life = 10;
            }
            else
            {
                Life = 5;
                Hearts[5].SetActive(false);
                Hearts[6].SetActive(false);
                Hearts[7].SetActive(false);
                Hearts[8].SetActive(false);
                Hearts[9].SetActive(false);

            }*/
        }

        // Update is called once per frame
        void Update()
        {
            if (EndTimer>0f&&!Ended)
            {
                EndTimer -= Time.deltaTime;

                if (VictoryBool == true && EndTimer >= 3f)
                {
                    iceCream.color = new Color(1f, 1f, 1f, 1f - (EndTimer-3));                    
                }

                if (EndTimer < 0.05f)
                {
                    if (VictoryBool == true) FadeOut.color = Color.white;
                    else FadeOut.color = Color.black;
                    iceCream.color = new Color(1f, 1f, 1f, 0f );
                    EndTime();
                    Ended = true;

                }
                else if (EndTimer < 2f)
                {
                    if (VictoryBool == true)
                        FadeOut.color = new Color(1f, 1f, 1f, 1f - EndTimer/2);
                    else FadeOut.color = new Color(0f, 0f, 0f, 1f - EndTimer/2);
                }
            }
            /*
            if (Input.GetKey("space"))
            {
                SingletonSimple.Instance.riverScore1 = Mathf.Max((int)Stage1ScoreFloat, SingletonSimple.Instance.riverScore1);
                SingletonSimple.Instance.riverScore2 = Mathf.Max((int)Stage1ScoreFloat, SingletonSimple.Instance.riverScore2);
                SingletonSimple.Instance.riverScore3 = Mathf.Max((int)Stage1ScoreFloat, SingletonSimple.Instance.riverScore3);
                SingletonSimple.Instance.riverTotalScore = Mathf.Max((int)Stage1ScoreFloat, SingletonSimple.Instance.riverTotalScore);

                SceneManager.LoadScene("Main");
            }*/
        }



        void CheckpointStart(int Checkn)
        {
            Checkpoint = Checkn;
        }

        void NavCount(int nav)  //use for checkpoints
        {
            CurrentNav = nav;
            if (CurrentNav == -2)
            {
                VictorySFX.PlayOneShot(VictorySFX.clip);
            }
        }

        void Hit(bool crocbite)
        {

            Life--;
            foreach (GameObject x in Hearts)
            {
                x.SetActive(false);
            }
            if (Life == 0 && EndTimer==0f && !Ended)
            {
                print("dead");
                if (crocbite) eaten = 1;
                LoseSFX.PlayOneShot(LoseSFX.clip);
                RiverPathobj.SendMessage("Kill");
                VictoryBool = false;
                EndTimer = 4f;
                WinScreen.SetActive(true);
            }
            else
            {
                for (int i = 0; i < Life; i++)
                {
                    Hearts[i].SetActive(true);
                }
            }
        }


        void fruitAddScore(float scoref)
        {
            switch (CurrentStage)  //probably drop this
            {
                case 1:
                    Stage1ScoreFloat += scoref;
                    break;
                case 2:
                    Stage2ScoreFloat += scoref;
                    break;
                case 3:
                    Stage3ScoreFloat += scoref;
                    break;

            };
            fruitScoreFloat += scoref;
            totalScoreFloat += scoref;

        }

        void extraLife()
        {
            Life++;
            foreach (GameObject x in Hearts)
            {
                x.SetActive(false);
            }

            for (int i = 0; i < Life; i++)
            {
                Hearts[i].SetActive(true);
            }
            
        }

        void addTime(float timef)
        {
            /*switch (CurrentStage) //deal with this if it actually helps gameplay
            {
                case 1:
                    Stage1ScoreFloat += scoref;
                    break;
                case 2:
                    Stage2ScoreFloat += scoref;
                    break;
                case 3:
                    Stage3ScoreFloat += scoref;
                    break;
            };*/

            timeTotal=timef;
            EndTimer=4f;
            VictoryBool = true;
            WinScreen.SetActive(true);
            print("addTime" + timef);
        }


        void EndTime()
        {
            print("RiverEndGame");
            Time.timeScale = 0f;


            if (VictoryBool == true) 
            {
                int vict = 1000;
                int timePen = 10;
 /*               if (SingletonSimple.Instance.riverBabyMode)
                {
                    vict = 10;
                    timePen = 1;
                }*/
                winStuff[0] = 1;
                winStuff[1] = (int)fruitScoreFloat;
                winStuff[2] = (int)timeTotal;
                timeScore = timePen * (200 - (int)timeTotal);
                if (timeScore < 0) timeScore = 0;
                winStuff[3] = timeScore;
                winStuff[4] = Life;
                winStuff[5] = vict + winStuff[1] + winStuff[3] + (Life * vict/10);
/*                SingletonSimple.Instance.riverTotalScore = Mathf.Max(winStuff[5], SingletonSimple.Instance.riverTotalScore);
*/
            }

            else //lose
            { 
                winStuff[0] = 0;
                winStuff[1] = eaten;
            }
            MainCamera.SetActive(true);
            WinScreen.SendMessage("DoWinScreen", winStuff);
        }


    }

}