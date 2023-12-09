using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.River.Crocodile
{
    public class CrocScript : MonoBehaviour
    {
        public GameObject riverPath;
        public GameObject boat;
        public int NavTrig = 1000;
        private bool swimming = false;
        public float delay = 1f;
        private float swimdelay = 0f;
        private float killdelay = 0f;
        private float rotatDeg = 0f;
        private bool doOnce = false;

        public AudioSource CrocSFX;
        private bool snapBool = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (swimdelay>0f)
            {
                swimdelay -= Time.deltaTime;
                if (swimdelay<0f)
                {
                    swimming = true;
                }
            }
            if (killdelay > 0f)
            {
                killdelay -= Time.deltaTime;
                if (killdelay < 0.5f && snapBool == false)
                {
                    CrocSFX.PlayOneShot(CrocSFX.clip);
                    snapBool = true;
                    gameObject.tag = "CrocodileBite";
                }
                if (killdelay <= 0f)
                {
                    gameObject.SetActive(false);
                }
            }
            if (swimming)
            {
            
                //should point at boat and flip appropriately
                rotatDeg = Mathf.Atan2(boat.transform.position.y - transform.position.y, boat.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                rotatDeg += 180f;
                if (rotatDeg >= 180f) rotatDeg -= 360f;
                if (rotatDeg < 0) GetComponent<SpriteRenderer>().flipY = false;
                else GetComponent<SpriteRenderer>().flipY = true;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, rotatDeg));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);

                //swim at boat
                transform.position = Vector2.MoveTowards(transform.position,boat.transform.position, 0.2f*Time.deltaTime);
           
            }

        }

        void CrocNav(int Nav)
        {
            if (Nav<=NavTrig && !doOnce)
            {
                doOnce = true;
                GetComponent<Animator>().SetBool("WakeBool", true);
                swimdelay = delay;
            }
            if (Nav <= NavTrig-2)
            {
                gameObject.SetActive(false);
            }
            
        }

        void CrocBite()
        {
            if (killdelay == 0f)
            {
                GetComponent<Animator>().SetBool("BiteBool", true);
                print("crocbite");
                killdelay = 1.5f;
            }
        }
    }
}