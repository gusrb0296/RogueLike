using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.ReorderableList;

public class MiniMapUI : MonoBehaviour
{
    private float startPosX = -120f;
    private float startPosY = -128f;

    private float sizeIconX = 60f;
    private float sizeIconY = 60f;

    private GameObject MapUI_Boss;
    private GameObject MapUI_Treasure;
    private GameObject MapUI_Question;
    private GameObject MapUI_Default;

    private void Start()
    {
        GameManager.instance.StageManager.TransitionAction += CreateMiniMap;
        MapUI_Boss = Resources.Load<GameObject>("Prefabs/Map/UI/MapUI_Boss");
        MapUI_Treasure = Resources.Load<GameObject>("Prefabs/Map/UI/MapUI_Treasure");
        MapUI_Question = Resources.Load<GameObject>("Prefabs/Map/UI/MapUI_Question");
        MapUI_Default = Resources.Load<GameObject>("Prefabs/Map/UI/MapUI_Default");
    }

    public void CreateMiniMap(Vector3Int MapsPos)
    {
        RemvoeIcons();

        Vector3Int mapPos = new Vector3Int(0, 0, 0);
        Vector3Int mapsize = GameManager.instance.StageManager.MapSize;

        for (int i = 0; i < mapsize.x; ++i)
        {
            for(int j = 0; j < mapsize.y; ++j)
            {
                mapPos.Set(i, j, 0);
                RoomInfo info = GameManager.instance.StageManager.GetIsRoomInfo(mapPos);
                
                if(info == null)
                    continue;

                if (!info.IsSelected)
                    continue;

                Vector3 pos = new Vector3(startPosX + sizeIconX * i, startPosY + sizeIconY * j);

                GameObject go = null;

                if (!info.Owner.IsVisited)
                {
                    go = Instantiate(MapUI_Question, transform);
                }
                else
                {
                    switch (info.RoomType)
                    {
                        case ERoomType.Boss:
                            go = Instantiate(MapUI_Boss, transform);
                            break;
                        case ERoomType.Reward:
                            go = Instantiate(MapUI_Treasure, transform);
                            break;
                        default:
                            go = Instantiate(MapUI_Default, transform);
                            break;
                    }
                }

                go.transform.localPosition = pos;

                RoomInfo curInfo = GameManager.instance.StageManager.
                    GetIsRoomInfo(GameManager.instance.StageManager.CurPlyerPos);

                if(curInfo == info)
                    go.GetComponent<Image>().color = Color.red;
            }
        }
    }

    private void RemvoeIcons()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
