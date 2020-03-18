using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


/// /// <summary>
/// 技能特效信息
/// </summary>
[Serializable]
public class SkillSE {
    /// <summary>
    /// 技能特效ID
    /// </summary>
    public int SkillSEID;

    /// <summary>
    /// 技能特效名称
    /// </summary>
    public string SkillSEName;

    /// <summary>
    /// 技能特效加载点名称
    /// </summary>
    public string SkillSELoadPointName;

    /// <summary>
    /// 是否跟随技能挂载点
    /// </summary>
    public bool IfFollowSkillSELoadPoint;

    /// <summary>
    /// 是否自动消亡
    /// </summary>
    public bool IfAutoDestroy;
    
    /// <summary>
    /// 技能特效开始时间
    /// </summary>
    public float SkillSEBeginTime;

    /// <summary>
    /// 技能特效持续时间
    /// </summary>
    public float SkillSEDuringTime;
}



/// <summary>
/// 单次攻击判断信息
/// </summary>
[Serializable]
public class SkillAttackJudgeInf {
    /// <summary>
    /// 技能判定开始时间
    /// </summary>
    public float SkillAttackJudgeBeginTime;

    /// <summary>
    /// 技能判定中心点离施法者的距离
    /// </summary>
    public float SkillAttackJudgeDistance;

    /// <summary>
    /// 技能判定区域类型 1:矩形   2:圆形   3:扇形
    /// </summary>
    public int SkillAttackJudgeAreaType;

    /// <summary>
    /// 技能判定区域半径，供 2,3 类型使用
    /// </summary>
    public float SkillAttackJudgeAreaRadius;
    /// <summary>
    /// 技能判定区域角度，供 3 类型使用
    /// </summary>
    public float SkillAttackJudgeAreaAngle;
    /// <summary>
    /// 技能判定区域宽度，供 1 类型使用
    /// </summary>
    public float SkillAttackJudgeAreaWidth;
    /// <summary>
    /// 技能判定区域长度，供 1 类型使用
    /// </summary>
    public float SkillAttackJudgeAreaHeight;

    /// <summary>
    /// 技能判定伤害系数 与攻击力Attack相关
    /// </summary>
    public float SkillAttackJudgeDamageRatio;

    /// <summary>
    /// 技能判定时人物的移动速度（即冲锋或者后退速度）
    /// </summary>
    public float SkillAttackJudgeMoveSpeed;

}
/// <summary>
/// 攻击型技能
/// </summary>
[Serializable]
public class SkillAttackType {
    /// <summary>
    /// 技能攻击段数
    /// </summary>
    public int SkillAttackTimes;

    /// <summary>
    /// 技能每段攻击信息表
    /// </summary>
    public List<SkillAttackJudgeInf> SkillAttackJudgeInfList;

    /// <summary>
    /// 当前判定达到的阶段
    /// </summary>
    public int CurSkillAttackJudgeStage;
}  


/// <summary>
/// BUFF型技能
/// </summary>
[Serializable]
public class SkillBUFFType {
    /// <summary>
    /// BUFF是否为DEBUFF
    /// </summary>
    public bool IfDEBUFF;

    /// <summary>
    /// BUFF作用属性 1:curHP  2:curMP  4:移动速度   8:防御力
    /// </summary>
    public int BUFFRoleAttribute;

    /// <summary>
    /// BUFF作用开始时间
    /// </summary>
    public float BUFFBeginTime;

    /// <summary>
    /// BUFF作用次数  移速和防御属性固定作用一次
    /// </summary>
    public int BUFFRoleTimes;

    /// <summary>
    /// BUFF每次作用间隔时间，移速和防御属性间隔时间即持续时间
    /// </summary>
    public float BUFFEachRoleTime;

    /// <summary>
    /// BUFF效果值
    /// </summary>
    public float BUFFEffectValue;
}

/// <summary>
/// 子弹型技能
/// </summary>
[Serializable]
public class SkillBulletType {
    /// <summary>
    /// 子弹加载的特效名称
    /// </summary>
    public string BulletSEName;

    /// <summary>
    /// 子弹发射时间
    /// </summary>
    public float BulletFireTime;

    public bool BulletFireEnemyPos;
    /// <summary>
    /// 子弹攻击单次判定信息
    /// </summary>
    public SkillAttackJudgeInf SkillAttackJudgeInfSingle;

    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    public float BulletFlySpeed;

    /// <summary>
    /// 子弹最大持续时间
    /// </summary>
    public float BulleMaxDuringTime;

    /// <summary>
    /// 子弹是否能够穿透目标，对具有飞行速度的子弹而言，不能穿透就会碰触消亡
    /// </summary>
    public bool IfPenetrate;

    /// <summary>
    /// 是否能够作用多个目标
    /// </summary>
    public bool IfRoleOnMultipleGoals;

    /// <summary>
    /// 子弹攻击段数  对于法术场子弹而言
    /// </summary>
    public int BulletAttackJudgeTimes;

    /// <summary>
    /// 子弹每次攻击判定时间间隔
    /// </summary>
    public float BulletEachAttackTime;
}

/// <summary>
/// 技能信息
/// </summary>
[Serializable]
public class SkillData
{
    /// <summary>
    /// 技能ID
    /// </summary>
    public int SkillID;

    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName;

    /// <summary>
    /// 技能状态名称
    /// </summary>
    public string SkillStateName;

    /// <summary>
    /// 技能动画名称
    /// </summary>
    public string SkillAnimationName;

    /// <summary>
    /// 技能特效列表
    /// </summary>
    public List<SkillSE> SkillSEList;

    /// <summary>
    /// 技能存在时间
    /// </summary>
    public float SkillDuringTime;

    /// <summary>
    /// 技能前摇判定结束时间
    /// </summary>
    public float SkillRollEndTime;

    /// <summary>
    /// 技能后摇判定开始时间
    /// </summary>
    public float ShakeBackBeginTime;

    /// <summary>
    /// 技能冷却时间
    /// </summary>
    public float SkillCoolingTime;

    /// <summary>
    /// 技能MP消耗
    /// </summary>
    public float SkillCostMP;

    /// <summary>
    /// 技能类型 1:攻击  2:BUFF  4:子弹。二进制表示，可同时存在多种状态
    /// </summary>
    public int SkillType;
    public float SkillJudgeTime;
    public float SkillDamage;
    public float MoveSpeed;
    public SkillData(int I=0, string SKN="", string STN="", string AN="", float DT=0, float RT=0, float BB=0, float CT=0, float JT=0, float MS=0, float DM=0) {
        SkillID = I;
        SkillName = SKN;
        SkillStateName = STN;
        SkillAnimationName = AN;
        SkillDuringTime = DT;
        SkillRollEndTime = RT;
        ShakeBackBeginTime = BB;
        SkillCoolingTime = CT;
        SkillJudgeTime = JT;
        MoveSpeed = MS;
        SkillDamage = DM;
    }


    //--------------1:攻击类型-----------------------------
    /// <summary>
    /// 攻击类技能信息
    /// </summary>
    //public SkillAttackType SkillAttack;


    //---------------2:BUFF/DEBUFF------------------------
    /// <summary>
    /// BUFF类技能信息
    /// </summary>
    //public SkillBUFFType SkillBUFF;

    /// <summary>
    /// 子弹类技能信息
    /// </summary>
    //public SkillBulletType SkillBullet;

}


[CreateAssetMenu(menuName = "Config/SkillConfig")]
[Serializable]
public class SkillConfig : ScriptableObject {
    public List<SkillData> SkillDataList = new List<SkillData>();
}