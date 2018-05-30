using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public SPlayerStats statsToDisplay;
    public Text txtHp;
    public Text txtGold;

    void Start()
    {
        GameObject temp = gameObject.transform.Find("Canvas").Find("txtHp").gameObject;
        txtHp = temp.GetComponent<Text>();
        temp = gameObject.transform.Find("Canvas").Find("txtGold").gameObject;
        txtGold = temp.GetComponent<Text>();
    }

    public void UpdateUI()
    {
        txtHp.text = "HP "+statsToDisplay.hp.ToString("0.0");
        txtGold.text = "Gold " + statsToDisplay.gold.ToString();
    }
}
