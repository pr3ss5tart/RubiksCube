using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControls : MonoBehaviour
{
    public List<GameObject> pieces;
    public GameObject UpPivot;
    public GameObject DownPivot;
    public GameObject FrontPivot;
    public GameObject BackPivot;
    public GameObject RightPivot;
    public GameObject LeftPivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Assign pieces based on arrow input and XYZ pos
    // if UP, check pieces[transform.Y == 1], parent to UpPivot

    void RotateFace()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("Getting face...");
            foreach(GameObject go in pieces)
            {
                //Debug.Log("Getting face...");
                if (go.transform.position.y == 2)    //piece is up face
                {
                    //make child of UpPivot
                    go.transform.SetParent(UpPivot.transform);
                    Debug.Log("Piece: " + go.name + go.transform + " made child of Up");
                }
                //GetFace(pieces[i],"U");
                UpPivot.transform.Rotate(UpPivot.transform.position + Vector3.up, 90f);

                
            }
        }
       /* else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (GameObject go in pieces)
            {
                GetFace(go);
                DownPivot.transform.Rotate(DownPivot.transform.position + Vector3.down, 90f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            foreach (GameObject go in pieces)
            {
                GetFace(go);
                UpPivot.transform.Rotate(UpPivot.transform.position + Vector3.up, 90f);
            }
        }*/
    }
    //Rotate Face 90, then unparent from pivot 

    // Update is called once per frame
    void Update()
    {
        RotateFace();
    }
}
