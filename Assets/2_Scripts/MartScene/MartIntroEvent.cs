using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MartIntroEvent : MonoBehaviour
{
    int lang;
    string gameMode;
    public GameObject btn_home;

    public GameObject trolley_test; 
    public GameObject trolley_real; 
    
    public TMP_Text text_before;
    public TMP_Text text_scan;
    public TMP_Text text_after;
    public TMP_Text text_card;

    public TMP_Text text_screen1_result;
    public TMP_Text text_screen1_name;
    public TMP_Text text_screen1_qty;
    public TMP_Text text_screen1_price;
    public TMP_Text text_screen1_total;
    public TMP_Text text_screen1_discount;
    public TMP_Text text_screen1_cancel;
    public TMP_Text text_screen1_pay;

    public TMP_Text text_screen2_result;
    public TMP_Text text_screen2_apply;
    public TMP_Text text_screen2_backspace;

    public TMP_Text text_screen3_result;

    public Material screen;
    public Sprite basic;

    private void Awake()
    {
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        gameMode =  GameObject.Find("v_gameMode").GetComponent<Text>().text;
        if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){
            trolley_real.SetActive(true);
            trolley_test.SetActive(false);
        }
        Debug.Log("==== LOADING MART ENV SETTING ====");
        Debug.Log("Lang:"+lang);
        Debug.Log("gameMode:" + gameMode);
    }

    public void Start()
    {
        text_before.text    = LangText.martText_self_before[lang];
        text_after.text = LangText.martText_self_after[lang];
        text_scan.text  = LangText.martText_self_scan[lang];
        text_card.text  = LangText.martText_self_card[lang];

        text_screen1_result.text    = LangText.screen1_scanning[lang];
        text_screen1_name.text  = LangText.screen1_text_itemName[lang];
        text_screen1_qty.text   = LangText.screen1_text_itemQty[lang];
        text_screen1_price.text = LangText.screen1_text_itemPrice[lang];
        text_screen1_total.text = LangText.screen1_text_total[lang];
        text_screen1_discount.text  = LangText.screen1_text_discount[lang];
        text_screen1_cancel.text    = LangText.screen1_button_cancel[lang];
        text_screen1_pay.text   = LangText.screen1_button_pay[lang];

        text_screen2_result.text    = LangText.screen2_enter_the_dc[lang];
        text_screen2_apply.text     = LangText.screen2_button_apply[lang];
        text_screen2_backspace.text = LangText.screen2_button_backspace[lang];

        text_screen3_result.text = LangText.screen3_result[lang];

        // Material rollbacks
        screen.SetTexture("_EmissionMap", basic.texture);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:MartIntroEvent.cs");

        // Set Home button
        if (gameMode.Equals(GlobalEnv.GAMEMODE_START)) {
            btn_home.SetActive(false);
        }
    }
}
