using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup statsCanvas;
    private bool skillTreeOpen = false;

    void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if(skillTreeOpen)
            {
                Time.timeScale = 1.0f;
                statsCanvas.alpha = 0;
                statsCanvas.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0f;
                statsCanvas.alpha = 1;
                statsCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }
    }
}
