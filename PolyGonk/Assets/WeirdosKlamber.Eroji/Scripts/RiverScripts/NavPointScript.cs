using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk.River.NavPoint
{
    public class NavPointScript : MonoBehaviour

    {
        private Vector3 navInfo;
        public float navnum;

        // Start is called before the first frame update
        void Start()
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            navInfo[0] = navnum;
            navInfo[1] = transform.localPosition[0];
            navInfo[2] = transform.localPosition[1];

            transform.parent.parent.gameObject.SendMessage("AddNav", navInfo);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}