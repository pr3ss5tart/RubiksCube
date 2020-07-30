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
    bool isSlerpingX = false;
    bool isSlerpingY = false;
    bool isSlerpingZ = false;
    bool isSlerpingBottom = false;
    bool isSlerpingTop = false;
    bool isSlerpingRight = false;
    bool isSlerpingLeft = false;
    public float rotationValue;

    //CubesList
    public GameObject[,,] pieces;

    // Start is called before the first frame update
    void Start()
    {
        ogRotation = transform.rotation;
        rotationValue = 90f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlerpingRight)
        {
            SlerpingX(targetRotation, "right");
        }
        if (isSlerpingLeft)
        {
            SlerpingX(targetRotation, "left");
        }
        if (isSlerpingTop)
        {
            SlerpingY(targetRotation, "top");
            //maybe reset quaternion here?
            //topRot.transform.rotation = ogRotation;
            //Debug.Log("quat reset");
        }
        /*if (isSlerpingZ)
        {
            Slerping(targetRotation, "front", false);
        }*/
        if (isSlerpingBottom)
        {
            SlerpingY(targetRotation, "bottom");
        }

    }

    /*void Slerping(Quaternion qt, string face, bool isPrime)
    {
        progress += speed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);

        //check face, apply slerp to correct rotator
        if(face == "right")
        {
            rightRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }
        else if(face == "top")
        {
            topRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }
        else if(face == "front")
        {
            frontRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }

        if (progress == 1)  //face is done rotating
        {
            //check face again, set correct bool as false
            if (face == "right")
            {
                isSlerpingX = false;
                UnparentRFace();
                targetRotation = Quaternion.Euler(rotationValue, 0, 0);

            }
            else if (face == "top")
            {
                isSlerpingY = false;
                UnparentTFace();
                targetRotation = Quaternion.Euler(0, rotationValue, 0);

            }
            else if (face == "front")
            {

            }

            if (rotationValue >= 360)
            {
                rotationValue = 90f;
                ogRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                rotationValue += 90f;
                ogRotation = targetRotation;

            }
            Debug.Log("RotationValue" + rotationValue);

            progress = 0f;
        }
    }*/

    void SlerpingY(Quaternion qt, string face)
    {
        progress += speed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);

        //assign which rotation to use
        if(face == "top")
        {
            topRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }else  if (face == "bottom")
        {
            bottomRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }

        if (progress == 1)  //face is done rotating
        {

            if (face == "top") //unparent top face
            {
                isSlerpingTop = false;

                UnparentTFace();
            }
            else      //unparent bottom face
            {
                isSlerpingBottom = false;

                UnparentBFace();
            }

            //if rotation angle > 360, reset to 0,0,0
            if (rotationValue >= 360)
            {
                rotationValue = 90f;
                ogRotation = Quaternion.Euler(0, 0, 0);
            }
            else  //adjust rotationValue
            {
                rotationValue += 90f;
                ogRotation = targetRotation;

            }
            Debug.Log("RotationValue" + rotationValue);

            targetRotation = Quaternion.Euler(0, rotationValue, 0);
           
            progress = 0f;
        }
    }

    void SlerpingX(Quaternion qt, string face)
    {
        progress += speed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);

        //assign which rotation to use
        if (face == "right")
        {
            Debug.Log("SlerpingX Right");
            rightRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }
        else if (face == "left")
        {
             leftRot.transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        }

        if (progress == 1)  //face is done rotating
        {

            if (face == "right") //unparent right face
            {
                isSlerpingRight = false;

                UnparentRFace();
            }
            else      //unparent left face
            {
                isSlerpingLeft = false;

                UnparentLFace();
            }

            //if rotation angle > 360, reset to 0,0,0
            if (rotationValue >= 360)
            {
                rotationValue = 90f;
                ogRotation = Quaternion.Euler(0, 0, 0);
            }
            else  //adjust rotationValue
            {
                rotationValue += 90f;
                ogRotation = targetRotation;

            }
            Debug.Log("RotationValue" + rotationValue);

            targetRotation = Quaternion.Euler(rotationValue, 0, 0);

            progress = 0f;
        }
    }

    public void ParentRFace()
    {
        Debug.Log("ParentRFace called");
        //targetRotation = Quaternion.Euler(rotationValue, 0, 0);
        SetRotationAxis("X");
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            if (temp.localPosition.x > 1.5f && temp.localPosition.x < 2.5f)    //right face
            {
                temps.Add(temp);
            }
        }
 
        foreach (Transform t in temps)
        {
            t.transform.parent = rightRot.transform; //parent to toptRot
        }

        isSlerpingRight = true;

    }

    public void ParentLFace()
    {
        //targetRotation = Quaternion.Euler(rotationValue, 0, 0);
        SetRotationAxis("X");
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            if (temp.localPosition.x > -0.5f && temp.localPosition.x < 0.5f)    //right face
            {
                temps.Add(temp);
            }
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = leftRot.transform; //parent to toptRot
        }

        isSlerpingLeft = true;

    }

    public void ParentTFace()
    {
        //targetRotation = Quaternion.Euler(0, rotationValue, 0);
        SetRotationAxis("Y");
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            if (temp.localPosition.y > 1.5f && temp.localPosition.y < 2.5f)    //right face
            {
                temps.Add(temp);
            }
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = topRot.transform; //parent to toptRot
        }

        isSlerpingTop = true;

    }

    public void ParentBFace()
    {
        //targetRotation = Quaternion.Euler(0, rotationValue, 0);
        SetRotationAxis("Y");
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            if (temp.localPosition.y > -0.5f && temp.localPosition.y < 0.5f)    //right face
            {
                temps.Add(temp);
            }
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = bottomRot.transform; //parent to toptRot
        }

        isSlerpingBottom = true;

    }

    void UnparentRFace()    //WHY YOU NO UNPARENT FULLY!?
    {
        Debug.Log("UnparentRFace called");
        List<Transform> temps = new List<Transform>();
        int kid_count = rightRot.transform.childCount;
        for (int i = 0; i < kid_count; i++)
        {
            //this parents cubes to main cube
            Transform temp = rightRot.transform.GetChild(0);
            //temp.parent = transform;
            temps.Add(temp);
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = transform; //parent to main cube
            Debug.Log("Right Child removed");

        }
        transform.rotation = Quaternion.identity;
        //targetRotation = Quaternion.identity;
        ogRotation = Quaternion.identity;
    }

    void UnparentLFace()    //WHY YOU NO UNPARENT FULLY!?
    {
        List<Transform> temps = new List<Transform>();
        int kid_count = leftRot.transform.childCount;
        for (int i = 0; i < kid_count; i++)
        {
            //this parents cubes to main cube
            Transform temp = leftRot.transform.GetChild(0);
            //temp.parent = transform;
            temps.Add(temp);
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = transform; //parent to main cube
            Debug.Log("Child removed");

        }
        transform.rotation = Quaternion.identity;
        //targetRotation = Quaternion.identity;
        //ogRotation = Quaternion.identity;
    }

    void UnparentTFace()    //WHY YOU NO UNPARENT FULLY!?
    {
        List<Transform> temps = new List<Transform>();
        int kid_count = topRot.transform.childCount;
        for (int i = 0; i < kid_count; i++)
        {
            //this parents cubes to main cube
            Transform temp = topRot.transform.GetChild(0);
            temp.parent = transform;
            Debug.Log("Child "+topRot.transform.childCount+" removed");
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = transform; //parent to main cube
        }
        transform.rotation = Quaternion.identity;
        //targetRotation = Quaternion.identity;
        //ogRotation = Quaternion.identity;
    }

    void UnparentBFace()    //WHY YOU NO UNPARENT FULLY!?
    {
        List<Transform> temps = new List<Transform>();
        int kid_count = bottomRot.transform.childCount;
        for (int i = 0; i < kid_count; i++)
        {
            //this parents cubes to main cube
            Transform temp = bottomRot.transform.GetChild(0);
            temp.parent = transform;
            Debug.Log("Child " + bottomRot.transform.childCount + " removed");
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = transform; //parent to main cube
        }
        //Reset Quaternions
        transform.rotation = Quaternion.identity;
        //targetRotation = Quaternion.identity;
        //ogRotation = Quaternion.identity;
    }

    void SetRotationAxis(string axis)
    {
        //Set target rotation for axis
        if (axis == "X")
        {
            targetRotation = Quaternion.Euler(rotationValue, 0, 0);
        }

        if (axis == "Y")
        {
            targetRotation = Quaternion.Euler(0, rotationValue, 0);
        }

        if (axis == "Z")
        {
            targetRotation = Quaternion.Euler(0, 0, rotationValue);
        }
    }
}
