using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeirdosKlamber.PolyGonk.River.Spider
{
    public class SpiderScript : MonoBehaviour
    {
        public GameObject follower;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        //CURRENTLY ABUSED  FOR TREES etc
        // Update is called once per frame
        void Update()
        {
            transform.rotation = follower.transform.rotation;
        }
    }
}