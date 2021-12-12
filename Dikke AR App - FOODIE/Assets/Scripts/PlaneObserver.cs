using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneObserver : MonoBehaviour
{
    public AppManager app;
    public ARRaycastManager raycastManager;         // de ARRaycastManager van 

    private Vector2 screenpoint = new Vector2(Screen.width / 2, Screen.height / 2);         // is het midden van je '2D' scherm (hoogte / 2 & breedte / 2)
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();                             // lijst voor alle hit coordinaten op te slaan
    private bool isEditor;

    private void Start()
    {
        isEditor = Application.installMode == ApplicationInstallMode.Editor;
    }

    private void Update()
    {
        if (isEditor == false)
        {
            if (raycastManager.Raycast(screenpoint, hits))                                      // kijk of er een hit is met een plane in het midden van het scherm
            {
                app.EnableARCursor(hits[0].pose.position, hits[0].pose.rotation);               // De eerste hit opvragen en de positie en rotatie toepassen op je cursor
            }
            else
            {
                app.DisableARCursor();
            }
        }
    }
}
