using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  1. In order, it is Korean, French, and English.
 *  2. vairable name is SceneNameObjectType_detailFunction
 */ 
public class LangText : MonoBehaviour
{   
    // ------------------------------------
    // ADMIN SCENE
    // ------------------------------------
    public static string[] adminText_lang   = {"언어", "LANG", "LANG"};
    public static string[] adminText_name   = {"USER ID", "IDENTIFIANT", "USER ID"};
    public static string[] adminButton_home = {"적용 & 홈으로", "APPLIQUER ET ACCUEIL", "APPLY & HOME"};
    public static string[] adminText_level  = {"난이도", "NIVEAU", "LEVEL"};
    public static string[] adminToggle_easy = {"쉬움", "FACILE", "EASY"};
    public static string[] adminToggle_normal   = {"보통", "ORDINAIRE", "NORMAL"};
    public static string[] adminToggle_hard = {"어려움", "DIFFICILE", "HARD"};

    // ------------------------------------
    // INTRO SCENE
    // ------------------------------------
    public static string[] introText_instruction
        = {"본 실험은 VR 쇼핑 과제를 통해 일상 생활 능력을 평가하기 위해 만들어졌습니다. 본 연구 결과는 연구 이외의 목적으로 사용 되지 않을 것을 말씀드립니다.\n바쁘신 와중에 실험에 참여 해주셔서 감사합니다."
        , "Cette expérience a été créée pour évaluer les activités de la vie quotidienne (ADL) à travers des tâches d'achat VR.  Nous sommes sûrs que le résultat de cette étude ne sera pas utilisé à d'autres fins que des raisons personnelles.\nMerci d'avoir participé à cette expérience."
        , "This experiment was created to assess the activities of daily living(ADL) through VR shopping tasks. We are sure that the result of this study will not be used for any purpose other than personal reasons.\nThank you for participating in this experiment."};
    public static string[] introButton_test     = {"연습하기", "TESTER", "TRAINING" };
    public static string[] introButton_start    = {"시작하기", "DÉBUT", "START"};

    // ------------------------------------
    // INSTRUCTION SCENE [page, lang]
    // Img path: Resources > Img > Instruction
    // @Update
    //      - 2020.05.28 Minjung Kim: initial update.
    // ------------------------------------
    public static string[,] instruction_title = {
        /* 1p */ {
           "미션"
           ,""
           ,"Mission"} 

        /* How to use the Self-checkout machine? */
        /* 2p:Scanning an item1 */ ,{
           "셀프 계산대 사용 방법"
           ,""
           ,"How to use the Self-checkout machine?(1)"}
        /* 3p:Cancel an item */ ,{
           "셀프 계산대 사용 방법"
           ,""
           ,"How to use the Self-checkout machine?(2)"}
        /* 4p:Discount code */ ,{
           "셀프 계산대 사용 방법"
           ,""
           ,"How to use the Self-checkout machine?(3)"}
        /* 5p:Payment */ ,{
           "셀프 계산대 사용 방법"
           ,""
           ,"How to use the Self-checkout machine?(4)"}

       /* How to use the VR-device? */
       /* 6p:Grap and Release */ ,{
           "VR 장비는 어떻게 사용하나요?"
           ,""
           ,"How to use the VR-device?"}
       /* 7p:Text message */ ,{
           "VR 장비는 어떻게 사용하나요?"
           ,""
           ,"How to use the VR-device?"}

       /* Rules */
       /* 8p */ ,{
           "규칙"
           ,""
           ,"Rules"}
       /* 9p */ ,{
           "규칙"
           ,""
           ,"Rules"}
    };
    public static string[,] instruction_subTitle = {
        /* 1p: what is your goal? */ {
           "임무"
           ,""
           ,"what is your goal?"} 

        /* How to use the Self-checkout machine? */
        /* 2p:Scanning an item */ ,{
           "아이템 스캔하는 방법"
           ,""
           ,"Scanning an item"}
        /* 3p:Cancel an item */ ,{
           "아이템 취소하는 방법"
           ,""
           ,"Cancel an item"}
        /* 4p:Discount code */ ,{
           "할인코드 입력 방법"
           ,""
           ,"Enter the discount code"}
        /* 5p:Payment */ ,{
           "결제 방법"
           ,""
           ,"Payment"}

       /* How to use the VR-device? */
       /* 6p:Grap and Release */ ,{
           "물건 잡는 방법"
           ,""
           ,"Grap and Release"}
       /* 7p:Text message */ ,{
           "문자와 전화받는 방법"
           ,""
           ,"Call / Message"}

       /* Rules */
       /* 8p */ ,{
           "< 규칙(1) >"
           ,""
           ,"Rule(1)"}
       /* 9p */ ,{
           "< 규칙(2) >"
           ,""
           ,"Rule(2)"}
    };

