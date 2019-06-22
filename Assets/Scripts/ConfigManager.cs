using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    [Header("PostProcessing")]
    public GameObject postProcesingVolume;

    [Header("Player")]
    public CharacterMove playerMovement;

    [Header("UI")]
    public GameObject fpsCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeViewSens(float sens)
    {
        playerMovement.xAxisSpeed = sens;
        playerMovement.yAxisSpeed = sens;
    }
    public void ChangeMoveSens(float sens)
    {
        playerMovement.forwardMovementSpeed = sens;
        playerMovement.sideMovementSpeed = sens;
    }

    public void EnableAA(bool enable)
    {
        if(enable)
        {
            QualitySettings.antiAliasing = 2;
        }
        else
        {
            QualitySettings.antiAliasing = 0;
        }
    }

    public void EnablePP(bool enable)
    {
        postProcesingVolume.SetActive(enable);
    }

    public void EnableVsync(bool enable)
    {
        if(enable)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void ShowFPS(bool show)
    {
        fpsCounter.SetActive(show);
    }
}
