using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
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

    //CubesList
    public GameObject[,,] pieces;
    // Start is called before the first frame update
    void Start()
    {
        topRot.transform.position = new Vector3(1f, 2f, 1f);
        bottomRot.transform.position = new Vector3(1f, -2f, 1f);
        rightRot.transform.position = new Vector3(2f, 1f, 1f);
        leftRot.transform.position = new Vector3(-2f, 1f, 1f);
        frontRot.transform.position = new Vector3(1f, 1f, 2f);
        backRot.transform.position = new Vector3(1f, 1f, -2f);

        rbc = GameObject.Find("CubeHolder").GetComponent<RotateBigCube>();
        
        GenerateCube();
    }

    private void GenerateCube()
    {
        pieces = new GameObject[cubeSize, cubeSize, cubeSize];
        CreateCube();
    }

    private void CreateCube()
    {
        for(int z = 0; z < cubeSize; z++)
        {
            for(int y = 0; y < cubeSize; y++)
            {
                for(int x = 0; x < cubeSize; x++)
                {
                    GameObject newPiece = Instantiate(cubePrefab);
                    pieces[x,y,z] = newPiece;
                    newPiece.transform.parent = transform;
                    newPiece.transform.localPosition = new Vector3(x, y, z);
                }
            }
        }
    }

    void Scramble()
    {
        System.Random r = new System.Random();

        int numRotations = r.Next(11);
        
        //Debug.Log(numRotations+ " " + numFaceRotation + " " + faceRotation);

        for(int i = 0; i < numRotations; i++)
        {
            int faceRotation = r.Next(1,7);
            int numFaceRotation = r.Next(11);

            if(faceRotation == 1)
            {
                for(int j = 0; j < numFaceRotation; j++)
                {
                    RotateFFace();
                }
            }
            if (faceRotation == 2)
            {
                Debug.Log(numFaceRotation);
            }
            if (faceRotation == 3)
            {
                Debug.Log(numFaceRotation);
            }
            if (faceRotation == 4)
            {
                Debug.Log(numFaceRotation);
            }
            if (faceRotation == 5)
            {
                Debug.Log(numFaceRotation);
            }
            if (faceRotation == 6)
            {
                Debug.Log(numFaceRotation);
            }


        }
    }

    void SlerpingY()
    {
        targetRotation = Quaternion.Euler(0, 90f, 0);
        ogRotation = topRot.transform.rotation;

        progress += speed * Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        Debug.Log(progress);
        topRot.transform.rotation = Quaternion.Slerp(ogRotation, targetRotation, progress);

        if (progress == 1)
        {
            //set og rotation to new rotation?
            //ogRotation = topRot.transform.rotation;
        }
    }

    void Rotate(string face)
    {
        if(face == "Right")
        {
            //rotate rightRot
            rightRot.transform.RotateAround(rightRot.transform.position, Vector3.right, 90f);
        }
        if(face == "Up")
        {
            //rotate topRot
            //topRot.transform.RotateAround(topRot.transform.position, Vector3.up, 180f*Time.deltaTime);
            isSlerpingY = true;

        }
        if(face == "UpPrime")
        {
            //rotate topRot
            topRot.transform.RotateAround(topRot.transform.position, Vector3.up, -90f);
        }
        if(face == "Front")
        {
            //rotate frontRot
            frontRot.transform.RotateAround(frontRot.transform.position, Vector3.forward, 90f);
        }
        
    }
    /*public void RotateRFace()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            //Debug.Log("Searching for cubes...");
            if (temp.localPosition.x > 1.5f && temp.localPosition.x < 2.5f)    //right face
            {
                Debug.Log("Cube parented...");
                temps.Add(temp);
                //temp.transform.parent = topRot.transform; //parent to rightRot
            }
        }
        Debug.Log("Items in temps... " + temps.Count);

        foreach (Transform t in temps)
        {
            t.transform.parent = rightRot.transform; //parent to toptRot
        }

        Rotate("Right");

        for (int i = 0; i < temps.Count; i++)
        {
            Transform t = rightRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }
*/
    public void RotateFFace()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            //Debug.Log("Searching for cubes...");
            if (temp.localPosition.z > -.5f && temp.localPosition.z < .5f)    //front face
            {
                //Debug.Log("Cube parented...");
                temps.Add(temp);
                //temp.transform.parent = topRot.transform; //parent to rightRot
            }
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = frontRot.transform; //parent to frontRot
        }

        Rotate("Front");

        for (int i = 0; i < temps.Count; i++)
        {
            Transform t = frontRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }

    public void RotateUFace()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            //Debug.Log("Searching for cubes...");
            if (temp.localPosition.y > 1.5f && temp.localPosition.y < 2.5f)    //top face
            {
                Debug.Log("Cube parented...");
                temps.Add(temp);
                //temp.transform.parent = topRot.transform; //parent to rightRot
            }
        }
        Debug.Log("Items in temps... " + temps.Count);

        foreach(Transform t in temps)
        {
            t.transform.parent = topRot.transform; //parent to toptRot
        }

        Rotate("Up");

        for (int i = 0; i < temps.Count; i++)
        {
            Transform t = topRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }


    void RotateCubeY()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            temps.Add(temp);
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = topRot.transform; //parent to toptRot
        }

        Rotate("Up");

        for (int i = 0; i < temps.Count; i++)
        {
            Transform t = topRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }

    void RotateCubeX()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            temps.Add(temp);
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = rightRot.transform; //parent to toptRot
        }

        Rotate("Right");

        for (int i = 0; i < temps.Count; i++)
        {
            Transform t = rightRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }

    void RotateCubeZ()
    {
        List<Transform> temps = new List<Transform>();

        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            temps.Add(temp);
        }

        foreach (Transform t in temps)
        {
            t.transform.parent = frontRot.transform; //parent to toptRot
        }

        Rotate("Front");

        for (int i = 0; i < temps.Count; i++)
        {
            Transform t = frontRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }

    void RotateFace()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Rotating top face");
            RotateUFace();
        }

        /*if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Rotating right face");
            RotateRFace();
        }*/

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Rotating front face");
            RotateFFace();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateCubeY();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateCubeX();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateCubeZ();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        RotateFace();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Scrambling");
            Scramble();
        }

        if (isSlerpingY)
        {
            SlerpingY();
        }
        //Rotate();
    }
}
