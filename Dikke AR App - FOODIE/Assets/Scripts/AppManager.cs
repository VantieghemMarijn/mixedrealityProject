using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.XR.ARFoundation;

public class AppManager : MonoBehaviour
{
    // Nieuwe classe waarin je in unity var's aan kan meegeven, de [System.Serializable] zorgt ervoor dat je er een array van kan maken
    [System.Serializable]
    public class ARPrefabsFood
    {
        public string Name;
        public GameObject Prefab;
        public string Description;
    }

    [System.Serializable]
    public class ARPrefabsDrinks
    {
        public string Name;
        public GameObject Prefab;
    }

    public ARPrefabsFood[] ARPrefabsFoodList;
    public ARPrefabsDrinks[] ARPrefabsDrinksList;
    public Dropdown dropdown;
    public GameObject MessagePanel;
    public Text MessageText;
    public Camera ARCamera;
    public GameObject BillBoardPrefab;
    public Material UIBlue;
    public Material UIPink;
    public GameObject InfoPanel;
    public Text FoodNameUI;
    public Text FoodPriceUI;
    public ARSession arSession;

    private GameObject BillBoardInstantie;
    private bool PlacingFood = true;
    private GameObject PlacedFood;
    private TextMesh BillBoardTitle;
    private TextMesh BillBoardText;
    private TouchPhase lastPhase = TouchPhase.Began;
    private GameObject ARCursor;
    private GameObject CurrentItemSelectedPrefab;
    private string CurrentItemSelectedName;
    private string CurrentItemSelectedDescr;


    private void Start()
    {
        arSession.Reset();
        Debug.Log(Storage.PrefabName);
        Debug.Log(Storage.PrefabPrice);
        InitSelectMenu();
        SelectItemFood(Storage.PrefabName);
        InfoPanel.gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        UnityEngine.Object.Destroy(ARCursor);
    }

    // wordt gecalled vanuit de PlaneObserver script
    public void EnableARCursor(Vector3 position, Quaternion rotation)
    {
        if (ARCursor)       // als er geenARCursor prefab is, de cursor niet tonen want dan is er niets geselecteerd
        {
            ARCursor.SetActive(true);
            rotation = Quaternion.Euler(0f, ARCamera.transform.rotation.eulerAngles.y, 0f);     // rotate the ARCursor to face the camera
            ARCursor.transform.position = position;
            ARCursor.transform.rotation = rotation;
            MessagePanel.SetActive(true);
            MessagePanel.GetComponent<Image>().material = UIBlue;      // maak de message box groen
            MessageText.text = "Tap the screen to place object!";
        }
        else
        {
            MessagePanel.SetActive(false);
        }
    }

    // wordt gecalled vanuit de PlaneObserver script
    public void DisableARCursor()
    {
        if (ARCursor)      // als er geenARCursor prefab is, de cursor niet tonen
        {
            MessagePanel.SetActive(true);
            MessagePanel.GetComponent<Image>().material = UIPink;      // maak de message box roze
            MessageText.text = "Searching for flat surface...";
            ARCursor.SetActive(false);
        }
        else
        {
            MessagePanel.SetActive(false);
        }
    }

    public void SelectItemFood(string name)
    {
        foreach (ARPrefabsFood arPrefabFood in ARPrefabsFoodList)
        {
            if (arPrefabFood.Name == name)
            {
                Debug.Log("------ Food found ------");
                CurrentItemSelectedName = arPrefabFood.Name;
                CurrentItemSelectedPrefab = arPrefabFood.Prefab;
                CurrentItemSelectedDescr = arPrefabFood.Description;
                FoodNameUI.text = CurrentItemSelectedName;
                FoodPriceUI.text = Storage.PrefabPrice;
                ARCursor = Instantiate(CurrentItemSelectedPrefab, transform);
                ARCursor.SetActive(false);
                return;
            }
        }
        // Geen matching item gevonden
        CurrentItemSelectedName = null;
        CurrentItemSelectedPrefab = null;
        CurrentItemSelectedDescr = null;
        throw new Exception("-------------------------------- Selected Food doesn't excist ---------------------------------------\n\n");
    }

    public void SelectItemDrinks(string name)
    {
        foreach (ARPrefabsDrinks arPrefabDrinks in ARPrefabsDrinksList)
        {
            if (arPrefabDrinks.Name == name)
            {
                CurrentItemSelectedName = arPrefabDrinks.Name;
                CurrentItemSelectedPrefab = arPrefabDrinks.Prefab;
                ARCursor = Instantiate(CurrentItemSelectedPrefab, transform);
                ARCursor.SetActive(false);
                return;
            }
        }
        // Geen matching Drink gevonden
        CurrentItemSelectedName = null;
        CurrentItemSelectedPrefab = null;
        throw new Exception("-------------------------------- Selected Drink doesn't excist ---------------------------------------\n\n");
    }

