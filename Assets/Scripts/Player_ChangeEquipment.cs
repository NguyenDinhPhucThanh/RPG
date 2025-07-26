using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeEquipment : MonoBehaviour
{
    public Player_combat combat;
    public Player_Bow bow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Henshin"))
        {
            combat.enabled = !combat.enabled;
            bow.enabled = !bow.enabled;
        }  
    }
}
