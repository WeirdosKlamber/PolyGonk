using UnityEngine;

namespace WeirdosKlamber.PolyGonk
{
    public class SquareHello : MonoBehaviour
    {
        public GameObject rightArm;
        private float delay = 4f;
        private bool upwards = true;
        private float waveTimer=0f;
        private float speed = 100f;
        private bool waving = false;
        private bool waveClockWise = false;
        private bool allowHello = true;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (delay> 0f) { delay -= Time.unscaledDeltaTime; }
            else if (upwards) 
            {
                transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Min(231f, transform.localPosition.y+speed*Time.unscaledDeltaTime));
                if (transform.localPosition.y > -230f) 
                {
                    upwards= false;
                    waving = true;
                    delay= 4f;
                }                
            }
            else
            {
                if (allowHello)
                {
                    waving = false;
                }
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * Time.unscaledDeltaTime);
                if (transform.localPosition.y < -500f)
                {
                    transform.localPosition = new Vector2(transform.localPosition.x, -500f);

                    if (allowHello)
                    {
                        upwards = true;
                        delay = 6f;
                    }
                }
            }

            if (waving) 
            {
                waveTimer += Time.unscaledDeltaTime;

                if (waveClockWise)
                {
                    rightArm.transform.Rotate(new Vector3(0f, 0f, -240f) * Time.deltaTime);

                    if (waveTimer>0.5f)
                    {
                        waveTimer = 0f;
                        waveClockWise= false;
                    }
                }
                else if (allowHello)
                {
                    rightArm.transform.Rotate(new Vector3(0f, 0f, 240f) * Time.deltaTime);

                    if (waveTimer > 0.5f)  
                    {
                        waveTimer = 0f;
                        waveClockWise = true;
                    }
                }
            }
        }

        public void GoDown()
        {
            allowHello = false;
            upwards = false;
            delay = -1f;
            if(!waveClockWise)
            {
                waveTimer = 0.5f - waveTimer;
            }
            waveClockWise = true;            
        }
    }
}