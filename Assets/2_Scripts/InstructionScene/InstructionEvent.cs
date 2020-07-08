using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class InstructionEvent : MonoBehaviour
{
    int lang;
    int currentPage;

    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    VideoClip[] videoClips;
    public TMP_Text text_title;
    public TMP_Text text_subTitle;
    public TMP_Text text_description;
    public GameObject txt_loading;

    private void Awake(){
        Screen.SetResolution(2160, 1080, true);
    }

    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        currentPage = 0;
        videoClips = Resources.LoadAll<VideoClip>("Video");
        SetInstructionVideoClips("");
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "InstructionEvent.cs");
    }

    /**
     * @Function: Play Instruction Video clips
     * 
     * @Author: Minjung KIM
     * @Date: 2020.05.25
     * @History: 
     *  - 2020.05.25 Minjung KIM: iniitial commit
     *  - 2020.06.03 Minjung KIM: Change Function Image to Video
     */
    private bool SetInstructionVideoClips(
        string btn_name
    ){
        try{
            if("prev".Equals(btn_name)){
                currentPage--;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "BtnPrev()", "page:"+currentPage.ToString());

            }else if("next".Equals(btn_name)){
                currentPage++;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "BtnNext()", "page:"+currentPage.ToString());
            }
            text_title.text     = LangText.instruction_title[currentPage, lang];
            text_subTitle.text  = LangText.instruction_subTitle[currentPage, lang];
            text_description.text = LangText.instruction[currentPage, lang]; //[page,lang]
            StartCoroutine(PlayVideo());
            return false;
        }catch{
            return true;
        }
    }

    IEnumerator PlayVideo(){
        videoPlayer.Prepare();
        videoPlayer.clip = videoClips[currentPage];
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        while (!videoPlayer.isPrepared){
            txt_loading.SetActive(true);
            yield return waitForSeconds;
            break;
        }
        txt_loading.SetActive(false);
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        // audioSource.Play();
    }

    private void BtnPrev(){
        bool lastpageTF = SetInstructionVideoClips("prev");
        if(lastpageTF == true){
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_CLICK, "BtnPrev()", "LAST_PAGE:1_IntroScene");
            SceneManager.LoadScene("1_IntroScene");
        }
    }
    
    private void BtnNext(){
        bool lastpageTF = SetInstructionVideoClips("next");
        if (lastpageTF == true){
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_CLICK, "BtnNext()", "LAST_PAGE:4_MartScene");
            SceneManager.LoadScene("4_MartScene");
        }
    }
}
