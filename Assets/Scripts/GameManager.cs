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
    public static GameObject mainCamera;

    private bool noPause=true;

    private int totalGunDamage;
    private int totalSwordDamage;
    private GameObject boss; //discuss with Jack how hitboxes work

    private float timer;

    public GameObject teleporterPrefab;
        
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
        mainCamera = GameObject.FindWithTag("MainCamera");
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
        SceneSelection.Instance.DeathScene();
        //UIManager.Instance.GameOverScreen();
        BossManager bossHealth= boss.GetComponent<BossManager>();
        bossHealth.getCurrentBossHealth();
    }

    public void onBossDeath()
    {
        //will need to find where I call this
        GameObject teleporter = Instantiate(teleporterPrefab,new Vector3(0,1,0),Quaternion.identity); 
    }

    public void onPlayerHit(string attack)
    {
        //same here
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
