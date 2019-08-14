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
        if (controlScript == null)
            controlScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
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
                AudioSource audio = GetComponent<AudioSource>();
                if (audio != null)
                {
                    GameControl.audioSource.volume = audio.volume;
                    GameControl.audioSource.pitch = audio.pitch;
                    GameControl.audioSource.PlayOneShot(audio.clip);
                }
                switch (type)
                {
                    case "Play":
                        controlScript.OnPlaySlash();
                        transform.parent.Find("Explosion").gameObject.SetActive(true);
                        transform.parent.Find("Ball").gameObject.SetActive(false);
                        break;

                    case "Weapon Select":
                        controlScript.OnWeaponSlash();
                        transform.parent.Find("Explosion").gameObject.SetActive(true);
                        transform.parent.Find("Ball").gameObject.SetActive(false);
                        break;

                    case "Exit":
                        controlScript.OnExitSlash();
                        transform.parent.Find("Explosion").gameObject.SetActive(true);
                        transform.parent.Find("Ball").gameObject.SetActive(false);
                        break;

                    case "DUAL SWORDS":
                        controlScript.OnDualSwordSlash();
                        break;

                    case "SWORD AND SHIELD":
                        controlScript.OnSwordAndShieldSlash();
                        break;

                    case "Back":
                        controlScript.OnBackSlash();
                        break;

                     

                }
                break;

        }

    }
}
