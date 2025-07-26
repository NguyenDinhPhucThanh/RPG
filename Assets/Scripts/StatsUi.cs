using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUi : MonoBehaviour
{
    public GameObject[] statSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
        }
    }

    public void UpdateDamage()
    {
        if (statSlots.Length > 0 && statSlots[0] != null)
            statSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance?.damage;
    }

    public void UpdateSpeed()
    {
        if (statSlots.Length > 1 && statSlots[1] != null)
            statSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance?.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }
}
