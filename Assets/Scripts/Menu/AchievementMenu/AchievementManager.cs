using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPrefab;

    public Sprite[] sprites;

    public GameObject[] CategoryList;

    public List<GameObject> CategoryL;

    private AchievementButton activeButton;

    public ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();

        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);
        CreateAchievement("GeneralCategory", "TestTitle", "This is the Description", 10, 0);

        CreateAchievement("OtherCategory", "TestTitle", "This is the Description", 10, 1);
       

        //foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        //{
        //    Debug.Log(achievementList);
        //    achievementList.SetActive(false);

        //}

        activeButton.Click();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAchievement(string category, string title, string description,  int points, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);
        SetAchievementInfo(category, achievement, title, description, points, spriteIndex);
    }

    public void SetAchievementInfo(string category, GameObject achievement, string title, string description, int points, int spriteIndex)
    {
        achievement.transform.SetParent(GameObject.Find(category).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        achievement.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = points.ToString();
        achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[spriteIndex];
    }

    public void ChangeCategory(GameObject button)
    {
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();

        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();

        achievementButton.Click();
        activeButton.Click();
        activeButton = achievementButton;
    }


}
