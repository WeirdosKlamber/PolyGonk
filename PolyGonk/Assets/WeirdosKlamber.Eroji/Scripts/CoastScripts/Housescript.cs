using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.Coast;

namespace WeirdosKlamber.PolyGonk.Coast
{
    public class Housescript : MonoBehaviour
    {
        public int countr = 0;
        Animator m_Animator;
        private float doorboolwaiter = 2f;
        private float EndGame = 3f;
        private bool endbool = false;
        public GameObject housepicbgd;
        // Start is called before the first frame update
        void Start()
        {
            m_Animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (doorboolwaiter > 0f)
            {
                doorboolwaiter -= Time.deltaTime;
                if (doorboolwaiter <= 0f)
                {
                    m_Animator.SetBool("HouseDoorBool", false);
                }
            }

            if (endbool == true)
            {
                EndGame -= Time.deltaTime;
                if (EndGame <= 0f)
                {
                    GameObject.Find("CoastMain").SendMessage("EndGame", 7);
                    endbool = false;
                }
            }

        }
        void FallHouse()
        {
            countr++;

            // if (countr == 14)
            housepicbgd.SetActive(false);
            Destroy(GetComponent<PolygonCollider2D>());
            m_Animator.SetBool("FallBool", true);
            m_Animator.enabled = true;
            endbool = true;

        }


        void HouseScare()
        {
            if (WeirdosKlamber.PolyGonk.Coast.CoastScript.Evacuated == false)
            {
                m_Animator.SetBool("HouseDoorBool", true);
                doorboolwaiter = 2f;
                m_Animator.enabled = true;
            }
        }
    }
}