using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk.Coast
{
    public class Seagullscript : MonoBehaviour
    {
        private float speed = 3f;
        public float starttime = 1f;
        private bool flying = false;
        public GameObject CoastMain;
        public AudioSource enterSFX;
        private bool playOnce = true;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            starttime -= Time.deltaTime;
            if (starttime < 0f) flying = true;

            if (flying)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (playOnce &&transform.position.x < 10f)
                {
                    enterSFX.Play();
                    playOnce = false;
                }
                if (transform.position.x < -5f) Destroy(gameObject);
            }
        }

        void fly()
        {
            flying = true;
        }
        void OnCollisionEnter2D(Collision2D col)
        {
            CoastMain.SendMessage("hitSeagull",transform);
            Destroy(gameObject);
        }
    }
}