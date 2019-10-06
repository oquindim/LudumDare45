using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuItemsBehaviour : MonoBehaviour
{
    public TextMeshProUGUI itemText;
    
    public void OnMouseOver() {
        print(itemText);
        itemText.fontSize = itemText.fontSize + 5;
    }    

    public void OnMouseExit() {
        print("saiu");
        itemText.fontSize = itemText.fontSize;
    }
}
