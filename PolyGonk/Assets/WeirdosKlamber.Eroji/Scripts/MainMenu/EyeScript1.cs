
using UnityEngine;
using TMPro;

public class EyeScriptTitle : MonoBehaviour
{
    public GameObject pupil;
    public TextMeshProUGUI testBox;
    public float moveSpeed = 1f;
    public Vector3 targetPos = Vector3.zero;
    private Vector3 nextPos = Vector3.zero;
    private Vector3 startPos = Vector3.zero;
    public bool rightEye = false;
    public bool centreEyes = false;
    public Vector3 mouseMod = new Vector3(0f, 0f,0f); //originally(70f, -11f, -700f)
    public float distanceLimit = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(0f, 0f, 0f); ;
        print ("right? " + rightEye + " worldpos:" + transform.position);
/*        if (rightEye)
        {
            print("righteye " + startPos);
            mouseMod = new Vector3(0f, 0f, -700f); //originally (32f, -11f, -700f)
        }*/
       // else print("lefteye " + startPos);
    }

    // Update is called once per frame
    void Update()
    {
  
        testBox.text = "m: " + (Input.mousePosition).ToString();
        if (centreEyes) { targetPos = startPos;}
        else 
        {
            targetPos = (Input.mousePosition - mouseMod);
            targetPos.z = 0f;
        }
        nextPos = Vector3.MoveTowards(pupil.transform.localPosition,targetPos, moveSpeed * Time.unscaledDeltaTime);

        if (Vector3.Distance(nextPos, startPos)<distanceLimit) //originally 6f
        {
            pupil.transform.localPosition=nextPos;
        }
        else
        {
            nextPos = Vector3.MoveTowards(pupil.transform.localPosition, startPos, moveSpeed * Time.unscaledDeltaTime);
            nextPos = Vector3.MoveTowards(nextPos, targetPos, moveSpeed * Time.unscaledDeltaTime);
            pupil.transform.localPosition = nextPos;

        }

/*        if (Vector3.Distance(pupil.transform.localPosition, startPos) > distancelimit) //originally 6f
        {
            pupil.GetComponent<SpriteRenderer>().enabled=false;
        }
        else
        {
            pupil.GetComponent<SpriteRenderer>().enabled = true;

        }*/ //not working


        //print(targetPos + "  " + pupil.transform.position + "   " + (targetPos-pupil.transform.position));
    }
    public void plusX()
    {
        mouseMod.x += 10f;
    }

    public void minusX()
    {
        mouseMod.x -= 10f;
    }
}
