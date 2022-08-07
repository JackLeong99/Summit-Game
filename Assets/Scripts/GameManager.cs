using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No GameManager in the scene");
            }
            return instance;
        }
    }

    public static GameObject player;

    private bool noPause=true;

    private int totalGunDamage;
    private int totalSwordDamage;
    private GameObject boss; //discuss with Jack how hitboxes work

    private float timer;
        
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Time.timeScale = 1;
        player=GameObject.FindWithTag("Player");
        boss=GameObject.FindWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetButtonDown("Escape"))
    {
        PauseGame();
    }
    timer+=Time.deltaTime;
    }

    public void onDeath() 
    {
        AkSoundEngine.PostEvent("Player_Death", player);
        UIManager.Instance.GameOverScreen();
        BossManager bossHealth= boss.GetComponent<BossManager>();
        bossHealth.getCurrentBossHealth();
            
         Analytics.CustomEvent("Player Death", new Dictionary<string, object>
        {
            {"Player Death time: ", timer},
            {"Total dealt damage: ", bossHealth.getCurrentBossHealth()}
        });
    }

    public void onBossDeath()
    {
        PlayerStats health = player.GetComponent<PlayerStats>();
         Analytics.CustomEvent("Boss Death Time", new Dictionary<string, object>
        {
            {"Boss death time: ", timer},
            {"Player's health: ",health.GetPlayerHealth()}
        });
        Analytics.CustomEvent("Player types of damage", new Dictionary<string, object>
        {
            {"Total damage by sword: ", totalSwordDamage},
            {"Total damage by gun: ",totalGunDamage}
        });
    }

    public void onPlayerHit(string attack)
    {
         /*Analytics.CustomEvent("Player hit time", new Dictionary<string, object>
        {
            {"Time of enemy hit: ", timer},
            {"Name of attack: ", attack}
        });*/
    }

    public void SwordDamge(int sDamage)
    {
        totalSwordDamage+=sDamage;
    }
    public void GunDamge(int gDamage)
    {
        totalGunDamage+=gDamage;
    }

    public void PauseGame ()
    {
        if(noPause)
        {
        Time.timeScale = 0;
         UIManager.Instance.PauseMenu();
         Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DisallowPause()
    {
        noPause=false;
    }
}
