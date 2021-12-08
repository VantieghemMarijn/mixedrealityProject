using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulatorController : MonoBehaviour
{       // script dat je aan de camera hangt zodat je deze kan bewegen

#if UNITY_EDITOR        // enkel uitvoeren als je in unity zelf zit, niet op je smartphone omdat dit script aan je ARCamera hangt

    public AppManager app;

    private Camera cam;
    private Vector2 mouse_position = new Vector2();

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Deze functies worden opgroepen vanuit je simulator window als je op een knop drukt
    public void MoveForward(float speed)
    {
        transform.localPosition += transform.TransformDirection(Vector3.forward * speed * Time.deltaTime);
    }

    public void MoveBackward(float speed)
    {
        transform.localPosition += transform.TransformDirection(Vector3.back * speed * Time.deltaTime);
    }

    public void MoveLeft(float speed)
    {
        transform.localPosition += transform.TransformDirection(Vector3.left * speed * Time.deltaTime);
    }

    public void MoveRight(float speed)
    {
        transform.localPosition += transform.TransformDirection(Vector3.right * speed * Time.deltaTime);
    }

    public void SetRotation(float xrot, float yrot)
    {
        transform.localRotation = Quaternion.Euler(xrot, yrot, 0.0f);
    }

    void OnGUI()
    {       // vraag de mouse position op wanneer je muis over het simulator window beweegt
        Event e = Event.current;
        if (e.type == EventType.Repaint)
        {
            mouse_position.x = e.mousePosition.x;
            mouse_position.y = Screen.height - e.mousePosition.y;
        }
    }

    //void Update()
    //{
    //    RaycastHit hit;
    //    Ray ray = cam.ScreenPointToRay(mouse_position);
    //    if (Physics.Raycast(ray, out hit))      // physics raycast ipv AR raycast omdat ik in een unity simulatie zit
    //    {
    //        if (hit.transform.gameObject.scene.name == "Simulator")
    //        {
    //            app.EnableARCursor(hit.point, Quaternion.FromToRotation(hit.transform.up, hit.normal));
    //        }
    //        else
    //        {
    //            app.DisableARCursor();
    //        }
    //    }
    //    else
    //    {
    //        app.DisableARCursor();
    //    }
    //}

#endif

}
