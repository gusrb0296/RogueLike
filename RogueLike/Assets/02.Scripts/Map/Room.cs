using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum EMoveType
{
    Right = 1 << 0,
    Left = 1 << 1,
    Up = 1 << 2,
    Down = 1 << 3,
}

public class Room : MonoBehaviour
{
    private static readonly Vector2 LEFTPOS = new Vector2(-11.5f, 0.5f);
    private static readonly Vector2 RIGHTPOS = new Vector2(11.5f, -1.5f);
    private static readonly Vector2 DOWNPOS = new Vector2(0.5f, -6.5f);
    private static readonly Vector2 UPPOS = new Vector2(-0.5f, 5.5f);

    public RoomInfo RoomInfo {  get; set; }
    public bool IsClear { get; set; }
    public byte moveType { get; protected set; } = 0;

    protected void SetMoveType()
    {
        // 오른쪽, 왼쪽, 위, 아래
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        for (int i = 0; i < 4; ++i)
        {
            Vector3Int pos = new Vector3Int(RoomInfo.Position.x + dx[i], RoomInfo.Position.y + dy[i], RoomInfo.Position.z);
            MapCreator creator = GameManager.instance.StageManager.MapCreator;
            RoomInfo Neighbor = creator.GetRoomInfo(pos);
            if (Neighbor != null)
            {
                if (Neighbor.IsSelected)
                {
                    switch (i)
                    {
                        case 0:
                            moveType |= (int)EMoveType.Right;
                            break;
                        case 1:
                            moveType |= (int)EMoveType.Left;
                            break;
                        case 2:
                            moveType |= (int)EMoveType.Up;
                            break;
                        case 3:
                            moveType |= (int)EMoveType.Down;
                            break;
                    }
                }
            }

        }
    }

    public virtual void RoomAction()
    {
        RoomClear();
    }

    public virtual void RoomClear()
    {
        IsClear = true;
        CreatePortal();
    }

    public virtual void AddTiles()
    {
        SetMoveType();

        if (RightRoomCheck()) 
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Map/RightTile");
            Instantiate(go, transform);
        }

        if (LeftRoomCheck())
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Map/LeftTile");
            Instantiate(go, transform);
        }

        if (UpRoomCheck())
        {
            GameObject go = Resources.Load<GameObject>("Prefabs/Map/UpTile");
            Instantiate(go, transform);
        }

        //test
        //CreatePortal();
    }

    private void CreatePortal()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Map/PortalObj");

        if (RightRoomCheck())
        {
            CreateRightPortal(go);
        }

        if (LeftRoomCheck())
        {
            CreateLeftPortal(go);
        }

        if (UpRoomCheck())
        {
            CreateUpPortal(go);
        }

        if (DownRoomCheck())
        {
            CreateDownPortal(go);
        }
    }

    private void CreateRightPortal(GameObject portalFrefab)
    {
        GameObject portal = Instantiate(portalFrefab, transform);
        portal.transform.localPosition = RIGHTPOS;

        PortalTransition Transition = portal.GetComponent<PortalTransition>();
        Vector3Int nextPos = new Vector3Int(RoomInfo.Position.x + 1, RoomInfo.Position.y + 0, RoomInfo.Position.z);
        Transition.TransitionLayouyPosition = nextPos;

        Transform nextRoomPos = GameManager.instance.StageManager.GetRoomTramsform(nextPos);
        Transition.TransitionPosition = new Vector2(nextRoomPos.position.x + LEFTPOS.x, nextRoomPos.position.y + LEFTPOS.y);
    }

    private void CreateLeftPortal(GameObject portalFrefab)
    {
        GameObject portal = Instantiate(portalFrefab, transform);
        portal.transform.localPosition = LEFTPOS;
        portal.GetComponent<SpriteRenderer>().flipX = true;

        PortalTransition Transition = portal.GetComponent<PortalTransition>();
        Vector3Int nextPos = new Vector3Int(RoomInfo.Position.x - 1, RoomInfo.Position.y + 0, RoomInfo.Position.z);
        Transition.TransitionLayouyPosition = nextPos;

        Transform nextRoomPos = GameManager.instance.StageManager.GetRoomTramsform(nextPos);
        Transition.TransitionPosition = new Vector2(nextRoomPos.position.x + RIGHTPOS.x, nextRoomPos.position.y + RIGHTPOS.y);
    }

    private void CreateUpPortal(GameObject portalFrefab)
    {
        GameObject portal = Instantiate(portalFrefab, transform);
        portal.transform.localPosition = UPPOS;

        PortalTransition Transition = portal.GetComponent<PortalTransition>();
        Vector3Int nextPos = new Vector3Int(RoomInfo.Position.x, RoomInfo.Position.y + 1, RoomInfo.Position.z);
        Transition.TransitionLayouyPosition = nextPos;

        Transform nextRoomPos = GameManager.instance.StageManager.GetRoomTramsform(nextPos);
        Transition.TransitionPosition = new Vector2(nextRoomPos.position.x + DOWNPOS.x, nextRoomPos.position.y + DOWNPOS.y);
    }

    private void CreateDownPortal(GameObject portalFrefab)
    {
        GameObject portal = Instantiate(portalFrefab, transform);
        portal.transform.localPosition = DOWNPOS;

        PortalTransition Transition = portal.GetComponent<PortalTransition>();
        Vector3Int nextPos = new Vector3Int(RoomInfo.Position.x, RoomInfo.Position.y - 1, RoomInfo.Position.z);
        Transition.TransitionLayouyPosition = nextPos;

        Transform nextRoomPos = GameManager.instance.StageManager.GetRoomTramsform(nextPos);
        Transition.TransitionPosition = new Vector2(nextRoomPos.position.x + UPPOS.x, nextRoomPos.position.y + UPPOS.y);
    }

    public bool RightRoomCheck()
    {
        return (int)EMoveType.Right == (moveType & (int)EMoveType.Right);
    }

    public bool LeftRoomCheck()
    {
        return (int)EMoveType.Left == (moveType & (int)EMoveType.Left);
    }

    public bool UpRoomCheck()
    {
        return (int)EMoveType.Up == (moveType & (int)EMoveType.Up);
    }

    public bool DownRoomCheck()
    {
        return (int)EMoveType.Down == (moveType & (int)EMoveType.Down);
    }
}
