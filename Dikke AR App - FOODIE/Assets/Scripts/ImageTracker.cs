using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARTrackedImageManager))]       // script kan niet gestart worden als er geen ARTrackedImageManager is (in unity)
public class ImageTracker : MonoBehaviour
{

    private ARTrackedImageManager Manager;
    private Dictionary<int, GameObject> InstantiatedObjects = new Dictionary<int, GameObject>();

    public string scene1 = "page2";
    public string scene2 = "page3";

    // Start is called before the first frame update
    void Start()
    {
        Manager = GetComponent<ARTrackedImageManager>();
        Manager.trackedImagesChanged += OnImageEvent;
    }

    // Update is called once per frame
    void Update()
    {

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
                Debug.Log("Got image 1");
                SceneManager.LoadScene(scene1);
                //InstantiatedObjects.Add(image.GetInstanceID(), Instantiate(Prefab1, image.transform.position, image.transform.rotation));
            }
            else if (image.referenceImage.name == "two")
            {
                Debug.Log("Got image 2");
                SceneManager.LoadScene(scene2);
                //InstantiatedObjects.Add(image.GetInstanceID(), Instantiate(Prefab2, image.transform.position, image.transform.rotation));
            }
        }

        foreach (ARTrackedImage image in Args.updated)
        {
            int id = image.GetInstanceID();
            if (InstantiatedObjects.ContainsKey(id))
            {
                InstantiatedObjects[id].transform.position = image.transform.position;
                InstantiatedObjects[id].transform.rotation = image.transform.rotation;
            }
        }

        foreach (ARTrackedImage image in Args.removed)
        {
            int id = image.GetInstanceID();
            if (InstantiatedObjects.ContainsKey(id))
            {
                Object.Destroy(InstantiatedObjects[id]);
                InstantiatedObjects.Remove(id);
            }
        }
    }
}