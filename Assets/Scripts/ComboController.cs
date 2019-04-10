using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Target
{
    public Vector3 location;
    public float shoot;
    public int type;
    public Target(Vector3 inLocation, float inShoot, int inType)
    {
        this.location = inLocation;
        this.shoot = inShoot;
        this.type = inType;


    }
}

public class ComboController : MonoBehaviour
{
    public GameObject[] targetPreFabs;

    private float comboTimer;
    private int currentTarget;
    private List<Target> currentCombo = new List<Target>();
    private List<List<Target>> comboList = new List<List<Target>>();

    // Start is called before the first frame update
    void Start()
    {
        OVRManager.display.RecenterPose();
        // TODO: Load combo list
        comboList.Add(currentCombo);
        StartCombo(0);
    }
    void StartCombo(int comboId)
    {
        comboTimer = 0f;
        currentTarget = 0;
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 2f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 3f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 4f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 5f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 6f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 7f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 8f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 9f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 10f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 11f, 0));
        currentCombo.Add(new Target(new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 10f), 12f, 0));
        //currentCombo = comboList[comboId];
    }
    // Update is called once per frame
    void Update()
    {
        comboTimer += Time.deltaTime;
        while (comboTimer >= currentCombo[currentTarget].shoot)
        {
            // Instantiate it
            GameObject target = Instantiate(targetPreFabs[currentCombo[currentTarget].type], currentCombo[currentTarget].location, Quaternion.identity);
            Movement targetScript = target.GetComponent<Movement>();
            if (targetScript != null)
            {
                targetScript.speed = currentCombo[currentTarget].location.z;
            }
            currentTarget++;
            if (currentTarget >= currentCombo.Count)
            {
                EndCombo();
            }
        }
    }



    void EndCombo()
    {
        Debug.Log("hey the combo died");
    }
}
