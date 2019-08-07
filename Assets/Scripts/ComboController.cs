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
    public static Target N = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 180f, 1);
    public static Target S = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 0f, 1);
    public static Target E = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 270f, 1);
    public static Target W = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 90f, 1);
    public static Target NE = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 225f, 1);
    public static Target SE = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 315f, 1);
    public static Target SW = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 45f, 1);
    public static Target NW = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 135f, 1);
    public static Target Stab = new Target(new Vector2(Random.Range(-1f, 1f), Random.Range(-0.2f, 0.8f)), 0f, 0);
}

public class Combos
{
    public static Target[][] SWORD = {
        new Target[4] {Targets.E, Targets.S, Targets.N, Targets.Stab},
        new Target[4] {Targets.E, Targets.W, Targets.E, Targets.W},
        new Target[4] {Targets.E, Targets.NW, Targets.SE, Targets.Stab},
        new Target[4] {Targets.S, Targets.W, Targets.E, Targets.Stab},
        new Target[4] {Targets.SE, Targets.W, Targets.Stab, Targets.N},
        new Target[4] {Targets.S, Targets.Stab, Targets.E, Targets.W},
        new Target[4] {Targets.SE, Targets.NW, Targets.SE, Targets.NW},
        new Target[4] {Targets.S, Targets.W, Targets.SE, Targets.W},
        new Target[4] {Targets.SE, Targets.NW, Targets.E, Targets.W},
        new Target[4] {Targets.E, Targets.Stab, Targets.SW, Targets.Stab},
        new Target[4] {Targets.S, Targets.NW, Targets.E, Targets.Stab},
        new Target[4] {Targets.S, Targets.W, Targets.NE, Targets.N},
        new Target[4] {Targets.SE, Targets.Stab, Targets.E, Targets.NW},
        new Target[4] {Targets.E, Targets.NW, Targets.E, Targets.Stab}
        };

    public static Target[][] REDRIGHT = {
        new Target[6] {null, null, Targets.SE, Targets.W, Targets.NE, Targets.S },
        new Target[6] {Targets.Stab, Targets.NE, null, Targets.E, Targets.W, Targets.S },
        new Target[6] {Targets.NE, Targets.W, null, Targets.E, Targets.S, Targets.N },
        new Target[6] {null, Targets.W, Targets.NE, Targets.S, Targets.E, Targets.N },
        new Target[6] {null, Targets.NW, Targets.SE, null, Targets.SW, Targets.NE },
        new Target[6] {Targets.SE, Targets.W, Targets.E, null, Targets.NW, Targets.SE },
        new Target[6] {Targets.S, Targets.NW, Targets.SE, null, null, Targets.S },
        new Target[6] {Targets.Stab, Targets.SE, Targets.NW, Targets.E, null, Targets.S },
        new Target[6] {null, null, Targets.NW, Targets.E, Targets.S, Targets.N },
        new Target[6] {Targets.SE, Targets.W, null, Targets.S, null, Targets.N },
        new Target[6] {Targets.Stab, null, Targets.SE, Targets.W, null, Targets.NE },
        new Target[6] {Targets.NE, null, Targets.E, Targets.NW, null, Targets.SE}
        };

    public static Target[][] BLUELEFT = {
        new Target[6] {Targets.S, Targets.NW, Targets.SE, null, null, Targets.S },
        new Target[6] {Targets.Stab, Targets.NW, Targets.SE, Targets.W, null, Targets.S },
        new Target[6] {null, null, Targets.NW, Targets.E, Targets.S, Targets.N },
        new Target[6] {Targets.SE, Targets.W, null, Targets.S, null, Targets.N },
        new Target[6] {Targets.Stab, null, Targets.SE, Targets.W, null, Targets.NE },
        new Target[6] {Targets.SW, null, Targets.E, Targets.NW, null, Targets.SE },
        new Target[6] {null, null, Targets.SE, Targets.W, Targets.NE, Targets.S },
        new Target[6] {Targets.Stab, Targets.SW, null, Targets.E, Targets.W, Targets.S },
        new Target[6] {Targets.NE, Targets.W, null, Targets.E, Targets.S, Targets.N },
        new Target[6] {null, Targets.W, Targets.NE, Targets.S, Targets.E, Targets.N },
        new Target[6] {null, Targets.NW, Targets.SE, null, Targets.SW, Targets.NE },
        new Target[6] {Targets.NW, Targets.W, Targets.E, null, Targets.NW, Targets.SE }
       };

}

