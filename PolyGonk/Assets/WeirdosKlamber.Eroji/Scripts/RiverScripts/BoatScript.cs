using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeirdosKlamber.PolyGonk.River;

namespace WeirdosKlamber.PolyGonk.River.Boat
{
    public class BoatScript : MonoBehaviour
    {
        public GameObject RiverMaster;
        public GameObject PathFollower;
        public GameObject RiverPath;
        public GameObject Kayak;
        public GameObject Oar;
        private float masterSideSpeed = 0.2f;
        private float sideSpeed = 0.2f;
        public Vector2 LeftRight;
        private Rigidbody2D R2D2;
        private float wanderlimit = 2.5f;
        private bool LeftPressed = false;
        private bool RightPressed = false;
        public Button leftButton;
        public Button rightButton;
        private bool immune = false;
        private float immuneFlashTime = 2f;
        private float immuneFlashCountr = 3f;
        private float flashRed = 1f;
        private float flashRedPerSec = 8f;
        private bool flashredup = false;
        public GameObject BangUpper;

        public AudioSource RockSFX;
        public AudioSource LogSFX;
        public AudioSource MonkeybiteSFX;
        public AudioSource CrocSFX;
        public AudioSource SpiderSFX;

        public AudioSource appleSFX;
        public AudioSource bananaSFX;
        public AudioSource melonSFX;
        public AudioSource heartSFX;

        private bool alive = true;
        // Start is called before the first frame update
        private void Awake()
        {
            R2D2 = gameObject.GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            LeftRight = new Vector2(0f, sideSpeed);

        }

        private void Update()
        {
            sideSpeed = (masterSideSpeed * 2f + River.RiverPath.RiverPathScript.currentspeed) / 3f;
            

            if (immune == true)
            {
                immuneFlashCountr -= Time.deltaTime;
                if (immuneFlashCountr < 0f)
                {
                    immune = false;
                    Kayak.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    if (flashredup == false)
                    {
                        flashRed -= Time.deltaTime * flashRedPerSec;
                        if (flashRed < 0f) flashredup = true;
                    }
                    else
                    {
                        flashRed += Time.deltaTime * flashRedPerSec;
                        if (flashRed > 1f) flashredup = false;
                    }

                    Kayak.GetComponent<SpriteRenderer>().color = new Color(1f, 1f - flashRed, 1f - flashRed);
                }

            }
        }