    private void InitSelectMenu()
    {
        // DROPDOWN MENU
        dropdown.ClearOptions();

        dropdown.options.Add(new Dropdown.OptionData() { text = "Choose your drink" });
        // FILL dropdown with items
        foreach (ARPrefabsDrinks ARDrinks in ARPrefabsDrinksList)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = ARDrinks.Name });
        }

        // Het standaard geselecteerde item invullen
        dropdown.RefreshShownValue();

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    private void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        if (index == 0)     // Checken of 'Choose your drink' is geselecteerd
        {
            CurrentItemSelectedName = null;
            CurrentItemSelectedPrefab = null;
            if (ARCursor)           // als de ARCursor bestaat, deze ook inactief zetten
            {
                ARCursor.SetActive(false);
                ARCursor = null;
            }
        }
        else
        {
            index -= 1;         // 'None' is het eerste item dus de lijst 1 opschuiven
            CurrentItemSelectedName = ARPrefabsDrinksList[index].Name;
            CurrentItemSelectedPrefab = ARPrefabsDrinksList[index].Prefab;
            ARCursor = Instantiate(CurrentItemSelectedPrefab, transform);
            ARCursor.SetActive(false);
        }
    }

    void Update()
    {
        if (CurrentItemSelectedPrefab)    
        {
            if (Input.touchCount == 1)       // hoeveel vingers staan er op het scherm, meer of minder dan 1 doe ik niets
            {
                Touch touch = Input.GetTouch(0);        // de huidige touch input opvragen van vinger 0
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) == false)       // check of mijn cursor niet boven een bestaant gameObject hovert. Zorgt ervoor dat er geen cubusen in elkaar spawnen
                {
                    if ((touch.phase == TouchPhase.Ended) && (lastPhase != TouchPhase.Ended))   // wacht tot je loslaat
                    {       // de huidige touch phase is negatief && de touch phase hievoor was actief
                        if (ARCursor.activeSelf)    // check of de AR cursor actief is, 
                        {
                            if (PlacingFood == true)
                            {       // enkel de eerste keer bij je food uitvoeren
                                PlacedFood = Instantiate(CurrentItemSelectedPrefab, ARCursor.transform.position, ARCursor.transform.rotation);      // GameObject onthouden om later de afstand te berekenen
                                PlacedFood.SetActive(true);
                                Vector3 BillBoardPosition = ARCursor.transform.position;
                                BillBoardPosition.y += (float)0.2;      // zorg ervoor dat de billboard center 20cm boven je 'eten' zweeft
                                BillBoardInstantie = Instantiate(BillBoardPrefab, BillBoardPosition, ARCursor.transform.rotation);
                                BillBoardTitle = GameObject.FindGameObjectWithTag("BillBoardTitle").GetComponent<TextMesh>();     // de TextMesh van het gameobject met tag BillBoardText
                                BillBoardTitle.text = CurrentItemSelectedName;
                                BillBoardText = GameObject.FindGameObjectWithTag("BillBoardText").GetComponent<TextMesh>();     // de TextMesh van het gameobject met tag BillBoardText
                                BillBoardText.text = CurrentItemSelectedDescr;
                                BillBoardInstantie.SetActive(false);        // je billboard nog niet tonen
                            }
                            else
                            {
                                Instantiate(CurrentItemSelectedPrefab, ARCursor.transform.position, ARCursor.transform.rotation);     // plaats je prefab
                            }
                            PlacingFood = false;         // reset alles
                            CurrentItemSelectedPrefab = null;
                            CurrentItemSelectedName = null;
                            ARCursor.SetActive(false);
                            ARCursor = null;
                        }
                    }
                    lastPhase = touch.phase;        // verander naar de huidige touch phase zodat je hierboven een dalende flank kan registeren
                }
            }
        }

        if (PlacingFood == false)       // enkel de afstand berekenen tussen de camera en je food als je food al geplaatst is
        {
            if (Vector3.Distance(PlacedFood.transform.position, ARCamera.transform.position) < 0.6)
            {
                InfoPanel.gameObject.SetActive(false);
                BillBoardInstantie.SetActive(true);
            }
            else
            {
                InfoPanel.gameObject.SetActive(true);
                BillBoardInstantie.SetActive(false);
            }
        }
    }
}
