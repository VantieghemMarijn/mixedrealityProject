using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
    // Start is called before the first frame update
    public void backToStart()
    {
        print("go back");
        UnityEngine.SceneManagement.SceneManager.LoadScene("page1");
    }
}
