using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScanningEvent_AfterTest : MonoBehaviour
{
    public TMP_Text result;
    private Dictionary<string, string> after_list = new Dictionary<string, string>();

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
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:ScanningEvent_After.cs");
    }

    void Update(){
        // If All items are placed on the left table, the result message appears.
        if(after_list.Count >= itemCount){
            result.text = LangText.screen1_after[lang];
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "[AFTER] Message Appears", LangText.screen1_after[lang]);
        }
    }

    /**
     * @Function: After check Method
     * 
     * @Author: Minjung KIM
     * @Date: 2020.May.06
     * @History: 
     *  - 2020.05.23 Minjung Kim : Add Event Log
     */
    void OnCollisionEnter(Collision collision){

        string o_item_code = collision.gameObject.name;  // unique item code: ex) A1
        string o_item_name = collision.gameObject.tag;   // item name: ex) Apple

        try{
            if (!o_item_name.Equals("Untagged") && !after_list.ContainsKey(o_item_code)){
                Debug.Log("AfterScanning onEnter()  o_item_code   :" + o_item_code);
                Debug.Log("AfterScanning onEnter()  o_item_name   :" + o_item_name);

                after_list.Add(o_item_code, o_item_name);
                Debug.Log("AfterScanning onEnter()  count:"+after_list.Count);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_AFTER_ENTER, o_item_name, "item_code:"+o_item_code);
            }
        }catch{

        }
    }
    
    /**
     * @Function: After check Method
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Jun.25
     */
    void OnCollisionExit(Collision collision){

        string o_item_code = collision.gameObject.name;  // unique item code: ex) A1
        string o_item_name = collision.gameObject.tag;   // item name: ex) Apple

        try{
            if (!o_item_name.Equals("Untagged") && after_list.ContainsKey(o_item_code)){
                Debug.Log("AfterScanning onExit()   o_item_code   :" + o_item_code);
                Debug.Log("AfterScanning onExit()   o_item_name   :" + o_item_name);

                after_list.Remove(o_item_code);
                Debug.Log("AfterScanning onExit()   count:"+after_list.Count);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_AFTER_EXIT, o_item_name, "item_code:"+o_item_code);
            }
        }catch{

        }
    }
}
