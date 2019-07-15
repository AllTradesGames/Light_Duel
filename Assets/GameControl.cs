using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.SimpleJSON;
using BeardedManStudios.Forge.Networking.Generated;

public class GameControl : PlayerBehavior

{
    public GameObject[] attackPreFabs;
    public bool isHost = false;
    public int playerID;
    public GameObject[] menuItems;
    public ComboController comboScript;

    public ushort portNumber = 5555;
    public bool useMainThreadManagerForRPCs = true;
    public GameObject networkManager = null;

    private NetworkManager mgr = null;
    private NetWorker server;
    private int hostChecks = 0;

    private int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu(0);
  
        // Do any firewall opening requests on the operating system
        NetWorker.PingForFirewall(portNumber);

        if (useMainThreadManagerForRPCs)
            Rpc.MainThreadRunner = MainThreadManager.Instance;
        
        NetWorker.localServerLocated += LocalServerLocated;
        NetWorker.RefreshLocalUdpListings(portNumber);
    }

    private void LocalServerLocated(NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        Debug.Log("Found local server endpoint: " + endpoint.Address + ":" + endpoint.Port);
        StopCoroutine("CheckForLocalHost");
        ConnectToHost(endpoint.Address, endpoint.Port);
    }

    public void Host()
    {
        server = new UDPServer(64);
        
        ((UDPServer)server).Connect(GetLocalIPAddress(), portNumber);

        server.playerTimeout += (player, sender) =>
        {
            Debug.Log("Player " + player.NetworkId + " timed out");
        };
        //LobbyService.Instance.Initialize(server);
        isHost = true;
        Connected(server);
    }

    public void ConnectToHost(string ipAddress, ushort port)
    {
        NetWorker client = new UDPClient();
        ((UDPClient)client).Connect(ipAddress, port);
        Connected(client);
    }

    public void Connected(NetWorker networker)
    {
        if (!networker.IsBound)
        {
            Debug.LogError("NetWorker failed to bind");
            return;
        }

        if (mgr == null && networkManager == null)
        {
            Debug.LogWarning("A network manager was not provided, generating a new one instead");
            networkManager = new GameObject("Network Manager");
            mgr = networkManager.AddComponent<NetworkManager>();
        }
        else if (mgr == null)
            mgr = Instantiate(networkManager).GetComponent<NetworkManager>();

        mgr.Initialize(networker, string.Empty, 15940, null);

        if (networker is IServer)
        {
            NetworkObject.Flush(networker); //Called because we are already in the correct scene!
        }
        if (!isHost)
        {
            // TODO: Call OnOpponent found rpc
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlaySlash()
    {
        HideMenu(0);
        StartMatchmaking();
    }
    public void OnWeaponSlash()
    {
        Debug.Log("This doesnt exist either yet");
    }

    public void OnExitSlash()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void StartMatchmaking()
    {
        StartCoroutine("CheckForLocalHost");
    }

    private void OnOpponentFound()
    {
        if (isHost)
        {
            if (CoinFlip())
            {
                // We go first
                if (comboScript != null)
                {
                    comboScript.StartCombo("sword");
                }
            }
            else
            {
                // Opponent goes first
                // TODO: Call pass turn RPC
            }
        }
    }

    public bool CoinFlip()
    {
        return true;
    }

    public void EndMatch(int winningPlayerID)
    {
        if (winningPlayerID == playerID)
        {
            ShowYeetSign();
        } else
        {
            ShowNoobSign();
        }
    }

    public void ShowYeetSign()
    {

    }

    public void ShowNoobSign()
    {

    }

    public void ShowMenu(int menuID)
    {
        if (menuItems[menuID] != null)
        {
            menuItems[menuID].SetActive(true);
        }
    }

    public void HideMenu(int menuID)
    {
        if (menuItems[menuID] != null)
        {
            menuItems[menuID].SetActive(false);
        }
    }

    public string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                Debug.Log("Local ip address is: " + ip);
                return ip.ToString();
            }
        }

        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    private void OnApplicationQuit()
    {
        NetWorker.EndSession();

        if (server != null) server.Disconnect(true);
    }

    IEnumerator CheckForLocalHost()
    {
        NetWorker.RefreshLocalUdpListings(portNumber);
        if(++hostChecks > 6)
        {
            StopCoroutine("CheckForLocalHost");
            Host();
        }
        yield return new WaitForSeconds(1);
    }

    public void SpawnStab(Vector3 pos, 
    Quaternion rotation,
    float speed,
    int type,
    bool isLast)
    {
        networkObject.SendRpc(RPC_SPAWN_ATTACK, Receivers.All, pos, rotation, speed, type, isLast);
    }

    public override void SpawnAttack(RpcArgs args)
    {
        Vector3 pos = args.GetNext<Vector3>();
        Quaternion rotation = args.GetNext<Quaternion>();
        float  speed = args.GetNext<float>();
        int type = args.GetNext<int>();
        bool isLast = args.GetNext<bool>();
        GameObject attack = Instantiate(attackPreFabs[type], pos, rotation);
        AttackMovement attackScript = attack.GetComponent<AttackMovement>();
        attackScript.speed = speed;
        attackScript.isLast = isLast;

    }



    void TakeDamage(int amount)
    {
        networkObject.SendRpc(RPC_DECREASE_HEALTH, Receivers.All, amount);
    }

    public override void DecreaseHealth(RpcArgs args)
    {
        // TODO which player takes the damage?
        int damage = args.GetNext<int>();
        Debug.Log("ouch i took damage");
        health -= damage;
        if (health<1)
        {

            networkObject.SendRpc(RPC_I_DIED, Receivers.All);
        }
    }

    public override void IDied(RpcArgs args)
    {
        Debug.Log("I died");
    }
}
