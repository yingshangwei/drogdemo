﻿using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel.Design;
using System.IO;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 逻辑对象数据
/// </summary>
public class ObjectInf {
    /// <summary>
    /// 逻辑对象id
    /// </summary>
    public int id = 0;
    /// <summary>
    /// 逻辑对象名字
    /// </summary>
    public string name = "";
    /// <summary>
    /// 逻辑对象标签属性
    /// </summary>
    public string tag = "";
    /// <summary>
    /// 逻辑对象全局位置
    /// </summary>
    public Vector3 pos = new Vector3();
    /// <summary>
    /// 逻辑对象全局旋转方向
    /// </summary>
    public Vector3 rot = new Vector3();
    public int dir = 0;
}
public class Manager : MonoBehaviour
{
    
    void Start()
    {
        //资源加载
        RSDB.Init();
        //逻辑层加载
        Logic.Init();
        //视图层加载
        View.Init();
    }

    float pret = 0;
    // Update is called once per frame
    void Update()
    {
        ListenKey();
        if(Client.dataQueue.Count < 10) {
            Logic.Update();
        }
        else if(Client.dataQueue.Count < 30) {
            for(int i = 0;i < 3;i++) Logic.Update();
        }
        else if(Client.dataQueue.Count < 60) {
            for(int i = 0;i < 10;i++) Logic.Update(); 
        }
        else {
            for(int i = 0;i < 50;i++) Logic.Update();
        }
        View.Update();
    }


    void ListenKey() {
        
        FramOpt opt = new FramOpt();
        opt.Doplayername = Client.name;
        bool ok = false;
        if(Input.GetKey(KeyCode.UpArrow)) {
            opt.Optype = Client.MOVETYPE;
            opt.Dir = 0;
            Client.Send(Client.FRAMOPTYPE,opt);
            ok = true;
            pret = Time.time;
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            opt.Optype = Client.MOVETYPE;
            opt.Dir = 6;
            Client.Send(Client.FRAMOPTYPE,opt);
            ok = true;
            pret = Time.time;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            opt.Optype = Client.MOVETYPE;
            opt.Dir = 12;
            Client.Send(Client.FRAMOPTYPE,opt);
            ok = true;
            pret = Time.time;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            opt.Optype = Client.MOVETYPE;
            opt.Dir = 18;
            Client.Send(Client.FRAMOPTYPE,opt);
            ok = true;
            pret = Time.time;
        }
        if(Input.GetKeyDown("x")||Input.GetKey("x")) {
            opt.Optype = Client.SKILLTYPE;
            opt.Skillid = 4;
            Client.Send(Client.FRAMOPTYPE,opt);
            pret = Time.time;
            ok = true;
        }
        if(Input.GetKeyDown("c")||Input.GetKey("c")) {
            opt.Optype = Client.SKILLTYPE;
            opt.Skillid = 5;
            Client.Send(Client.FRAMOPTYPE,opt);
            pret = Time.time;
            ok = true;
        }
        if(!ok && Time.time - pret >= 0.1f){
            opt.Optype = Client.SKILLTYPE;
            opt.Skillid = 0;
            Client.Send(Client.FRAMOPTYPE,opt);
            pret = Time.time+2f;
        }
    }
    void OnDestroy() {
        Client.ifrun = false;    
    }
}
