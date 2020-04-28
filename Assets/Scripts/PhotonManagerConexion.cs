using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.UtilityScripts
{
    public class PhotonManagerConexion : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            //Photon.ConnectUsingSettings();
        }

        // ...
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN.");
            PhotonNetwork.JoinRandomRoom();
        }
        // ...


    }

}