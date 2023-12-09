using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WeirdosKlamber.PolyGonk.Glacier
{
    public class GlacierMainScript : MonoBehaviour
    {
        static public List<GameObject> GlacierPath = new List<GameObject>(8);
        public GameObject ArrowObj;
        public Image TimeBarBgd;
        public Image TimeBar;
        public Image TimeBarEnd;
        public Image[] ThermExplodArr;
        public Image WarmOverlay;
        private float WarmOver = -0.15f;
        private int ExplodInt = 0;
        private float ExplodeTime = 0.1f;
        private float ExplodingTimer = 0f;
        private float yearX = 0f;
        private float gameLength = 180f;
        static public bool glacierEndBool = false;
        private float score = 0;
        private int maxscore = 1000;
        private bool victoryBool=false;
        private bool endTimeBool=false;
        static public int comboCount = 0;
        private int comboMax = 0;
        public AudioSource thermoExplodeSFX;
        public GameObject WinScreen;
        private int[] winStuff = new int [8];
        // Start is called before the first frame update
        void Start()
        {
            TimeBarEnd.enabled = false;
            foreach (Image x in ThermExplodArr)
            {
                x.enabled = false;
            };
            GlacierPath = new List<GameObject>(8);
            glacierEndBool = false;
            comboCount = 0;
/*            if (SingletonSimple.Instance.glacierBabyMode) 
            {
                maxscore = 100;
                gameLength = 240;
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            if (comboCount > comboMax) comboMax = comboCount;

            if (endTimeBool == false)
            {
                yearX += Time.deltaTime;

                if (victoryBool == true) //speedup if won
                {
                    yearX += Time.deltaTime*4;
                }

                if (yearX >= gameLength * 0.85f)
                {
                    print("End Game");
                    if (victoryBool == false)    //Losegame
                    {

                        score = 0;// GlacierPath.Count * 100;
                        Destroy(ArrowObj);

                    }
                    WarmOverlay.enabled = true;
                    thermoExplodeSFX.PlayOneShot(thermoExplodeSFX.clip, 0.8f);
                    endTimeBool = true;
                    
                    TimeBarBgd.enabled = false;
                    TimeBar.enabled = false;
                    TimeBarEnd.enabled = true;
                }
                else
                {
                    TimeBar.fillAmount = yearX / gameLength + 0.15f;
                }
            }
            else
            {   
                ExplodingTimer -= Time.deltaTime;
                if (ExplodingTimer < 0f)
                {
                    ExplodingTimer = ExplodeTime;
                    if (ExplodInt < ThermExplodArr.Length)
                    {
                        ThermExplodArr[ExplodInt].enabled = true;
                        if (ExplodInt > 0) ThermExplodArr[ExplodInt - 1].enabled = false;
                    }
                    else if (ExplodInt == ThermExplodArr.Length) ExplodeTime = 1f;
                    else if (GlacierPath.Count > 2)
                    {
                        if (ExplodeTime == 1f) ExplodeTime = 0.5f;
                        ExplodeTime -= 0.02f;
                        if (ExplodeTime<0.1f) ExplodeTime = 0.1f;
                        GlacierPath[GlacierPath.Count - 2].GetComponent<SpriteRenderer>().color = Color.white;
                        GlacierPath[GlacierPath.Count - 1].GetComponent<SpriteRenderer>().enabled = false;
                        GlacierPath.RemoveAt(GlacierPath.Count - 1);
                    }
                    else if (glacierEndBool == false)    //Losegame
                    {
                        glacierEndBool = true;

                        if (victoryBool == false)
                        {
                            winStuff[0] = 0;
                            WinScreen.SetActive(true);
                            WinScreen.SendMessage("WinLose", winStuff);
                        }

                        else
                        {
                            WinScreen.SetActive(true);
                            WinScreen.SendMessage("WinLose", winStuff);
                        }
                    }
                    ExplodInt++;
                }

                
                if (WarmOver<0.4f) WarmOver += Time.deltaTime / 8;
                if (WarmOver>0f) WarmOverlay.color = new Color(1f, 0.85f, 0f, WarmOver);
                
            }
        }

        void LakeTime()
        {
            print("LakeTimeMainFunc");
            if (victoryBool == false)
            {
                Destroy(ArrowObj);
                victoryBool = true;
                score = maxscore - ((maxscore/gameLength)*yearX);
                print("max possible:" + maxscore + " currentyear:" + yearX + " maxscore/gameLength)*yearX:" + ((maxscore / gameLength) * yearX));
                print("Finished year: "+ (-100000+((90000/gameLength)*yearX))+"  Speed Bonus:" + (0+score));
                score += GlacierPath.Count * 10;
                print("glacier length bonus = " + (GlacierPath.Count*5));
                score += comboMax * 100;
                print("max Combo bonus = " + (comboMax * 100));
                print("LakeTime Win Overall Score = " + score);

                winStuff[0]=1;
                winStuff[1] = (int)(-85000 + ((90000 / gameLength) * yearX));
                winStuff[2] = (int)(maxscore - ((maxscore / gameLength) * yearX));
                winStuff[3] = (int)(GlacierPath.Count /6);
                winStuff[4] = (int)(GlacierPath.Count * 4.17);
                winStuff[5] = (int)comboMax;
                winStuff[6] = (int)comboMax*(maxscore/10);
                winStuff[7] = winStuff[2] + winStuff[4] + winStuff[6];

/*                SingletonSimple.Instance.glacierTotalScore = Mathf.Max(winStuff[7], SingletonSimple.Instance.glacierTotalScore);
*/
            }
        }
    }
}