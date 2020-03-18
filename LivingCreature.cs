using System.Runtime.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 生物体
/// </summary>
public class LivingCreature : ObjectInf {

    /// <summary>
    /// 当前创建的生命体的唯一标识
    /// </summary>
    //public int ID;

    /// <summary>
    /// 当前生物体的信息
    /// </summary>
    public LivingCreatureData  curLivingCreatureData = new LivingCreatureData();

    /// <summary>
    /// 控制当前生物体状态的状态机
    /// </summary>
    public FSM curFSM;

    /*
    /// <summary>
    /// 所有的技能配置表
    /// </summary>
    public SkillConfig allSkillConfig;
    */

//
    /// <summary>
    /// 当前生物可使用的技能数据表
    /// idex0  :站立
    /// idex1  :奔跑
    /// idex2  :受击
    /// idex3  :死亡
    /// idex4  :普通攻击
    /// idex5  :闪避技能
    /// idex6  :小技能
    /// idex7  :远程技能
    /// idex8  :buff技能
    /// idex...:可扩展
    /// </summary>
    public List<SkillData> curSkillDataList = new List<SkillData>();  

    /// <summary>
    /// 当前生物使用的技能表中的技能上一次施法时间
    /// </summary>
    public List<float> SkillLastFireTime = new List<float>();

    /// <summary>
    /// 当前正在使用的技能在可使用技能表中的下标
    /// </summary>
    public int curSID = 0;

    /// <summary>
    /// 当前正在使用的技能达到的阶段
    /// </summary>
    //public ValueTuple<int,int,int> curSkillStage;

    /// <summary>
    /// 当前生物具有的BUFF,BUFF装载时间，BUFF执行阶段
    /// </summary>
    //public Queue<ValueTuple<SkillBUFFType,float,int>> BUFFQueue;

    /// <summary>
    /// 头顶血条UI管理器
    /// </summary>
    //public HeadHPUIManager headHPUIManager;

    /// <summary>
    /// 记录当前帧是否有使用的命令
    /// </summary>
    public bool IfFramCmd = false;


    
    //..
    public void InitLivinngCreature(int LivingCreatureID = 0) {

        //---------更改名字
        //gameObject.name = PlayerName;
        //---------加载全局技能表-----------------------------------------------------------------------------------
        //SkillConfig allSkillConfig = Resources.Load("SkillConfig") as SkillConfig;
        //---------加载全局生物信息--------------------------------------------------------------------------------
        //LivingCreatureConfig allLivingCreature = Resources.Load("LivingCreatureConfig") as LivingCreatureConfig;
        //---------初始化当前生物为ID==LivingCreatureID的生物信息-------------------------------------------------------------
        curLivingCreatureData.ID = 0;
        curLivingCreatureData.Name = "Alex";
        curLivingCreatureData.curHP = 100;
        curLivingCreatureData.maxHP = 100;
        curLivingCreatureData.Speed = 2;
        curLivingCreatureData.EnableSkillIDList.Add(0);
        curLivingCreatureData.EnableSkillIDList.Add(1);
        //---------初始化当前生物使用的技能表------------------------------------------------------------------------------
        //curSkillDataList = new List<SkillData>();
        //foreach(var e in curLivingCreatureData.EnableSkillIDList) 
        //    curSkillDataList.Add(allSkillConfig.SkillDataList[e]);
        //---------初始化当前生物使用技能的时间戳--------------------------------------------------------------------------
        //SkillLastFireTime = new List<float>();
        //foreach(var e in curSkillDataList)
        //    SkillLastFireTime.Add(-10000);
        //---------初始化当前生物正在使用的技能为Idle idex=0---------------------------------------------------------------------
        //curSkillIdex = 0;
        //---------初始化当前生物正在使用的技能已经到达的阶段------------------------------------------------------------------
        //curSkillStage = (0,0,0);
        // ---------初始化当前生物的状态机--------------------------------------------------------------------------------------
        //curFSM = new FSM();
        //foreach(var e in curSkillDataList) {
            //Debug.Log("ADD FSM STATE " + e.SkillStateName);
            //curFSM.AddState(new IState(gameObject,e));
        //}
        //curFSM.AddState()
        //curFSM.ChangeState("Idle");
        //---------获取当前生物的唯一标识
        // = IDManager.GetID();
        //---------初始化BUFF队列-----------------------------------------------------------------------------------------------
        //BUFFQueue = new Queue<(SkillBUFFType, float, int)>();
        //---------初始化头顶HPUI-------------------------------------------------------------------------------------------
        //headHPUIManager = new HeadHPUIManager();
        //headHPUIManager.Init(transform.Find("HPUIPoint"));
    }

