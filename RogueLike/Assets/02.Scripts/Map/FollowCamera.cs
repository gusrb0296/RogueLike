using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class FollowCamera : MonoBehaviour
{
    public GameObject Player { get; set; }
    public Vector2 Center = Vector2.zero;
    public bool RangeRock { get; set; } = true;

    [SerializeField] private float maxPosX;
    [SerializeField] private float maxPosY;

    private Vector3 cameraPos = Vector3.zero;

    private void LateUpdate()
    {
        if (Player == null)
            return;

        Transform playerTransform2 = Player.transform;
        cameraPos.Set(playerTransform2.localPosition.x, playerTransform2.localPosition.y, transform.position.z);

        if (RangeRock)
        {
            float height = Camera.main.orthographicSize;
            float width = height * Screen.width / Screen.height;

            float lx = maxPosX - width;
            float clampX = Mathf.Clamp(cameraPos.x, Center.x - lx, Center.x + lx);

            float ly = maxPosY - height;
            float clampY = Mathf.Clamp(cameraPos.y, Center.y - ly, Center.y + ly);

            cameraPos.Set(clampX, clampY, transform.position.z);
        }

        transform.position = cameraPos;
    }
}