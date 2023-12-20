using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject BtnUi;

    public void SetText(bool active)
    {
        BtnUi.SetActive(active);
    }

    public void OnInteract(bool active)
    {
        if (!active) return;
        SceneManager.LoadScene("StartScene");
    }
}
