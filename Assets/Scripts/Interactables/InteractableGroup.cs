﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableGroup : MonoBehaviour
{
    [Header("Interaction")]
    public InteractionType interactionType = InteractionType.ChangeColor;
    MeshRenderer objectMeshRenderer;
    Material objectMaterial;
    [HideInInspector]
    public Outline outlineEffect;

    [Header("Group")]
    public List<InteractableGroup> othersInGroup = new List<InteractableGroup>();

    [Header("Change Color")]
    public List<Color> availableColors = new List<Color>();

    [Header("Change Material")]
    public bool isMultiMaterial = false;
    public int multiMaterialIndex = 0;
    Material[] mats;
    public List<Material> availableMaterials = new List<Material>();

    [Header("On/Off")]
    public List<GameObject> objectsToActivate = new List<GameObject>();
    public bool isOn = false;

    // Start is called before the first frame update
    void Awake()
    {
        objectMeshRenderer = GetComponent<MeshRenderer>();
        objectMaterial = objectMeshRenderer.materials[0];
        outlineEffect = GetComponent<Outline>();
    }

    private void Start()
    {
        if (isMultiMaterial)
            mats = objectMeshRenderer.materials;
    }

    public void Interact()
    {
        if (interactionType == InteractionType.OnOff)
        {
            if (!isOn)
            {
                foreach (GameObject go in objectsToActivate)
                {
                    if (!go.activeInHierarchy)
                    {
                        go.SetActive(true);
                    }
                }
                //
                isOn = true;
            }
            else
            {
                foreach (GameObject go in objectsToActivate)
                {
                    if (go.activeInHierarchy)
                    {
                        go.SetActive(false);
                    }
                }
                //
                isOn = false;
            }
            //
            return;
        }
        //
        UIManager.instance.ShowInteractionPanel();
        UIManager.instance.FillInteractionPanel(this);
    }

    public void ToggleOthersOutline(bool enable)
    {
        foreach (InteractableGroup i in othersInGroup)
        {
            i.outlineEffect.enabled = enable;
        }
    }

    #region Change Color

    public void ChangeColor(Color color)
    {
        objectMaterial.SetColor("_Color", color);

        foreach(InteractableGroup i in othersInGroup)
        {
            i.ChangeColorOnly(color);
        }
    }
    public void ChangeColorOnly(Color color)
    {
        objectMaterial.SetColor("_Color", color);
    }

    #endregion

    #region Change Material

    public void ChangeMaterial(int materialIndex)
    {
        if (isMultiMaterial)
        {
            mats[multiMaterialIndex] = availableMaterials[materialIndex];
            objectMeshRenderer.materials = mats;

            foreach (InteractableGroup i in othersInGroup)
            {
                i.ChangeMaterialOnly(materialIndex);
            }
        }
        else
        {
            objectMeshRenderer.material = availableMaterials[materialIndex];

            foreach (InteractableGroup i in othersInGroup)
            {
                i.ChangeMaterialOnly(materialIndex);
            }
        }
    }
    public void ChangeMaterialOnly(int materialIndex)
    {
        if (isMultiMaterial)
        {
            mats[multiMaterialIndex] = availableMaterials[materialIndex];
            objectMeshRenderer.materials = mats;
        }
        else
        {
            objectMeshRenderer.material = availableMaterials[materialIndex];
        }
    }

    #endregion
}
