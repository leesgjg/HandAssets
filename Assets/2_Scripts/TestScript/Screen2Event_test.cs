using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class Screen2Event_test : MonoBehaviour
    {
        int lang;
        string gameMode;

        public CustomEvents.UnityEventHand onHandClick;
        protected Hand currentHand;

        public TMP_Text result_message;
        public Image result_background;

        public Canvas screen1;
        public Canvas screen2;

        public Button currentButtonObj;

        public TMP_InputField input_discount_code;
        public TMP_Text v_discount;
        public TMP_Text screen1_total_amount;
        public Button screen1_btn_dc;
        public Text v_errors;
        int errors;

        Boolean handHoverBegin_TF = false;
        Boolean handHoverEnd_TF = false;
        Boolean handHoverUpdate_TF = false;

        //-------------------------------------------------
        protected virtual void Awake(){
            Button button = GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        void Start(){
            lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
            gameMode = GameObject.Find("v_gameMode").GetComponent<Text>().text;
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:Screen2Event.cs");
        }

        //-------------------------------------------------
        protected virtual void OnHandHoverBegin(Hand hand){
            currentHand = hand;
            InputModule.instance.HoverBegin(gameObject);
            ControllerButtonHints.ShowButtonHint(hand, hand.uiInteractAction);
            //Logging
        }

        /**
        * @ Function : Screen1(Scan & Cancle) Touch Interaction Function
        * 
        * @ Author : Minjung KIM
        * @ Date : 2020.04.19
        * @ History :
        *   - 2020.04.19 Minjung Kim : Initial commit
        *   - 2020.05.03 Minjugn Kim : Remove checkbox interaction and Add cancel interacton
        *   - 2020.05.23 Minjung Kim : Add Event Log
        *   - 2020.06.16 Minjung Kim : Clear the disocunt code input field
        **/
        protected virtual void OnHandHoverEnd(Hand hand){
            InputModule.instance.HoverEnd(gameObject);
            ControllerButtonHints.HideButtonHint(hand, hand.uiInteractAction);

            // Change Screen2 to Screen1
            string key = currentButtonObj.tag;
            if (key.Equals("btn_back")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:btn_back()", tag);

                string org_text = input_discount_code.text;
                if (org_text.Length > 0){
                    string new_text = org_text.Substring(0, org_text.Length - 1);
                    input_discount_code.text = new_text;
                }

            // Applying the Discount code! Change Screen2 to Screen1
            }else if (key.Equals("btn_enter")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:btn_enter()", tag);

                // Todo: Levels of difficulty 
                result_message.text = "";
                if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){
                    // get Levels of difficulty
                    int v_level = Int32.Parse(GameObject.Find("v_level").GetComponent<Text>().text);
                    if (GlobalEnv.DISCOUNT_CODE_NORMAL.Equals(input_discount_code.text)){
                        v_discount.text = GlobalEnv.DISCOUNT_PRICE.ToString();
                        int total = Int32.Parse(screen1_total_amount.text);
                        screen1_total_amount.text = (total - GlobalEnv.DISCOUNT_PRICE).ToString();
                        result_message.text = LangText.screen2_succ_dc[lang];
                        result_message.color = Color.blue;
                        result_background.color = Color.white;
                        screen1_btn_dc.interactable = false;
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_SUCC, lang);
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen2:btn_back()", "msg:"+LangText.screen2_succ_dc[lang]+"|^|SUCC");
                        Invoke("ChangeScreen2toScreen1After1s", 1f);
                
                    }else{
                        // If the use does 3 errors, they will receive a full discount code.
                        result_message.text = LangText.screen2_fail_dc[lang];
                        result_message.color = Color.red;
                        result_background.color = Color.yellow;
                        input_discount_code.text = ""; // clear the input field
                        errors += 1;
                        v_errors.text = errors.ToString();
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen2:btn_back()", "msg:"+LangText.screen2_fail_dc[lang]+"|^|failed:"+errors);
                    }

                // TEST 
                } else {
                    if (GlobalEnv.DISCOUNT_CODE_TEST.Equals(input_discount_code.text)){
                        // Todo: wrong sound, text color
                        v_discount.text = GlobalEnv.DISCOUNT_PRICE.ToString();
                        int total = Int32.Parse(screen1_total_amount.text);
                        screen1_total_amount.text = (total - GlobalEnv.DISCOUNT_PRICE).ToString();
                        result_message.text = LangText.screen2_succ_dc[lang];
                        result_message.color = Color.blue;
                        result_background.color = Color.white;
                        screen1_btn_dc.interactable = false;
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_SUCC, lang);
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen2:btn_back()", "msg:"+LangText.screen2_succ_dc[lang]+ "|^|SUCC|^|gameMode:TEST");
                        Invoke("ChangeScreen2toScreen1After1s", 1f);
                
                    }else{
                        // If the use does 3 errors, they will receive a full discount code.
                        result_message.text = LangText.screen2_fail_dc[lang];
                        result_message.color = Color.red;
                        result_background.color = Color.yellow;
                        input_discount_code.text = ""; // clear the input field
                        errors += 1;
                        v_errors.text = errors.ToString();
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen2:btn_back()", "msg:"+LangText.screen2_fail_dc[lang]+"|^|failed:"+errors+ "|^|gameMode:TEST");
                    }
                }

            // SKIP button
            }else if (key.Equals("btn_skip")){
                input_discount_code.text = ""; // clear the input field
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:btn_skip()", "SKIP the d.c");
                Invoke("SkiptheDiscountCode", 1f);

            // @Screen2 Keyboard Button
            }else if (key.Equals("Key_1")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "1";

            }else if (key.Equals("Key_2")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "2";

            }else if (key.Equals("Key_3")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "3";

            }else if (key.Equals("Key_4")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "4";

            }else if (key.Equals("Key_A")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "A";

            }else if (key.Equals("Key_B")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "B";

            }else if (key.Equals("Key_C")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "C";

            }else if (key.Equals("Key_D")){
                 M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen2:"+key+"()", key);
                input_discount_code.text += "D";
            }

            currentHand = null;
        }

        /**
         * @ Function : Change Screen2 to SCreen1 with d.c 
         * 
         * @ Author : Minjung KIM
         * @ Date : 2020.04.19
         * @ History :
         *   - 2020.04.19 Minjung Kim : Initial commit
         *   - 2020.05.03 Minjung Kim : Add Discount code, cancel button
         **/
        void ChangeScreen2toScreen1After1s(){
            GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text = "Y";
            GameObject.Find("v_current_canvas").GetComponent<Text>().text = "screen1"; // block scan interaction when screen2 is on
            screen2.gameObject.SetActive(false);
            screen1.gameObject.SetActive(true);
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_CHANGE, "ChangeCanvas2toCanvas1After1s()", "Screen2 to Screen1");
        }

        /**
         * @ Function: Change Screen2 to Screen1 withouth d.c
         *
         * @ Author: Minjung KIM
         * @ Date : 2020.07.02
         */
        void SkiptheDiscountCode(){
            screen2.gameObject.SetActive(false);
            screen1.gameObject.SetActive(true);
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_CHANGE, "SkiptheDiscountCode()", "Screen2 to Screen1");
        }

        //-------------------------------------------------
        protected virtual void HandHoverUpdate(Hand hand){
            if (hand.uiInteractAction != null && hand.uiInteractAction.GetStateDown(hand.handType)){
                InputModule.instance.Submit(gameObject);
                ControllerButtonHints.HideButtonHint(hand, hand.uiInteractAction);
            }
        }

        //-------------------------------------------------
        protected virtual void OnButtonClick(){
            onHandClick.Invoke(currentHand);
        }
    }

#if UNITY_EDITOR
    //-------------------------------------------------------------------------
    [UnityEditor.CustomEditor(typeof(UIElement))]
    public class Screen2Editor_test : UnityEditor.Editor{
        //-------------------------------------------------
        // Custom Inspector GUI allows us to click from within the UI
        //-------------------------------------------------
        public override void OnInspectorGUI(){
            DrawDefaultInspector();
            UIElement uiElement = (UIElement)target;
            if (GUILayout.Button("Click")){
                InputModule.instance.Submit(uiElement.gameObject);
            }
        }
    }
#endif
}
