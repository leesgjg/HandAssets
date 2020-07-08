using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScanningEvent_BeforeTest : MonoBehaviour
{
    public TMP_Text result;
    private Dictionary<string, string> before_list = new Dictionary<string, string>();

    int itemCount;
    int lang;

    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        string gameMode =  GameObject.Find("v_gameMode").GetComponent<Text>().text;
        if (gameMode.Equals(GlobalEnv.GAMEMODE_TEST)){
            itemCount = 3; 
        }else{
            itemCount = 6;
        }
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:ScanningEvent_Before.cs");
    }
    
    void Update(){
        // If All items are placed on the left table, the result message appears.
        if(before_list.Count >= itemCount){
            result.text = LangText.screen1_before[lang];
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "[BEFORE] Message Appears", LangText.screen1_before[lang]);
        }
    }

    /**
     * @Function: Before check Method
     * 
     * @Author: Minjung KIM
     * @Date: 2020.May.06
     * @History: 
     *  - 2020.05.23 Minjung Kim : Add Event Log
     */
    // void OnTriggerEnter(Collider collision) { 
    void OnCollisionEnter(Collision collision){

        string o_item_code = collision.gameObject.name;  // unique item code: ex) A1
        string o_item_name = collision.gameObject.tag;   // item name: ex) Apple

        try{
            if (!o_item_name.Equals("Untagged") && !before_list.ContainsKey(o_item_code)){
                Debug.Log("BeforeScanning onEnter() o_item_code   :" + o_item_code);
                Debug.Log("BeforeScanning onEnter() o_item_name   :" + o_item_name);

                before_list.Add(o_item_code, o_item_name);
                Debug.Log("BeforeScanning onEnter() count:"+before_list.Count);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BEFORE_ENTER, o_item_name, "item_code:"+o_item_code);
            }
        }catch(Exception e){

        }
    }
    
    /**
     * @Function: Before check Method
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Jun.25
     */
    //  void OnTriggerExit(Collider collision) { 
    void OnCollisionExit(Collision collision){

        string o_item_code = collision.gameObject.name;  // unique item code: ex) A1
        string o_item_name = collision.gameObject.tag;   // item name: ex) Apple

        try{
            if (!o_item_name.Equals("Untagged") && before_list.ContainsKey(o_item_code)){
                Debug.Log("BeforeScanning onExit()   o_item_code   :" + o_item_code);
                Debug.Log("BeforeScanning onExit()   o_item_name   :" + o_item_name);

                before_list.Remove(o_item_code);
                Debug.Log("BeforeScanning onExit()   count   :" + before_list.Count);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BEFORE_EXIT, o_item_name, "item_code:"+o_item_code);
            }
        }catch{

        }
    }
}