    public static string[,] instruction = {
        /* 1p: what is your goal? */ {
           "친구들과 야외에서 샌드위치를 먹기 위해 쇼핑을 끝냈고, 당신은 결제만 남았습니다! \n친구가 보내준 메시지를 확인하여 성공적으로 계산을 완료해 주세요."
           ,"Vous venez de terminer vos achats pour manger des sandwichs à l'extérieur avec vos amis et vous n'avez laissé que le processus de paiement maintenant! Veuillez vérifier le message que votre ami a envoyé et effectuer le paiement à l'aide de la machine de paiement automatique."
           ,"You just finished shopping to eat sandwiches outside with your friends and you just left only the payment process now! Please check the message that your friend sent and complete the make a payment using the self-checkout machine."} 

        /* How to use the Self-checkout machine? */
        /* 2p:Scanning an item */ ,{
           "1. 구매하려는 물건을 왼쪽 테이블에 모두 올려둡니다. 그리고 하나씩 스캔한 후, 오른쪽 테이블에 올려둡니다. \n2. 화면에 표시된 아이템 이름과 금액을 확인 한 후, 결제 합니다."
           ,"1. Mettez tout ce que vous voulez acheter sur le tableau de gauche. Scannez ensuite un par un et placez-le sur la table de droite."
           ,"1. Put everything you want to buy on the left table. Then scan one by one and place it on the right table. \n2. Check the item name and amount displayed on the screen, then pay."}
        /* 3p:Cancel an item */ ,{
           "1. 화면에서 취소 버튼을 선택합니다. \n2. 오른쪽 테이블에 있던 아이템을 스캔한 뒤, 왼쪽 테이블에 올려둡니다. \n3. 더 이상 취소할 상품이 없으면, 결제 버튼을 눌러 결제를 완료합니다."
           ,"1. Appuyez sur le bouton Annuler à l'écran."
           ,"1. Touch the cancel button on the bottom of the screen. \n2. Scan an item then put on the left table. \n3. If there are no more items to cancel, Please touch the pay button to complete the payment."}
        /* 4p:Discount code */ ,{
           "할인코드가 있으면 화면을 터치하여 할인코드를 입력하세요."
           ,"Lorsque l'écran de paiement apparaît, touchez la carte sur le terminal."
           ,"If you have a discount code, touch the screen to enter the discount code."}
        /* 5p:Payment */ ,{
           "결제 화면이 나타나면 단말기에 카드를 터치하여 결제를 완료하세요. 카드는 왼쪽 손 핸드폰 뒤에 있습니다."
           ,"Lorsque l'écran de paiement apparaît, touchez la carte sur le terminal."
           ,"When the payment screen appears, touch the card on the terminal then complete the payment. \n<The card is on the behind of the left-hand phone.>"}

       /* How to use the VR-device? */
       /* 6p:Grap and Release */ ,{
           "물건을 잡거나 놓을려면 오른쪽 트리거 버튼을 누르세요."
           ,"Saisissez ou relâchez un élément à l'aide du bouton de déclenchement droit."
           ,"Press the right trigger button to grab or release an item."}
       /* 7p:Text message */ ,{
           "반대 손에는 휴대폰이 있어 문자나 전화를 확인할 수 있습니다."
           ,"Vous pouvez lire un message texte à l'aide du déclencheur gauche."
           ,"You can read a text message or taking a call using the opposite hand. \nIt will be automatically accepted."}

       /* Rules */
       /* 8p */ ,{
           "1. 구입하기 전, 구입할 모든 아이템은 왼쪽 테이블에 올려 두어야 합니다."
           ,"Avant d'acheter, tous les articles à acheter doivent être placés sur la table de gauche."
           ,"Before you buy, all items to be bought must be placed on the left table."}
       /* 9p */ ,{
           "2. 스캔은 한번에 한 개씩 해야 합니다. "
           ,"Vous ne devez numériser qu'un seul élément à la fois."
           ,"You should only scan one item at a time."}
    };
    // ------------------------------------
    // MART SCENE: Notification
    // ------------------------------------
    /* public static string[] martNoti_msg1 = {
        "집에 오는 길에 장 좀 봐줄 수 있어? 지금 샌드위치 만들고 있어: 바게트 1개, 토마토 1개, 양배추 1개, 치즈 2개, 커피 1개, 호박 1개 부탁해!"
        ,""
        ,"Could you get me some food on your way home? We are making some sandwiches now. We need: 1 Baguette, 1 Tomato, 1 Cabbage, 2 Cheese, 1 Box of coffee bean, 1 Pumkin. Thanks! "}; */
    public static string[] martNoti_msg1 = {
        "할인코드는 A1B1이야."
        ,""
        ,"Again! The code is A1B1."};
    public static string[] martNoti_msg2 = {
        "미안. 할인코드 알려주는걸 깜빡했어. 여기 상점 할인코드가 있는데 내일 만료되니까 꼭 사용해야 해. \n코드는 A1B1이야. 까먹지마 A1B1. \n그리고 카드 한도 "+GlobalEnv.BUDGET+"유로 밖에 안남았어! \n미안해. "
        ,""
        ,"Sorry. I've forgotton, \nI have a discount code for this shop and it will expire tomorrow. \nYou should use it. The code is A1B1. Do not forget A1B1!\n And I've only "+GlobalEnv.BUDGET+"Euros left on a card!\n sorry."};

