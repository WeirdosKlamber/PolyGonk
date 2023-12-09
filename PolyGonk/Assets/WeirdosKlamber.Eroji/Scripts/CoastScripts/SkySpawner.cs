using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class SkySpawner : MonoBehaviour
    {
        public GameObject SandObj;
        private GameObject RipRapObj;
        public GameObject RipRapObj1;
        public GameObject RipRapObj2;
        public GameObject RipRapObj3;
        public GameObject GabionObj;
        public GameObject SeaWallObj;
        public GameObject CoastMain;
        public AudioSource popFX;
        public bool SandEmitter = false;
        private float SandTimer = 0.0f;
        public float SandFrequency;
        public float SandSideGap;
        public float SandSideSweepWidth;
        public bool RipRapEmitter = false;
        private float RipRapTimer = 0.0f;
        public float RipRapFrequency;
        public bool GabionEmitter = false;
        private float GabionTimer = 0.0f;
        public float GabionFrequency;
        public bool SeaWallEmitter = false;
        private float SeaWallTimer = 0.0f;
        public float SeaWallFrequency;

        private float EmitSideSweeper = 0.0f;
        private bool EmitSideSwitch = false;
        public float LeftLimit;
        public float RightLimit;
        public float Speed;
        public float SmoothTime;
        public bool MovingRight = false;
        public Vector2 RightTarget;
        public Vector2 LeftTarget;

        private bool seawalldropped = false;
        Vector2 velocity;

        // Start is called before the first frame update
        void Start()
        {
            RightTarget = new Vector2(RightLimit, transform.localPosition[1]);
            LeftTarget = new Vector2(LeftLimit, transform.localPosition[1]);
            RipRapObj = RipRapObj1;
            RipRapTimer = RipRapFrequency;
            GabionTimer = GabionFrequency;
            SeaWallTimer = SeaWallFrequency;
/*            if (SingletonSimple.Instance.coastBabyMode) SmoothTime = 1f;
*//*            else SmoothTime = 0.5f;*/
        }

        // Update is called once per frame
        void Update()
        {
            if (MovingRight == true)
            {
                transform.localPosition = Vector2.SmoothDamp(transform.localPosition, RightTarget, ref velocity, SmoothTime);
                if (transform.localPosition[0] > RightLimit - 0.1)
                {
                    MovingRight = false;
                }
            }
            else
            {
                transform.localPosition = Vector2.SmoothDamp(transform.localPosition, LeftTarget, ref velocity, SmoothTime);
                if (transform.localPosition[0] < LeftLimit + 0.1)
                {
                    MovingRight = true;

                }
            };

            if (SandEmitter == true)
            {
                SandTimer += Time.deltaTime;
                if (SandTimer > SandFrequency)
                {
                    SandTimer = 0.0f;
                    CoastMain.SendMessage("BuyFunc", "Sand");
                    Instantiate(SandObj, new Vector2(transform.position[0] + EmitSideSweeper, transform.position[1]), Quaternion.identity);
                    popFX.Play();
                    if (EmitSideSwitch == true)
                    {
                        EmitSideSweeper += SandSideGap;
                        if (EmitSideSweeper > SandSideSweepWidth)
                        {
                            EmitSideSwitch = false;
                        }
                    }
                    else EmitSideSweeper -= SandSideGap;
                    if (EmitSideSweeper < 0 - SandSideSweepWidth)
                    {
                        EmitSideSwitch = true;
                    }
                }
            }

            if (RipRapEmitter == true)
            {
                RipRapTimer += Time.deltaTime;
                if (RipRapTimer > RipRapFrequency)
                {
                    RipRapTimer = 0.0f;
                    CoastMain.SendMessage("BuyFunc", "RipRap");
                    Instantiate(RipRapObj, transform.position, transform.rotation);
                    popFX.Play();
                    if (RipRapObj == RipRapObj1) { RipRapObj = RipRapObj2; }
                    else if (RipRapObj == RipRapObj2) { RipRapObj = RipRapObj3; }
                    else { RipRapObj = RipRapObj1; }
                }
            }

            if (GabionEmitter == true)
            {
                GabionTimer += Time.deltaTime;
                if (GabionTimer > GabionFrequency)
                {
                    GabionTimer = 0.0f;
                    CoastMain.SendMessage("BuyFunc", "Gabion");
                    Instantiate(GabionObj, transform.position, transform.rotation);
                    popFX.Play();
                }
            }

            if (SeaWallEmitter == true & seawalldropped == false)
            {
                seawalldropped = true;

                CoastMain.SendMessage("BuyFunc", "SeaWall");
                Instantiate(SeaWallObj, transform.position, transform.rotation);
            }

        }
        void SandButtonPress(bool what)
        {
            SandEmitter = what;

        }

        void RipRapButtonPress(bool what)
        {
            RipRapEmitter = what;
            RipRapTimer = RipRapFrequency;

        }
        void GabionButtonPress(bool what)
        {
            GabionEmitter = what;
            GabionTimer = GabionFrequency;

        }

        void SeaWallButtonPress(bool what)
        {
            SeaWallEmitter = what;

        }

    }
}