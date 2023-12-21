using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI descText;
    private GameObject testShop;

    private void Awake()
    {
        testShop = GameManager.instance.UiManager.statsWindow;
    }

    public void OnInteract(bool active)
    {
        if (testShop == null) return;
        testShop.SetActive(active);
    }

    public void SetText(bool active)
    {
        descText.gameObject.SetActive(active);
    }
}
