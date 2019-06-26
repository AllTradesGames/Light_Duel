using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Target
{
    public Vector3 location;
    public float angle;
    public int type;
    public Target(Vector2 inLocation, float inAngle, int inType)
    {
        this.location = inLocation;
        this.type = inType;
        this.angle = inAngle;
    }
}

public class Targets
{
    public static Target N = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 180f, 1);
    public static Target S = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 0f, 1);
    public static Target E = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 270f, 1);
    public static Target W = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 90f, 1);
    public static Target NE = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 225f, 1);
    public static Target SE = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 315f, 1);
    public static Target SW = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 45f, 1);
    public static Target NW = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 135f, 1);
    public static Target Stab = new Target(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.8f)), 0f, 0);
}

public class Combos
{
    public static Target[][] sword = {
        new Target[4] {Targets.E, Targets.S, Targets.N, Targets.Stab},
        new Target[4] {Targets.E, Targets.W, Targets.E, Targets.W}
        };
}

public class ComboController : MonoBehaviour
{
    public GameObject[] targetPreFabs;
    public int comboLength = 4;
    public float timeBetweenTargets = 1f;
    public float timeDecreasePerRound = 0.125f;
    public float distanceBetweenTargets = 4f;

    private float comboTimer;
    private int currentTarget;
    private List<Target> currentCombo = new List<Target>();
    private List<List<Target>> comboList = new List<List<Target>>();
    private int combosCompleted = 0;
    private bool comboing = false;

    // Start is called before the first frame update
    void Start()
    {
        OVRManager.display.RecenterPose();
    }

    public void StartCombo(string comboType)
    {
        comboing = true;
        comboTimer = 0f;
        currentTarget = 0;
        switch (comboType)
        {
            case "sword":
                // Build sword combo
                currentCombo.Clear();
                for (int ii = 0; ii < comboLength; ii++)
                {
                    currentCombo.AddRange(Combos.sword[Random.Range(0, Combos.sword.Length - 1)]);
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (comboing)
        {
            comboTimer += Time.deltaTime;
            while (comboTimer >= timeBetweenTargets)
            {
                // Instantiate it
                GameObject target = Instantiate(targetPreFabs[currentCombo[currentTarget].type], Vector3.zero, Quaternion.Euler(0f, 0f, currentCombo[currentTarget].angle), transform);
                target.transform.localPosition = new Vector3(currentCombo[currentTarget].location.x, currentCombo[currentTarget].location.y);
                target.transform.parent = null;
                Movement targetScript = target.GetComponent<Movement>();
                if (targetScript != null)
                {
                    targetScript.speed = distanceBetweenTargets / timeBetweenTargets;
                }
                currentTarget++;
                comboTimer = 0f;
                if (currentTarget >= currentCombo.Count)
                {
                    EndCombo();
                }
            }
        }
    }

    void EndCombo()
    {
        comboing = false;
        combosCompleted++;
        if (combosCompleted % 2 == 0)
        {
            // Round Completed
            timeBetweenTargets -= timeDecreasePerRound;
            if (timeBetweenTargets <= 0f)
            {
                timeBetweenTargets = timeDecreasePerRound;
            }
            // TODO: Add/Check score
        }
        // TODO: Next Player's Turn
        Debug.Log("hey the combo died");
    }
}
