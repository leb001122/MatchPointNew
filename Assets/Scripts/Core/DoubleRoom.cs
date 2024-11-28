using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DoubleRoom : Singleton<DoubleRoom>
{
    public GameObject meetingResultPanel;
    
    public GameObject defaultPanel;

    private void Start()
    {
        meetingResultPanel.SetActive(false);
        defaultPanel.SetActive(false);
    }

    private void OnEnable()
    {
        meetingResultPanel.SetActive(false);
        defaultPanel.SetActive(false);
    }


    public void Enter()
    {
        ShowDefaultPanel();
    }

    public void ShowDefaultPanel()
    {
        defaultPanel.SetActive(true);
    }
    

    public void ShowBalanceGameListButton() // 밸런스게임 목록 버튼
    {
        defaultPanel.SetActive(false);
    }

    public void CloseBalanceGameListButton() // back 버튼 
    {
        defaultPanel.SetActive(true);
    }
    


    public void Quit()  // Quit Button Clicked 
    {
        Debug.Log("Exit, 결과 UI 보여주기");
        MeetingResult();
    }

    public void GoToWorld()  // 결과 UI 보여준 후 World Button Clicked
    {
        Debug.Log("Go to world");
        meetingResultPanel.SetActive(false);

        GameManager.Instance.EnterWorld();
    }
    
    
    
        
    private void MeetingResult()    
    {
        // todo 미팅을 종료합니다 UI 필요
        
        meetingResultPanel.SetActive(true);
        defaultPanel.SetActive(false);
    }
}
