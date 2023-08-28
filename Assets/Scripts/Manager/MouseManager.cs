using System;
using UnityEngine;
using UnityEngine.EventSystems;

//[System.Serializable]  u0001
//public class EventVector3 : UnityEvent<Vector3> { }

public class MouseManager : Singleton<MouseManager>
{
    //public static MouseManager Instance;               lession 20 beause of already initialized in Singleton

    public Texture2D point, doorway, attack, target, arrow;

    RaycastHit hitInfo;
    //public EventVector3 OnMouseClicked;  u0001

    public event Action<Vector3> OnMouseClicked;
    public event Action<GameObject> OnEnemyClicked;


    //void Awake()
    //{
    //    if (Instance != null)
    //        Destroy(gameObject);


    //    Instance = this;
    //}                                                   lession 20 beause of already initialized in Singleton

    protected override void Awake()
    {
        base.Awake();
     DontDestroyOnLoad(this);              // lession 20 beause of just a example no meaning  fixed lession 33 use in change scene
    }

    void Update()
    {
        SetCursorTexture();
        if (interracrIwthUI()) return;
        MouseControl();
        
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (interracrIwthUI())
        {
            Cursor.SetCursor(point, Vector2.zero, CursorMode.Auto);
            return;
        }

        if(Physics.Raycast(ray, out hitInfo))
        {
            //change mouse texture
            switch(hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Portal":
                    Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Item":
                    Cursor.SetCursor(point, new Vector2(16, 16), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                    break;

            }
        }
    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
                OnMouseClicked?.Invoke(hitInfo.point);
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            if (hitInfo.collider.gameObject.CompareTag("Attackable"))
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            if (hitInfo.collider.gameObject.CompareTag("Portal"))
                OnMouseClicked?.Invoke(hitInfo.point);
            if (hitInfo.collider.gameObject.CompareTag("Item"))
                OnMouseClicked?.Invoke(hitInfo.point);

        }
    }

    bool interracrIwthUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else return false;
    }
}
