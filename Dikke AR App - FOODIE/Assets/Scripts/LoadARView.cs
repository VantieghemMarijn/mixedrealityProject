using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadARView : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void LoadAR(){
        Storage.PrefabName=transform.parent.gameObject.GetComponent<Text>().text;
        Storage.LastVisitedIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene("AR Scene");
    }
    
}

