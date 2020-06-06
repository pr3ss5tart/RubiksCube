// Interpolates rotation between the rotations "from" and "to"
// (Choose from and to not to be the same as
// the object you attach this script to)

using UnityEngine;
using System.Collections;

public class Slerper : MonoBehaviour
{
    public Transform from;
    public Transform to;

    Quaternion ogRotation;
    Quaternion targetRotation;

    public float progress;
    public float speed = 2f;
    public bool rotating = false;

    void Start()
    {
        ogRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 90f, 0);

    }

    void Slerping(Quaternion qt)
    {
        progress += speed*Time.deltaTime;
        progress = Mathf.Clamp01(progress);
        Debug.Log(progress);
        transform.rotation = Quaternion.Slerp(ogRotation, qt, progress);

        if(progress == 1)
        {
            //set og rotation to new rotation?
            ogRotation = transform.rotation;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            targetRotation = Quaternion.Euler(90f, 0, 0);
            rotating = true;

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            targetRotation = Quaternion.Euler(0, 0, 90f);
            progress = 0f;
            rotating = true;

        }

        Slerping(targetRotation);
        
    }

}