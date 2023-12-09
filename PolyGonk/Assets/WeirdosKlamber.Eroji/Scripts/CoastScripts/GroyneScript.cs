using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class GroyneScript : MonoBehaviour
    {
        public int GroyneNum;
        public GameObject SandObj;
        private float sandfreq = 0.5f;
        private float sandtimer;
        private float Sweeper;
        private float Sweepmax = 2.4f;

        // Start is called before the first frame update
        void Start()
        {

            sandtimer = sandfreq;
        }

        // Update is called once per frame
        void Update()
        {
            sandtimer -= Time.deltaTime;
            if (sandtimer < 0f)
            {
                sandtimer = sandfreq;

                Sweeper = Random.Range(0, Sweepmax);
                Instantiate(SandObj, new Vector2(transform.position[0] - Sweeper, transform.position[1] + 1f), Quaternion.identity);
            }

        }
    }
}