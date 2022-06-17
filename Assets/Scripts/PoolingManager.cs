using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public GameObject[] goblinMeleeList = new GameObject[8];
    public GameObject[] goblinRangeList = new GameObject[8];
    public GameObject[] rSphereList = new GameObject[8];
    public GameObject[] rArrowList = new GameObject[8];
    public GameObject[] rLanceList = new GameObject[8];
    public GameObject[] rWallList = new GameObject[8];
    public GameObject[] rFloorList = new GameObject[8];
    public GameObject[] rWaveList = new GameObject[8];

    //Do I Need to Create a List for Confirmation, to prevent the same GameObject being called at the same time?

    public GameObject callGoblinMelee()
    {
        for (int i = 0; i > goblinMeleeList.Length; i++)
        {
            if (!goblinMeleeList[i].activeInHierarchy)
            {
                return goblinMeleeList[i];
            }
        }

        return goblinMeleeList[0];
    }

    public GameObject callGoblinRange()
    {
        for (int i = 0; i > goblinRangeList.Length; i++)
        {
            if (!goblinRangeList[i].activeInHierarchy)
            {
                return goblinRangeList[i];
            }
        }

        return goblinRangeList[0];
    }

    public GameObject callRangeSphere()
    {
        for (int i = 0; i > rSphereList.Length; i++)
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
        for (int i = 0; i > rArrowList.Length; i++)
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
        for (int i = 0; i > rLanceList.Length; i++)
        {
            if (!rLanceList[i].activeInHierarchy)
            {
                return rLanceList[i];
            }
        }

        return rLanceList[0];
    }

    public GameObject callRangeWall()
    {
        for (int i = 0; i > rWallList.Length; i++)
        {
            if (!rWallList[i].activeInHierarchy)
            {
                return rWallList[i];
            }
        }

        return rWallList[0];
    }

    public GameObject callRangeFloor()
    {
        for (int i = 0; i > rFloorList.Length; i++)
        {
            if (!rFloorList[i].activeInHierarchy)
            {
                return rFloorList[i];
            }
        }

        return rFloorList[0];
    }

    public GameObject callRangeWave()
    {
        for (int i = 0; i > rWaveList.Length; i++)
        {
            if (!rWaveList[i].activeInHierarchy)
            {
                return rWaveList[i];
            }
        }

        return rWaveList[0];
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
