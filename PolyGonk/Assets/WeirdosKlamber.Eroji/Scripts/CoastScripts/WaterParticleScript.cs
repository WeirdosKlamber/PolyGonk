using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class WaterParticleScript : MonoBehaviour
    {
        public GameObject WaveObj;
        public int Life = 1;
        public bool Dying = false;
        public float Deathtime = 1.0f;
        public float _speed;
        private Vector2 _direction = Vector2.left;
        private Rigidbody2D rb;
        public float waitstarttime;
        private bool wait = true;
        private float speedmod = 60f;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _speed -= speedmod;
            WaveObj = transform.parent.gameObject;

        }

        // Update is called once per frame
        void Update()
        {
            waitstarttime -= Time.deltaTime;
            if (waitstarttime < 0f)
            {
                if (wait == true)
                {
                    WaveObj.SendMessage("AddParticle");
                    wait = false;
                }

                if (Dying == true)
                {
                    Deathtime -= Time.deltaTime;
                    if (Deathtime < 0.0f)
                    {
                        gameObject.GetComponent<Renderer>().enabled = false;
                        gameObject.SetActive(false);
                        WaveObj.SendMessage("KillParticle", transform.position[0]);
                    }
                    gameObject.tag = "Deadwater";

                }
                else { Move(); };
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Dying == false)
            {
                if (collision.gameObject.tag == "Sand" | collision.gameObject.tag == "Rock" | collision.gameObject.tag == "SeaWall")
                {

                    rb.gravityScale = 0.1f;

                    Dying = true;

                }

            }
        }
        private void Move()
        {
            rb.velocity = _direction * _speed * Time.deltaTime;
            //set all of the drag variables on the Rigidbody
            //to very high, so it slows down when they stop moving.
        }

    }
}