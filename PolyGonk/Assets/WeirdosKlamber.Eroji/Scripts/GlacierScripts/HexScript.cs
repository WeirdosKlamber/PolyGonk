using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.Glacier;

namespace WeirdosKlamber.PolyGonk.Glacier.Hex
{
    public class HexScript : MonoBehaviour
    {
        public GameObject Main;
        public GameObject Arrow;

        public GameObject[] RightArray;
        public GameObject[] UpRightArray;
        public GameObject[] DownRightArray;
        public GameObject[] LeftArray;
        public GameObject[] DownLeftArray;

        public GameObject RightUValley1;
        public GameObject RightUValley2;
        public GameObject UpRightUValley1;
        public GameObject UpRightUValley2;
        public GameObject DownRightUValley1;
        public GameObject DownRightUValley2;
        public GameObject LeftUValley1;
        public GameObject LeftUValley2;
        public GameObject DownLeftUValley1;
        public GameObject DownLeftUValley2;

        private GameObject[] currentArray;
        private GameObject[] currentArray2;
        private int rightAnim1 = 0;
        private int rightAnim2 = 0;
        private int upRightAnim1 = 0;
        private int upRightAnim2 = 0;
        private int downRightAnim1 = 0;
        private int downRightAnim2 = 0;
        private int LeftAnim1 = 0;
        private int LeftAnim2 = 0;
        private int downLeftAnim1 = 0;
        private int downLeftAnim2 = 0;

        private float animSpeed = 0.3f;
        private float animTimer = 0f;

        public GameObject MountainHex;

        public GameObject ULBridge1;
        public GameObject ULBridge2;
        public GameObject DRBridge1;
        public GameObject DRBridge2;
        public GameObject DBridge1;
        public GameObject DBridge2;
        public GameObject DLBridge1;
        public GameObject DLBridge2;

        public GameObject currentBridge1;
        public GameObject currentBridge2;

        public GameObject BLCollider;
        public GameObject BCollider;
        public GameObject BRCollider;

        public GameObject currentCollider;

        private Vector2 TLPoint = new Vector2(-0.3f, 0.5f);
        private Vector2 TRPoint = new Vector2(0.3f, 0.5f);
        private Vector2 LPoint = new Vector2(-0.58f, 0f);
        private Vector2 RPoint = new Vector2(0.58f, 0f);
        private Vector2 BLPoint = new Vector2(-0.3f, -0.54f);
        private Vector2 BRPoint = new Vector2(0.3f, -0.54f);

        private float moveCode = 0f;

        private bool mountainHit = false;

        public bool isHill = false;

        private int draLayer = 1;

        private GameObject flashObj;
        private float flashTime = 0.3f;
        private float flashTimer = 0.3f;
        private bool flashing = false;

        public AudioSource GlacierSFX1;
        public AudioSource GlacierSFX2;
        public AudioSource RockfallSFX1;
        public AudioSource RockfallSFX2;

