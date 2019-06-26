using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    public bool isHost = false;
    public int playerID;
    public GameObject[] menuItems;
    public ComboController comboScript;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu(0);
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
        // Decide who is the host
        isHost = true;
        OnOpponentFound();
    }

    public void OnOpponentFound()
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


}
