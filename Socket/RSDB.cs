using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RSDB
{
    public static Dictionary<string,GameObject> DB = new Dictionary<string, GameObject>();
    public static SkillConfig skillConfig = ScriptableObject.CreateInstance<SkillConfig>();
    public static List<Material> mat = new List<Material>();
    public static void Init() {
        //-----------------小球测试用-------------------------------
        DB["blackS"] = Resources.Load("blackS") as GameObject;
        DB["blueS"] = Resources.Load("blueS") as GameObject;
        DB["yellowS"] = Resources.Load("yellowS") as GameObject;
        DB["redS"] = Resources.Load("redS") as GameObject;
        DB["PlayerW"] = Resources.Load("PlayerW") as GameObject;

        mat.Add(Resources.Load("c") as Material);
        mat.Add(Resources.Load("a") as Material);
        mat.Add(Resources.Load("b") as Material);
        mat.Add(Resources.Load("d") as Material);
        mat.Add(Resources.Load("e") as Material);


       /* mat.Add(new Material(Shader.Find("Custom/Skin/Cube")));
        mat.Add(new Material(Shader.Find("Custom/Effect/DiffuseCapGlass")));
        mat.Add(new Material(Shader.Find("Custom/Skin/CubeNoMask")));
        mat.Add(new Material(Shader.Find("Custom/Skin/Effect")));
        mat.Add(new Material(Shader.Find("Custom/Skin/MainPlayer")));
        mat.Add(new Material(Shader.Find("Custom/Effect/DiffuseCapGlass")));*/


        //skillConfig = Resources.Load("SkillConfig") as SkillConfig;
        skillConfig.SkillDataList = new List<SkillData>();                                    //持续时间   前摇        后摇        冷却时间   伤害时间  位移速度   伤害
        skillConfig.SkillDataList.Add(new SkillData(0,"zhanli","Idle","W_Idle"                 ,999999f    ,999999f    ,999999f    ,0         ,0                  ));
        skillConfig.SkillDataList.Add(new SkillData(1,"bengpao","Run","W_Run"                  ,999999f    ,999999f    ,999999f    ,0         ,0                  ));
        skillConfig.SkillDataList.Add(new SkillData(2,"shouji","BeAttacked","W_BeAttacked"     ,0.333f     ,0.333f     ,0.333f     ,0         ,0                  ));
        skillConfig.SkillDataList.Add(new SkillData(3,"siwang","Dead","W_Dead"                 ,999999f    ,999999f    ,999999f    ,0         ,0                  ));
        skillConfig.SkillDataList.Add(new SkillData(4,"putonggongji","SkillA","W_SkillA"       ,1.333f     ,0.2f       ,0.7f       ,1.4f      ,0.23f    ,0         ,5f));
        skillConfig.SkillDataList.Add(new SkillData(5,"shanbi","SkillD","W_SkillD"             ,0.666f     ,0f         ,0.66f      ,2f        ,0        ,2f        ));
    }
}
