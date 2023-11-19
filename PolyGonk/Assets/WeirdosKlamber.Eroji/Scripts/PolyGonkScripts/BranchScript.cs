using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;

public class BranchScript : MonoBehaviour
{
    public Sprite[] imagearray;
    public GameObject branchHolder;
    private bool animateBool = false;
    private float timerMaster = 0.03f;
    private float timerr = 0.03f;
    private int currentImage = 0;

    // Start is called before the first frame update
    void Start()
    {
        branchHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animateBool)
        {
            timerr -= Time.unscaledDeltaTime; //running fast?

            if (timerr < 0f)
            {
                print("hello");
                timerr = timerMaster;
                GetComponent<UnityEngine.UI.Image>().sprite = imagearray[currentImage];
                currentImage++;
                print(currentImage);
                if (currentImage >= imagearray.Length) { animateBool = false; }
            }

        }
    }

    void AnimateBranch()
    {
        animateBool = true;

    }
}
