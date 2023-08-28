using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    static AudioManagement current;
    AudioSource siFi_ambientScource;
    AudioSource playerSource;

    [Header("“Ù–ßÃÌº”")]
    public AudioClip ambientClip;
    public AudioClip playerMoveClip;
    public AudioClip playerShootClip;
    public AudioClip playerHarmClip;
    public AudioClip playerDeathClip;
    public AudioClip playerLevelIpClip;
    public AudioClip monsterMoveClip;
    public AudioClip monsterHarmClip;
    public AudioClip monsterDeathClip;
    public AudioClip bossMoveClip;
    public AudioClip bossHarmClip;
    public AudioClip bossDeathClip;
    public AudioClip bossAttackClip;
    public AudioClip buffSpwanClip;
    public AudioClip buffGainClip;



    // Start is called before the first frame update
    void Start()
    {
        siFi_ambientScource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void StartLevelAudio()
    {
        current.siFi_ambientScource.clip = ambientClip;
        current.siFi_ambientScource.loop = true;
        current.siFi_ambientScource.Play();
    }

    public static void PlayerMovingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.playerMoveClip;
        current.playerSource.Play();
    }

    public static void PlayerShootingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.playerShootClip;
        current.playerSource.Play();
    }

    public static void PlayerHarmAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.playerHarmClip;
        current.playerSource.Play();
    }

    public static void PlayerDeathAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.playerDeathClip;
        current.playerSource.Play();
    }

    public static void PlayerLevelUpAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.playerLevelIpClip;
        current.playerSource.Play();
    }

    public static void MonsterMovingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.monsterMoveClip;
        current.playerSource.Play();
    }

    public static void MonsterHarmingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.monsterHarmClip;
        current.playerSource.Play();
    }

    public static void MonsterDeathAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.monsterDeathClip;
        current.playerSource.Play();
    }

    public static void BossMovingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.bossMoveClip;
        current.playerSource.Play();
    }

    public static void BossHarmingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.bossHarmClip;
        current.playerSource.Play();
    }

    public static void BossDeathAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.bossDeathClip;
        current.playerSource.Play();
    }

    public static void BossAttackAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.bossAttackClip;
        current.playerSource.Play();
    }

    public static void BuffSpwaningAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.buffSpwanClip;
        current.playerSource.Play();
    }

    public static void BuffGainingAudio() 
    {
        if (current == null || current.playerSource.isPlaying)
        {
            return;
        }
        current.playerSource.clip = current.buffGainClip;
        current.playerSource.Play();
    }
}
