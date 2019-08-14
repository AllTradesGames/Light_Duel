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
    public static AudioSource audioSource;

    private NetworkManager mgr = null;
    private NetWorker server;

    private Coroutine findWifiHost = null;
    private bool alreadyClient = false;

    private int health = 10;

    private Transform right;
    private Transform left;
    private string selectedWeapon = "SWORD";

    private int readyPlayers = 0;
    private MoveHead myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ShowMenu(0);

        Application.runInBackground = true;

        mgr = GetComponent<NetworkManager>();

        // Do any firewall opening requests on the operating system
        NetWorker.PingForFirewall(portNumber);

        if (useMainThreadManagerForRPCs)
            Rpc.MainThreadRunner = MainThreadManager.Instance;
        
        NetWorker.localServerLocated += LocalServerLocated;
        findWifiHost = StartCoroutine(CheckForLocalHost());
    }

    private void LocalServerLocated(NetWorker.BroadcastEndpoints endpoint, NetWorker sender)
    {
        // Stop coroutine doesn't work here for SOME DUMB REASON
        // StopCoroutine(findWifiHost);
        if (!alreadyClient)
        {
            Debug.Log("Found local server endpoint: " + endpoint.Address + ":" + endpoint.Port);
            ConnectToHost(endpoint.Address, endpoint.Port);
        }
        StopCoroutine(findWifiHost);
    }

    public void Host()
    {
        server = new UDPServer(64);
        
        ((UDPServer)server).Connect(GetLocalIPAddress(), portNumber);

        server.playerTimeout += (player, sender) =>
        {
            Debug.Log("Player " + player.NetworkId + " timed out");
        };
        server.playerConnected += (player, sender) =>
        {
            Debug.Log("Player " + player.NetworkId + " connected");
        };
        //LobbyService.Instance.Initialize(server);
        Connected(server);
    }

    public void ConnectToHost(string ipAddress, ushort port)
    {
        NetWorker client = new UDPClient();
        Debug.Log("Connecting");
        ((UDPClient)client).Connect(ipAddress, port);
        Debug.Log("Connected");
        alreadyClient = true;
        Connected(client);
    }

    public void Connected(NetWorker networker)
    {
        Debug.Log("Connected as: " + (networker is IServer ? "host" : "client"));
        if (!networker.IsBound)
        {
            Debug.LogError("NetWorker failed to bind");
            return;
        }

        Debug.Log("Initialize mgr");
        mgr.Initialize(networker, string.Empty, 15940, null);

        if (networker is IServer)
        {
            isHost = true;
            NetworkObject.Flush(networker); //Called because we are already in the correct scene!
            Debug.Log("Instantiate Player Server");
            mgr.InstantiateMovementHead(0, new Vector3(2f, 2.5f, -31.26f), Quaternion.Euler(Vector3.zero));
            menuItems[0].transform.parent.position = new Vector3(2f, 2.5f, -31.26f);
            menuItems[0].transform.parent.rotation = Quaternion.Euler(Vector3.zero);
            right = GameObject.FindGameObjectWithTag("right").transform;
            left = GameObject.FindGameObjectWithTag("left").transform;
        }
        else
        {
            Debug.Log("Set accepted event Client");
            networker.serverAccepted += OnAccepted;
        }
    }

    public void OnAccepted(NetWorker sender)
    {
        NetworkObject.Flush(sender);
        MainThreadManager.Run(() => {
            Debug.Log("Instantiate Player Client");
            mgr.InstantiateMovementHead(0, new Vector3(2f, 2.5f, 11.5f), Quaternion.Euler(new Vector3(0f, 180f, 0f)));
            menuItems[0].transform.parent.position = new Vector3(2f, 2.5f, 11.5f);
            menuItems[0].transform.parent.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            right = GameObject.FindGameObjectWithTag("right").transform;
            left = GameObject.FindGameObjectWithTag("left").transform;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlaySlash()
    {
        HideMenu(0);
        myPlayer = GameObject.FindGameObjectWithTag("Player").transform.root.GetComponent<MoveHead>();
        myPlayer.SendReadyRPC();
    }

    public void OnWeaponSlash() 
    {
        Debug.Log("Pick your weapon");
        HideMenu(0);
        ShowMenu(1);
    }

    public void OnExitSlash()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void OnDualSwordSlash()
    {
        Debug.Log("You picked dual wielding like a scrub");
        right.Find("Viking sword").gameObject.SetActive(true);
        left.Find("Viking sword").gameObject.SetActive(true);
        right.Find("Shield").gameObject.SetActive(false);
        left.Find("Shield").gameObject.SetActive(false);
        selectedWeapon = "DUALSWORD";

    }

    public void OnSwordAndShieldSlash()
    {
        Debug.Log("coward");
        if (right.Find("Shield").gameObject.active || right.Find("Viking sword").gameObject.active && left.Find("Viking sword").gameObject.active)
        {
            right.Find("Viking sword").gameObject.SetActive(true);
            left.Find("Viking sword").gameObject.SetActive(false);
            left.Find("Shield").gameObject.SetActive(true);
            right.Find("Shield").gameObject.SetActive(false);
        } else if (left.Find("Shield").gameObject.active)
        {
            left.Find("Viking sword").gameObject.SetActive(true);
            right.Find("Viking sword").gameObject.SetActive(false);
            right.Find("Shield").gameObject.SetActive(true);
            left.Find("Shield").gameObject.SetActive(false);
        }
        selectedWeapon = "SWORD";
    }

    public void OnBackSlash()
    {
        HideMenu(1);
        ShowMenu(0);
    }

    public void OnOpponentFound()
    {
        readyPlayers++;
        Debug.Log("opponents ready "+ readyPlayers);
        if (readyPlayers > 1 && isHost)
        {
            comboScript.StartCombo(selectedWeapon);
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
        for (int ii = 0; ii < 3; ii++)
        {
            NetWorker.RefreshLocalUdpListings(portNumber);
            yield return new WaitForSeconds(1);
        }
        if (!alreadyClient)
            Host();
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
