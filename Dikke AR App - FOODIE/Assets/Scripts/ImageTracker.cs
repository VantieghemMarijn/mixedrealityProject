using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))]       // script kan niet gestart worden als er geen ARTrackedImageManager is (in unity)
public class ImageTracker : MonoBehaviour
{

    private ARTrackedImageManager Manager;
    public ARSession arSession;

    public string scene1 = "page2";
    public string scene2 = "page3";

    // Start is called before the first frame update
    void Start()
    {
        Manager = GetComponent<ARTrackedImageManager>();
        Manager.trackedImagesChanged += OnImageEvent;
        arSession.Reset();
    }

    private void OnDisable()
    {
        Manager.trackedImagesChanged -= OnImageEvent;
    }

    void OnImageEvent(ARTrackedImagesChangedEventArgs Args)
    {
        foreach (ARTrackedImage image in Args.added)
        {
            if (image.referenceImage.name == "one")
            {
                Debug.Log("------------------------------------- Got image 1 -------------------------------------");
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene1);
            }
            else if (image.referenceImage.name == "two")
            {
                Debug.Log("------------------------------------- Got image 2 -------------------------------------");
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene2);
            }
        }
    }
}


