using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTransition : MonoBehaviour
{
    [SerializeField] private GameObject BtnUi;
    public Vector2 TransitionPosition { get; set; }
    public Vector3Int TransitionLayouyPosition {  get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            BtnUi.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Transform roomtransform = GameManager.instance.StageManager.GetRoomTramsform(TransitionLayouyPosition);
                Vector3 position = roomtransform.position;

                FollowCamera cam = Camera.main.GetComponent<FollowCamera>();
                cam.SetCameraBoundaryCenter(position);

                cam.Player.transform.position = TransitionPosition;

                GameManager.instance.StageManager.Transition(TransitionLayouyPosition);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            BtnUi.SetActive(false);
        }
    }
}
