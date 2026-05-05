using UnityEngine;

public class Sky : MonoBehaviour
{
    private bool flashingRed = false;
    private float flashTimerMaster = 0.5f;
    private float flashTimer = 0.5f;
    private float gb = 1f;

    void Update()
    {
        if (flashingRed)
        {
            flashTimer-=Time.deltaTime;
            if (flashTimer < flashTimerMaster/2)
            { 
                gb -= Time.deltaTime*5f;
                if (gb < 0f) gb = 0f;
                GetComponent<SpriteRenderer>().color = new Color(1f, gb, gb, 1f);
            }
            else 
            {
                gb += Time.deltaTime * 4f;
                if (gb > 1f) gb = 1f;
                GetComponent<SpriteRenderer>().color = new Color(1f, gb, gb, 1f);
            }
            if (flashTimer < 0f) 
            {
                flashTimer = flashTimerMaster;
                gb = 1f;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                flashingRed = false;
            }
        }        
    }

    public void FlashRed()
    {
        flashingRed = true;
    }
}
