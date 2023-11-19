using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Shapes
{
    public class LimbsScript : MonoBehaviour
    {
        public GameObject leftArm;
        public GameObject rightArm;
        public GameObject leftLeg;
        public GameObject rightLeg;
        private GameObject[] arms = new GameObject[2];
        private GameObject[] legs = new GameObject[2];
        private GameObject[] limbs = new GameObject[4];
        private float leftArmTarget;
        private float rightArmTarget;
        private float leftLegTarget;
        private float rightLegTarget;
        private float rotateSpeedMaster = 100f;
        private float rotateSpeed = 0.5f;
        private float springLegYTarget = -0.25f;
        private float normalLegY = -0.61f;
        private float legSpringTimeMaster = 0.15f;
        private float legSpringTimer = -1f;
        private bool swimming = false;
        private float swimTimer = 4f;
        private bool pencil = false;
        private bool stopIt = false;
        private bool edgeRester = false;
        private bool swimDown = false;
        private bool flying = false;
        private bool wavingArms = false;
        private bool wavingLegs = false;

        // Start is called before the first frame update
        void Start()
        {
            arms[0] = leftArm;
            arms[1] = rightArm;
            legs[0] = leftLeg;
            legs[1] = rightLeg;
            limbs[0] = leftArm;
            limbs[1] = rightArm;
            limbs[2] = leftLeg;
            limbs[3] = rightLeg;
            leftArmTarget = leftArm.transform.rotation.z;
            rightArmTarget = rightArm.transform.rotation.z;
            leftLegTarget = leftLeg.transform.rotation.z;
            rightLegTarget = rightLeg.transform.rotation.z;
        }

        // Update is called once per frame
        void Update()
        {
            if (!stopIt)
            {
                rotateSpeed = rotateSpeedMaster * Time.deltaTime;
                if (leftArm.transform.rotation.z > leftArmTarget + rotateSpeed)
                {
                    leftArm.transform.Rotate(0f, 0f, -rotateSpeed);
                }
                else if (leftArm.transform.rotation.z < leftArmTarget - rotateSpeed)
                {
                    leftArm.transform.Rotate(0f, 0f, rotateSpeed);
                }
                if (rightArm.transform.rotation.z > rightArmTarget + rotateSpeed)
                {
                    rightArm.transform.Rotate(0f, 0f, rotateSpeed);
                }
                else if (rightArm.transform.rotation.z < rightArmTarget - rotateSpeed)
                {
                    rightArm.transform.Rotate(0f, 0f, -rotateSpeed);
                }
                if (leftLeg.transform.rotation.z > leftLegTarget + rotateSpeed)
                {
                    leftLeg.transform.Rotate(0f, 0f, -rotateSpeed);
                }
                else if (leftLeg.transform.rotation.z < leftLegTarget - rotateSpeed)
                {
                    leftLeg.transform.Rotate(0f, 0f, rotateSpeed);
                }
                if (rightLeg.transform.rotation.z > rightLegTarget + rotateSpeed)
                {
                    rightLeg.transform.Rotate(0f, 0f, rotateSpeed);
                }
                else if (rightLeg.transform.rotation.z < rightLegTarget - rotateSpeed)
                {
                    rightLeg.transform.Rotate(0f, 0f, -rotateSpeed);
                }

                if (legSpringTimer > 0f)
                {
                    float adder = (normalLegY - springLegYTarget) * Time.deltaTime / legSpringTimeMaster;
                    legSpringTimer += Time.deltaTime;
                    if (legSpringTimer < legSpringTimeMaster)
                    {
                        leftLeg.transform.localPosition = new Vector2(leftLeg.transform.localPosition.x, leftLeg.transform.localPosition.y - (2 * adder));
                        rightLeg.transform.localPosition = new Vector2(rightLeg.transform.localPosition.x, rightLeg.transform.localPosition.y - adder);

                    }
                    else if (legSpringTimer >= legSpringTimeMaster)
                    {
                        leftLeg.transform.localPosition = new Vector2(leftLeg.transform.localPosition.x, leftLeg.transform.localPosition.y + (4 * adder));
                        rightLeg.transform.localPosition = new Vector2(rightLeg.transform.localPosition.x, rightLeg.transform.localPosition.y + (2 * adder));
                    }
                    if (legSpringTimer > legSpringTimeMaster * 1.5)
                    {
                        leftLeg.transform.localPosition = new Vector2(leftLeg.transform.localPosition.x, normalLegY);
                        rightLeg.transform.localPosition = new Vector2(rightLeg.transform.localPosition.x, normalLegY);
                        leftLeg.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        rightLeg.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        legSpringTimer = -1f;

                    }

                }

                if (swimming)
                {
                    swimTimer -= Time.deltaTime;
                    if (swimTimer < 2.22f && edgeRester)
                    {
                        if (swimDown) { rotateSpeedMaster = 300f; }
                        else 
                        { 
                            rotateSpeedMaster = -500f; 
                        }
                    }
                    else if (swimTimer < 2.05f)
                    {
                        if (swimDown) { rotateSpeedMaster = 300f; }
                        else 
                        { 
                            rotateSpeedMaster = -500f; 
                        }
                    }
                    if (swimTimer < 0f)
                    {
                        stopIt = true;
                    }
                }
            }

            if (flying)
            {
                swimTimer += Time.deltaTime;
                if (swimTimer> 0.3f) 
                {
                    leftLeg.SetActive(true);
                    rightLeg.SetActive(true);
                    wavingLegs = true;
                }
            }

        }

        void pencilShape()
        {
            leftArmTarget = 60f;
            rightArmTarget = 60f;
            leftLegTarget = 45f;
            rightLegTarget = -45f;
        }

        void bounce()
        {
            leftArmTarget = -60f;
            rightArmTarget = 60f;
            leftLegTarget = 0f;
            rightLegTarget = 0f;
        }

        void splashDown()
        {
            swimming = true;
            leftArm.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            rightArm.transform.eulerAngles = new Vector3(0f, 0f, 180f);
            leftLeg.SetActive(false);
            rightLeg.SetActive(false);
        }

        void springLegs()
        {
            legSpringTimer = 0.01f;

        }

        void edgeRest()
        {
            edgeRester = true;
        }

        void SwimDown()
        {
            swimDown = true;
            swimming = true;
            swimTimer = 5f;
            stopIt = false;
            leftArm.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            rightArm.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        }
        void FlyUp()
        {
            flying = true;
            swimTimer = 0f;
            leftLeg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            rightLeg.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            wavingArms = true;
            stopIt= false;
            swimming= false;
        }
    }
}