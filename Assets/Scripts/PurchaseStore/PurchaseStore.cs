using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PurchaseStore : MonoBehaviour
{
    int coins;
    // int ship1, ship2, ship3;
    int[] shipSold = new int[2];
    public Button buyButton, selectButton, defaultShip;
    public Text sellingPrice1;
    public Text moneyLeft;
    int isShipSold;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetString("ItemsSold", "0,0");
        coins = PlayerPrefs.GetInt("coins");
        // PlayerPrefs.SetInt("coins", 1000);
    }

    // Update is called once per frame
    void Update()
    {   
        isShipSold = PlayerPrefs.GetInt("Ship1");
        moneyLeft.text = "Coins: " + coins;
        if(coins >= 5 && isShipSold == 0) {
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            
        } else {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            if(PlayerPrefs.GetInt("PlayerShip") == 1) {
                selectButton.GetComponentInChildren<Text>().text = "Selected";
                selectButton.interactable = false;
            } else {
                selectButton.GetComponentInChildren<Text>().text = "Select";
                selectButton.interactable = true;
            }
        }

        if(PlayerPrefs.GetInt("PlayerShip") == 0) {
            defaultShip.interactable = false;
            defaultShip.GetComponentInChildren<Text>().text = "Selected";
        } else {
            defaultShip.interactable = true;
            defaultShip.GetComponentInChildren<Text>().text = "Select";
        }
    }

    public void buyShip1() {
        coins -= Convert.ToInt32(sellingPrice1.text);
        PlayerPrefs.SetInt("Ship1", 1);
        PlayerPrefs.SetInt("coins", coins);
        // sellingPrice1.text = "Bought";
        buyButton.gameObject.SetActive(false);
        // shipSold[position] = 1;
    }

    public void SelectShip1() {
        PlayerPrefs.SetInt("PlayerShip", 1);
        selectButton.GetComponentInChildren<Text>().text = "Selected";
        selectButton.interactable = false;
        defaultShip.interactable = true;
        defaultShip.GetComponentInChildren<Text>().text = "Select";
    }

    public void SelectDefault() {
        PlayerPrefs.SetInt("PlayerShip", 0);
        selectButton.GetComponentInChildren<Text>().text = "Select";
        selectButton.interactable = true;
        defaultShip.interactable = false;
        defaultShip.GetComponentInChildren<Text>().text = "Selected";
    }

    public void onExit() {
        Debug.Log("Returning to Main Menu");
        SceneManager.LoadScene("Menu");
    }

}
