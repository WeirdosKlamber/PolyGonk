using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LoLSDK;
using SimpleJSON;
using WeirdosKlamber;
namespace WeirdosKlamber.PolyGonk
{
    public class LessonScript : MonoBehaviour
    {
        public string nextSceneName;
        public int lessonNumber;
        public GameObject[] slides;
        public AudioSource buttClick;
        private int stepper = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void NextSlide()
        {
            buttClick.Play();
            slides[stepper].gameObject.SetActive(false);
            stepper++;
            if (stepper == slides.Length)
            {
                SingletonSimple.Instance.LessonCompleted(lessonNumber);
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                slides[stepper].SetActive(true);
            }
        }

        public void TTSButtonPressed()
        {
            SingletonSimple.Instance.Speak();
        }
    }
}