        // Start is called before the first frame update
        void Start()
        {
            currentArray = RightArray;
            currentArray2 = RightArray;
            currentBridge1 = DRBridge1;
            currentBridge2 = DRBridge2;
            currentCollider = BLCollider;

            draLayer = MountainHex.GetComponent<SpriteRenderer>().sortingOrder+1;

            UpRightUValley1.GetComponent<SpriteRenderer>().sortingOrder = draLayer-1;
            UpRightUValley2.GetComponent<SpriteRenderer>().sortingOrder = draLayer+1;
            RightUValley1.GetComponent<SpriteRenderer>().sortingOrder = draLayer-1;
            RightUValley2.GetComponent<SpriteRenderer>().sortingOrder = draLayer;
            DownRightUValley1.GetComponent<SpriteRenderer>().sortingOrder = draLayer-1;
            DownRightUValley2.GetComponent<SpriteRenderer>().sortingOrder = draLayer;
            LeftUValley1.GetComponent<SpriteRenderer>().sortingOrder = draLayer-1;
            LeftUValley2.GetComponent<SpriteRenderer>().sortingOrder = draLayer;
            DownLeftUValley1.GetComponent<SpriteRenderer>().sortingOrder = draLayer-1;
            DownLeftUValley2.GetComponent<SpriteRenderer>().sortingOrder = draLayer;
            draLayer++;
            foreach (GameObject x in RightArray)
            {
                x.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            }
            foreach (GameObject x in UpRightArray)
            {
                x.GetComponent<SpriteRenderer>().sortingOrder = draLayer + 1; //special
            }
            foreach (GameObject x in DownRightArray)
            {
                x.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            }
            foreach (GameObject x in LeftArray)
            {
                x.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            }
            foreach (GameObject x in DownLeftArray)
            {
                x.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            }

            DRBridge1.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            DRBridge2.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            DBridge1.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            DBridge2.GetComponent<SpriteRenderer>().sortingOrder = draLayer ;
            DLBridge1.GetComponent<SpriteRenderer>().sortingOrder = draLayer;
            DLBridge2.GetComponent<SpriteRenderer>().sortingOrder = draLayer;

        }