        // Update is called once per frame
        void FixedUpdate()  //using fixed because directly changing transforms because the alternatives don't work
        {
            if (alive)
            {

                if ((Input.GetKey("left") || LeftPressed == true) && (Input.GetKey("right") || RightPressed == true))
                {
                    RiverPath.SendMessage("turboboost", true);
                    leftButton.GetComponent<Image>().color = Color.green;
                    rightButton.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    RiverPath.SendMessage("turboboost", false);
                    leftButton.GetComponent<Image>().color = Color.white;
                    rightButton.GetComponent<Image>().color = Color.white;



                    if (Input.GetKey("left") || LeftPressed == true)
                    {

                        //R2D2.MovePosition(R2D2.position + LeftRight * Time.deltaTime);
                        //print(R2D2.position);
                        transform.localPosition -= Vector3.down * sideSpeed * Time.deltaTime;
                        //GetComponent<Rigidbody2D>().velocity= new Vector3(0,10 * Time.fixedDeltaTime,0);
                        //velocity causes problems, whole thing starts rotating
                        Kayak.GetComponent<SpriteRenderer>().flipX = true;
                        Oar.GetComponent<SpriteRenderer>().flipX = true;
                        // GetComponent<Rigidbody2D>().velocity = new Vector3(0f, sideSpeed, 0f) * Time.deltaTime; // Creat
                    }
                    else if (Input.GetKey("right") || RightPressed == true)
                    {
                        //GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position - LeftRight* Time.deltaTime);
                        //GetComponent<Rigidbody2D>().velocity = new Vector3(0, -10 * Time.fixedDeltaTime, 0);
                        transform.localPosition -= Vector3.up * sideSpeed * Time.deltaTime;
                        Kayak.GetComponent<SpriteRenderer>().flipX = false;
                        Oar.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    /* else 
                     {
                         GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                     }*/
                }

                if (transform.localPosition[1] > wanderlimit)
                {
                    transform.localPosition += Vector3.down * 10 * sideSpeed * Time.deltaTime;
                }
                if (transform.localPosition[1] < 0 - wanderlimit)
                {
                    transform.localPosition -= Vector3.down * 10 * sideSpeed * Time.deltaTime;
                }
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Leftbank")
            {
                // print("hitleft");
                transform.localPosition -= Vector3.up * 10 * sideSpeed * Time.deltaTime;
                //   Kayak.GetComponent<SpriteRenderer>().flipX = false;
            }

            if (col.gameObject.tag == "Rightbank")
            {
                transform.localPosition -= Vector3.down * 10 * sideSpeed * Time.deltaTime;
                //  Kayak.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (col.gameObject.tag == "Rock")
            {
                if (immune == false)
                {
                    RiverMaster.SendMessage("Hit", false);
                    immuneFlashCountr = immuneFlashTime;
                    immune = true;
                    flashredup = false;
                    RockSFX.PlayOneShot(RockSFX.clip);
                }
            }
            if (col.gameObject.tag == "Monkey")
            {
                if (immune == false)
                {
                    RiverMaster.SendMessage("Hit", false);
                    immuneFlashCountr = immuneFlashTime;
                    immune = true;
                    flashredup = false;
                    MonkeybiteSFX.PlayOneShot(MonkeybiteSFX.clip);
                }
            }
            if (col.gameObject.tag == "Log")
            {
                if (immune == false)
                {
                    RiverMaster.SendMessage("Hit", false);
                    immuneFlashCountr = immuneFlashTime;
                    immune = true;
                    flashredup = false;
                    LogSFX.PlayOneShot(LogSFX.clip);
                }
            }
            if (col.gameObject.tag == "Spider")
            {
                if (immune == false)
                {
                    RiverMaster.SendMessage("Hit", false);
                    immuneFlashCountr = immuneFlashTime;
                    immune = true;
                    flashredup = false;
                    col.gameObject.SetActive(false);
                    SpiderSFX.PlayOneShot(SpiderSFX.clip);
                }
            }
            if (col.gameObject.tag == "Crocodile")
            {
                    col.gameObject.SendMessage("CrocBite");                
            }

            if (col.gameObject.tag == "Apple")
            {
                RiverMaster.SendMessage("fruitAddScore", 10f);
                BangUpper.SendMessage("BangUpSpawn", 10);
                Destroy(col.gameObject);
                print("Chomp Apple");
                appleSFX.PlayOneShot(appleSFX.clip);
            }
            if (col.gameObject.tag == "Banana")
            {
                RiverMaster.SendMessage("fruitAddScore", 20f);
                BangUpper.SendMessage("BangUpSpawn", 20);
                Destroy(col.gameObject);
                print("Chomp Banana");
                bananaSFX.PlayOneShot(bananaSFX.clip);
            }
            if (col.gameObject.tag == "Melon")
            {
                
                BangUpper.SendMessage("BangUpSpawn", 50);
                Destroy(col.gameObject);
                print("Chomp Melon");
                melonSFX.PlayOneShot(melonSFX.clip);
                
            }
            if (col.gameObject.tag == "Heart")
            {
                
                
                Destroy(col.gameObject);
                print("Chomp Melon");
                heartSFX.PlayOneShot(heartSFX.clip);
                if (WeirdosKlamber.PolyGonk.River.RiverMasterScript.Life==5) 
                {
                    RiverMaster.SendMessage("fruitAddScore", 50f);
                    BangUpper.SendMessage("BangUpSpawn", 100);
                }
                else 
                {
                    RiverMaster.SendMessage("extraLife");
                }

            }
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.tag == "CrocodileBite")
            {
                if (immune == false)
                {
                    RiverMaster.SendMessage("Hit", true);
                    immuneFlashCountr = immuneFlashTime;
                    immune = true;
                    flashredup = false;
                }
            }
        }

        void PressRight()
        {
            RightPressed = true;
        }
        void ReleaseRight()
        {
            RightPressed = false;
        }
        void PressLeft()
        {
            LeftPressed = true;
        }
        void ReleaseLeft()
        {
            LeftPressed = false;
        }

        void Kill()
        {
            alive = false;
        }
    }
}