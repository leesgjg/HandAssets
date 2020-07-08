using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent( typeof( Interactable ) )]
	public class Screen1Event_test : MonoBehaviour
    {
        public CustomEvents.UnityEventHand onHandClick;
        protected Hand currentHand;

        int lang;
        string gameMode;

        public TMP_Text result_message;
        public Image result_background;

        public Canvas screen1;          // scan code & pay error
        public Canvas screen2_test;     // discount code: test
        public Canvas screen2_easy;     // discount code: easy
        public Canvas screen2_normal;   // discount code: normal
        public Canvas screen2_hard;     // discount code: hard
        public Canvas screen3;          // result
        public GameObject currentBtnObj;
        public GameObject btn_completed;
        public Text trying_to_pay_yn;

        bool discount_message_update = false;

		//-------------------------------------------------
		protected virtual void Awake(){
			Button button = GetComponent<Button>();
			if ( button ){
				button.onClick.AddListener( OnButtonClick );
			}
		}
        
        void Start(){
            lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
            gameMode = GameObject.Find("v_gameMode").GetComponent<Text>().text;
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:Screen1Event.cs");
        }

        void Update(){
            string discount_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
            if (discount_message_update == false && discount_yn.Equals("Y")){
                // result_message.text = "Please make a payment.";
                // result_message.color = Color.blue;
                discount_message_update = true;
            }
        }


		//-------------------------------------------------
		protected virtual void OnHandHoverBegin( Hand hand ){
			currentHand = hand;
			InputModule.instance.HoverBegin( gameObject );
			ControllerButtonHints.ShowButtonHint( hand, hand.uiInteractAction);
            //Logging
		}


        /**
        * @ Function : Screen1(Scan & Cancle) Touch Interaction Function
        * 
        * @ Author : Minjung KIM
        * @ Date : 2020.04.19
        * @ History :
        *   - 2020.04.19 Minjung KIM: Initial commit
        *   - 2020.05.03 Minjugn KIM: Remove checkbox interaction and Add cancel interacton
        *   - 2020.05.23 Minjung KIM: Add Event Log
        *   - 2020.06.01 Minjung KIM: Before payment, check item counting.
        *   - 2020.07.01 Minjung KIM: Add btn_discount_code button
        **/
        protected virtual void OnHandHoverEnd( Hand hand ){
			InputModule.instance.HoverEnd( gameObject );
			ControllerButtonHints.HideButtonHint( hand, hand.uiInteractAction);

            string tag = currentBtnObj.tag;
            string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
            int item_counting = Int32.Parse(GameObject.Find("v_scanned_item_cnt").GetComponent<Text>().text);

            // ----------------------------------
            // BTN_PAY
            // ----------------------------------
            if (tag.Equals("btn_pay")) {
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen1:btn_pay()", "After discount code");
                
                if (item_counting > 0) {
                    result_message.text = LangText.alert_tryingToPay[lang];
                    result_message.color = Color.red;
                    result_background.color = Color.yellow;
                    
                    trying_to_pay_yn.text = "Y";
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen1:btn_pay()", "msg:" + LangText.alert_tryingToPay[lang] + "|^|msg_type:스캔한 아이템 있음");
                } else {
                    result_message.text = LangText.screen1_noitem[lang];
                    result_message.color = Color.red;
                    result_background.color = Color.white;
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen1:btn_pay()", "msg:" + LangText.screen1_noitem[lang] + "|^|msg_type:no_item");
                }

            // ----------------------------------
            // BTN_DC
            // ----------------------------------
            }else if (tag.Equals("btn_dc")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen1:btn_cancel()", tag);

                string discount_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
                if (item_counting > 0) {
                    if(!discount_yn.Equals("Y"))
                    {
                        Invoke("ChangeCanvas1toCanvas2After1s", 1f);
                    }
                } else {
                    result_message.text = LangText.screen1_noitem[lang];
                    result_message.color = Color.red;
                    result_background.color = Color.white;
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen1:btn_pay()", LangText.screen1_noitem[lang] + "|^|msg_type:no_item");
                }

            // ----------------------------------
            // BTN_CANCEL
            // ----------------------------------
            }else if (tag.Equals("btn_cancel")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen1:btn_cancel()", tag);

                if (item_counting > 0){
                    result_message.text = LangText.screen1_cancel[lang];
                    result_message.color = Color.blue;
                    result_background.color = Color.white;

                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen1:btn_cancel()", "msg:"+LangText.screen1_cancel[lang]);
                    GameObject.Find("v_cancel_yn").GetComponent<Text>().text = "Y";
                    btn_completed.SetActive(true);

                }/*else{
                    result_message.text = "취소할 물건이 없습닏다..";
                    result_message.color = Color.red;
                }*/


            // ----------------------------------
            // BTN_CANCEL
            // ----------------------------------
            }else if (tag.Equals("btn_completed")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_TOUCH, "Screen1:btn_completed()", tag);
                result_message.text = LangText.alert_tryingToPay[lang];
                result_message.color = Color.blue;
                result_background.color = Color.white;

                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Screen1:btn_completed()", "msg:"+LangText.alert_tryingToPay[lang]);
                GameObject.Find("v_cancel_yn").GetComponent<Text>().text = "N";
                btn_completed.SetActive(false);
            }
            currentHand = null;
		}

        //-------------------------------------------------
        protected virtual void HandHoverUpdate( Hand hand ){
			// Debug.Log("Screen1Event() HandHoverUpdate");
			if ( hand.uiInteractAction != null && hand.uiInteractAction.GetStateDown(hand.handType) )
			{
				InputModule.instance.Submit( gameObject );
				ControllerButtonHints.HideButtonHint( hand, hand.uiInteractAction);
			}
        }

        /**
        * @ Function : Change Canvas After few seconds
        * 
        * @ Author : Minjung KIM
        * @ Date : 2020.04.19
        * @ History :
        *   - 2020.04.19 Minjung Kim : Initial commit
        *   - 2020.06.27 Minjung Kim : (1) Add todo list (levels of difficulty), (2) Add test screen
        **/
        private void ChangeCanvas1toCanvas2After1s()
        {
            // Debug.Log("Screen2Event.ChangeCanvas1toCanvas2After1s()");
            GameObject.Find("v_current_canvas").GetComponent<Text>().text = "screen2"; // block scan interaction when screen2 is on
            screen1.gameObject.SetActive(false);

            // todo: levels of difficulty
            screen2_normal.gameObject.SetActive(true);
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_CHANGE, "ChangeCanvas1toCanvas2After1s()", "Screen1 to Screen2");
        }

        //-------------------------------------------------
        protected virtual void OnButtonClick()
		{
			onHandClick.Invoke( currentHand );
		}
	}

#if UNITY_EDITOR
	//-------------------------------------------------------------------------
	[UnityEditor.CustomEditor( typeof( UIElement ) )]
	public class Screen1Editor_test : UnityEditor.Editor
	{
		//-------------------------------------------------
		// Custom Inspector GUI allows us to click from within the UI
		//-------------------------------------------------
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			UIElement uiElement = (UIElement)target;
			if ( GUILayout.Button( "Click" ) )
			{
				InputModule.instance.Submit( uiElement.gameObject );
			}
		}
	}
#endif
}
