using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public void back()
    {
        print("go back");
        UnityEngine.SceneManagement.SceneManager.LoadScene(Storage.LastVisitedIndex);
    }
}
