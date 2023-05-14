using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughFSM;

public class EnemyAttackState : BaseFSM<EnemyFSM>
{
    #region Static
    private static readonly EnemyAttackState instance = new EnemyAttackState();
    public static EnemyAttackState GetInstance
    {
        get
        {
            return instance;
        }
    }
    #endregion
    public override void EnterState(EnemyFSM state)
    {
        state.MovementStop();
        state.PlayAnimation(2);
    }

    public override void UpdateState(EnemyFSM state)
    {
        //만약 Enemy 공격 범위에 적이 있다면
        //아니라면 -> state.ChangeState(EnemyMoveState.GetInstance);

        if (state.IsAttackRange)
        {
            state.AttackEnemy();
        }
    }

    public override void ExitState(EnemyFSM state)
    {
        state.PlayAnimation(0);
    }
}
