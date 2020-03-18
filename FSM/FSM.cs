using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象状态机
/// </summary>


public class FSM
{
    private Dictionary<string,IState> StateDict; //存储状态的字典
    public IState CurState = null;
    public IState DefaultState = null;
    public FSM() {
        StateDict = new Dictionary<string, IState>();
        CurState = null;
    }

    //当前状态的持续运行
    public void Update() {
        //Debug.Log("FSM UPDATE BEGIN");
        CurState.OnUpdate();
        //if(CurState.IfExit()) CurState = DefaultState;
    }

    //添加新的状态
    public void AddState(IState cState) {
        IState tState = null;

        if(!StateDict.TryGetValue(cState.GetStateName(),out tState)) {  //cState状态不存在
            StateDict[cState.GetStateName()] = cState;
        }
        else {                                             //cState状态存在
            Debug.Log(cState.GetStateName() + " 添加状态已经存在！");
        }
        //Debug.Log("FSM add " + cState.GetStateName() + " ok ");
        //ChangeState(cState.GetStateName());
    }

    //移出状态cState
    public void RemoveState(IState cState) {
        IState tState = null;

        if(!StateDict.TryGetValue(cState.GetStateName(),out tState)) {
            StateDict.Remove(cState.GetStateName());
        }
        else {
            //Debug.Log(cState.GetStateName() + "删除状态不存在！");
        }
    }

    //改变当前状态为cState
    public bool ChangeState(string cState) {
        IState tState = GetState(cState);
        if(tState==null)
            return false;
        if(CurState==null) {
            CurState = tState;
            CurState.OnEnter();
            return true;
        }
        if(tState.GetStateName()==CurState.GetStateName()) {
                 if(tState.GetStateName()=="Idle"      ) return true;
            else if(tState.GetStateName()=="Run"       ) return true;
            else if(tState.GetStateName()=="BeAttacked") return true;
            else if(tState.GetStateName()=="Death     ") return true;
            else return false;
        }
        if(!IfCanChang(CurState,StateDict[cState]))
            return false;

        CurState.OnExit();
        CurState = tState;
        CurState.OnEnter();
        return true;
    }

    public void RemoveAllState() {

    }
    IState GetState(string cState) {
        IState tState = null;
        if(!StateDict.TryGetValue(cState,out tState)) return null;
        return StateDict[cState];
    }

    public virtual bool IfCanChang(IState StateNameA, IState StateNameB) {
        //当前默认不能转化为被攻击态
        //if(StateNameB.GetStateName()=="BeAttacked")
        //    return false;
        //奔跑态和站立态可以转化为任意态//
        if(StateNameA.GetStateName()=="Run"||StateNameA.GetStateName()=="Idle")
            return true;
        //死亡态不能转化为其他态
        if(StateNameA.GetStateName()=="Death")
            return false;
        //必定能够转化为死亡态
        if(StateNameB.GetStateName()=="Death")
            return true;
        
        //闪避不会被打断
        if(StateNameA.GetStateName()=="Dodge"&&!StateNameA.IfCanExitTo())
            return false;

        //前摇会被攻击打断
        if(!StateNameA.IfRollEnd() && StateNameB.GetStateName()=="BeAttacked")
            return true;
        
        //后摇可以用移动取消
        if(StateNameA.IfShakeBegin() && (StateNameB.GetStateName()=="Run" || StateNameB.GetStateName()=="Dodge"))
            return true;
        
        if(StateNameA.IfCanExitTo())
            return true;
        else 
            return false;
    }
}

public class IState {
    SkillData skillData;
    float StateBeginTime;
    string name = "";
    public IState(SkillData newSkillData=null, string newname="") {
        name = newname;
        skillData = newSkillData;
        StateBeginTime = -1000;
    }
    public void OnEnter() {
        //播放动画
        //gameObject.GetComponent<Animator>().CrossFade(skillData.SkillAnimationName,0.1f);
        View.EventHandle += delegate() {
            View.obmp[name].GetComponent<Animator>().CrossFade(skillData.SkillAnimationName,0.1f);
        };
        StateBeginTime = Logic.time;
    }
    public void OnUpdate() {

    }
    public void OnExit() {
    }
    public bool IfCanExitTo() {
        if(Logic.time-StateBeginTime>skillData.SkillDuringTime)
            return true;
        else
            return false;
    }
    public bool IfRollEnd() {
        if(Logic.time-StateBeginTime<skillData.SkillRollEndTime)
            return false;
        else 
            return true;
    }
    public bool IfShakeBegin() {
        if(Logic.time-StateBeginTime>skillData.ShakeBackBeginTime)
            return true;
        else 
            return false;
    }
    public string GetStateName() {
        return skillData.SkillStateName;
    }
}

