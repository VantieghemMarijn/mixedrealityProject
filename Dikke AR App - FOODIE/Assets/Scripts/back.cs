using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class back : MonoBehaviour
{
        public void Back(){
        print("go back");
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(Storage.LastVisitedIndex);
    }
}
