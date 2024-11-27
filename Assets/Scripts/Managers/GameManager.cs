using System;
using Fusion;
using Network;
using UI;
using UnityEngine;

public enum GameState
{
    Login, World, Group, Double
}

public class GameManager: Singleton<GameManager>
{
    public GameObject BalanceGameSettingButton;
    public GameState GameState = GameState.Login;
    
    public static NetworkController NetworkController;
    private const string BaseURI = "https://eternal-leopard-hopelessly.ngrok-free.app";

    [Header("NetworkRunner")]
    public Transform worldSpawnPosition;
    public GameObject worldRunnerPrefab;
    
    public Transform groupRoomSpwanPosition;
    public GameObject groupRoomRunnerPrefab;
    
    public GameObject doubleRoomRunnerPrefab;

    public LoadingCanvas loadingCanvas;

    private void Awake()
    {
        NetworkController = new NetworkController(new HttpClient(BaseURI));
    }


    // 월드 접속 
    public async void EnterWorld()
    {
        GameState = GameState.World;
        
        var args = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = "World",
            PlayerCount = 20,
        };

        // session 생성/접속 및 플레이어 스폰
        await SessionManager.Instance.StartSessionAsync(args, worldRunnerPrefab);
        
        World.Instance.Enter();
    }
    
    
    
// 그룹 미팅 룸 조인 
    public async void EnterGroupRoom(RoomInfo roomInfo)
    {
        await loadingCanvas.Show("접속중...");
        GameState = GameState.Group;
        
        // BalanceGameSettingButton.SetActive(false);
        
        var args = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = roomInfo.roomId,
            PlayerCount = roomInfo.maxPlayerCount,
        };

        await SessionManager.Instance.StartSessionAsync(args, groupRoomRunnerPrefab);

        await loadingCanvas.Hide();

        GroupRoom.Instance.Enter(roomInfo);
    }
    
    
    

// 1:1 미팅 룸 조인        
    public async void EnterDoubleRoom(string myId, string otherId)
    {
        Debug.Log("GameManager.EnterDoubleRoom()");
        GameState = GameState.Double;

        string[] arr = { myId, otherId };
        Array.Sort(arr);
        string roomId = $"{arr[0]}_{arr[1]}";
        
        var args = new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = roomId,
            PlayerCount = 2,
        };

        await SessionManager.Instance.StartSessionAsync(args, doubleRoomRunnerPrefab);

        DoubleRoom.Instance.Enter();
    }
}
