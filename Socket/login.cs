using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour {

    public GameObject buttonLogin;
    public GameObject buttonRegistered;
    public Client myClient;
    void Start()
    {
        Client.Init();
        buttonLogin = GameObject.Find("Login");
        buttonRegistered = GameObject.Find("Registered");
        buttonLogin.GetComponent<Button>().onClick.AddListener(Login);
        buttonRegistered.GetComponent<Button>().onClick.AddListener(Registered);
        Client.ifrun = true;
        Thread thread = new Thread(myClientRecv);
        thread.IsBackground = true;
        thread.Name = "Recv";
        thread.Start();
    }

    void myClientRecv() {
        while(true) {
            if(!Client.ifrun) {
                break;
            }
            Client.Recv();
            
        }
        Client._ClientSocket.Close();
    }

    void Update() {
        Debug.Log("login sc dataq count " + Client.dataQueue.Count);
        if(Client.dataQueue.Count > 0) {
            //已获得帧消息就开始游戏，场景跳
            SceneManager.LoadScene("ssstest2");
        }
    }

    void Login() {
        Debug.Log("Login begin");
        string name = GameObject.Find("Account").GetComponent<InputField>().text;
        string password = GameObject.Find("Password").GetComponent<InputField>().text;
        LoginReq req = new LoginReq();
        req.Name = name;
        req.Password = password;
        Client.Send(Client.LOGINTYPE, Client.Serialize(req).Length, Client.Serialize(req));
        LoginRes res = new LoginRes();
        res.Res = 0;
        /*if(Client.dataQueue.Count > 0) {
            var temp = Client.dataQueue.Dequeue();
            Debug.Log(temp.Item1 + " " + temp.Item2.ToString());
        }*/
        float beginTime = Time.time;
        while(true) {
            if(Time.time-beginTime > 5) {
                Debug.Log("超时");
                break;
            }
            if(Client.dataQueue.Count<=0) 
                continue;
            var temp = Client.dataQueue.Dequeue();
            if(temp.Item1 != Client.LOGINTYPE) {
                Debug.Log("TYPE ERROR");
                break;
                //continue;
            }
            else {
                res = Client.Deserialize<LoginRes>(temp.Item2,0,temp.Item2.Length);
                break;
            }
        }
        if(res.Res == 0) {
            Debug.Log("login no");
        }
        else {
            Debug.Log("login yes");
            Client.name = name;
            //SceneManager.LoadScene("ssstest");
        }
    }
    void Registered() {
        string name = GameObject.Find("Account").GetComponent<InputField>().text;
        string password = GameObject.Find("Password").GetComponent<InputField>().text;
        RegisteredReq req = new RegisteredReq();
        req.Name = name;
        req.Password = password;
        Client.Send(Client.REGISTEREDTYPE, Client.Serialize(req).Length,Client.Serialize(req));
        RegisteredRes res = new RegisteredRes();
        float beginTime = Time.time;
        while(true) {
            if(Time.time-beginTime > 5) {
                Debug.Log("超时");
                break;
            }
            if(Client.dataQueue.Count<=0)
                continue;
            var temp = Client.dataQueue.Dequeue();
            if(temp.Item1 != Client.REGISTEREDTYPE) {
                Debug.Log("TYPE ERROR");
                break;
                //continue;
            }
            else {
                res = Client.Deserialize<RegisteredRes>(temp.Item2,0,temp.Item2.Length);
                break;
            }
        }
        if(res.Res == 0) {
            Debug.Log("registered no");
        }
        else {
            Debug.Log("registered yes");
        }
    }

}