        // Update is called once per frame
        void Update()
        {

            if (downRightAnim1 == 1 || rightAnim1 == 1 || upRightAnim1 == 1 || downLeftAnim1 == 1 || LeftAnim1 == 1)
            {
                if (downRightAnim1 == 1)
                {
                    currentArray = DownRightArray;
                    currentBridge1 = DLBridge1;
                    currentBridge2 = DLBridge2;

                }
                else if (rightAnim1 == 1)
                {
                    currentArray = RightArray;
                    currentBridge1 = DBridge1;
                    currentBridge2 = DBridge2;

                }
                else if (upRightAnim1 == 1)
                {
                    currentArray = UpRightArray;
                    currentBridge1 = DRBridge1;
                    currentBridge2 = DRBridge2;

                }
                else if (downLeftAnim1 == 1)
                {
                    currentArray = DownLeftArray;
                    currentBridge1 = DRBridge1;
                    currentBridge2 = DRBridge2;

                }
                else if (LeftAnim1 == 1)
                {
                    currentArray = LeftArray;
                    currentBridge1 = DBridge1;
                    currentBridge2 = DBridge2;

                }

                animTimer -= Time.deltaTime;

                if (currentArray[0].activeSelf == false)
                {
                    GlacierSFX1.PlayOneShot(GlacierSFX1.clip, 0.8f);
                    currentArray[0].SetActive(true);
                    Glacier.GlacierMainScript.GlacierPath.Add(currentArray[0]);
                    animTimer = animSpeed;
                    Glacier.Arrow.ArrowScript.arrowAvailable = false;
                    Glacier.Arrow.ArrowScript.currentarrowtime = 1f;
                }
                else if (currentArray[1].activeSelf == false && animTimer < 0f)
                {
                    currentArray[0].GetComponent<SpriteRenderer>().color = Color.clear;
                    currentArray[1].SetActive(true);
                    Glacier.Arrow.ArrowScript.arrowAvailable = false;
                    Glacier.Arrow.ArrowScript.currentarrowtime = 1f;
                    Glacier.GlacierMainScript.GlacierPath.Add(currentArray[1]);
                    animTimer = animSpeed;

                }
                else if (currentArray[2].activeSelf == false && animTimer < 0f)
                {
                    currentArray[1].GetComponent<SpriteRenderer>().color = Color.clear;
                    currentArray[2].SetActive(true);
                    Glacier.GlacierMainScript.GlacierPath.Add(currentArray[2]);
                    Glacier.Arrow.ArrowScript.arrowAvailable = false;
                    Glacier.Arrow.ArrowScript.currentarrowtime = 1f;
                    animTimer = animSpeed;
                    if (currentBridge2.activeSelf == true)
                    {
                        flashRed(currentBridge2);
                    }
                    else if (currentBridge1.activeSelf == true)
                    {
                        flashRed(currentBridge1);
                    }


                    if (downRightAnim1 == 1)
                    {
                        downRightAnim1 = 2;
                        DownRightUValley1.SetActive(true);
                        if (currentBridge1.activeSelf == false && currentBridge2.activeSelf == false)
                        {
                            downRightAnim2 = 1;
                        }
                    }
                    else if (rightAnim1 == 1)
                    {
                        rightAnim1 = 2;
                        RightUValley1.SetActive(true);
                        if (currentBridge1.activeSelf == false && currentBridge2.activeSelf == false)
                        {
                            rightAnim2 = 1;
                        }
                    }
                    else if (upRightAnim1 == 1)
                    {
                        upRightAnim1 = 2;
                        UpRightUValley1.SetActive(true);
                        if (currentBridge1.activeSelf == false && currentBridge2.activeSelf == false)
                        {
                            upRightAnim2 = 1;
                        }
                    }
                    else if (downLeftAnim1 == 1)
                    {
                        downLeftAnim1 = 2;
                        DownLeftUValley1.SetActive(true);
                        if (currentBridge1.activeSelf == false && currentBridge2.activeSelf == false)
                        {
                            downLeftAnim2 = 1;
                        }
                    }
                    else if (LeftAnim1 == 1)
                    {
                        LeftAnim1 = 2;
                        LeftUValley1.SetActive(true);
                        if (currentBridge1.activeSelf == false && currentBridge2.activeSelf == false)
                        {
                            LeftAnim2 = 1;
                        }
                    }
                }

            }


            if (downRightAnim2 == 1 || rightAnim2 == 1 || upRightAnim2 == 1 || downLeftAnim2 == 1 || LeftAnim2 == 1)
            {
                if (downRightAnim2 == 1)
                {
                    currentArray2 = DownRightArray;
                    currentBridge1 = DLBridge1;
                    currentBridge2 = DLBridge2;
                    currentCollider = BLCollider;
                    moveCode = 1f;
                }
                else if (rightAnim2 == 1)
                {
                    currentArray2 = RightArray;
                    currentBridge1 = DBridge1;
                    currentBridge2 = DBridge2;
                    currentCollider = BCollider;
                    moveCode = 2f;
                }
                else if (upRightAnim2 == 1)
                {
                    currentArray2 = UpRightArray;
                    currentBridge1 = DRBridge1;
                    currentBridge2 = DRBridge2;
                    currentCollider = BRCollider;
                    moveCode = 3f;
                }
                else if (downLeftAnim2 == 1)
                {
                    currentArray2 = DownLeftArray;
                    currentBridge1 = DRBridge1;
                    currentBridge2 = DRBridge2;
                    currentCollider = BRCollider;
                    moveCode = 4f;
                }
                else if (LeftAnim2 == 1)
                {
                    currentArray2 = LeftArray;
                    currentBridge1 = DBridge1;
                    currentBridge2 = DBridge2;
                    currentCollider = BCollider;
                    moveCode = 5f;
                }

                animTimer -=  Time.deltaTime;
                if (currentArray2[3].activeSelf == false && animTimer < 0f)
                {
                    currentArray2[2].GetComponent<SpriteRenderer>().color = Color.clear;
                    currentArray2[3].SetActive(true);
                    GlacierSFX2.PlayOneShot(GlacierSFX2.clip, 0.8f);
                    Glacier.GlacierMainScript.GlacierPath.Add(currentArray2[3]);
                    Glacier.Arrow.ArrowScript.arrowAvailable = false;
                    Glacier.Arrow.ArrowScript.currentarrowtime = 1f;
                    animTimer = animSpeed;
                }
                else if (currentArray2[4].activeSelf == false && animTimer < 0f)
                {
                    currentArray2[3].GetComponent<SpriteRenderer>().color = Color.clear;
                    currentArray2[4].SetActive(true);
                    Glacier.GlacierMainScript.GlacierPath.Add(currentArray2[4]);
                    Glacier.Arrow.ArrowScript.arrowAvailable = false;
                    Glacier.Arrow.ArrowScript.currentarrowtime = 1f;
                    animTimer = animSpeed;
                    if (Arrow!=null)
                    Arrow.SendMessage("NewTurn", new Vector3(moveCode, transform.position[0], transform.position[1]));
                }

                else if (currentArray2[5].activeSelf == false && animTimer < 0f)
                {
                    currentArray2[4].GetComponent<SpriteRenderer>().color = Color.clear;
                    currentArray2[5].SetActive(true);
                    Glacier.GlacierMainScript.GlacierPath.Add(currentArray2[5]);
                    Glacier.Arrow.ArrowScript.arrowAvailable = false;
                    Glacier.Arrow.ArrowScript.currentarrowtime = 0.5f;
                    animTimer = animSpeed;
                    Destroy(currentCollider);
                    if (downRightAnim2 == 1)
                    {
                        downRightAnim2 = 2;
                        DownRightUValley2.SetActive(true);
                    }
                    else if (rightAnim2 == 1)
                    {
                        rightAnim2 = 2;
                        RightUValley2.SetActive(true);
                    }
                    else if (upRightAnim2 == 1)
                    {
                        upRightAnim2 = 2;
                        UpRightUValley2.SetActive(true);
                    }
                    else if (downLeftAnim2 == 1)
                    {
                        downLeftAnim2 = 2;
                        DownLeftUValley2.SetActive(true);
                    }
                    else if (LeftAnim2 == 1)
                    {
                        LeftAnim2 = 2;
                        LeftUValley2.SetActive(true);
                    }
                    /* else if (currentArray2[5].activeSelf == true && animTimer < 0f)
                        {
                            if (currentCollider != null)
                            {
                                Destroy(currentCollider);
                                Arrow.SendMessage("NewTurn", new Vector3(moveCode, transform.position[0], transform.position[1]));
                                if (downRightAnim2 == 1) downRightAnim2 = 2;
                                else if (rightAnim2 == 1) rightAnim2 = 2;
                                else if (upRightAnim2 == 1) upRightAnim2 = 2;
                                else if (downLeftAnim2 == 1) downLeftAnim2 = 2;
                                else if (LeftAnim2 == 1) LeftAnim2 = 2;
                            }

                        }*/
                }
            }

            

            if (flashing == true && flashObj != null)
            {
                flashTimer -= Time.deltaTime;
                if (flashTimer < 0f)
                {
                    flashObj.GetComponent<SpriteRenderer>().color = Color.white;
                    flashing = false;
                }
            }

        }

