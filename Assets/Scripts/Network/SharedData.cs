using System.Collections.Generic;
using Fusion;
using Player;
using UnityEngine;

public class SharedData: NetworkBehaviour
{
    public static SharedData Instance;

    [Networked] 
    public string UserId { get; private set; }
    
    [Networked] 
    public string UserName { get; private set; }
    
    [Networked]
    public string Gender { get; private set; }
    
    
    public static bool Trigger { get; private set; } = false;

    public static bool VoteDoneTrigger { get; private set; } = false;
    public static bool BalanceGameTrigger { get; private set; } = false;
    
    
    // 사랑의 작대기 key: 선택하는사람, value: 선택된사람 UserId
    public static Dictionary<string, string> LoveDict { get; } = new();
    
    
    private void Awake()    
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
    }

    public override void Spawned()
    {
        Debug.Log("SharedData Spawned");
        RunnerController.AddSharedData(this);

        if (HasStateAuthority)
        {
            CameraController.Instance.SetWorldCamera(transform); 
            Instance = this;
        
            GetComponent<NetworkTransform>().enabled = true;
            
            string id = PlayerData.Instance.UserId;
            string name = PlayerData.Instance.Profile.user.name;
            string gender = PlayerData.Instance.Profile.user.gender;
            SetUser(id, name, gender);
        }
        AvatarSetter.Instance.SetAvatar(transform, UserId);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("SharedData Despawned");

        RunnerController.RemoveSharedData(this);
    }


    public void SetUser(string id, string name, string gender)
    {
        Debug.Log("SetUser");
        if (HasStateAuthority)
        {
            UserId = id;
            UserName = name;
            Gender = gender;
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcSetTrigger(bool flag)
    {
        Trigger = flag;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcSetVoteDoneTrigger(bool flag)
    {
        VoteDoneTrigger = flag;
    }
    

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcSetBalanceGameTrigger(bool flag)
    {
        BalanceGameTrigger = flag;
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcVote(string otherId)
    {
        Debug.Log($"RpcVote: {otherId}");
        LoveDict[UserId] = otherId; 
    }
    
    
}
