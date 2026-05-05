
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WeirdosKlamber.PolyGonk
{
    public class LessonScript : MonoBehaviour
    {
        public string nextSceneName;
        public int lessonNumber;
        public GameObject[] slides;
        public AudioSource buttClick;
        private int stepper = 0;

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