using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardEvent : MonoBehaviour
{
    int lang;
    public TMP_Text result_message;
    public Image result_background;

    public Text trying_to_pay_yn;
    public TMP_Text total_amount;
    public Canvas screen1; // scan code & pay error
    public Canvas screen3; // SUCC

    bool card_tag = false;

    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:CardEvent.cs");
    }

    /**
     * @Function: Card Function
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Mar.25 
     * @History: 
     *  - 2020.05.25 Minjung KIM: initial commit
     *  - 2020.05.29 Minjung KIM: ADD credit exceeded case
     *  - 2020.06.16 Minjung KIM: Block duplicate tagging 
     *  - 2020.07.01 Minjung KIM: Add discount_yn
     *  - 2020.07.03 Minjung KIM: Add sound effect
     */
    void OnTriggerExit(Collider collision){
        int total = Int32.Parse(total_amount.text);
        string tag = collision.gameObject.tag;
        string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
        
        if (card_tag == false && !tag.Equals("Untagged") && tag.Equals("card") && (trying_to_pay_yn.text).Equals("Y")){
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_PAY, "OnTriggerEnter()", "touch the card!"+", discount_yn:"+ discount_auth_yn);
            card_tag = true;

            // -----------------------
            // FAIL
            // -----------------------
            if (total > GlobalEnv.BUDGET){
                result_message.text = LangText.alert_err[lang];
                result_message.color = Color.red;
                result_background.color = Color.yellow;

                SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_PAY, "OnTriggerExit()", "Faild to pay" + ", discount_yn:" + discount_auth_yn);
                trying_to_pay_yn.text = "N";
                card_tag = false;

            // -----------------------
            // SUCC
            // -----------------------
            }else{
                result_message.text = LangText.alert_succ[lang];
                result_message.color = Color.blue;
                result_background.color = Color.white;

                SoundManager.instance.PlaySound(GlobalEnv.SOUND_SUCC, lang);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_PAY, "OnTriggerExit()", "Succed to pay" + ", discount_yn:" + discount_auth_yn);
                Invoke("ChangeCanvas1toCanvas3After2s", 2f);
            }
        }
    }

    private void ChangeCanvas1toCanvas3After2s(){
        screen1.gameObject.SetActive(false);
        screen3.gameObject.SetActive(true);
        // Call Ending Scene
        Invoke("CallEndingScneneAfter5f", 5f);
    }

    private void CallEndingScneneAfter5f() { 
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_CHANGE, "ChangeCanvas1toCanvas3After2s()", "Scene Change!");
        SceneManager.LoadScene("5_EndScene");
    }
}
