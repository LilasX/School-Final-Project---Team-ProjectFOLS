using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_EarthQuake : BaseSpell
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;

    public void ShowFirstQuake(GameObject[] list)
    {

        ShowSpellEffect(list, list[0]);
        ShowSpellEffect(list, list[1]);
        ShowSpellEffect(list, list[2]);
        ShowSpellEffect(list, list[3]);
        ShowSpellEffect(list, list[4]);
        ShowSpellEffect(list, list[5]);
        ShowSpellEffect(list, list[6]);
        ShowSpellEffect(list, list[7]);
    }

    public void ShowSecondQuake(GameObject[] list)
    {
        list[0].SetActive(false);
        list[1].SetActive(false);
        list[2].SetActive(false);
        list[3].SetActive(false);
        list[4].SetActive(false);
        list[5].SetActive(false);
        list[6].SetActive(false);
        list[7].SetActive(false);

        ShowSpellEffect(list, list[8]);
        ShowSpellEffect(list, list[9]);
        ShowSpellEffect(list, list[10]);
        ShowSpellEffect(list, list[11]);
        ShowSpellEffect(list, list[12]);
        ShowSpellEffect(list, list[13]);
        ShowSpellEffect(list, list[14]);
        ShowSpellEffect(list, list[15]);
    }

    public void HideSecondQuake(GameObject[] list)
    {
        list[8].SetActive(false);
        list[9].SetActive(false);
        list[10].SetActive(false);
        list[11].SetActive(false);
        list[12].SetActive(false);
        list[13].SetActive(false);
        list[14].SetActive(false);
        list[15].SetActive(false);
    }

    public override void InitializeSpell()
    {
        for (int i = 0; i < warningZone.Length; i++)
        {
            warningZone[i].SetActive(false);
        }

        for (int i = 0; i < spellZone.Length; i++)
        {
            spellZone[i].SetActive(false);
        }
    }

    public override void StartSpell()
    {
        InitializeSpell();
        start = true;
        timer = 0;
        once1 = false;
        once2 = false;
        once3 = false;
    }

    public override void ShowSpellEffect(GameObject[] list, GameObject zone)
    {
        zone.SetActive(true);

        if (list == spellZone)
        {
            zone.GetComponent<SpellDamageManager>().InitializeSpellEffect(30);
        }
    }

    public override void ShowWarningZone()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            timer += Time.deltaTime;

            if (timer >= 0f && !once1)
            {
                once1 = true;
                ShowFirstQuake(warningZone);
            }

            if (timer >= 1f && !once2)
            {
                once2 = true;
                ShowSecondQuake(warningZone);
                ShowFirstQuake(spellZone);
            }

            if (timer >= 2f && !once3)
            {
                once3 = true;
                HideSecondQuake(warningZone);
                ShowSecondQuake(spellZone);
            }

            if (timer >= 3f)
            {
                timer = 0;
                start = false;
                once1 = false;
                once2 = false;
                once3 = false;
                HideSecondQuake(spellZone);
                this.gameObject.SetActive(false);
            }
        }
    }
}
