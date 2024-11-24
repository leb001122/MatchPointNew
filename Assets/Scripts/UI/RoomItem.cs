﻿using TMPro;
using UnityEngine;

namespace UI
{
    public class RoomItem: MonoBehaviour
    {
        public TMP_Text roomNameText;
        public TMP_Text playerCountText;
        public TMP_Text captainNameText;
        
        public RoomInfo roomInfo;

        public void SetRoomItem(RoomInfo roomInfo)
        {
            this.roomInfo = roomInfo;
            
            captainNameText.text = roomInfo.captainName;
            roomNameText.text = roomInfo.roomName;
            playerCountText.text = $"{roomInfo.currentCount}/{roomInfo.maxPlayerCount}";
        }
    }
}