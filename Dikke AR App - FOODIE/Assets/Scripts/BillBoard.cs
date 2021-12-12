using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Camera ARCamera;

    // Start is called before the first frame update
    void Start()
    {
        ARCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(0f, ARCamera.transform.rotation.eulerAngles.y, 0f);     // rotate the Billboard to face the camera
        transform.LookAt(ARCamera.transform);
    }
}
