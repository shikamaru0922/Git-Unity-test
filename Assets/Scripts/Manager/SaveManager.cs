using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    string sceneName = "";
    private CharacterData_SO templateData;
    //Mysqlaccess mysql;
    //public Userlogin userlogin;





    public string SceneName { get { return PlayerPrefs.GetString(sceneName); } }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
      /* 
        mysql = new Mysqlaccess(GameManager.Instance.userlogin.host, GameManager.Instance.userlogin.port,
            GameManager.Instance.userlogin.userName,
            GameManager.Instance.userlogin.password,
            GameManager.Instance.userlogin.databaseName,
            GameManager.Instance.userlogin.charSet);*/
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneController.Instance.TransitionToMain();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SavePlayerData();
            //QuestManager.Instance.SaveQuestManager();
           
           
           



            //GameManager.Instance.playerStats.attackData.minDamge = 90;
            //GameManager.Instance.playerStats.attackData.maxDamge = 990;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayerData();

            //QuestManager.Instance.LoadQuestManager();
        }

        


    }

    public void SavePlayerData()
    {
        Save(GameManager.Instance.playerStats.characterData, GameManager.Instance.playerStats.characterData.name);

        //Debug.Log(mysql.Update("usertable", new string[] { "level" }, new string[] { "123,123" },new string[] { "`" + "account" + "`", "`" + "password" + "`" }, new string[] { "=", "=" }, new string[] { GameManager.Instance.userName, GameManager.Instance.userPassword }));
    }

    public void LoadPlayerData()
    {
        Load(GameManager.Instance.playerStats.characterData, GameManager.Instance.playerStats.characterData.name);
        Debug.Log(123);
        //SelectfromDatabase();
        //OnClickedLoginButton();
        //OnClickedLoadDatatest();
        



    }


    public void Save(Object data,string key)
    {
        var jsonData = JsonUtility.ToJson(data,true);
        PlayerPrefs.SetString(key, jsonData);
        Debug.Log(jsonData);
        
        PlayerPrefs.SetString(sceneName, SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        //UpdatetoDatabase();


    }

    public  void Load(Object data, string key)
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        
    }

    public void Save_mysql()
    {
        //mysql.OpenSql();
        
        
        //GameManager.Instance.playerStats.characterData.maxHealth
    }

    public void UpdatetoDatabase()
    {
        //mysql.OpenSql();
        //string loginMsg = "";
       // DataSet ds = mysql.Update("usertable", new string[] { "`" + "account" + "`", "`" + "password" + "`" }, new string[] { "=", "=" }, new string[] { GameManager.Instance.userName, GameManager.Instance.userPassword });
       /* if (ds != null)
        {
            Debug.Log("success!!!");
            
        }*/
       // mysql.CloseSql();

    }

    public void OnClickedLoadData()
    {
        //mysql.OpenSql();
        //string loginMsg = "";
        //DataSet ds = mysql.Update_1();
        /*if (ds != null)
        {
            //DataTable table = ds.Tables[0];
            if (table.Rows.Count > 0)
            {
                Debug.Log("currenthp：" + table.Rows[0][0]);
             
            }
            else
            {
                //loginMsg = "用户名或密码错误！";
         
            }
            
        }*/
       // mysql.CloseSql();

    }


    public void SelectfromDatabase()
    {
        //mysql.OpenSql();
        //tring loginMsg = "";
        //DataSet ds = mysql.Select("usertable", new string[] { " maxHealth " +","+ " currentHealth "+ "," + " baseDefence " + "," + " currentDefence " + "," + " currentLevel "  }, new string[] { "`" + "account" + "`", "`" + "password" + "`" }, new string[] { "=", "=" },  new string[] { GameManager.Instance.userName, GameManager.Instance.userPassword });
        /*if (ds != null)
        {
            DataTable table = ds.Tables[0];
            if (table.Rows.Count > 0)
            {
                Debug.Log("currenthp：" + table.Rows[0][0]);
                Debug.Log(table.Rows[0][1]);
                Debug.Log(table.Rows[0][2]);
                Debug.Log(table.Rows[0][3]);
                Debug.Log(table.Rows[0][4]);
                GameManager.Instance.playerStats.characterData.maxHealth = (int)table.Rows[0][0];
                GameManager.Instance.playerStats.characterData.currentHealth = (int)table.Rows[0][1];
                GameManager.Instance.playerStats.characterData.baseDefence = (int)table.Rows[0][2];
                GameManager.Instance.playerStats.characterData.curretDefence = (int)table.Rows[0][3];
                GameManager.Instance.playerStats.characterData.currentLevel = (int)table.Rows[0][4];

            }
            else
            {
                loginMsg = "用户名或密码错误！";

            }

        }*/
        //mysql.CloseSql();

    }


}
