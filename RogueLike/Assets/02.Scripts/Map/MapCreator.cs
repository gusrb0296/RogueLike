using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERoomType
{
    Start,
    Battle,
    Reward,
    Boss,
    Clear
}

public class MapCreator : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private int numberOfRooms = 10;

    //TEST
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject bossRoomPrefab;
    [SerializeField] private GameObject treasureRoomPrefab;
    [SerializeField] private GameObject playerPrefab;

    private RoomInfo[,] _map;
    private List<RoomInfo> _endRooms = new List<RoomInfo>();

    public Vector3Int MapSize { get; } = new Vector3Int(28, 18);

    private void Start()
    {
        GameManager.instance.StageManager.SetStageInfo(this, width, height);
        CreateNewMap();
        StartStage();
    }

    public void CreateNewMap()
    {
        RemoveRooms();
        CreateMapLayout();
        PlaceRooms();
        AddTiles();
        CreatePlayer();
    }

    private void StartStage()
    {
        GameManager.instance.StageManager.Transition(GameManager.instance.StageManager.CurPlyerPos);
    }

    //TEST
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (Camera.main.GetComponent<FollowCamera>().Player != null)
            {
                Destroy(Camera.main.GetComponent<FollowCamera>().Player);
                Camera.main.GetComponent<FollowCamera>().Player = null;
            }
            Start();
        }
    }

    private void CreateMapLayout()
    {
        int roomsCount = 0;
        while (roomsCount < numberOfRooms)
        {
            InitializeMap();
            _endRooms.Clear();

            RoomInfo startingRoom = _map[width / 2, height / 2];
            startingRoom.IsSelected = true;

            Queue<RoomInfo> roomsToVisit = new Queue<RoomInfo>();
            roomsToVisit.Enqueue(startingRoom);

            roomsCount = 1;
            while (roomsToVisit.Count > 0)
            {
                RoomInfo currentRoom = roomsToVisit.Dequeue();
                List<RoomInfo> currentRoomNeighbors = GetNeighbors(currentRoom);
                bool hasAddedNewRoom = false;

                foreach (RoomInfo neighbor in currentRoomNeighbors)
                {
                    if (neighbor.IsSelected)
                    {
                        continue;
                    }

                    if (HasMoreThanOneNeighbor(neighbor))
                    {
                        continue;
                    }

                    bool hasEnoughRoom = roomsCount >= numberOfRooms;
                    if (hasEnoughRoom)
                    {
                        continue;
                    }

                    bool shouldGiveUp = Random.Range(0, 1f) < 0.5f;
                    if (shouldGiveUp)
                    {
                        continue;
                    }

                    neighbor.IsSelected = true;
                    roomsToVisit.Enqueue(neighbor);
                    roomsCount++;
                    hasAddedNewRoom = true;
                }

                if (!hasAddedNewRoom && currentRoom.IsSelected)
                {
                    _endRooms.Add(currentRoom);
                }
            }
        }
    }

    private void InitializeMap()
    {
        _map = new RoomInfo[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _map[x, y] = new RoomInfo(new Vector3Int(x, y));
            }
        }
    }

    private List<RoomInfo> GetNeighbors(RoomInfo room)
    {
        List<RoomInfo> output = new List<RoomInfo>();
        int up = room.Position.y + 1;
        if (IsRoomPositionValid(new Vector3Int(room.Position.x, up)))
        {
            output.Add(_map[room.Position.x, up]);
        }
        int down = room.Position.y - 1;
        if (IsRoomPositionValid(new Vector3Int(room.Position.x, down)))
        {
            output.Add(_map[room.Position.x, down]);
        }
        int right = room.Position.x + 1;
        if (IsRoomPositionValid(new Vector3Int(right, room.Position.y)))
        {
            output.Add(_map[right, room.Position.y]);
        }
        int left = room.Position.x - 1;
        if (IsRoomPositionValid(new Vector3Int(left, room.Position.y)))
        {
            output.Add(_map[left, room.Position.y]);
        }
        return output;
    }

    private bool IsRoomPositionValid(Vector3Int position)
    {
        if (position.x < 0 || position.x >= width)
        {
            return false;
        }

        if (position.y < 0 || position.y >= height)
        {
            return false;
        }
        return true;
    }

    private bool HasMoreThanOneNeighbor(RoomInfo room)
    {
        List<RoomInfo> neighbors = GetNeighbors(room);
        int selectedNeighborsCount = 0;
        foreach (RoomInfo neighbor in neighbors)
        {
            if (neighbor.IsSelected)
            {
                selectedNeighborsCount++;
            }
        }
        return selectedNeighborsCount > 1;
    }

    private void PlaceRooms()
    {
        PlaceBossRoom();
        PlaceTreasureRooms();
        PlaceNormalRooms();
    }

    private GameObject InstantiateRoom(GameObject roomPrefab, RoomInfo roomInfo, ERoomType type = ERoomType.Battle)
    {
        Vector3 roomPosition = new Vector3(roomInfo.Position.x * MapSize.x, roomInfo.Position.y * MapSize.y);
        GameObject room = Instantiate(roomPrefab, roomPosition, Quaternion.identity, transform);

        Room roomScrip = null;

        if(roomInfo == _map[width / 2, height / 2])
            roomScrip = room.AddComponent<Room>();
        else if (type == ERoomType.Battle)
            roomScrip = room.AddComponent<BattleRoom>();
        else if (type == ERoomType.Boss)
            roomScrip = room.AddComponent<BossRoom>();
        else if (type == ERoomType.Reward)
            roomScrip = room.AddComponent<TreasureRoom>();

        roomScrip.RoomInfo = roomInfo;
        roomScrip.RoomInfo.Owner = roomScrip;

        return room;
    }


    private void PlaceBossRoom()
    {
        RoomInfo room = _endRooms[_endRooms.Count - 1];
        InstantiateRoom(bossRoomPrefab, room, ERoomType.Boss);
    }

    private void PlaceTreasureRooms()
    {
        for(int i = 0; i <  _endRooms.Count - 1; i++) 
        {
            float treasureChance = Random.Range(0f, 1f);
            bool hasTreasure = treasureChance > 0.7f;
            if (hasTreasure)
            {
                InstantiateRoom(treasureRoomPrefab, _endRooms[i], ERoomType.Reward);
            }
            else
            {
                InstantiateRoom(roomPrefab, _endRooms[i]);
            }
        }
    }

    private void PlaceNormalRooms()
    {
        foreach (RoomInfo room in _map)
        {
            if (room.IsSelected && !_endRooms.Contains(room))
            {
                InstantiateRoom(roomPrefab, room);
            }
        }
    }

    private void RemoveRooms()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public Transform GetStartPos()
    {
        RoomInfo startingRoom = _map[width / 2, height / 2];
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Room>().RoomInfo == startingRoom)
                return child;
        }
        return null;
    }

    public RoomInfo GetRoomInfo(Vector3Int position)
    {
        if(!IsRoomPositionValid(position))
            return null;

        return _map[position.x, position.y];
    }

    public Transform GetRoomTramsform(Vector3Int position)
    {
        RoomInfo roomInfo = _map[position.x, position.y];
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Room>().RoomInfo.Position == roomInfo.Position)
            {
                if (roomInfo.Owner == null)
                    roomInfo.Owner = child.GetComponent<Room>();
                return child;
            }
        }
        return null;
    }

    private void CreatePlayer()
    {
        Transform startPos = GetStartPos();
        if( startPos != null )
            Camera.main.GetComponent<FollowCamera>().Player = 
                Instantiate(playerPrefab, startPos.position, Quaternion.identity);

        Camera.main.GetComponent<FollowCamera>()
            .SetCameraBoundaryCenter(new Vector2(startPos.position.x, startPos.position.y));
    }

    private void AddTiles()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Room>().AddTiles();
        }
    }
}
