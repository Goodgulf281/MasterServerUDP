using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


/*
 * This partial class adds UPD Masterserver capabilities
 * 
 * 
 */

namespace BeardedManStudios.Forge.Networking.Unity
{
    public partial class NetworkManager : MonoBehaviour
    {

        public void InitializeUDP(NetWorker networker, string masterServerHost = "", ushort masterServerPort = 15942, JSONNode masterServerRegisterData = null)
        {

            Debug.Log("NetworkManager:InitializeUDP()");

            Networker = networker;
            networker.objectCreated += CreatePendingObjects;
            Networker.binaryMessageReceived += ReadBinary;
            SetupObjectCreatedEvent();

            UnityObjectMapper.Instance.UseAsDefault();
            NetworkObject.Factory = new NetworkObjectFactory();

            if (Networker is IServer)
            {
                if (!string.IsNullOrEmpty(masterServerHost))
                {
                    _masterServerHost = masterServerHost;
                    _masterServerPort = masterServerPort;

                    RegisterOnMasterServerUDP(masterServerRegisterData);
                }

                Networker.playerAccepted += PlayerAcceptedSceneSetup;

#if FN_WEBSERVER
				string pathToFiles = "fnwww/html";
				Dictionary<string, string> pages = new Dictionary<string, string>();
				TextAsset[] assets = Resources.LoadAll<TextAsset>(pathToFiles);
				foreach (TextAsset a in assets)
					pages.Add(a.name, a.text);

				webserver = new MVCWebServer.ForgeWebServer(networker, pages);
				webserver.Start();
#endif
            }
        }


        private void RegisterOnMasterServerUDP(JSONNode masterServerData)
        {

            Debug.Log("NetworkManager:RegisterOnMasterServerUDP()");

            // The Master Server communicates over TCP
            UDPClient client = new UDPClient();

            // Once this client has been accepted by the master server it should send it's get request
            client.serverAccepted += (sender) =>
            {

                Debug.Log("NetworkManager:MasterServer server Accepted");

                try
                {
                    //Text temp = Text.CreateFromString(client.Time.Timestep, masterServerData.ToString(), true, Receivers.Server, MessageGroupIds.MASTER_SERVER_REGISTER, true);
                    Text temp = Text.CreateFromString(client.Time.Timestep, masterServerData.ToString(), false, Receivers.Server, MessageGroupIds.MASTER_SERVER_REGISTER, true);

                    //Debug.Log(temp.GetData().Length);
                    // Send the request to the server
                    Debug.Log("MasterServer sending data");
                    client.Send(temp,true);

                    Networker.disconnected += s =>
                    {
                        Debug.Log("MasterServer client disconnected");
                        client.Disconnect(false);
                        MasterServerNetworker = null;
                    };
                }
                catch
                {
                    Debug.Log("MasterServer client failed");

                    // If anything fails, then this client needs to be disconnected
                    client.Disconnect(true);
                    client = null;
                }
            };

            Debug.Log("NetworkManager: MasterServer server Connecting... to "+_masterServerHost+":"+_masterServerPort);

            client.Connect(_masterServerHost, _masterServerPort);

            Networker.disconnected += NetworkerDisconnected;
            MasterServerNetworker = client;
        }



    }

}
