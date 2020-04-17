using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeSize = 3;
    public GameObject topRot;
    public GameObject bottomRot;
    public GameObject rightRot;
    public GameObject leftRot;
    public GameObject frontRot;
    public GameObject backRot;

    //CubesList
    public GameObject[,,] pieces;
    // Start is called before the first frame update
    void Start()
    {
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

    private void ResetCubePosition()
    {
        for (int z = 0; z < cubeSize; z++)
        {
            for (int y = 0; y < cubeSize; y++)
            {
                for (int x = 0; x < cubeSize; x++)
                {
                    pieces[x,y,z].transform.localPosition = new Vector3(x, y, z);
                }
            }
        }
    }

    void RotAroundY()
    {
        int faceToRot = 2;
        //GameObject newRotation = new GameObject();
        topRot.transform.position = new Vector3(1f, 2.75f, 1f);

        //unparent cubes to be rotated
        for(int x=0; x < cubeSize; x++)
        {
            for(int z=0; z < cubeSize; z++)
            {
                pieces[x, faceToRot, z].transform.parent = topRot.transform;
            }
        }

        //Rotate
        //topRot.transform.Rotate(new Vector3(0f, 90f, 0f));
        topRot.transform.RotateAround(topRot.transform.position,Vector3.up,90f);

        //Parent back to main cube
        for (int x = 0; x < cubeSize; x++)
        {
            for (int y = 0; y < cubeSize; y++)
            {
                for (int z = 0; z < cubeSize; z++)
                {
                    pieces[x, y, z].transform.parent = transform;
                }
            }
        }
        Debug.Log("unparented");

        //Reset
        //topRot.transform.Rotate(new Vector3(0f, -90f, 0f));
        //topRot.transform.RotateAround(topRot.transform.position, Vector3.up, -90f);
        ResetCubePosition();
    }

    void RotAroundX(int leftOrRight)
    {
        int faceToRot;
        //GameObject newRotation = new GameObject();
        if(leftOrRight == 1)
        {
            rightRot.transform.position = new Vector3(2.75f, 1f, 1f);
            faceToRot = 2;
        }
        else
        {
            leftRot.transform.position = new Vector3(-2.75f, 1f, 1f);
            faceToRot = 0;
        }
        

        //unparent cubes to be rotated
        for (int y = 0; y < cubeSize; y++)
        {
            for (int z = 0; z < cubeSize; z++)
            {
                if (leftOrRight == 1)
                {
                    pieces[faceToRot, y, z].transform.parent = rightRot.transform;

                }
                else
                {
                    pieces[faceToRot, y, z].transform.parent = leftRot.transform;

                }
            }
        }

        //Rotate
        //rightRot.transform.Rotate(new Vector3(90f, 0f, 0f));
        if(leftOrRight == 1) //right
        {
            rightRot.transform.RotateAround(rightRot.transform.position, Vector3.right, 90f);

        }
        else                //left
        {
            leftRot.transform.RotateAround(leftRot.transform.position, Vector3.left, 90f);

        }

        //Parent back to main cube
        for (int x = 0; x < cubeSize; x++)
        {
            for (int y = 0; y < cubeSize; y++)
            {
                for (int z = 0; z < cubeSize; z++)
                {
                    pieces[x, y, z].transform.parent = transform;
                }
            }
        }

        //Rotate
        //rightRot.transform.Rotate(new Vector3(-90f, 0f, 0f));
        //rightRot.transform.RotateAround(rightRot.transform.position, Vector3.right, -90f);
        ResetCubePosition();
    }
    void RotateFace()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Rotating Around Y...");
            RotAroundY();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Rotating Around X...");
            RotAroundX(1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Rotating Around X...");
            RotAroundX(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        RotateFace();
    }
}
