using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(menuName = "Boss/Ability/Golem/IdleState")]
public class GolemIdleState : BaseState
{
    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();

        switch (boss.Alive())
        {
            case true:
                switch (RocksLeft())
                {
                    case false:
                        boss.ChangeState(boss.GetState<ReplenishState>());
                        return;
                }

                bool meleeDist = Vector3.Distance(boss.transform.position, GameManager.instance.player.transform.position) <= boss.attributes.minPlayerDist;
                bool rangedDist = Vector3.Distance(boss.transform.position, GameManager.instance.player.transform.position) >= boss.attributes.maxPlayerDist;

                switch (meleeDist)
                {
                    case bool x when meleeDist:
                        boss.ChangeState(boss.GetState<MeleeState>());
                        break;
                    case bool y when rangedDist:
                        boss.ChangeState(boss.GetState<RangedState>());
                        break;
                    default:
                        boss.ChangeState(boss.GetState<RangedState>());
                        break;
                }
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool RocksLeft()
    {
        return GameObject.FindGameObjectsWithTag("rocks").Length > 0;
    }
}
