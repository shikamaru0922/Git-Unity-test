using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMachine : MonoBehaviour
{
    /// <summary>
    /// 基本属性
    /// </summary>
    //法术抽取过程中的抽取数
    [SerializeField]
    private int nowPoint;
    //初始抽取数
    [SerializeField]
    private int magicPoint;
    //
    private float powerNum;
    public float maxPower;
    public float revivePower;
    bool allowShoot_power;
    private float charge_time;
    private float recharge_time;
    bool allowShoot;
   
    private float firecount;//发射间隔计数
    public int NowPoint { get => nowPoint; set => nowPoint = value; }
    public int MagicPoint { get => magicPoint; set => magicPoint = value; }
    //public List<ShootObject> shootObject_init;
    //枪口
    public Transform firePoint;
    //使用中的法术队列
    private List<ShootObject> shootObjects_UsingList;
    //法典列表
    public List<ShootObject> shootObjects_List;
    //法术组下标存储
    private Queue<int> objectPacket_Queue;
    //法术组发射节点列表
    List<List<ShootGroup>> shootGroupsList;

    //法术抽取当前法术指针
    private int usedIndex;
    //节点列表的索引号
    int groupIndex;
    ShootGroup _shootGroupTemp;

    private static ShootMachine instance;
    public static ShootMachine Instance
    {
        get
        {
            if (instance == null)
                instance = new ShootMachine();
            return instance;
        }
    }


    /// <summary>
    /// 法术发射
    /// </summary>
    
    
    int i, j;
    
    //角度获取
    float rotate;//发射总角度
    float begin;//角度起点

    private ShootMachine()
    {
    }

    private void Awake()
    {
        allowShoot = true;
        instance = this;
        groupIndex = 0;
        firecount = 1;
        NowPoint = 1;
        magicPoint = 1;
        usedIndex = 0;
        objectPacket_Queue = new Queue<int>();
        shootObjects_UsingList = new List<ShootObject>();
        allowShoot_power = true;
        firecount = 0f;
        powerNum = maxPower;
        shootGroupsList = new List<List<ShootGroup>>();


    }
    private void Start()
    {
        
        GetShootGroup();
        

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            allowShoot = !allowShoot;
        }
        if (Input.GetMouseButton(0))
        {
            if (firecount <= 0 && shootObjects_List.Count > 0 && allowShoot && allowShoot_power)
            {
                _shootGroupTemp = shootGroupsList[groupIndex = groupIndex % shootGroupsList.Count][0];
                //Debug.Log(groupIndex % shootGroupsList.Count);
                ShootGroups(_shootGroupTemp);
                Debug.Log(_shootGroupTemp.fireGap);

                firecount = _shootGroupTemp.fireGap;
                groupIndex++;
                if ((powerNum -= _shootGroupTemp.energy) <= 0)
                {
                    allowShoot_power = false;
                }

            }
        }
       
        SetPower();
        firecount -= Time.deltaTime;
    }


    void SetPower()
    {
        

        if (powerNum < maxPower)
        {
            powerNum += revivePower * Time.deltaTime;
        }
        if (powerNum >= maxPower)
        {
            powerNum = maxPower;
            allowShoot_power = true;
        }
        UIRootDontDestroy.uiRootDontDestroy.slider_mp.value = powerNum / maxPower;
    }
    /// <summary>
    /// 获取发射列表
    /// </summary>
    void GetShootGroup()
    {
        int listIndex = 0;
        usedIndex = 0;
        while(usedIndex < shootObjects_List.Count){
            Debug.Log("draw");
            DrawCards();
            Debug.Log("Using_Count:" + shootObjects_UsingList.Count+":"+listIndex+":"+usedIndex);
            PacketObjList(listIndex);
            listIndex++;

        }
        
        
    }
    public void SetShootGroup(List<ShootObject> shootObjects)
    {
      shootObjects_List=shootObjects;
        groupIndex = 0;

        shootGroupsList.Clear();
        
        GetShootGroup();
        

    }
    
    #region 对法术的解析
    #region
    /// <summary>
    /// 法术抽取
    /// </summary>
    /// shootObjects_UsingList

    public void DrawCards()
    {
        int index_Last = usedIndex;
        int index_cemetery = 0;
        shootObjects_UsingList.Clear();
        objectPacket_Queue.Clear();
        while (NowPoint > 0)
        {
            if (usedIndex < shootObjects_List.Count)
            {
                shootObjects_UsingList.Add(shootObjects_List[usedIndex]);
                NowPoint = shootObjects_List[usedIndex].SpendPoint(NowPoint);
                usedIndex++;
            }
            else if (index_cemetery < index_Last)
            {
                shootObjects_UsingList.Add(shootObjects_List[index_cemetery]);
                NowPoint = shootObjects_List[index_cemetery].SpendPoint(NowPoint);
                index_cemetery++;
            }
            else
            {
                break;
            }
        }
        NowPoint = MagicPoint;
        //if (usedIndex >= shootObjects_List.Count)
        //{
        //    usedIndex = 0;

        //}

    }
    #endregion
 
    void PacketObjList(int listIndex)
    {
        if (shootObjects_UsingList.Count == 0)
            return;

        float shootGap=0;

       List<ShootGroup> treeNodes=new List<ShootGroup>();
        
        NodePaket(0, shootObjects_UsingList,shootObjects_UsingList.Count,treeNodes,listIndex);
        SetSpend(treeNodes);
        shootGroupsList.Add(treeNodes);

    }
    void SetSpend(List<ShootGroup> _group)
    {
        _group[0].fireGap = 0.15f;
        foreach (ShootObject i in shootObjects_UsingList)
        {
            _group[0].energy += i.energy;
            _group[0].fireGap += i.fireGap;
        }
    }
    
    int NodePaket(int startIndex,List<ShootObject> shootObjects,int recover_Parent,List<ShootGroup> TreeNodes,int listIndex)
    {
        
        int r = recover_Parent;
        ShootGroup group = new ShootGroup();
        group.listIndex = listIndex;
        
            TreeNodes.Add(group);
        do
        {
            if (startIndex >= shootObjects_UsingList.Count)
                break;
            switch (shootObjects[startIndex].objtype)
            {
                case ShootObject.Objtype.magic:
                    group.Magic_List.Add(shootObjects[startIndex]);                  
                    break;
                case ShootObject.Objtype.bullet:
                    group.Bullets_List.Add(shootObjects[startIndex]);
                    break;
                case ShootObject.Objtype.trigger:
                    group.triggerChildrenDic.Add(group.Bullets_List.Count,TreeNodes.Count);
                    group.Bullets_List.Add(shootObjects[startIndex]);
                    startIndex= NodePaket(startIndex+1,shootObjects, shootObjects[startIndex].recover,TreeNodes,listIndex)-1;
                    break;
                case ShootObject.Objtype.modified:
                    group.Modified_List.Add(shootObjects[startIndex]);
                    break;
            }
            r = r - shootObjects[startIndex].spend + shootObjects[startIndex].recover;
            startIndex++;
        } while (r > 0);
        TreeNodes.Add(group);
        return startIndex;
    }
    #endregion


    //发射角度获取
    float BulletRotate (List<ShootObject> _magicList)
    {
        rotate = 0;
        foreach(ShootObject o in _magicList)
        {
            rotate += o.Parse_Rotate();
        }
        if (rotate > 360)
        {
            rotate = 360;
        }
        return rotate;
    }
    //发射生成好的法术组
     void ShootGroups(ShootGroup shootGroup)
    {
        shoot(BulletRotate(shootGroup.Magic_List), shootGroup.Bullets_List.Count, shootGroup.Bullets_List, shootGroup.Modified_List,firePoint,shootGroup.triggerChildrenDic,shootGroup.listIndex);
    }
    public void ShootGroups(int listIndex,int index,Transform trigger)
    {
        ShootGroups(shootGroupsList[listIndex][index],trigger);
    }
    void ShootGroups(ShootGroup shootGroup, Transform trigger)
    {
        shoot(BulletRotate(shootGroup.Magic_List), shootGroup.Bullets_List.Count, shootGroup.Bullets_List, shootGroup.Modified_List, trigger, shootGroup.triggerChildrenDic, shootGroup.listIndex);
    }
    //法术发射

    public void shoot(float _rotate, int _bulletNum, List<ShootObject> _bulletList, List<ShootObject> _modifileList,Transform holeTransform,Dictionary<int,int> IndexDic,int listindex)
    {
        //Debug.Log("bulletCount"+_bulletList.Count);
        bool triggerflag = false;


        //if (IndexDic.Count > 0)
        //    triggerflag = !triggerflag;


        Vector3 firdir;
        Vector3 firdir_temp;
        Bullet bullettemp;
        Quaternion offset = Quaternion.AngleAxis(_rotate / (_bulletNum - 1), Vector3.up);//获取子弹的单位间隔

        if (_rotate == 360)
        {
            
            offset= Quaternion.AngleAxis(_rotate / _bulletNum , Vector3.up);//获取子弹的单位间隔
            
        }
            
        Quaternion sanshe_offset;
        begin = -_rotate / 2;
        #region
        //for (i = 0; i <=num / maxBulletnum_OnceFire ; i++)
        //{
        //    firdir = Quaternion.AngleAxis(begin, Vector3.up) * transform.forward;
        //    for (j = 1; j <= maxBulletnum_OnceFire; j++)
        //    {

        //        GameObject tmp = Instantiate(bullet, transform.position, transform.rotation);

        //        Quaternion enddir = Quaternion.LookRotation(firdir);
        //        tmp.transform.rotation = enddir;
        //        firdir = offset * firdir;



        //    }
        //}
        #endregion
        firdir = Quaternion.AngleAxis(begin, Vector3.up) * holeTransform.forward;
        firdir_temp = firdir;
        for (i = 0; i < _bulletNum; i++)
        {
            Debug.Log("1");
            GameObject tmp=null;
            bullettemp = _bulletList[i] as Bullet;
            sanshe_offset = Quaternion.AngleAxis(Random.Range(- bullettemp.euler,bullettemp.euler), Vector3.up);//散射角
            firdir = sanshe_offset * firdir_temp;
            if (bullettemp.objtype==ShootObject.Objtype.trigger)
            {
                int index;
                
                if (IndexDic.TryGetValue(i,out index))
                {
                    Debug.Log("2");
                    //tmp = Instantiate(_bulletList[i].Parse_BulletPrefab(), holeTransform.position, Quaternion.identity);
                    tmp = PoolManager.Instance.GetPoolGameObject(bullettemp.b_name);
                    
                    tmp.transform.position = holeTransform.position+bullettemp.offset;
                    tmp.GetComponent<BulletData>().tgChildrenIndex = index;//有触发时index必大于0
                    tmp.GetComponent<BulletData>().listIndex = listindex;
                    
                    
                    Add_Modfied(_modifileList, tmp);
                    Quaternion enddir = Quaternion.LookRotation(firdir);
                    tmp.transform.rotation = enddir;
                    tmp.SetActive(true);
                    firdir_temp = offset * firdir_temp;
                }

                
            }
            else
            {
                
                tmp = PoolManager.Instance.GetPoolGameObject(bullettemp.b_name);
                tmp.transform.position = holeTransform.position + bullettemp.offset;
                
                
                Add_Modfied(_modifileList, tmp);



                Quaternion enddir = Quaternion.LookRotation(firdir);
                tmp.transform.rotation = enddir;
                tmp.SetActive(true);
                firdir_temp = offset * firdir_temp;
            }

           
        }

    }

    void Add_Modfied(List<ShootObject> _modifileList,GameObject temp)
    {
        Modfied modfied;
        for(int i = 0; i < _modifileList.Count; i++)
        {
            modfied = _modifileList[i] as Modfied;
            modfied.AddModified(temp);
        }
    }

}
