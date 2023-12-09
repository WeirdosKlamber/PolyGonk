using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeirdosKlamber.PolyGonk.River;
namespace WeirdosKlamber.PolyGonk.River.Logs
{
    public class logScript : MonoBehaviour
    {
        public GameObject riverPath;
        public int NavTrig = 1000;
        private bool swimming = false;
        public float delay = 0.1f;
        private float swimdelay = 0f;
        private float killdelay = 0f;
        public GameObject[] Targets;
        private GameObject currentTarget;
        public GameObject follower;
        private int currentTN = 0;
        private bool doOnce = false;
        // Start is called before the first frame update
        void Start()
        {
            currentTarget = Targets[currentTN];
        }

        // Update is called once per frame
        void Update()
        {
            if (swimdelay > 0f)
            {
                swimdelay -= Time.deltaTime;
                if (swimdelay <= 0f)
                {
                    swimming = true;
                }
            }
            if (swimming)
            {
                //transform.rotation = follower.transform.rotation;
                //not working but bit of chaos looks good
                float angle = Mathf.Atan2(currentTarget.transform.position[1] - transform.position.y, currentTarget.transform.position[0] - transform.position.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 20f * Time.deltaTime);



                transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, 0.1f * Time.deltaTime);
                if (transform.position == currentTarget.transform.position)
                {
                    if (currentTN + 1 < Targets.Length)
                    {
                        currentTN++;
                        currentTarget = Targets[currentTN];
                    }
                }
            }

        }

        void LogNav(int Nav)
        {
            if (Nav <= NavTrig && !doOnce)
            {
                doOnce = true;
                swimdelay = delay / River.RiverPath.RiverPathScript.currentspeed;
            }
            if (Nav <= NavTrig - 10)
            {
                gameObject.SetActive(false);
            }

        }

    }
}