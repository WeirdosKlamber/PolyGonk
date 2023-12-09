using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class GroyneMaster : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject Groyne1;
        public GameObject Groyne2;
        public GameObject Groyne3;
        public GameObject Groyne4;
        public GameObject Groyne5;
        public GameObject Groynebutton;


        private int GroyneAvailable = 2;
        private int GroyneActive = 0;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void GroyneGrow()
        {
            GroyneAvailable++;
            GroyneTest();
        }

        void GroyneTest()
        {

            if (GroyneActive <= GroyneAvailable && GroyneActive < 5) //testing <=
            {

                Groynebutton.SetActive(true);
            }
            else Groynebutton.SetActive(false);

        }

        void BuyGroyne()
        {

            GroyneActive++;
            if (GroyneActive > 0)
            {
                Groyne1.SetActive(true);
            };
            if (GroyneActive > 1)
            {
                Groyne2.SetActive(true);

            }
            if (GroyneActive > 2)
            {
                Groyne3.SetActive(true);

            }
            if (GroyneActive > 3)
            {
                Groyne4.SetActive(true);

            }
            if (GroyneActive > 4)
            {
                GroyneActive++;
                Groyne5.SetActive(true);
            }

            GroyneTest();


        }



    }
}