using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerater : MonoBehaviour
{
    public enum Direction {up, down, right, left };
    public Direction direction;

    [Header("Room Info")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("Place Control")]
    public Transform generatorPoint;
    public float xOffset ;
    public float yOffset;
    public float zOffset;

    public GameObject player;

    public LayerMask roomLayer;

    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();
    public int maxStep;

    [Header("NavMeshBake Obj")]
    public GameObject bakeTool;
    void Start()
    {
        //GameObject.Instantiate(player, generatorPoint);
        bakeTool.SetActive(false);
        for (int i = 0; i < roomNumber; i++) 
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());
            
            //Change Point Position
            ChangePointPosition();
        }

        //rooms[0].GetComponent<SpriteRenderer>().material.color = startColor;

        endRoom = rooms[0].gameObject;
        foreach (var room in rooms) 
        {
            //if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            //{
            //    endRoom = room.gameObject;
            //}
            SetupRoom(room, room.transform.position);
        }

        FindEndRoom();
        //endRoom.GetComponent<SpriteRenderer>().material.color = endColor;
        NavMeshBake();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// 设置随机生成房间
    /// </summary>
    public void ChangePointPosition() 
    {
        Vector3 temp = generatorPoint.position;
        do
        {
            generatorPoint.position = temp;
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, 0, zOffset*2);

                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, 0, -zOffset*2);
        
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset*2, 0, 0);
                   
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset*2, 0, 0);
                    
                    break;

            }
        } while (Physics.OverlapBox(generatorPoint.position, new Vector3(1f,1f,1f), Quaternion.identity).Length>0);

    }

    /// <summary>
    /// 设置房间之间的门的连接
    /// </summary>
    /// <param name="newRoom"></param>
    /// <param name="roomPosition"></param>
    public void SetupRoom(Room newRoom, Vector3 roomPosition) 
    {
        newRoom.roomUp = Physics.OverlapBox(roomPosition + new Vector3(0, 0, zOffset * 2), new Vector3(1f, 1f, 1f), Quaternion.identity).Length > 0;
        newRoom.roomDown = Physics.OverlapBox(roomPosition + new Vector3(0, 0, -zOffset * 2), new Vector3(1f, 1f, 1f), Quaternion.identity).Length > 0;
        newRoom.roomLeft = Physics.OverlapBox(roomPosition + new Vector3(-xOffset * 2, 0, 0), new Vector3(1f, 1f, 1f), Quaternion.identity).Length > 0;
        newRoom.roomRight = Physics.OverlapBox(roomPosition + new Vector3(xOffset * 2, 0, 0), new Vector3(1f, 1f, 1f), Quaternion.identity).Length > 0;

        newRoom.UpdateRoom();
    }

    public void FindEndRoom() 
    {
        for (int i = 0; i < rooms.Count; i++) 
        {
            if (rooms[i].stepToStart > maxStep) 
            {
                maxStep = rooms[i].stepToStart;
            }
        }

        foreach (var room in rooms) 
        {
            if (room.stepToStart == maxStep)          
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessRooms.Add(room.gameObject);
            
        }

        for (int i = 0; i < farRooms.Count; i++) 
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }
        for (int i = 0; i < lessRooms.Count; i++)
        {
            if (lessRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessRooms[i]);
        }

        if (oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }

    }
    void NavMeshBake()
    {
        Vector3 b_size = new Vector3(0,0,0);
        Vector3 max_posion = new Vector3(0,0,0);
        Vector3 min_posion = new Vector3(0,0,0);
        Vector3 offset_ = new Vector3(0, 0, zOffset) + new Vector3(xOffset, 0, 0);

        foreach (Room room in rooms)
        {
            max_posion.x = Mathf.Max(max_posion.x, room.transform.position.x);
            max_posion.z = Mathf.Max(max_posion.z, room.transform.position.z);
            min_posion.x = Mathf.Min(min_posion.x, room.transform.position.x);
            min_posion.z = Mathf.Min(min_posion.z, room.transform.position.z);
        }
        
        min_posion-= offset_;
        b_size = max_posion - min_posion+ offset_+new Vector3(5,0,5);
        bakeTool.transform.position = (max_posion + min_posion)/2;

        b_size.y = 20f;
        bakeTool.GetComponent<LocalNavMeshBuilder>().m_Size = b_size;
        bakeTool.SetActive(true);
    }
}
