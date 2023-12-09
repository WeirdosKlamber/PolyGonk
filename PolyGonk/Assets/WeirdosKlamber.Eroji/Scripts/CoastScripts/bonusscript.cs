using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WeirdosKlamber.PolyGonk.Coast
{
    public class bonusscript : MonoBehaviour
    {
        private float timer = 0f;
        // Start is called before the first frame update
        void Start()
        {
            transform.localScale = (new Vector3(1f, 1f, 1f));
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime);
            transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            if (timer > 1f) Destroy(gameObject);

        }
    }
}