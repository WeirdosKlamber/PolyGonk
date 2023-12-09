using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.River.Rock
{
    public class Rockscript : MonoBehaviour
    {
        public GameObject follower;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = follower.transform.rotation*Quaternion.Euler(0f,0f,-90f);
        }
    }
}