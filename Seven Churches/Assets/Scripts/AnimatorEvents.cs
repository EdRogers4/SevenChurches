using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField] private ScreenManager scriptScreenManager;
    [SerializeField] private Animator animatorScreenPrompt;
    public void StopShowScreenPrompt()
    {
        animatorScreenPrompt.SetBool("isShow", false);
        scriptScreenManager.ToggleScreenPromptOff();
    }

    public void ToggleButtonContinue()
    {
        animatorScreenPrompt.speed = 0;

        if (scriptScreenManager.progressState <= 2)
        {
            scriptScreenManager.ToggleButtonContinue(true);
        }
    }
}
