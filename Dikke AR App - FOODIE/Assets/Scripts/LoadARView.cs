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
        Storage.PrefabName="Sushi Plate";
        UnityEngine.SceneManagement.SceneManager.LoadScene("AR Scene");
    }
    
}

public static class Storage {
    public static string PrefabName {get;set;}
}