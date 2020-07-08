using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScannerEvent : MonoBehaviour
{
    int lang;

    public TMP_Text result_message; // Screen1 > Result_message
    public Image result_background; // Screen1 > Result_background

    public TMP_Text v_total;
    public TMP_Text v_discount;
    public Text v_firstItem_yn;
    bool firstItem_tf = false;
    public TMP_Text total_amount;
    bool update_tf = false;

    Dictionary<string, string> price_list   = new Dictionary<string, string>();       // key(item_name), value(price)
    Dictionary<string, string> item_list    = new Dictionary<string, string>();        // key(item_code), value(item_name)
    Dictionary<string, string> canceled_list    = new Dictionary<string, string>();    // key(item_code), value(item_name)
    Dictionary<string, string> scanned_list = new Dictionary<string, string>();     // key(item_name), value(qty)
    public Dictionary<string, string> dupcheck_scanned_list = new Dictionary<string, string>(); // key(item_code), value(item_name)

    public GameObject leftHands;
    public GameObject rightHand;
    public Collider scanner;

    void Awake(){

        // Initial shopping list total 39, budget 28
        // enssential 28 - disocunt 3 = 25

        // ------------------------------------------------
        // Real
        // ------------------------------------------------
        price_list.Add("Tomato", "2"); // essential stuff
        item_list.Add("Tomato1", "Tomato");
        price_list.Add("Baguette", "10");
        item_list.Add("Baguette1", "Baguette");
        price_list.Add("Cheese", "4");
        item_list.Add("Cheese1", "Cheese");
        item_list.Add("Cheese2", "Cheese"); 
        price_list.Add("Cabbage", "8");
        item_list.Add("Cabbage1", "Cabbage");
        price_list.Add("Coffee", "6"); // not essential stuff
        item_list.Add("Coffee1", "Coffee");
        price_list.Add("Pumpkin", "5");
        item_list.Add("Pumpkin1", "Pumpkin");

        // ------------------------------------------------
        // test
        // ------------------------------------------------
        price_list.Add("Juice", "2");
        item_list.Add("Juice1", "Juice");
        price_list.Add("Apple", "10");
        item_list.Add("Apple1", "Apple");
        price_list.Add("Chip", "6");
        item_list.Add("Chip1", "Chip");
    }

    void Start()
    { 
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Scanner"), true);
        // Physics.IgnoreCollision(leftHands.GetComponent<Collider>(), scanner, true);
        // Physics.IgnoreCollision(rightHand.GetComponent<Collider>(), scanner, true);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:ScannerEvent.cs");
    }

    /**
     * Update Screen information
     */
    void Update(){
        int p_discount = 0;
        
        // Applying discount code
        string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
        if (discount_auth_yn.Equals("Y")){
            p_discount = GlobalEnv.DISCOUNT_PRICE;
            v_discount.color = Color.red;
        }

        // Update counting 
        GameObject.Find("v_scanned_item_cnt").GetComponent<Text>().text = scanned_list.Count.ToString();

        // Update item info
        if (update_tf == true){
        
            // 1. Reset all information on the self-checkout screen
            ClearAllInformation();

            // 3. Update items list
            int i = 1;
            int p_total = 0;
            foreach (KeyValuePair<string, string> kv in scanned_list){
                string o_item_name  = kv.Key;
                string o_item_qty   = kv.Value;
                string o_item_price = price_list[o_item_name];

                // Notify the first calling
                if(firstItem_tf == false && i == 1){
                    v_firstItem_yn.text = "Y";
                    firstItem_tf = true;
                }

                // GameObject.Find("checkbox"+i).GetComponent<Toggle>().interactable = true;
                GameObject.Find("checkbox"+i).GetComponent<Toggle>().isOn = true;
                GameObject.Find("item_name"+i.ToString()).GetComponent<TextMeshProUGUI>().text      = o_item_name;
                GameObject.Find("item_qty"+ i.ToString()).GetComponent<TextMeshProUGUI>().text      = o_item_qty;
                GameObject.Find("item_price"+ i.ToString()).GetComponent<TextMeshProUGUI>().text    = o_item_price;
                p_total += Int32.Parse(o_item_qty) * Int32.Parse(o_item_price);
                i++;
            }
            v_total.text = p_total.ToString();

            // 4. Update total price Info
            // Todo: 다시 아이템 제거할 때 총 금액이 마이너스 안되도록
            total_amount.text = (p_total-p_discount).ToString();
        }

        // Update
        update_tf = false;
    }

    /**
     * @Function: Initialize the self-checkout screen
     * @UI Format: L1{checkbox1, item_name1, item_qty1, item_price1}
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Mar.25
     * @History: 
     *  - 2020.03.25 Minjung KIM: Initial commit
     */
    void ClearAllInformation(){
        for(int i=1; i<7; i++){
            GameObject.Find("checkbox"+i).GetComponent<Toggle>().interactable = false;
            GameObject.Find("checkbox"+i).GetComponent<Toggle>().isOn = false;
            GameObject.Find("item_name"+i).GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("item_qty"+i).GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("item_price"+i).GetComponent<TextMeshProUGUI>().text = "";
        }
        GameObject.Find("total_amount").GetComponent<TextMeshProUGUI>().text = "0";
    }
    
    /**
     * @Function: Item Scanner Function
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Mar.17 
     * @History: 
     *  - 2020.03.17 Minjung KIM: 최초 작성 (1개 타입의 종류만 되도록 개발)
     *  - 2020.03.18 Minjung KIM: 여러 개의 오브젝트가 스캔되도록 개발
     *  - 2020.04.19 Minjung KIM: Block scan interaction when the canvas = screen2 and discount_auth_yn = Y
     *  - 2020.05.03 Minjugn KIM: Allow scan interaction when the user touched cancel buttton
     *  - 2020.05.23 Minjung KIM: Add Event Log
     *  - 2020.06.01 Minjung KIM: Bugfix qty Counting Error
     *  - 2020.07.01 Minjung KIM: Allow the scan anytime
     *  - 2020.07.08 Minjung KIM: Add scanned item name to result_message
     */
     void OnTriggerEnter(Collider collision) {
    // void OnCollisionEnter(Collision collision){
        string o_item_code = collision.gameObject.name;  // unique item code: ex) A1
        string o_item_name = collision.gameObject.tag;   // item name: ex) Apple
        string current_canvas = GameObject.Find("v_current_canvas").GetComponent<Text>().text;
        string cancel_yn = GameObject.Find("v_cancel_yn").GetComponent<Text>().text;

        if ( !o_item_name.Equals("Untagged") && current_canvas.Equals("screen1") && !o_item_name.Equals("card")){
            Debug.Log("ScannerEvent() o_item_code:" + o_item_code);
            Debug.Log("ScannerEvent() o_item_name:" + o_item_name);

            // ----------------------------------
            // 1. Scan to Add items
            // ----------------------------------
            if(!cancel_yn.Equals("Y")){ 

                // 1-1 Allow only unscanned item
                if(!dupcheck_scanned_list.ContainsKey(o_item_code)){

                    // 1.1. Update Message
                    result_message.text = o_item_name + " is added.";
                    result_message.color = Color.black;
                    result_background.color = Color.white;
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCAN_ADD, o_item_name, "msg:"+o_item_name+" is added.");

                    // 1-2. Update list
                    int qty = 1;
                    if(scanned_list.ContainsKey(o_item_name)){
                        qty = Int32.Parse(scanned_list[o_item_name])+1;
                    }
                    scanned_list[o_item_name] = qty.ToString();
                    dupcheck_scanned_list.Add(o_item_code, o_item_name);

                    // 1-3. Play the beep sound
                    SoundManager.instance.PlaySound(GlobalEnv.SOUND_SCANNED, 99);

                    // 1-4. Event logging
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_ADD, o_item_name, "item_code:"+o_item_code);

                }

            // ----------------------------------
            // 2. Scan to remove items
            // ----------------------------------
            }else{

                // 2-1. Allow only scanned items & Blocking already canceled items
                if (scanned_list.ContainsKey(o_item_name) && !canceled_list.ContainsKey(o_item_code)){

                    // 2-2. Update Message
                    result_message.text = o_item_name + " is removed.";
                    result_message.color = Color.red;
                    result_background.color = Color.white;
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCAN_REMOVE, o_item_name, "msg:" + o_item_name + " is removed.");

                    // 2-3. Update list
                    int o_qty = Int32.Parse(scanned_list[o_item_name])-1;
                    if(o_qty <= 0){
                        scanned_list.Remove(o_item_name);
                    }else{
                        scanned_list[o_item_name] = o_qty.ToString();
                    }
                    canceled_list.Add(o_item_code, o_item_name);
                    dupcheck_scanned_list.Remove(o_item_code);

                    // 2-4. Play beep sound
                    SoundManager.instance.PlaySound(GlobalEnv.SOUND_SCANNED, 99);

                    // 2-5. Event logging
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_REMOVE, o_item_name, "item_code:"+o_item_code);

                }else{
                    // Todo
                    result_message.text = o_item_name+"는 없는 아이템입니다.";
                    result_message.color = Color.red;
                    result_background.color = Color.white;
                }
            }

            // ----------------------------------
            // 3. Request receipt update
            // ----------------------------------
            update_tf = true;
        }
    }
}