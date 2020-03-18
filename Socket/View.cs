using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View
{
    /// <summary>
    /// 预先加载的资源库
    /// </summary>
    public static Dictionary<string,GameObject> MyResources;
    /// <summary>
    /// 人物加载对应的资源名字
    /// </summary>
    public static List<string> PlayerResourcesName;
    /// <summary>
    /// 视图层对象映射:名称标识->GameOject实例
    /// </summary>
    public static Dictionary<string,GameObject> obmp;

    public delegate void EventMsg();
    public static event EventMsg EventHandle;


    public static void Init() {
        var obinfmp = Logic.obinfmp;
        //资源加载
        MyResources =  RSDB.DB;
        PlayerResourcesName = new List<string>();
        obmp = new Dictionary<string, GameObject>();

        PlayerResourcesName.Add("blackS");
        PlayerResourcesName.Add("blueS");
        PlayerResourcesName.Add("yellowS");
        PlayerResourcesName.Add("redS");
        PlayerResourcesName.Add("PlayerW");

    }
    public static void Update() {
        if(EventHandle != null) {
            EventHandle();
            EventHandle -= EventHandle;
        }

        //Debug.Log("user counts " + obmp.Count);
        var obinfmp = Logic.obinfmp;
        var needDes = new List<string>();
        foreach(var e in obinfmp) {
            var name = e.Key;
            var obinf = e.Value;
            //用户不存在就新建
            if(!obmp.ContainsKey(name)) {
                obmp[name] = GameObject.Instantiate(MyResources["PlayerW"],obinf.pos,Quaternion.identity);
                obmp[name].transform.Find("body").GetComponent<SkinnedMeshRenderer>().material = RSDB.mat[obinf.id-1];
                obmp[name].name = name;
            }
            
            //缓动
            float angle = obinf.rot.y;
            float f = (float)(angle*Math.PI/180.0f);
            obmp[name].transform.position = Vector3.Lerp(obmp[name].transform.position, obinf.pos, 0.2f);
            //obmp[name].transform.localEulerAngles = Vector3.Lerp(obmp[name].transform.localEulerAngles, obinf.rot, 0.1f);
            obmp[name].transform.rotation = Quaternion.Slerp(obmp[name].transform.rotation
                , Quaternion.LookRotation((new Vector3((float)Math.Sin(f),0,(float)Math.Cos(f)))), 0.1f);
            obmp[name].transform.Find("HPUI").transform.eulerAngles = new Vector3(0,0,0);
            obmp[name].transform.Find("HPUI").Find("Slider").GetComponent<Slider>().value = Logic.obinfmp[name].curLivingCreatureData.curHP/Logic.obinfmp[name].curLivingCreatureData.maxHP;
        }

    }
}
