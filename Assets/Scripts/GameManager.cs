using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isInIsoMode = false, isInMenu = false, isInInteractionMenu = false;

    public void IsoMode(bool active)
    {
        isInIsoMode = active;
    }
    public void InMenu(bool active)
    {
        isInMenu = active;
    }
}