public class ComboController : MonoBehaviour
{
    public GameObject[] targetPreFabs;
    public int comboLength = 4;
    public float timeBetweenTargets = 5f;
    public float timeDecreasePerRound = 0.125f;
    public float distanceBetweenTargets = 4f;

    private float comboTimer;
    private int currentTarget;
    private List<Target> currentCombo = new List<Target>();
    private List<Target> currentCombo2 = new List<Target>();
    private List<List<Target>> comboList = new List<List<Target>>();
    private int combosCompleted = 0;
    private bool comboing = false;
    private bool isDual = false;
    private int[] indices;

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
            case "SWORD":
                // Build sword combo
                isDual = false;
                comboLength = 4;
                currentCombo.Clear();
                indices = new int[Combos.SWORD.Length];
                Shuffle(indices);
                for (int ii = 0; ii < comboLength; ii++)
                {
                    currentCombo.AddRange(Combos.SWORD[Random.Range(0, Combos.SWORD.Length - 1)]);
                }
                break;

            case "DUALSWORD":
                // Build sword combo
                isDual = true;
                comboLength = 6;
                currentCombo2.Clear();
                indices = new int[Combos.REDRIGHT.Length];
                Shuffle(indices);
                for (int ii = 0; ii < comboLength; ii++)
                {
                    currentCombo.AddRange(Combos.REDRIGHT[Random.Range(0, Combos.REDRIGHT.Length - 1)]);
                    currentCombo2.AddRange(Combos.BLUELEFT[Random.Range(0, Combos.BLUELEFT.Length - 1)]);
                }
                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (comboing)
        {
            float Offset = isDual ? 0.5f: 0f;
            comboTimer += Time.deltaTime;
            while (comboTimer >= timeBetweenTargets)
            {
                // Instantiate it
                GameObject target;
                Movement targetScript;
                if (currentCombo[indices[currentTarget]] != null){
                    target = Instantiate(targetPreFabs[currentCombo[indices[currentTarget]].type], Vector3.zero, transform.rotation*Quaternion.Euler(0f, 0f, currentCombo[indices[currentTarget]].angle), transform);
                    target.transform.localPosition = new Vector3(-Offset, Random.Range(-0.2f, 0.8f));
                    target.transform.parent = null;
                    targetScript = target.GetComponent<Movement>();
                    if (targetScript != null)
                    {
                        Debug.Log("Set Speed " + distanceBetweenTargets / timeBetweenTargets);
                        targetScript.speed = distanceBetweenTargets / timeBetweenTargets;
                    }
                }
                if (isDual && currentCombo2[indices[currentTarget]] != null)
                {
                    target = Instantiate(targetPreFabs[currentCombo2[indices[currentTarget]].type], Vector3.zero, transform.rotation*Quaternion.Euler(0f, 0f, currentCombo2[indices[currentTarget]].angle), transform);
                    target.transform.localPosition = new Vector3(Offset, Random.Range(-0.2f, 0.8f));
                    target.transform.parent = null;
                    targetScript = target.GetComponent<Movement>();
                    if (targetScript != null)
                    {
                        targetScript.speed = distanceBetweenTargets / timeBetweenTargets;
                    }
                }

                currentTarget++;
                comboTimer = 0f;
                if (indices[currentTarget] >= currentCombo.Count)
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


    void Shuffle(int[] indices)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < indices.Length; t++)
        {
            indices[t] = t;
        }

         


            for (int t = 0; t < indices.Length; t++)
        {
            int tmp = indices[t];
            int r = Random.Range(t, indices.Length);
            indices[t] = indices[r];
            indices[r] = tmp;
        }
    }

}
