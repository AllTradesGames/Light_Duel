using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectMenu : MonoBehaviour
{
    public string type;
    public GameControl controlScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "weapon":
                switch (type)
                {
                    case "Play":
                        controlScript.OnPlaySlash();
                        break;

                    case "Weapon Select":
                        controlScript.OnWeaponSlash();
                        break;

                    case "Exit":
                        controlScript.OnExitSlash();
                        break;

                }
                transform.parent.Find("Explosion").gameObject.SetActive(true);
                transform.parent.Find("Ball").gameObject.SetActive(false);
                break;

        }

    }
}
