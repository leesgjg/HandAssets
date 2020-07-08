using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnv : MonoBehaviour
{
    // -------------------------
    // LANG
    // -------------------------
    public static string GAMEMODE_TEST = "TEST";
    public static string GAMEMODE_START = "START";
    public static string GAMEMODE_ADMIIN = "ADMIN";

    // -------------------------
    // LANG
    // check: LangText.ccs
    // -------------------------
    public static string KR = "0";
    public static string FR = "1";
    public static string EN = "2";

    // -------------------------
    // SCAN LOG FILE NAME
    // -------------------------
    public static string SCAN_LOG = "SCAN_LOG";
    public static string CONTROLLER_LOG = "CONTROLLER_LOG";
    public static string CART_LOG = "CART_LOG";

    // -------------------------
    // STAGE INFO
    // -------------------------
    public static string STAGE_INTRO = "INTRO";
    public static string STAGE_ADMIN = "ADMIN";
    public static string STAGE_INSTRUC = "INSTRUCTION";
    public static string STAGE_PREPARE = "PREPARE";
    public static string STAGE_END = "END";

    public static string STAGE_1 = "PREPARING";
    public static string STAGE_2 = "SCANNING";
    public static string STAGE_3 = "DISCOUNT";
    public static string STAGE_4 = "DECISION";
    public static string STAGE_5 = "PAY";


    // -------------------------
    // ACTOR
    // https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
    // -------------------------
    public static string ACTOR_USER = "USER";
    public static string ACTOR_ADMIN = "ADMIN";
    public static string ACTOR_SYSTEM = "SYSTEM";
    
    // -------------------------
    // EVNET CATEGORY
    // https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
    // -------------------------
    public static string EVENT_CATE_SCENE  = "SCENE";
    public static string EVENT_CATE_ACT = "ACTION";
    public static string EVENT_CATE_SYS_MSG = "SYSTEM_MESSAGE";
    public static string EVENT_CATE_SCREEN = "SCREEN";

    // -------------------------
    // EVNET LOG TY
    // https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
    // -------------------------
    public static string EVENT_TYPE_START = "START";

    public static string EVENT_TYPE_CLICK = "CLICK";    // Scene
    public static string EVENT_TYPE_TOUCH = "TOUCH";    // VR Contents
    public static string EVENT_TYPE_CLICK_BUTTON = "BUTTON";

    public static string EVENT_TYPE_CHANGE = "CHANGE";

    public static string EVENT_TYPE_NOTI = "NOTIFICATION";
    public static string EVENT_TYPE_RESULT_MSG = "RESULT_MSG"; // Screen Message
    // public static string EVENT_TYPE_STAGE_CHANGE= "STAGE_CHANGE";
    
    public static string EVENT_TYPE_ACTION_GRAB = "GRAB";
    public static string EVENT_TYPE_ACTION_RELEASE = "RELEASE";

    public static string EVENT_TYPE_BEFORE_ENTER = "BEFORE_SCANNING_ENTER";
    public static string EVENT_TYPE_BEFORE_EXIT = "BEFORE_SCANNING_EXIT";

    public static string EVENT_TYPE_AFTER_ENTER = "AFTER_SCANNING_ENTER";
    public static string EVENT_TYPE_AFTER_EXIT = "AFTER_SCANNING_EXIT";
    
    public static string EVENT_TYPE_SCAN_ADD = "ADD";
    public static string EVENT_TYPE_SCAN_REMOVE = "REMOVE";
    
    public static string EVENT_TYPE_CODE_FAIL = "FAIL";
    public static string EVENT_TYPE_CODE_SUCC = "SUCC";

    public static string EVENT_TYPE_PAY = "PAY";

    // -------------------------
    // NOTICE MSG
    // -------------------------
    public static string del_msg = "Please scan what you want to cancel then put on the left table then try again to make a payment.";
    
    // -------------------------
    // SOUND NAME
    // -------------------------
    public static string SOUND_SUCC  = "SYS_SUCC";
    public static string SOUND_ERROR  = "SYS_ERROR";
    public static string SOUND_SCANNED  = "SCANNED";
    public static string SOUND_MESSAGE  = "MESSAGE";
    public static string SOUND_CALL     = "CALL";
    public static string SOUND_VOCAL    = "VACAL";

    // -------------------------
    // BUTTON INFO
    // -------------------------
    public static string BTN_NEXT       = "btn_next";
    public static string BTN_CONFIRM    = "btn_confirm";
    public static string BTN_APPLY      = "btn_apply";
    public static string BTN_PAY        = "btn_pay";
    public static string BTN_BACKSPACE  = "btn_back";
    public static string BTN_SKIP       = "btn_skip";

    // -------------------------
    // Screen Message
    // -------------------------
    public static string MSG_PAYMENT = "";

    // -------------------------
    // BUDGET INFO
    // -------------------------
    public static int BUDGET = 26;

    // -------------------------
    // For Discount Code
    // -------------------------
    public static string DISCOUNT_CODE_TEST  = "A1B1";

    public static string DISCOUNT_CODE_EASY = "A1B1";
    public static string DISCOUNT_CODE_HARD = "A1B1";
    public static string DISCOUNT_CODE_NORMAL = "A1B1";
    public static int DISCOUNT_PRICE    = 3;
}