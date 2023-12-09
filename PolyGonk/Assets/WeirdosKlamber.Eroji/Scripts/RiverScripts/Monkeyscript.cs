using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.River;
namespace WeirdosKlamber.PolyGonk.River.Monkey
{
    public class Monkeyscript : MonoBehaviour
    {
        
        public GameObject Monkey;
        public GameObject Rope;
        public int NavTrig = 1000;
        private bool swinging = false;
        public float delay = 1f;
        public float swinglimit = 80f;
        public float stupideulerlimit = 35f;
        private float swingdelay = 0f;
        private float killdelay = 0f;
        private float rotatDeg = 0f;
        private bool swingright = true;
        private float swingspeed = 1f;
        public AudioSource SwingSFX;
        private bool doOnce = false;
        private float reSwingTimer = 3f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (swingdelay > 0f)
            {
                swingdelay -= Time.deltaTime;
                if (swingdelay < 0f)
                {
                    swinging = true;
                    Rope.GetComponent<Animator>().SetBool("swingBoo", true);
                    SwingSFX.PlayOneShot(SwingSFX.clip);
                }
            }

            if (swinging)
            {
                if (swingright==true)
                {
                    swingspeed += Time.deltaTime * 100f;
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, swinglimit));
                    Quaternion targetRotation2 = Quaternion.Euler(new Vector3(0, 0, -150f));
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, swingspeed * Time.deltaTime);
                    Monkey.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation2, swingspeed * Time.deltaTime);
                    //if (transform.rotation[2]>=30f) swingspeed -= Time.deltaTime * 100f;
                    if (transform.rotation[2] >= 10f* Mathf.Deg2Rad) swingspeed -= Time.deltaTime * 100f;
                    if (transform.rotation[2] >= 20f * Mathf.Deg2Rad) swingspeed -= Time.deltaTime * 100f;
                    if (transform.rotation[2] >= 30f * Mathf.Deg2Rad) swingspeed -= Time.deltaTime * 100f;
                    //if (transform.rotation[2] >= 70f * Mathf.Deg2Rad) swingspeed -= Time.deltaTime * 100f;
                   // print(transform.rotation[2] * Mathf.Rad2Deg);
                    if (transform.localRotation[2] >= stupideulerlimit * Mathf.Deg2Rad)  //No idea why but stops at 36, stupid euler, local no help, 20 for the other one
                    {
                        swingspeed = 0f;
                        swingright = false;
                        print("swingback");
                    }
                }
                else
                {
                    swingspeed += Time.deltaTime * 100f;
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, swinglimit-140f));
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, swingspeed * Time.deltaTime);
                    reSwingTimer -= Time.deltaTime;

                    if (reSwingTimer < 0f)  //No idea why but stops at 36, stupid euler, local no help, 20 for the other one
                    {
                        swingspeed = 0f;
                        swingright = true;
                        reSwingTimer = 3f;
                    }
                }
            }
        }

        void MonkNav(int Nav)
        {
            if (Nav <= NavTrig && !doOnce)
            {
                doOnce = true;
                swingdelay = delay;
            }
            if (Nav <= NavTrig - 5)
            {
                gameObject.SetActive(false);
            }

        }
    }
}