        void flashRed(GameObject x)
        {
            flashTimer = flashTime;
            flashObj = x;
            flashObj.GetComponent<SpriteRenderer>().color = Color.red;
            flashing = true;
        }


        void collided(string which)
        {
            print("collide: " + which);
            Glacier.Arrow.ArrowScript.arrowAvailable = false;
            Glacier.Arrow.ArrowScript.currentarrowtime = 1f;
            switch (which)
            {
                case "BL":
                    if (downRightAnim1 >= 1)
                    {

                        if (DLBridge2.activeSelf == true)
                        {
                            DLBridge2.SetActive(false);
                            RockfallSFX1.PlayOneShot(RockfallSFX1.clip, 0.8f);
                            DLBridge1.SetActive(true);
                            flashRed(DLBridge1);
                        }
                        else if (DLBridge1.activeSelf == true)
                        {
                            DLBridge1.SetActive(false);
                            RockfallSFX2.PlayOneShot(RockfallSFX2.clip, 0.8f);
                            downRightAnim2 = 1;
                        }
                        else
                        {
                            downRightAnim2 = 1;

                            // Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0] - 0.3f, transform.position[1] - 0.54f));
                        }
                    }
                    else downRightAnim1 = 1;
                    break;

                case "B":
                    if (Glacier.Arrow.ArrowScript.rightwards == true)
                    {
                        if (rightAnim1 >= 1)
                        {
                            if (DBridge2.activeSelf == true)
                            {
                                DBridge2.SetActive(false);
                                RockfallSFX1.PlayOneShot(RockfallSFX1.clip, 0.8f);
                                DBridge1.SetActive(true);
                                flashRed(DBridge1);
                                print("second hit");
                            }
                            else if (DBridge1.activeSelf == true)
                            {
                                DBridge1.SetActive(false);
                                RockfallSFX2.PlayOneShot(RockfallSFX2.clip, 0.8f);
                                rightAnim2 = 1;
                                print("third hit");
                            }
                            else
                            {
                                rightAnim2 = 1;
                                print("other third hit");
                                //Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0], transform.position[1]));
                                //Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0] - 0.3f, transform.position[1] - 0.54f));
                            }
                        }

                        else
                        {
                            print("first hit");
                            rightAnim1 = 1;
                        }
                    }
                    else
                    {
                        if (LeftAnim1 >= 1)
                        {
                            if (DBridge2.activeSelf == true)
                            {
                                DBridge2.SetActive(false);
                                RockfallSFX1.PlayOneShot(RockfallSFX1.clip, 0.8f);
                                DBridge1.SetActive(true);
                                flashRed(DBridge1);
                            }
                            else if (DBridge1.activeSelf == true)
                            {
                                DBridge1.SetActive(false);
                                RockfallSFX2.PlayOneShot(RockfallSFX2.clip, 0.8f);
                                LeftAnim2 = 1;
                            }
                            else
                            {
                                LeftAnim2 = 1;
                                //Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0], transform.position[1]));
                                //Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0] - 0.3f, transform.position[1] - 0.54f));
                            }
                        }
                        else LeftAnim1 = 1;
                    }
                    break;

                case "BR":
                    if (Glacier.Arrow.ArrowScript.rightwards == true)
                    {
                        if (upRightAnim1 >= 1)
                        {
                            if (DRBridge2.activeSelf == true)
                            {
                                DRBridge2.SetActive(false);
                                RockfallSFX1.PlayOneShot(RockfallSFX1.clip, 0.8f);
                                DRBridge1.SetActive(true);
                                flashRed(DRBridge1);
                            }
                            else if (DRBridge1.activeSelf == true)
                            {
                                DRBridge1.SetActive(false);
                                RockfallSFX2.PlayOneShot(RockfallSFX2.clip, 0.8f);
                                upRightAnim2 = 1;
                            }
                            else
                            {
                                upRightAnim2 = 1;
                                //Arrow.SendMessage("NewTurn", new Vector3(3f, transform.position[0], transform.position[1]));
                                //Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0] - 0.3f, transform.position[1] - 0.54f));
                            }
                        }
                        else upRightAnim1 = 1;
                    }

                    else
                    {
                        if (downLeftAnim1 >= 1)
                        {
                            if (DRBridge2.activeSelf == true)
                            {
                                DRBridge2.SetActive(false);
                                RockfallSFX1.PlayOneShot(RockfallSFX1.clip, 0.8f);
                                DRBridge1.SetActive(true);
                                flashRed(DRBridge1);
                            }
                            else if (DRBridge1.activeSelf == true)
                            {
                                DRBridge1.SetActive(false);
                                RockfallSFX2.PlayOneShot(RockfallSFX2.clip, 0.8f);
                                downLeftAnim2 = 1;
                            }
                            else
                            {
                                downLeftAnim2 = 1;
                                //Arrow.SendMessage("NewTurn", new Vector3(3f, transform.position[0], transform.position[1]));
                                //Arrow.SendMessage("NewTurn", new Vector3(2f, transform.position[0] - 0.3f, transform.position[1] - 0.54f));
                            }
                        }
                        else downLeftAnim1 = 1;
                    }
                    break;


            }

        }
    }
}