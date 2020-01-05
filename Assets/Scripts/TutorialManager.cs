using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;

public class TutorialManager : MonoBehaviour
{
    public LeanLocalizedText moveLookText, interactText;

    // Start is called before the first frame update
    void Start()
    {
        ChangeLanguage();
    }

    public void FinishTutorial()
    {
        GameManager.isInTutorial = false;
    }

    public void ChangeLanguage()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            moveLookText.TranslationName = "SticksPCTutorial";
            interactText.TranslationName = "InteractPCTutorial";
        }
    }
}
