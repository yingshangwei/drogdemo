using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class UIManager
{
    public EasyTouchMove touch; //遥感控件
    private GameObject buttonSkill1;
    private GameObject buttonSkill2;
    private GameObject buttonSkill3;
    private GameObject buttonSkillA;
    private GameObject buttonSkillD;
    private GameObject buttonPlayerInfPanel;
    private GameObject PlayerInfPanel;
    void Init()
    {
        //----------初始化遥感-----------------------------------------------------------------------
        touch = GameObject.Find("touch").GetComponent<EasyTouchMove>();
        //----------初始化生物信息-------------------------------------------------------------------
        //InitCreature(0);
        //-----------加载人物面板控件--------------------------------------------------------------------
        PlayerInfPanel = GameObject.Instantiate(Resources.Load("PlayerInfPanel") as GameObject, new Vector3(512,384,0), Quaternion.identity);
        PlayerInfPanel.SetActive(false);
        //----------加载按钮-----------------------------------------------------------------------
        buttonSkill1 = GameObject.Find("Skill1");
        buttonSkill1.GetComponent<Button>().onClick.AddListener(PlaySkill1);
		buttonSkill2 = GameObject.Find("Skill2");
		buttonSkill2.GetComponent<Button>().onClick.AddListener(PlaySkill2);
        buttonSkill3 = GameObject.Find("Skill3");
        buttonSkill3.GetComponent<Button>().onClick.AddListener(PlaySkill3);
		buttonSkillA = GameObject.Find("SkillA");
		buttonSkillA.GetComponent<Button>().onClick.AddListener(PlaySkillA);
        buttonSkillD = GameObject.Find("SkillD");
		buttonSkillD.GetComponent<Button>().onClick.AddListener(PlaySkillD);
        buttonPlayerInfPanel = GameObject.Find("buttonPlayerInfPanel");
        buttonPlayerInfPanel.GetComponent<Button>().onClick.AddListener(ShowPlayerInfPanel);
    }

    void Update()
    {
        
        UpdateYaogan();
        UpdateButton();
        UpdateInfPanel();
        
        if(Input.GetKeyDown("x")) {
            ToUseSkill(4);
        }
        else if(Input.GetKeyDown("c")) {
            ToUseSkill(5);
        }
        else if(Input.GetKeyDown("a")) {
            ToUseSkill(6);
        }
        else if(Input.GetKeyDown("s")) {
            ToUseSkill(7);
        }
        else if(Input.GetKeyDown("d")) {
            ToUseSkill(8);
        }
        else if(Input.GetKeyDown("f")) {
            ToUseSkill(9);
        }
    }
    void UpdateButton() {
         buttonSkillA.transform.Find("mask").GetComponent<Image>().fillAmount = GetSkillRemainingCoolingTime(4)/curSkillDataList[4].SkillCoolingTime;
         buttonSkillD.transform.Find("mask").GetComponent<Image>().fillAmount = GetSkillRemainingCoolingTime(5)/curSkillDataList[5].SkillCoolingTime;
         buttonSkill1.transform.Find("mask").GetComponent<Image>().fillAmount = GetSkillRemainingCoolingTime(6)/curSkillDataList[6].SkillCoolingTime;
         buttonSkill2.transform.Find("mask").GetComponent<Image>().fillAmount = GetSkillRemainingCoolingTime(7)/curSkillDataList[7].SkillCoolingTime;
         buttonSkill3.transform.Find("mask").GetComponent<Image>().fillAmount = GetSkillRemainingCoolingTime(8)/curSkillDataList[8].SkillCoolingTime;
    }
    void UpdateYaogan() {
        //hor = 遥感脚本中的localPosition.x//
        float hor = touch.Horizontal;
        //hor = 遥感脚本中的localPosition.y
        float ver = touch.Vertical;
 
        Vector3 direction = new Vector3(hor, 0, ver);
 
        if (direction != Vector3.zero) {
            var Direction = Quaternion.LookRotation(direction);
            PosSynReq req = new PosSynReq();
            Direction.x *= 1000;
            req.Rx = (int)Direction.x;
            Direction.x = (float)req.Rx/1000;

            Direction.y *= 1000;
            req.Ry = (int)Direction.y;
            Direction.y = (float)req.Ry/1000;

            Direction.z *= 1000;
            req.Rz = (int)Direction.z;
            Direction.z = (float)req.Rz/1000;
            
            req.Name = Client.name;
            if(Time.time - send_pre_time > 1) {
                send_pre_time = Time.time;
                Client.Send(Client.POSSYNTYPE, Client.Serialize<PosSynReq>(req).Length, Client.Serialize<PosSynReq>(req));
            }
            Client.Send()
            ToMove(Quaternion.Lerp(Logic.obinfmp[Client.name].rot, Direction, Time.deltaTime*10));

        }else {
            //站立状态
            ToUseSkill(0);
        }
        oid PlaySkillA() {
        Debug.Log("CLICK A");
        ToUseSkill(4);
    }
    void PlaySkillD() {
        ToUseSkill(5);
    }
    void PlaySkill1() {
        ToUseSkill(6);
    }
    void PlaySkill2() {
        ToUseSkill(7);
    }
    void PlaySkill3() {
        ToUseSkill(8);
    }
    void ShowPlayerInfPanel() {
        //Debug.Log("SHOW INFPANEL " + PlayerInfPanel.activeInHierarchy);
        if(!PlayerInfPanel.activeInHierarchy) {
            PlayerInfPanel.SetActive(true);
        }
        else PlayerInfPanel.SetActive(false); 
    }
    void UpdateInfPanel() {
        PlayerInfPanel.transform.Find("Background").transform.
            Find("PlayerInfPanel").GetComponent<Text>().text = 
                "昵称   :"+curLivingCreatureData.Name+"\n"+
                "HP     :"+curLivingCreatureData.curHP+"/"+curLivingCreatureData.maxHP+"\n"+
                "MP     :"+curLivingCreatureData.curMP+"/"+curLivingCreatureData.maxMP+"\n"+
                "Attack :"+curLivingCreatureData.Attack+"\n"+
                "Defence:"+curLivingCreatureData.Defence+"\n"+
                "Speed  :"+curLivingCreatureData.Speed;
    }
        //Debug.Log("DEBUG NOWSTATE  " + curFSM.CurState.GetStateName());
}
*/