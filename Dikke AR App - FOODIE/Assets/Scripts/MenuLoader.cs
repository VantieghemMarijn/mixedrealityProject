using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    private string menuItemsPath = "./mr_test_data.json";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script has started");
        LoadMenuItems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadMenuItems()
    {
        Debug.Log("Load function has started");


        ListMenuItem listMenuItem;

        using (StreamReader stream = new StreamReader(menuItemsPath))
        {
            string json = stream.ReadToEnd();
            Debug.Log(json);
            listMenuItem = JsonUtility.FromJson<ListMenuItem>(json);
        }
        


        Debug.Log("Menu items: " + listMenuItem.data.Count);

        foreach (MenuItemm menuItemm in listMenuItem.data)
        {
            Debug.Log("Found menu item: " + menuItemm.title + " "  + menuItemm.desc);
        }



    }
}

[System.Serializable]
public class ListMenuItem
{
    public List<MenuItemm> data = new List<MenuItemm>();
}

[System.Serializable]
public class MenuItemm
{
    public string id;
    public string title;
    public string desc;
    public float price;
    public bool popular;
}
