using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class TestCamera : MonoBehaviour
{
    public GameObject player;

    [SerializeField] Vector2 center = Vector2.zero;
    [SerializeField] private float maxPosX;
    [SerializeField] private float maxPosY;

    private Vector3 cameraPos = Vector3.zero;

    private void LateUpdate()
    {
        if (player == null)
            return;

        Transform playerTransform2 = player.transform;
        cameraPos.Set(playerTransform2.localPosition.x, playerTransform2.localPosition.y, transform.position.z);

        float height = Camera.main.orthographicSize;
        float width = height * Screen.width / Screen.height;

        float lx = maxPosX - width;
        float clampX = Mathf.Clamp(cameraPos.x, center.x - lx, center.x + lx);

        float ly = maxPosY - height;
        float clampY = Mathf.Clamp(cameraPos.y, center.y - ly, center.y + ly);

        cameraPos.Set(clampX, clampY, transform.position.z);
        transform.position = cameraPos;
    }
}