    public void UpdateCreature() {

        if(IfFramCmd) {

        }
        else {

        }


        //---------死亡更新-----------------------------------------------------------------------------------------------
        /*if(curLivingCreatureData.curHP<=0)
            ToUseSkill(3);
        if(curLivingCreatureData.curHP<=0)
            return ;

        //---------BUFF效果更新-----------------------------------------------------------------------------------------
        int cnt = BUFFQueue.Count;
        while(cnt-- > 0) {
            var BUFF = BUFFQueue.Dequeue();
            //BUFF效果时间结束
            if(Logic.time-BUFF.Item2 > BUFF.Item1.BUFFBeginTime+BUFF.Item1.BUFFEachRoleTime*BUFF.Item1.BUFFRoleTimes 
                && BUFF.Item3>=BUFF.Item1.BUFFRoleTimes) {
                //回收移速加成
                if((BUFF.Item1.BUFFRoleAttribute&4)!=0) {
                    curLivingCreatureData.Speed -= BUFF.Item1.BUFFEffectValue;
                }
                //回收防御加成
                if((BUFF.Item1.BUFFRoleAttribute&8)!=0) {
                    curLivingCreatureData.Defence -= BUFF.Item1.BUFFEffectValue;
                }
            }
            //BUFF未结束
            else {
                if(BUFF.Item3<BUFF.Item1.BUFFRoleTimes) {
                    if(Logic.time-BUFF.Item2>=BUFF.Item1.BUFFBeginTime+BUFF.Item1.BUFFEachRoleTime*BUFF.Item3) {
                        if((BUFF.Item1.BUFFRoleAttribute&1)!=0) {
                            curLivingCreatureData.curHP += BUFF.Item1.BUFFEffectValue;
                            if(curLivingCreatureData.curHP>curLivingCreatureData.maxMP)
                                curLivingCreatureData.curHP = curLivingCreatureData.maxHP;
                        }
                        if((BUFF.Item1.BUFFRoleAttribute&2)!=0) {
                            curLivingCreatureData.curMP += BUFF.Item1.BUFFEffectValue;
                        }
                        if(BUFF.Item3==0&&(BUFF.Item1.BUFFRoleAttribute&4)!=0) {
                            curLivingCreatureData.Speed += BUFF.Item1.BUFFEffectValue;
                        }
                        if(BUFF.Item3==0&&(BUFF.Item1.BUFFRoleAttribute&8)!=0) {
                            curLivingCreatureData.Defence += BUFF.Item1.BUFFEffectValue;
                        }
                        if((BUFF.Item1.BUFFRoleAttribute&3)!=0||BUFF.Item3==0)
                            BUFF.Item3++;
                    }
                }
                BUFFQueue.Enqueue(BUFF);
            }
        }
        //---------当前使用技能判定更新-------------------------------------------------------------------------------------
        if(Logic.time-SkillLastFireTime[curSkillIdex]>curSkillDataList[curSkillIdex].SkillDuringTime) {
            ToUseSkill(0);

            //测试用
            //Logic.timeScale = 1;
        }
        else {
            //攻击型技能 伤害判定+位移效果附加
            if((curSkillDataList[curSkillIdex].SkillType&1)!=0) {
                //伤害判定
                if(curSkillStage.Item1<curSkillDataList[curSkillIdex].SkillAttack.SkillAttackTimes 
                    && Logic.time-SkillLastFireTime[curSkillIdex]>=curSkillDataList[curSkillIdex].SkillAttack.SkillAttackJudgeInfList[curSkillStage.Item1].SkillAttackJudgeBeginTime) {
                    // TODO:用curSkillDataList[curSkillIdex].SkillAttack[curSkillStage.Item1]段攻击 进行攻击判定

                    ObjectManager.AttackObjectSolve(transform.position,transform.rotation,
                        curSkillDataList[curSkillIdex].SkillAttack.SkillAttackJudgeInfList[curSkillStage.Item1],
                            curSkillDataList[curSkillIdex].SkillAttack.SkillAttackJudgeInfList[curSkillStage.Item1].SkillAttackJudgeDamageRatio*curLivingCreatureData.Attack,
                                gameObject.tag);
                    curSkillStage.Item1++;
                }
                //位移效果附加
                if(curSkillStage.Item1!=0) {
                    transform.Translate(Vector3.forward*Logic.eachframtime
                        *curSkillDataList[curSkillIdex].SkillAttack.SkillAttackJudgeInfList[curSkillStage.Item1-1].SkillAttackJudgeMoveSpeed);
                }
            }
            if((curSkillDataList[curSkillIdex].SkillType&2)!=0&&curSkillStage.Item2<1) {
                if(Logic.time-SkillLastFireTime[curSkillIdex]>=curSkillDataList[curSkillIdex].SkillBUFF.BUFFBeginTime) {
                    BUFFQueue.Enqueue((curSkillDataList[curSkillIdex].SkillBUFF,Logic.time,0));
                    curSkillStage.Item2++;
                }
            }


            if((curSkillDataList[curSkillIdex].SkillType&4)!=0&&curSkillStage.Item3<1) {
                //Debug.Log("PANDING " + curSkillDataList[curSkillIdex].SkillBullet.BulletSEName);
                if(Logic.time-SkillLastFireTime[curSkillIdex]>=curSkillDataList[curSkillIdex].SkillBullet.BulletFireTime) {
                    // TODO:加载当前技能的子弹，并添加至子弹池
                    GameObject temp = GameObject.Instantiate(Resources.Load(curSkillDataList[curSkillIdex].SkillBullet.BulletSEName),
                        transform.Find("BulletPoint").position,transform.Find("BulletPoint").rotation) as GameObject;
                    if(curSkillDataList[curSkillIdex].SkillBullet.BulletFireEnemyPos) {
                        var npos = ObjectManager.GetNearPos(transform.position,gameObject.tag);
                        if(Vector3.Distance(npos,gameObject.transform.position)<4) {
                            temp.transform.position = npos;
                        }
                    }
                    //Debug.Log("TEXIAO JIAZAI " + curSkillDataList[curSkillIdex].SkillBullet.BulletSEName);
                    temp.GetComponent<BulletController>().SetInf(curSkillDataList[curSkillIdex].SkillBullet,
                        curSkillDataList[curSkillIdex].SkillBullet.SkillAttackJudgeInfSingle.SkillAttackJudgeDamageRatio*curLivingCreatureData.Attack,
                            this.tag);
                    curSkillStage.Item3++;
                }
            }
        }*/
        //
        //---------当前状态机更新-----------------------------------------------------------------------------------------
        //curFSM.Update();
        //---------头顶血条更新-------------------------------------------------------------------------------------------
        //headHPUIManager.Update(curLivingCreatureData.curHP/curLivingCreatureData.maxHP, gameObject.name);
        //---------同步人物信息至服务器
        
        //Debug.Log("DEBUG NOW SKILL ID " + curSkillIdex);
    }

