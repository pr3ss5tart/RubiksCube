using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceRotator : MonoBehaviour
{
    public RotateBigCube rbc;
    public GameObject cubePrefab;
    public int cubeSize = 3;
    public GameObject topRot;
    public GameObject bottomRot;
    public GameObject rightRot;
    public GameObject leftRot;
    public GameObject frontRot;
    public GameObject backRot;

    Quaternion ogRotation;
    Quaternion targetRotation;

    public float progress;
    public float speed = 2f;
    bool isSlerpingY = false;
    float rotationValue;

    //CubesList
    public GameObject[,,] pieces;

    // Start is called before the first frame update
    void Start()
    {
        ogRotation = transform.rotation;
        rotationValue = 90f;
        targetRotation = Quaternion.Euler(rotationValue, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlerpingY)
        {
            SlerpingRight(targetRotation);
            //Debug.Log(targetRotation.x);
        }
       
    }

    void SlerpingRight(Quaternion qt)
    {
        progress += speed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        rightRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        if (progress == 1)  //face is done rotating
        {
            isSlerpingY = false;
            UnparentRFace();

            rotationValue += 90f;
            Debug.Log("RotationValue" + rotationValue);

            targetRotation = Quaternion.Euler(rotationValue, 0, 0);
            progress = 0f;
        }
    }

    public void ParentRFace()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            //Debug.Log("Searching for cubes...");
            if (temp.localPosition.x > 1.5f && temp.localPosition.x < 2.5f)    //right face
            {
                //Debug.Log("Cube parented...");
                temps.Add(temp);
                //temp.transform.parent = topRot.transform; //parent to rightRot
            }
        }
        //Debug.Log("Items in temps... " + temps.Count);

        foreach (Transform t in temps)
        {
            t.transform.parent = rightRot.transform; //parent to toptRot
        }

        isSlerpingY = true;

    }

    void UnparentRFace()    //WHY YOU NO UNPARENT FULLY!?
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 0; i < rightRot.transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = rightRot.transform.GetChild(0);
            temp.parent = transform;
            //Debug.Log("unparented... " + rightRot.transform.childCount);

            //temps.Add(temp);
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = transform; //parent to main cube
        }
    }
}
