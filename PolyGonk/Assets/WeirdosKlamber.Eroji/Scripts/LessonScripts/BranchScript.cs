
using UnityEngine;
using Image = UnityEngine.UIElements.Image;

public class BranchScript : MonoBehaviour
{
    public Sprite[] imagearray;
    public GameObject branchHolder;
    private bool animateBool = false;
    private float timerMaster = 0.03f;
    private float timerr = 0.03f;
    private int currentImage = 0;

    void Start()
    {
        branchHolder.SetActive(false);
    }

    void Update()
    {
        if (animateBool)
        {
            timerr -= Time.unscaledDeltaTime; 

            if (timerr < 0f)
            {
                timerr = timerMaster;
                GetComponent<UnityEngine.UI.Image>().sprite = imagearray[currentImage];
                currentImage++;
                print(currentImage);
                if (currentImage >= imagearray.Length) animateBool = false; 
            }

        }
    }

    void AnimateBranch()
    {
        animateBool = true;
    }
}