    // ------------------------------------
    // MART SCENE: self-checkout
    // ------------------------------------
    public static string[] martText_self_before = {"1. 스캔 전","","1. Before scanning"};
    public static string[] martText_self_scan   = {"2. 스캔해주세요.","","2. Scan this way"};
    public static string[] martText_self_after  = {"3. 스캔 후","","3. After scanning"};
    public static string[] martText_self_card   = {"4. 카드","","4. CARD"};

    // ------------------------------------
    // MART SCENE: alert
    // ------------------------------------
    public static string[] alert_err =
    {
        "< 한도 초과 > \n\n 카드 한도가 초과했습니다."
        , ""
        , "< Credit insufficient > \n\nYou've exceeded your limit." //If you want to remove an article,\nClick the cancel button then\nscan it again and put it on the left.
    };
    public static string[] alert_succ =
    {
        "결제가 성공적으로 완료되었습니다."
        , ""
        , "Payment has been completed successfully!"
    };
    public static string[] alert_tryingToPay =
    {
        "카드를 단말기에 태그해주세요."
        , ""
        , "Please make a payment."
    };

    // ------------------------------------
    // MART SCENE: screen1
    // ------------------------------------
    public static string[] screen1_text_itemName    = {"상품명","","Name"};
    public static string[] screen1_text_itemQty = {"수량","","Qty"};
    public static string[] screen1_text_itemPrice   = {"가격","","Price"};
    public static string[] screen1_text_total   = {"합계","","Total"};
    public static string[] screen1_text_discount    = {"할인","","Discount"};

    public static string[] screen1_button_cancel    = {"취소","","CANCEL"};
    public static string[] screen1_button_pay   = {"결제","","PAY"};


    public static string[] screen1_scanning  = { 
        "구입할 물건을 왼쪽 테이블에 올려주세요."
        , ""
        , "Please put items to buy on the left table." 
    };

    public static string[] screen1_before = {
        "물건을 스캔한 뒤, 오른쪽 테이블에 올려주세요."
        ,""
        ,"Scan an item then put on the right table."
    };

    public static string[] screen1_after = {
        "결제 버튼을 눌러 결제를 시도해주세요."
        , ""
        , "Make a payment to touch the PAY button."
    };

    public static string[] screen1_cancel =  {
        "취소할 물건을 스캔하여 왼쪽 테이블에 올려주세요.\n취소할 물건이 없으면 완료 버튼을 누르세요."
        , ""
        , "Scan an item you need to cancel \nthen put it on the left table.\nAnd touch the COMPLETE button."
    };
    public static string[] screen1_noitem =
    {
        "구입할 물건을 스캔해주세요.",
        "",
        "Please, scan an item to buy."
    };

    // ------------------------------------
    // MART SCENE: screen2
    // ------------------------------------
    public static string[] screen2_enter_the_dc =  {
        "할인코드를 입력해주세요.\n(원치 않을 경우, 다음 버튼을 눌러주세요.)"
        , ""
        , "Please, Enter the Discount code. \n(If you want to skip, touch the SKIP button)"
    };
    public static string[] screen2_succ_dc = {
        "할인코드 적용이 완료되었습니다!"
        , ""
        , "Discount code has been applied!"
    };
    public static string[] screen2_fail_dc = {
        "할인코드를 다시 확인해주세요.."
        , ""
        , "Please check your discount code.."
    };
    public static string[] screen2_button_backspace = {"지우기","","back space"};
    public static string[] screen2_button_apply = {"적용","","APPLY"};

    // ------------------------------------
    // MART SCENE: screen3
    // ------------------------------------
    public static string[] screen3_result =  {
        "결제가 완료되었습니다. :) 또 만나요!"
        , ""
        , "Thank you :) Have a nice day!"
    };

    // ------------------------------------
    // END SCENE
    // ------------------------------------
    public static string[] ending_ment = {
        "실험이 종료되었습니다. 참여해주셔서 감사합니다."
        , ""
        , "Thank you for participating in the experiment."
    };
}
