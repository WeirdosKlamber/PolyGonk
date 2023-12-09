using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace WeirdosKlamber.PolyGonk.MainMenu
{
    public class LevelButton : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject face;
        public TextMeshProUGUI levelLabel;
        private bool isStars = false;
        public GameObject labelContainer;
        public GameObject starsContainer;
        public GameObject star1;
        public GameObject star1disabled;
        public GameObject star2;
        public GameObject star2disabled;
        public GameObject star3;
        public GameObject star3disabled;
        public AudioSource clickFx;
        public AudioSource clickReleaseFx;
        public int levelNumber;
        private bool star1won = false;
        private bool star2won = false;
        private bool star3won = false;

        void Start()
        {
            levelLabel.text = PolyGonkScript.GetText("Level") +" "+levelNumber.ToString();
            starsUpdate();
            if (GetComponent<Button>().interactable)
            {
                ButtonEnable();
            }
            else
            {
                ButtonDisable();
            }
        }

        void starsUpdate()
        {
            if (isStars)
            {
                starsContainer.SetActive(true);
                labelContainer.SetActive(false);
                if (star3won)
                {
                    star3.SetActive(true);
                    star3disabled.SetActive(false);
                    star2.SetActive(true);
                    star2disabled.SetActive(false);
                    star1.SetActive(true);
                    star1disabled.SetActive(false);

                    star1.GetComponent<SpriteRenderer>().color = Color.yellow;
                    star2.GetComponent<SpriteRenderer>().color = Color.yellow;
                    star3.GetComponent<SpriteRenderer>().color = Color.yellow;

                    ButtonDisable();
                }
                else if (star2won)
                {
                    star3.SetActive(false);
                    star3disabled.SetActive(true);

                    star2.SetActive(true);
                    star2disabled.SetActive(false);
                    star1.SetActive(true);
                    star1disabled.SetActive(false);
                }
                else if (star1won)
                {
                    star3.SetActive(false);
                    star3disabled.SetActive(true);
                    star2.SetActive(false);
                    star2disabled.SetActive(true);

                    star1.SetActive(true);
                    star1disabled.SetActive(false);
                }

            }
            else
            {
                starsContainer.SetActive(false);
                labelContainer.SetActive(true);
            }
        }

        public void ButtonPress()
        {
            clickFx.Play();
            starsContainer.transform.localPosition = new Vector2(0f, 0f);
            labelContainer.transform.localPosition = new Vector2(0f, 0f);
            levelLabel.color = new Color(0.8f, 0.8f, 0.8f);
            starsUpdate();
        }

        public void ButtonRelease()
        {
            clickReleaseFx.Play();
            starsContainer.transform.localPosition = new Vector2(-2f, 1f);
            labelContainer.transform.localPosition = new Vector2(-1f, 1f);
            levelLabel.color = Color.white;
            if (!star3won)
            {
                star1.GetComponent<SpriteRenderer>().color = Color.white;
                star2.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (levelNumber < 7)
            {
                mainMenu.SendMessage("LevelButtonPress", levelNumber);
            }
            else
            {
                face.SendMessage("Eat");
                gameObject.SetActive(false);
            }
        }

        public void ButtonDisable()
        {
            GetComponent<Button>().interactable = false;
            starsContainer.transform.localPosition = new Vector2(0f, 0f);
            labelContainer.transform.localPosition = new Vector2(0f, 0f);
            levelLabel.color = Color.grey;
        }

        public void ButtonEnable()
        {
            GetComponent<Button>().interactable = true;
            starsContainer.transform.localPosition = new Vector2(-2f, 1f);
            labelContainer.transform.localPosition = new Vector2(-1f, 1f);
            levelLabel.color = Color.white;
        }

        public void StarsWon(int starsNumber)
        {
            if (starsNumber > 0) { isStars= true; }
            if (starsNumber >= 1) { star1won = true; }
            if (starsNumber >= 2) { star2won = true; }
            if (starsNumber >= 3) { star3won = true; }
            starsUpdate();
        }
    }
}
