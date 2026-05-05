using UnityEngine;

namespace WeirdosKlamber.PolyGonk
{
    public class BoardScript : MonoBehaviour
    {
        public Sprite[] boardSprites = new Sprite[7];
        private float animTimerMaster = 0.1f;
        private float animTimer = 0f;
        private float[] springSFXTimers = new float[5];
        private float[] springAnimTimers = new float[5];
        private bool springBool = false;
        public bool boardIsLeft;
        private int currentFrame = 0;
        private AudioSource audioSource;
        private AudioClip springFX;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            springFX = audioSource.clip;

        }

        void Update()
        {
            if (springBool)
            {
                animTimer -= Time.deltaTime;
                if (animTimer <= 0f)
                {
                    currentFrame++;
                    if (currentFrame >= boardSprites.Length - 1)
                    {
                        currentFrame = 0;
                        springBool = false;
                    }
                    gameObject.GetComponent<SpriteRenderer>().sprite = boardSprites[currentFrame];
                    animTimer = animTimerMaster; //anim framerate
                }
            }

            else if (currentFrame>0)
            {
                currentFrame = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = boardSprites[currentFrame];
            }

            for (int i = 0; i < springSFXTimers.Length; i++)
            {
                if (springSFXTimers[i]>0f && springSFXTimers[i]-Time.deltaTime <= 0f)
                {
                    springSFXTimers[i] = 0f;
                    audioSource.pitch = 0.95f + (Random.value / 5f);
                    audioSource.PlayOneShot(springFX);                    
                }
                springSFXTimers[i] -= Time.deltaTime;
            }

            for (int i = 0; i < springAnimTimers.Length; i++)
            {
                if (springAnimTimers[i] > 0f && springAnimTimers[i] - Time.deltaTime <= 0f)
                {
                    springAnimTimers[i] = 0f;
                    springBool = true;
                    animTimer = 0f;
                }
                springAnimTimers[i] -= Time.deltaTime;
            }
        }

        public void Spring()
        {
            for (int i = 0; i < springAnimTimers.Length; i++)
            {
                if (springAnimTimers[i] <= 0f)
                {
                    springAnimTimers[i] = 1.8f;
                    break;
                }
            }

            for (int i = 0; i < springSFXTimers.Length; i++) 
            {
                if (springSFXTimers[i]<=0f)
                {
                    springSFXTimers[i] = 1.1f;
                    break;
                }
            }
        }
    }
}