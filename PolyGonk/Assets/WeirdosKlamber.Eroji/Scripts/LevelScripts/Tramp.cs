using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace WeirdosKlamber.PolyGonk
{
    public class Tramp : MonoBehaviour
    {
        public GameObject main;
        public GameObject leftButton;
        public GameObject rightButton;
        public GameObject glower;
        public GameObject glower2;
        public Sprite[] trampolineSprites = new Sprite[7];
        private AudioSource audioSource;
        private AudioClip boingFX;
        public AudioClip[] yeahsFX;

        private int currentPlace = 0;
        private float[] bounceXs = { -2.3f, 0.7f, 3.7f, 6.7f, 9.7f }; //improve
        public int numberOfCollectors = 5;
        private float timerMaster = 0.02f;
        private float currentTimer = 0f;
        private int currentFrame = 0;
        private bool buttonLeftPressed = false;
        private bool buttonRightPressed = false;
        private bool keyPressed = false;
        private bool playEnded = false;
        private bool flashRed = false;
        private bool flashGreen = false;
        private bool flashYellow = false;
        private float flashTimerMaster = 0.3f;
        private float flashTimer = 0f;
        private float killTime = 1f;
        public bool rightSpawner = false;
        
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            boingFX = audioSource.clip;
        }

        // Update is called once per frame
        void Update()
        {
            if (!playEnded)
            {
                if (!keyPressed && (Input.GetKeyDown("left") || Input.GetKeyDown("right")))
                {
                    keyPressed = true;
                    leftButton.SetActive(false);
                    rightButton.SetActive(false);
                }

                if (Input.GetKeyDown("left") || buttonLeftPressed)
                {
                    if (currentPlace > 0)
                    {
                        currentPlace--;
                        transform.position = new Vector2(bounceXs[currentPlace], transform.position.y);
                    }
                    buttonLeftPressed = false;
                }
                else if (Input.GetKeyDown("right") || buttonRightPressed)
                {
                    if (currentPlace < numberOfCollectors - 2 || rightSpawner && currentPlace < numberOfCollectors - 1)
                    {
                        currentPlace++;
                        transform.position = new Vector2(bounceXs[currentPlace], transform.position.y);
                    }
                    buttonRightPressed = false;
                }

                if (currentFrame > 0)
                {
                    currentTimer -= Time.deltaTime;
                    if (currentTimer < 0f)
                    {
                        currentTimer = timerMaster;
                        currentFrame++;
                        if (currentFrame >= trampolineSprites.Length)
                        {
                            currentFrame = 0;
                        }

                        GetComponent<SpriteRenderer>().sprite = trampolineSprites[currentFrame];

                    }
                }
            }
            else
            {
                killTime -= Time.deltaTime;
                if (killTime < 0f) { gameObject.SetActive(false); }
            }

            if (flashTimer>0f)
            {
                flashTimer -= Time.deltaTime;
                if (flashTimer<0f)
                {
                    flashTimer = flashTimerMaster;
                    flashRed= false;
                    flashGreen= false;
                    flashYellow= false;
                    glower.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                    glower2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

                }
            }
        }
        private void trampBounce()
        {
            if (currentFrame == 0)
            {
                currentFrame = 1; currentTimer = timerMaster;
                GetComponent<SpriteRenderer>().sprite = trampolineSprites[currentFrame];
                audioSource.pitch = 0.95f + (Random.value / 5f);
                audioSource.PlayOneShot(boingFX);
            }
        }

        public void buttonPressLeft()
        {
            buttonLeftPressed = true;
        }
        public void buttonPressRight()
        {
            buttonRightPressed = true;
        }
        public void endPlay()
        {
            playEnded = true;
        }

        public void FlashRed()
        {
            flashRed = true;
            flashGreen = false;
            flashYellow = false;
            flashTimer = flashTimerMaster;
            glower.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
            glower2.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
        }

        public void FlashGreen()
        {
            flashRed = false;
            flashGreen = true;
            flashYellow = false;
            flashTimer = flashTimerMaster;
            glower.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f);
            glower2.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.5f);
        }

        public void FlashYellow()
        {
            flashRed = false;
            flashGreen = false;
            flashYellow = true;

            flashTimer = flashTimerMaster;
            glower.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0.5f);
            glower2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0.5f);
        }
    }
}