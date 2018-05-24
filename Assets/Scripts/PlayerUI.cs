using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public SPlayerStats statsToDisplay;
    public Text txtHp;

    void Start()
    {
        GameObject temp = gameObject.transform.Find("Canvas").Find("txtHp").gameObject;
        txtHp = temp.GetComponent<Text>();
    }

    public void UpdateUI()
    {
        txtHp.text = "HP "+statsToDisplay.hp.ToString("0.0");
    }
}
