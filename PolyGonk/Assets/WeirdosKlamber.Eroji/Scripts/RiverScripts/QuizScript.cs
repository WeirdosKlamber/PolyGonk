using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace WeirdosKlamber.PolyGonk.River
{
    public class QuizScript : MonoBehaviour
    {
        public GameObject CaveCard;
        public GameObject OxbowCard;
        public GameObject FloodCard;

        public GameObject Quiz1;
        public GameObject Quiz2;
        public GameObject Quiz3;
        public GameObject Instructions;

        public GameObject IndyFace;
        public GameObject monocleFace;
        public GameObject happyFace;
        public GameObject SadFace;

        public GameObject MainCamera;
        public GameObject PickText;


        // Start is called before the first frame update, doesn't seem like it
        void Start()
        {
            Time.timeScale = 0;
            PickText.GetComponent<TextMeshProUGUI>().text = WeirdosKlamber.PolyGonk.PolyGonkScript.GetText("RiverPick");
            if (SingletonSimple.Instance.level5Completed == true)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void questionFinish(int questionN)
        {
            switch (questionN)
            {
                case (-1):
                    {
                        IndyFace.SetActive(false);
                        monocleFace.SetActive(true);
                        happyFace.SetActive(false);
                        SadFace.SetActive(false);
                        Quiz1.SetActive(true);
                        break;
                    }
                case (1):
                    {
                        monocleFace.SetActive(true);
                        happyFace.SetActive(false);
                        SadFace.SetActive(false);
                        Quiz2.SetActive(true);
                        break;
                    }
                case (2):
                    {
                        monocleFace.SetActive(true);
                        happyFace.SetActive(false);
                        SadFace.SetActive(false);
                        Quiz3.SetActive(true);
                        break;
                    }
                case (3):
                    {
                        IndyFace.SetActive(true);
                        monocleFace.SetActive(false);
                        happyFace.SetActive(false);
                        SadFace.SetActive(false);
                        Instructions.SetActive(true);
                        break;
                    }
                case (5):
                    {
                        SingletonSimple.Instance.level5Completed = true;
                        SingletonSimple.Instance.SaveGame();
                        Time.timeScale = 1f;
                        //MainCamera.SetActive(false);
                        gameObject.SetActive(false);
                        break;
                    }
            }
        }
        void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }


    }
}