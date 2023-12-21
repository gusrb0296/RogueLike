using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTransition : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject BtnUi;
    public Vector2 TransitionPosition { get; set; }
    public Vector3Int TransitionLayouyPosition {  get; set; }
    private bool isFKeyPressed = false;

    public void SetText(bool active)
    {
        BtnUi.SetActive(active);
    }

    public void OnInteract(bool active)
    {
        if (!active) return;
        Transform roomtransform = GameManager.instance.StageManager.GetRoomTramsform(TransitionLayouyPosition);
        Vector3 position = roomtransform.position;

        FollowCamera cam = Camera.main.GetComponent<FollowCamera>();
        cam.SetCameraBoundaryCenter(position);

        cam.Player.transform.position = TransitionPosition;

        GameManager.instance.StageManager.Transition(TransitionLayouyPosition);
    }
}
