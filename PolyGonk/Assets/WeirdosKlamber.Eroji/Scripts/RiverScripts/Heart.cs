using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WeirdosKlamber.PolyGonk.River;

namespace WeirdosKlamber.PolyGonk.River.Heart
{
    public class Heart : MonoBehaviour
    {
        private float speed = 0.11f;
        private float height;
        private bool upwards = true;
        public GameObject splash;
        // Start is called before the first frame update
        void Start()
        {
            height = transform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.z >height+0.02)
            {
                upwards=false;
            }
            if (transform.position.z < height-0.04)
            {
                upwards = true;
            }

            if (upwards)
            {
                transform.position += (Vector3.forward*speed*Time.deltaTime);
            }
            else
            {
                transform.position -= (Vector3.forward * speed * Time.deltaTime);
            }

            if (transform.position.z > height+0.015)
            {
                splash.SetActive(true);
            }
            else
            {
                splash.SetActive(false);
            }
        }
    }
}