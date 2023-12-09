using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class EarthSectionScript : MonoBehaviour
    {
        // Start is called before the first frame update
        public int Life = 10;
        public bool alive = true;
        public bool hit = false;
        private float flasher = 0.0f;
        public bool falling = false;
        private float Redcol = 1.0f;
        private float Greencol = 1.0f;
        private float Bluecol = 1.0f;
        public float redflashrate = 10f;
        private bool redswitcher = false;
        private Color spritcolour = new Color(1f, 1f, 1f, 1f);
        public float killtimer = 1f;
        public bool killable = true;
        public bool housesupporter = false;
        public bool housescarer = false;
        public bool houseroller = false;
        public bool baseofcliff = false;
        private float landingdelay = 1f;
        public int cliffbreaksfxn = 0;
        public GameObject Groynemaster;
        void Start()
        {
            landingdelay = killtimer - 0.1f;

        }

        // Update is called once per frame
        void Update()
        {
            if (alive == false)
            {
                killtimer -= Time.deltaTime;
                if (killtimer < 0f)
                {
                    Destroy(gameObject, .5f);
                }
            }

            if (hit == true)
            {
                flasher += Time.deltaTime;
                Greencol = Mathf.Max(0f, 1f - flasher * 10);
                Bluecol = Mathf.Max(0f, 1f - flasher * 10);
                if (redswitcher == false)
                {
                    Redcol -= redflashrate * Time.deltaTime;
                    if (Redcol < 0f)
                    {
                        redswitcher = true;
                        Redcol = 0f;
                    }
                }
                else
                {
                    Redcol += redflashrate * Time.deltaTime;
                    if (Redcol > 1f)
                    {
                        redswitcher = false;
                        Redcol = 1f;
                    }
                }




                spritcolour = new Color(Redcol, Greencol, Bluecol, 1f);
                GetComponent<SpriteRenderer>().color = spritcolour;
                if (flasher > 0.5f)
                {

                    hit = false;
                    flasher = 0.0f;
                    Redcol = 1f;
                    Greencol = 1f;
                    Bluecol = 1f;
                    spritcolour = new Color(Redcol, Greencol, Bluecol, 1f);
                    redswitcher = false;

                    GetComponent<SpriteRenderer>().color = spritcolour;
                }
            }

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Water" | collision.gameObject.tag == "WaterTidal")
            {
                hit = true;
                flasher = 0;
                Life--;
                if (Life < 1 & killable == true & alive == true)
                {
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    alive = false;
                    if (GetComponent<BoxCollider2D>() != null) //this will also stop if from running again
                    {
                        Destroy(GetComponent<BoxCollider2D>());
                    }
                    else if (GetComponent<PolygonCollider2D>() != null)


                    { Destroy(GetComponent<PolygonCollider2D>()); }
                    // 

                    if (housesupporter == true)
                    {

                        GameObject.Find("House").SendMessage("FallHouse");
                    }

                    if (housescarer == true)
                    {

                        GameObject.Find("House").SendMessage("HouseScare");

                    }

                    if (baseofcliff == true)
                    {


                        Groynemaster.SendMessage("GroyneGrow");
                    }


                    if (houseroller == true)
                    {

                        GameObject.Find("CoastMain").SendMessage("ScaredRoll");
                        Groynemaster.SendMessage("GroyneGrow");
                    }

                    GameObject.Find("CoastMain").SendMessage("SectionFall");
                    transform.parent.gameObject.SendMessage("CliffFall", landingdelay);



                }
            }
        }

        void Typhoon()
        {
            Life = 1;

        }

    }
}