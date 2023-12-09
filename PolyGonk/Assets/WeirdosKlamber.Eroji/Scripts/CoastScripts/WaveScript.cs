using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class WaveScript : MonoBehaviour
    {

        private int partcount = 0;
        private int maxpartcount = 0;
        public float killbackup = 10f;

        private bool audiodone = false;
        public string Wavesize;
        private float furthestright = 0f;


        // Start is called before the first frame update
        void Start()

        {
            if (audiodone == false)
            {
                audiodone = true;
                transform.parent.gameObject.SendMessage("PlayWaveSFX", Wavesize);
            }
        }
        // Update is called once per frame
        void Update()
        {


            killbackup -= Time.deltaTime;
            if (killbackup < 0)
            {
                Destroy(gameObject);
            }

        }
        void AddParticle()
        {
            partcount++;
            maxpartcount++;
        }

        void KillParticle(float wherex)
        {
            partcount--;

            if (wherex > furthestright)
            {
                furthestright = wherex;
            }

            if (partcount < maxpartcount / 2)
            {

                transform.parent.gameObject.SendMessage("FurthestRight", furthestright);
                Destroy(gameObject);


            }


        }
        void Typhoon()
        {
            if (Wavesize != "typhoon")
            {
                Destroy(gameObject);
            }

        }
    }
}