﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ConfigManager : MonoBehaviour
{
    [Header("PostProcessing")]
    public GameObject postProcesingVolume;
    public PostProcessLayer ppLayer;

    [Header("Player")]
    public CharacterMove playerMovement;

    [Header("UI")]
    public GameObject fpsCounter;

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
            ppLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
        }
        else
        {
            ppLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
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