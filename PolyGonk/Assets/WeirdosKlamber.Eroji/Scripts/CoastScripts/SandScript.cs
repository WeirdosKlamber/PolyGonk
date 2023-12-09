using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class SandScript : MonoBehaviour
    {
        public int Life = 5;
        public bool StartSand;
        public bool AliveSand;
        public bool IsSand = false;
        public bool IsRipRap = false;
        public bool IsGabion = false;
        public GameObject RipRapObj1;
        public GameObject RipRapObj2;
        public GameObject RipRapObj3;
        private float riprapgap = 0.5f;
        public AudioSource landingsound;
        public AudioSource splashsound;
        public bool landed = false;

        // Start is called before the first frame update
        void Start()
        {
            //Life = Random.Range(1, Life); 
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Water" | collision.gameObject.tag == "WaterTidal")
            {
                Life--;
                if (collision.gameObject.tag == "WaterTidal") Life = 1;


                if (Life < 1 & AliveSand == true)
                {

                    if (IsGabion == true)
                    {
                        Instantiate(RipRapObj1, new Vector2(gameObject.transform.position[0] - riprapgap, gameObject.transform.position[1] - riprapgap), transform.rotation);
                        Instantiate(RipRapObj2, new Vector2(gameObject.transform.position[0] + riprapgap, gameObject.transform.position[1] - riprapgap), transform.rotation);
                        Instantiate(RipRapObj3, new Vector2(gameObject.transform.position[0] - riprapgap, gameObject.transform.position[1] + riprapgap), transform.rotation);
                        Instantiate(RipRapObj1, new Vector2(gameObject.transform.position[0] + riprapgap, gameObject.transform.position[1] + riprapgap), transform.rotation);
                        IsGabion = false;
                    }

                    else
                    {
                        AliveSand = false;
                        Destroy(gameObject, .5f);
                    }
                }

                if (landed == false && IsSand == false)
                {
                    landed = true;

                    splashsound.PlayOneShot(splashsound.clip, 1f);

                }
            }
            else if (landed == false && IsSand == false)
            {
                landed = true;

                landingsound.PlayOneShot(landingsound.clip, 1f);
            }

        }

        void Typhoon()
        {
            Life = 1;

        }
    }
}