using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public List<GameObject> panels = new List<GameObject>();
    [Space(10)]
    public int configPanelIndex = 4;
    public int creditsPanelIndex = 5;
    int activePanel = 0, lastActivePanel = 0;

    [Header("Interaction Panel")]
    public GameObject optionPrefab;
    public Transform optionContainer;

    #region Singleton

    public static UIManager instance = null;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    #endregion

    public void ShowPanel(int panelIndex)
    {
        lastActivePanel = activePanel;
        foreach (GameObject g in panels)
        {
            g.SetActive(false);
        }
        //
        panels[panelIndex].SetActive(true);
        activePanel = panelIndex;
    }

    public void ReturnToGame()
    {
        ShowPanel(lastActivePanel);
    }
    //
    public void ShowConfig()
    {
        panels[configPanelIndex].SetActive(true);
    }
    public void HideConfig()
    {
        panels[configPanelIndex].SetActive(false);
    }
    //
    public void ShowCredits()
    {
        panels[creditsPanelIndex].SetActive(true);
    }
    public void HideCredits()
    {
        panels[creditsPanelIndex].SetActive(false);
    }
    //
    public void ShowInteractionPanel()
    {
        ShowPanel(1);
        GameManager.isInInteractionMenu = true;
    }
    public void HideInteractionPanel()
    {
        ShowPanel(0);
        GameManager.isInInteractionMenu = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #region Interaction Panel

    public void FillInteractionPanel(Interactable interactableElement)
    {
        ClearInteractionContainer();

        switch (interactableElement.interactionType)
        {
            case InteractionType.ChangeColor:
                foreach (Color c in interactableElement.availableColors)
                {
                    InteractableOption io = Instantiate(optionPrefab, optionContainer).GetComponent<InteractableOption>();
                    io.optionButton.onClick.AddListener(() => interactableElement.ChangeColor(c));
                    io.optionImage.color = c;
                }
                break;
            case InteractionType.ChangeMaterial:
                foreach(Material m in interactableElement.availableMaterials)
                {
                    InteractableOption io = Instantiate(optionPrefab, optionContainer).GetComponent<InteractableOption>();
                    Sprite s = Sprite.Create((Texture2D)m.GetTexture("_MainTex"), io.optionImage.sprite.rect, io.optionImage.sprite.pivot);
                    io.optionButton.onClick.AddListener(() => interactableElement.ChangeMaterial(interactableElement.availableMaterials.IndexOf(m)));
                    io.optionImage.sprite = s;
                    io.optionImage.color = m.GetColor("_Color");
                }
                break;
            case InteractionType.ChangeModel:
                break;
        }
    }
    public void FillInteractionPanel(InteractableGroup interactableGroupElement)
    {
        ClearInteractionContainer();

        switch (interactableGroupElement.interactionType)
        {
            case InteractionType.ChangeColor:
                foreach (Color c in interactableGroupElement.availableColors)
                {
                    InteractableOption io = Instantiate(optionPrefab, optionContainer).GetComponent<InteractableOption>();
                    io.optionButton.onClick.AddListener(() => interactableGroupElement.ChangeColor(c));
                    io.optionImage.color = c;
                }
                break;
            case InteractionType.ChangeMaterial:
                foreach (Material m in interactableGroupElement.availableMaterials)
                {
                    InteractableOption io = Instantiate(optionPrefab, optionContainer).GetComponent<InteractableOption>();
                    Sprite s = Sprite.Create((Texture2D)m.GetTexture("_MainTex"), io.optionImage.sprite.rect, io.optionImage.sprite.pivot);
                    io.optionButton.onClick.AddListener(() => interactableGroupElement.ChangeMaterial(interactableGroupElement.availableMaterials.IndexOf(m)));
                    io.optionImage.sprite = s;
                    io.optionImage.color = m.GetColor("_Color");
                }
                break;
            case InteractionType.ChangeModel:
                break;
        }
    }

    void ClearInteractionContainer()
    {
        foreach(Transform t in optionContainer)
        {
            Destroy(t.gameObject);
        }
    }

    #endregion
}
