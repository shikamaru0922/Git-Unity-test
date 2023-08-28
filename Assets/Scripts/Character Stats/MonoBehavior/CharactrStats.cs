using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class CharactrStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;

    public CharacterData_SO templateData;
    public CharacterData_SO characterData;
    

    public AttackData_SO attackData;
    private AttackData_SO baseAttackData;
    private RuntimeAnimatorController baseAnimator;

    public Animator anim;

    [Header("Weapon")]
    public Transform weaponSolt;

    [HideInInspector]
    public bool isCritical;
    public bool isCount;

    //public GameObject UIRoot;
    //public GameObject failed;


    /*private PostProcessVolume volume;
    private Vignette vignette;*/
       
    


    //Userlogin userlogin;


    private void Awake()
    {
        if(templateData != null)
        {
            characterData = Instantiate(templateData);
        }
        //baseAttackData = Instantiate(attackData);
        baseAnimator = GetComponent<Animator>().runtimeAnimatorController;
        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        /* vignette = ScriptableObject.CreateInstance<Vignette>();
         vignette.enabled.Override(true);
         vignette.intensity.Override(1.0f);

         volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 1, vignette);*/

        
    }



    private void Update()
    {
        if (CurrentHealth == 0) 
        {
            anim.SetTrigger("isDead");

            if (GetComponent<CharacterMove>() != null) 
            {
                GetComponent<CharacterMove>().enabled = false;
            }
            



        }

        //vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);

    }


    #region read from Data_SO
    public int MaxHealth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHealth;
            else return 0;
        }
        set
        {
            characterData.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
                return characterData.currentHealth;
            else return 0;
        }
        set
        {
           
            characterData.currentHealth = value;
            if (characterData.currentHealth <= 0   ) 
            {
                if(gameObject.tag != "Player")
                GameManager.Instance.EnemyCount++;

                if (gameObject.tag == "Player") 
                {
                    this.GetComponent<Collider>().enabled = false;
                    //显示失败UI
                    UIRootDontDestroy.uiRootDontDestroy.SetFailedUI();
                }
                
            }
           
        }
    }

    public int BaseDefence
    {
        get
        {
            if (characterData != null)
                return characterData.baseDefence;
            else return 0;
        }
        set
        {
            characterData.baseDefence = value;
        }
    }

    public int CurrentDefence
    {
        get
        {
            if (characterData != null)
                return characterData.curretDefence;
            else return 0;
        }
        set
        {
            characterData.curretDefence = value;
        }
    }
    #endregion

    #region Character Combat
    public void TakeDamage(CharactrStats attacker,CharactrStats defener)
    {
        int damage = Mathf.Max((attacker.CurrentDamage() - defener.CurrentDefence),0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage,0);

        if (attacker.isCritical)
        {
            defener.GetComponent<Animator>().SetTrigger("Hit");
        }

        //Update UI   fixed lession 28  9min
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //experienment update   fixed lession 29  14min
        if(CurrentHealth <= 0)
        {
            attacker.characterData.UpdateExp(characterData.killPoint);
        }
    }

    public void TakeDamage(int damage, CharactrStats defener)
    {
        int currentDamage = Mathf.Max(damage - defener.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - currentDamage, 0);
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);

        //if(CurrentHealth <= 0)
        //GameManager.Instance.playerStats.characterData.UpdateExp(characterData.killPoint);
    }


    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamge, attackData.maxDamge);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log(coreDamage);
        }
        return (int)coreDamage;
    }
    #endregion

    #region Equip Weapon

    public void ChangeWeapon(ItemData_SO weapon)
    {
        UnEquipWeapon();
        EquipWeapon(weapon);
    }

    public void EquipWeapon (ItemData_SO weapon)
    {
        if (weapon.weaponPrefab != null)
            Instantiate(weapon.weaponPrefab, weaponSolt);

        //TODO:update arttribute
        //:TODO: change animation
        attackData.ApplyWeaponData(weapon.weaponData);
        GetComponent<Animator>().runtimeAnimatorController = weapon.weaponAnimator;
    }

    public void UnEquipWeapon()
    {
        if(weaponSolt.transform.childCount != 0)
        {
            for (int i = 0; i < weaponSolt.transform.childCount; i++)
            {
                Destroy(weaponSolt.transform.GetChild(i).gameObject);
            } 
        }
        attackData.ApplyWeaponData(baseAttackData);
        //TODO:chage animation
        GetComponent<Animator>().runtimeAnimatorController = baseAnimator;
    }
    #endregion

    /// <summary>
    /// 玩家加血方法
    /// </summary>
    /// <param name="amount"></param>
    #region Apply Data Change
    public void ApplyHealth(int amount)
    {
        if (CurrentHealth + amount <= MaxHealth)
            CurrentHealth += amount;
        else
            CurrentHealth = MaxHealth;
    }

    public void GetHit(int amount)
    {
        if (CurrentHealth - amount >= 0) 
        {
            CurrentHealth -= amount;
            /*GameManager.Instance.isDamaging = true;
            Debug.Log(GameManager.Instance.isDamaging);*/
        }
            
        else
            CurrentHealth = 0;
    }
    #endregion


}