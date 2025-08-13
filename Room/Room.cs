using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GorillaNetworking;
using Photon.Pun;
using VioletFree.Mods.Spammers;

namespace VioletFree.Mods.Room
{
    class Room
    {
        public static void Disconect()
        {
            PhotonNetwork.Disconnect();
        }

        public static void QuitApplication()
        {
            UnityEngine.Application.Quit();
        }

        public static void JoinRoom(string Name)
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(Name.ToUpper(), GorillaNetworking.JoinType.Solo);
        }
    }
}
