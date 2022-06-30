using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    //Pooling System Instance
    public static PoolingManager instance = null;
    public static PoolingManager Instance { get => instance; set => instance = value; }

    //Enemy Character
    public GameObject[] goblinMeleeList = new GameObject[6];
    public GameObject[] goblinRangeList = new GameObject[6];
    public GameObject[] ghostRangeList = new GameObject[6];
    public GameObject goblinWarriorBoss;
    public GameObject goblinShamanBoss;
    //public GameObject golemBoss;

    //Enemy Range Attack
    public GameObject[] rSphereList = new GameObject[8];
    public GameObject[] rArrowList = new GameObject[4];
    public GameObject[] rLanceList = new GameObject[4];

    //Enemy Magic Attack
    public GameObject[] fireBallList = new GameObject[8];
    public GameObject[] fireBombList = new GameObject[4];
    public GameObject[] fireWallList = new GameObject[4];
    public GameObject[] fireFloorList = new GameObject[4];
    public GameObject[] fireWaveList = new GameObject[2];
    public GameObject[] lightningStrikeList = new GameObject[2];
    public GameObject[] lightningFieldList = new GameObject[2];
    public GameObject[] lightningWaveList = new GameObject[2];
    public GameObject[] earthQuakeList = new GameObject[2];
    public GameObject[] earthQuakeV2List = new GameObject[2];
    public GameObject[] earthStompList = new GameObject[2];
    public GameObject[] earthWaveList = new GameObject[2];
    public GameObject[] boulderThrowList = new GameObject[4];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    //Do I Need to Create a List for Confirmation, to prevent the same GameObject being called at the same time?

    public GameObject callGoblinMelee()
    {
        for (int i = 0; i < goblinMeleeList.Length; i++)
        {
            if (!goblinMeleeList[i].activeInHierarchy)
            {
                //Initialize
                return goblinMeleeList[i];
            }
        }

        return goblinMeleeList[0];
    }

    public GameObject callGoblinRange()
    {
        for (int i = 0; i < goblinRangeList.Length; i++)
        {
            if (!goblinRangeList[i].activeInHierarchy)
            {
                return goblinRangeList[i];
            }
        }

        return goblinRangeList[0];
    }

    public GameObject callGhostRange()
    {
        for (int i = 0; i < ghostRangeList.Length; i++)
        {
            if (!ghostRangeList[i].activeInHierarchy)
            {
                return ghostRangeList[i];
            }
        }

        return ghostRangeList[0];
    }

    public GameObject callGoblinWarrior()
    {
        return goblinWarriorBoss;
    }

    public GameObject callGoblinShaman()
    {
        return goblinShamanBoss;
    }

    public GameObject callRangeSphere()
    {
        for (int i = 0; i < rSphereList.Length; i++)
        {
            if (!rSphereList[i].activeInHierarchy)
            {
                return rSphereList[i];
            }
        }

        return rSphereList[0];
    }

    public GameObject callRangeArrow()
    {
        for (int i = 0; i < rArrowList.Length; i++)
        {
            if (!rArrowList[i].activeInHierarchy)
            {
                return rArrowList[i];
            }
        }

        return rArrowList[0];
    }

    public GameObject callRangeLance()
    {
        for (int i = 0; i < rLanceList.Length; i++)
        {
            if (!rLanceList[i].activeInHierarchy)
            {
                return rLanceList[i];
            }
        }

        return rLanceList[0];
    }

    public GameObject callFireBall()
    {
        for (int i = 0; i < fireBallList.Length; i++)
        {
            if (!fireBallList[i].activeInHierarchy)
            {
                return fireBallList[i];
            }
        }

        return fireBallList[0];
    }

    public GameObject callFireBomb()
    {
        for (int i = 0; i < fireBombList.Length; i++)
        {
            if (!fireBombList[i].activeInHierarchy)
            {
                return fireBombList[i];
            }
        }

        return fireBombList[0];
    }

    public GameObject callFireWall()
    {
        for (int i = 0; i < fireWallList.Length; i++)
        {
            if (!fireWallList[i].activeInHierarchy)
            {
                return fireWallList[i];
            }
        }

        return fireWallList[0];
    }

    public GameObject callFireFloor()
    {
        for (int i = 0; i < fireFloorList.Length; i++)
        {
            if (!fireFloorList[i].activeInHierarchy)
            {
                return fireFloorList[i];
            }
        }

        return fireFloorList[0];
    }

    public GameObject callWaveBall()
    {
        for (int i = 0; i < fireWaveList.Length; i++)
        {
            if (!fireWaveList[i].activeInHierarchy)
            {
                return fireWaveList[i];
            }
        }

        return fireWaveList[0];
    }

    public GameObject callLightningStrike()
    {
        for (int i = 0; i < lightningStrikeList.Length; i++)
        {
            if (!lightningStrikeList[i].activeInHierarchy)
            {
                return lightningStrikeList[i];
            }
        }

        return lightningStrikeList[0];
    }

    public GameObject callLightningField()
    {
        for (int i = 0; i < lightningFieldList.Length; i++)
        {
            if (!lightningFieldList[i].activeInHierarchy)
            {
                return lightningFieldList[i];
            }
        }

        return lightningFieldList[0];
    }

    public GameObject callLightningWave()
    {
        for (int i = 0; i < lightningWaveList.Length; i++)
        {
            if (!lightningWaveList[i].activeInHierarchy)
            {
                return lightningWaveList[i];
            }
        }

        return lightningWaveList[0];
    }

    public GameObject callEarthQuake()
    {
        for (int i = 0; i < earthQuakeList.Length; i++)
        {
            if (!earthQuakeList[i].activeInHierarchy)
            {
                return earthQuakeList[i];
            }
        }

        return earthQuakeList[0];
    }

    public GameObject callEarthQuakeV2()
    {
        for (int i = 0; i < earthQuakeV2List.Length; i++)
        {
            if (!earthQuakeV2List[i].activeInHierarchy)
            {
                return earthQuakeV2List[i];
            }
        }

        return earthQuakeV2List[0];
    }

    public GameObject callEarthStomp()
    {
        for (int i = 0; i < earthStompList.Length; i++)
        {
            if (!earthStompList[i].activeInHierarchy)
            {
                return earthStompList[i];
            }
        }

        return earthStompList[0];
    }

    public GameObject callEarthWave()
    {
        for (int i = 0; i < earthWaveList.Length; i++)
        {
            if (!earthWaveList[i].activeInHierarchy)
            {
                return earthWaveList[i];
            }
        }

        return earthWaveList[0];
    }

    public GameObject callBoulderThrow()
    {
        for (int i = 0; i < boulderThrowList.Length; i++)
        {
            if (!boulderThrowList[i].activeInHierarchy)
            {
                return boulderThrowList[i];
            }
        }

        return boulderThrowList[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
