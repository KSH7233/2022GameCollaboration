using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameButton : MonoBehaviour
{
    public GameObject menu;

    public void PopupMenu()
    {
        menu.SetActive(true);
        GameManager.instance.PlaceContainer.gameObject.SetActive(false);
    }
}
