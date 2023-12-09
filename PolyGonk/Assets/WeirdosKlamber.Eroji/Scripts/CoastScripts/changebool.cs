using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class changebool : MonoBehaviour
    {
        public int countr = 0;
        private float timex = 0.0f;
        Animator m_Animator;
        private SpriteRenderer spriteR;
        // Start is called before the first frame update
        void Start()
        {
            spriteR = gameObject.GetComponent<SpriteRenderer>();
            m_Animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Animator.GetBool("PlayBool") == true)
            {
                timex += Time.deltaTime;
                if (timex > 0.8f)
                {
                    timex = 0.0f;
                    m_Animator.SetBool("PlayBool", false);
                    m_Animator.enabled = false;
                    countr++;
                    //print(countr);


                }
            }
        }

        void ChangeBoo()
        {

            if (m_Animator.GetBool("PlayBool") == false)
            {
                m_Animator.SetBool("PlayBool", true)
                    ;
                m_Animator.enabled = true;

            }
            /*
            else { m_Animator.SetBool("PlayBool", false);
                m_Animator.enabled = false; 
            }
            */
        }


    }
}