    /// <summary>
    /// 控制生命体移动
    /// </summary>
    /// <param name="Forward">前进向量</param>
    /// <param name="Direction">方向角</param>
    public virtual void ToMove(int d) {
        //ToUseSkill(1);
        //if(curSID==1) {
            dir = d;
            float angle = 15.0f * dir;
            float f = (float)(angle*Math.PI/180.0f);
            pos += (new Vector3((float)Math.Sin(f),0,(float)Math.Cos(f)))*((float)(curLivingCreatureData.Speed*Logic.eachframtime));
            rot = new Vector3(0,15.0f*dir,0);
        //}
    }

    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="useSkillIdex">使用的技能在当前生物可用技能表中的下标</param>
    public virtual bool ToUseSkill(int useSID) {
        //当前帧有命令
        IfFramCmd = true;

        //技能冷却、MP判定
        if(Logic.time-SkillLastFireTime[useSID] <curSkillDataList[useSID].SkillCoolingTime) 
            return false; //技能未冷却

        //询问状态机状态是否能够改变
        if(!curFSM.ChangeState(curSkillDataList[useSID].SkillStateName)) 
            return false; //不可更改为当前技能;
        
        //更改当前正在释放的技能为请求释放的技能
        curSID = useSID;

        //技能进入冷却时间
        SkillReset(curSID);

        //curSkillStage = (0,0,0);

        //技能释放成功
        return true;
    }
    /*
    public virtual void ToBeAttacked(float Damage) {
        curLivingCreatureData.curHP -= Damage;
        if(curFSM.CurState.GetStateName()=="Dodge")
            return ;
        ToUseSkill(2);
        if(curFSM.CurState.GetStateName()!="BeAttacked")
            return ;
        if(curLivingCreatureData.curHP < 0)
            curLivingCreatureData.curHP = 0;
    }*/

    /// <summary>
    /// 重置技能，即重新开始计算冷却
    /// </summary>
    /// <param name="SkillIdex">需要重置的技能在当前生物可用技能表中的下标</param>
    public virtual void SkillReset(int SID) {
        SkillLastFireTime[SID] = Logic.time;
    }

    /// <summary>
    /// 获得技能的剩余冷却时间
    /// </summary>
    /// <param name="SkillIdex">需要获得剩余冷却时间的技能在当前生物可用技能表中的下标</param>
    /// <returns>目标技能的剩余冷却时间</returns>
    /*public virtual float GetSkillRemainingCoolingTime(int SkillIdex) {
        return (Logic.time-SkillLastFireTime[SkillIdex]>curSkillDataList[SkillIdex].SkillCoolingTime)?0:(curSkillDataList[SkillIdex].SkillCoolingTime-Logic.time+SkillLastFireTime[SkillIdex]);
    }*/

}



