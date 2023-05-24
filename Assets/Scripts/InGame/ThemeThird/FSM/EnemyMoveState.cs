using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HughFSM;

public class EnemyMoveState : BaseFSM<EnemyFSM>
{
    #region Static
    private static readonly EnemyMoveState instnace = new EnemyMoveState();
    public static EnemyMoveState GetInstance
    {
        get
        {
            return instnace;
        }
    }
    #endregion

    private float curTime = 0.0f;
    private float secTime = 0.0f;

    public override void EnterState(EnemyFSM state)
    {
        state.PlayAnimation(1);

        curTime = 9.0f;
        secTime = 0.0f;
    }

    public override void UpdateState(EnemyFSM state)
    {
        if (!ThemeThirdPresenter.GetInstance.IsCallEnemyAnimation)
        {
            if (curTime > 0.0f)
            {
                curTime -= Time.deltaTime;
                secTime = Mathf.FloorToInt(curTime % 60);
            }
            else
            {
                if (curTime != 0.0f)
                {
                    curTime = 0.0f;
                    secTime = Mathf.FloorToInt(curTime % 60);
                }
            }

            if (secTime == 0.0f || state.IsMoveDone)
            {
                state.ChangeState(EnemyIdleState.GetInstance);
            }
            else
            {
                state.MovementStart();
            }
        }
    }

    public override void ExitState(EnemyFSM state)
    {
        Debug.Log("EnemyMove 나감");
        state.MovementStop();
    }

}
