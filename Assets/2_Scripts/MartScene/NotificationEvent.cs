using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class NotificationEvent : MonoBehaviour
{
    int lang;
    string gameMode;

    public SteamVR_Input_Sources leftHand;
    public SteamVR_Action_Vibration hapticAction;

    public Material screen;
    public Sprite basic;
    public Sprite calling1;
    public Sprite calling2;
    public Sprite message1_kr;
    public Sprite message1_fr;
    public Sprite message1_en;
    public Sprite test_message;

    public GameObject subtitle;
    
    /* Types of Messages
    - 1st message: Discount Code (Phone call) and Budget information
    - 2nd message: (If the user failed 3 times) Full discount code
    - Finished: Completed all notification */
    NotificationType notificationType;
    enum NotificationType { First, Second, Finished };
    float hintDuration = 0; // All types of messages are initially shows for few seconds. => Todo. 없애기

    void Start(){
        notificationType = NotificationType.First;
        gameMode   = GameObject.Find("v_gameMode").GetComponent<Text>().text;
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:Notification.cs");
    }

    /**
     * @ Function : Notification Function
     * 
     * @ Author : Minjung Kim
     * @ Date : 2020.Jun.07
     * @ History :
     *   - 2020.04.03 Euisung Kim: 최초 작성
     *   - 2020.04.10 Minjung KIM: Modify Function structure and Add disappear message function
     *   - 2020.05.23 Minjung KIM: Add Event Log
     *   - 2020.05.31 Minjung KIM: Add Function Comment 
     *   - 2020.06.17 Minjung Kim: Update screen materials
     *   - 2020.06.27 Minjung Kim: Add gameMode check
     **/
    void Update()
    {
        GameObject phoneObj = null;
        try{
            phoneObj = GameObject.FindGameObjectWithTag("left_cellphone");
        }catch (Exception e){
            Debug.Log("없으면 재시작해야합니다. 로딩까지 좀 걸리지");
        }

        // TODO- Duraion 필요가 없음!!! 트리거로 정리
        // This function starts when the hint duration is bigger than 0
        if (hintDuration > 0){
            hintDuration -= Time.deltaTime;
            if (hintDuration <= 0){
                Debug.Log("[Notification: Finished]");
                // cellPhoneCanvas.SetActive(false);
                screen.SetTexture("_EmissionMap", basic.texture);
                subtitle.SetActive(false);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_NOTI, "Update()", "NOTIFICATION_DISAPPEARS");
            }
        }

        // ----------------------------------
        // 1st phone calling 
        // Contents: Discount Code and budget
        // ----------------------------------
        string v_first_scan_yn = GameObject.Find("v_first_scan_yn").GetComponent<Text>().text;
        if (notificationType.Equals(NotificationType.First) & v_first_scan_yn.Equals("Y")){
            if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){

                // Todo levels of difficulty  == Screen2Event.cs
                Debug.Log("[Notification: Phone Calling]");
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_CALL, lang);
                screen.SetTexture("_EmissionMap", calling1.texture);
                CallHapticAction(0, 2, 150, 75, leftHand);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_NOTI, "Phone Calling", "Taking a phone call");
                Invoke("ChangePhoneScreen", 1.5f);
                notificationType = NotificationType.Second;
                hintDuration = 19;

            // TEST MODE
            }else { 
                Debug.Log("[Notification: TEST]");
                CallHapticAction(0, 2, 150, 75, leftHand);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_MESSAGE, lang);
                screen.SetTexture("_EmissionMap", test_message.texture);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_NOTI, "[TEST] Phone Message", "Sent a d.c text(User failed to enter the discount code)");
                notificationType = NotificationType.Second;
            }
        }

        // ----------------------------------
        // 2nd text message  
        // Contents: (If the user does 3 errors) send a full discount code.
        // ----------------------------------
        int v_discount_errors = Int32.Parse(GameObject.Find("v_discount_errors").GetComponent<Text>().text);
        if (notificationType.Equals(NotificationType.Second) && v_discount_errors >= 3 && gameMode.Equals(GlobalEnv.GAMEMODE_START)){
            Debug.Log("[Notification: Message]");
            CallHapticAction(0, 2, 150, 75, leftHand);
            SoundManager.instance.PlaySound(GlobalEnv.SOUND_MESSAGE, lang);
            if(lang == 0) { 
                screen.SetTexture("_EmissionMap", message1_kr.texture);
            }else if(lang == 1) { 
                screen.SetTexture("_EmissionMap", message1_fr.texture);
            }else if(lang == 2) { 
                screen.SetTexture("_EmissionMap", message1_en.texture);
            }
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_NOTI, "Phone Message", "Sent a d.c text(User failed to enter the discount code)");
            notificationType = NotificationType.Finished;
        }
    }

    void ChangePhoneScreen(){
        screen.SetTexture("_EmissionMap", calling2.texture);
        SoundManager.instance.PlaySound(GlobalEnv.SOUND_VOCAL, lang);
        subtitle.SetActive(true);
    }

    /**
     * @ Function : HapticAction Method For Test
     * 
     * @ Author : Minjung Kim
     * @ Date : 2020.Jun.27
     * @ History :
     *  - To ignore the error message(Without VR Controller)
     */
    void CallHapticAction(
        float secondsFromNow
        , float durationSeconds
        , float frequency
        , float amplitude
        , SteamVR_Input_Sources inputSource
    ){
        try{
            hapticAction.Execute(0, 2, 150, 75, leftHand);
        }catch(Exception e){
            Debug.Log("Haption Error");
        }
    }
}