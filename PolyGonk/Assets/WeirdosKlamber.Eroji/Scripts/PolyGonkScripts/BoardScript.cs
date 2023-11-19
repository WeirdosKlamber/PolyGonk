using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk
{
    public class BoardScript : MonoBehaviour
    {
        public Sprite[] BoardSprites = new Sprite[7];
        private float AnimTimerMaster = 0.1f;
        private float AnimTimer = 0f;
        private float[] SpringsfxTimers = new float[5];
        private float[] SpringanimTimers = new float[5];
        private bool springBool = false;
        public bool boardIsLeft;
        private int currentFrame = 0;
        private AudioSource audioSource;
        private AudioClip springFX;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            springFX = audioSource.clip;

        }

        // Update is called once per frame
        void Update()
        {
            if (springBool)
            {
                AnimTimer -= Time.deltaTime;
                if (AnimTimer <= 0f)
                {
                    currentFrame++;
                    if (currentFrame >= BoardSprites.Length - 1)
                    {
                        currentFrame = 0;
                        springBool = false;
                    }
                    gameObject.GetComponent<SpriteRenderer>().sprite = BoardSprites[currentFrame];
                    AnimTimer = AnimTimerMaster; //anim framerate
                }
            }
            else if (currentFrame>0)
            {
                currentFrame = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = BoardSprites[currentFrame];
            }

            for (int i = 0; i < SpringsfxTimers.Length; i++)
            {
                if (SpringsfxTimers[i]>0f && SpringsfxTimers[i]-Time.deltaTime <= 0f)
                {
                    SpringsfxTimers[i] = 0f;
                    audioSource.pitch = 0.95f + (Random.value / 5f);
                    audioSource.PlayOneShot(springFX);                    
                }
                SpringsfxTimers[i] -= Time.deltaTime;
            }

            for (int i = 0; i < SpringanimTimers.Length; i++)
            {
                if (SpringanimTimers[i] > 0f && SpringanimTimers[i] - Time.deltaTime <= 0f)
                {
                    SpringanimTimers[i] = 0f;
                    springBool = true;
                    AnimTimer = 0f;
                }
                SpringanimTimers[i] -= Time.deltaTime;
            }
        }

        public void Spring()
        {
            /*            if (springBool && currentFrame < 5)
                        {
                            AnimTimerMaster *= 0.9f;
                            print("SPEEDING UP SPRINGBOARD");
                        }*/
            //AnimTimer = 1.8f;
            //springBool = true;
            for (int i = 0; i < SpringanimTimers.Length; i++)
            {
                if (SpringanimTimers[i] <= 0f)
                {
                    SpringanimTimers[i] = 1.8f;
                    break;
                }
            }
            for (int i = 0; i < SpringsfxTimers.Length; i++) 
            {
                if (SpringsfxTimers[i]<=0f)
                {
                    SpringsfxTimers[i] = 1.1f;
                    break;
                }
            }

        }

    }
}