using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI descText;
    public GameObject testShop;
    public void OnInteract(bool active)
    {
        //TODO: ���� ����
        testShop.SetActive(active);
    }

    public void SetText(bool active)
    {
        descText.gameObject.SetActive(active);
    }
}
