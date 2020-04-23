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
        topRot.transform.position = new Vector3(1f, 2f, 1f);
        bottomRot.transform.position = new Vector3(1f, -2f, 1f);
        rightRot.transform.position = new Vector3(2f, 1f, 1f);
        leftRot.transform.position = new Vector3(-2f, 1f, 1f);
        frontRot.transform.position = new Vector3(1f, 1f, 2f);
        backRot.transform.position = new Vector3(1f, 1f, -2f);
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


    void Rotate(char face)
    {
        if(face == 'R')
        {
            //rotate rightRot
            rightRot.transform.RotateAround(rightRot.transform.position, Vector3.right, 90f);
        }else if(face == 'U')
        {
            //rotate topRot
            topRot.transform.RotateAround(topRot.transform.position, Vector3.up, 90f);
        }
        
    }
    void RotateRFace()
    {

        for(int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            if (temp.localPosition.x > 1.5f && temp.localPosition.x < 2.5f)    //right face
            {
                temp.transform.parent = rightRot.transform; //parent to rightRot
            }
        }

        Rotate('R');

        for(int i = 0; i < rightRot.transform.childCount; i++)
        {
            Transform t = rightRot.transform.GetChild(0);
            t.transform.parent = transform;
        }
    }

    void RotateUFace()
    {
        for (int i = 6; i < transform.childCount; i++)
        {
            //this parents cubes to main cube
            Transform temp = transform.GetChild(i);

            if (temp.localPosition.y > 1.5f && temp.localPosition.y < 2.5f)    //top face
            {
                temp.transform.parent = topRot.transform; //parent to rightRot
            }
        }

        Rotate('U');

        for (int i = 0; i < topRot.transform.childCount; i++)
        {
            Transform t = topRot.transform.GetChild(0);
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

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Rotating right face");
            //RotAroundX(1);
            RotateRFace();
        }

    }

    // Update is called once per frame
    void Update()
    {
        RotateFace();
        //Rotate();
    }
}
