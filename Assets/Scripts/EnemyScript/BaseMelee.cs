using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy }

public class BaseMelee : MonoBehaviour
{
    private GameManager gameManager;
    private AchievementManager achievementManager;
    public Striker striker;
    public bool canDmg = false;
    public GameObject otherObject;

    private void OnTriggerEnter(Collider other)
    {
        if (striker == Striker.enemy && other.gameObject.GetComponent<PlayerEntity>() && canDmg)
        {
            if (!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield)
            {
                if (gameManager.player.GetComponent<PlayerEntity>().isInvincible)
                {
                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", false);
                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(0);
                    canDmg = false;
                }
                else
                {
                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(5);
                    other.gameObject.GetComponent<PlayerEntity>().isKnocked = true;
                    other.gameObject.GetComponent<PlayerEntity>().Animator.SetBool("Knocked", true);
                    canDmg = false;
                }
            }
        }

        if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
        {

            if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
            {
                if (!gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                {
                    gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Add(other.gameObject);
                    other.gameObject.GetComponent<EnemyMain>().OnHurt(gameManager.player.GetComponent<PlayerEntity>()._currentMeleeDamage);
                    gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
                    if (other.gameObject.GetComponent<EnemyMain>().GetCurrentHP <= 0)
                    {
                        gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(other.gameObject);
                    }
                }
            }

            if (gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
            {
                if (!gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                {
                    gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Add(other.gameObject);
                    other.gameObject.GetComponent<EnemyMain>().OnHurt(gameManager.player.GetComponent<PlayerEntity>()._currentSlashDamage);
                    gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash = false;
                    if (other.gameObject.GetComponent<EnemyMain>().GetCurrentHP <= 0)
                    {
                        gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(other.gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        otherObject = other.gameObject;
        if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(otherObject))
            {
                otherObject.GetComponent<EnemyMain>().OnHurt(0);
            }
        }

        if (gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(otherObject))
            {
                otherObject.GetComponent<EnemyMain>().OnHurt(0);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        achievementManager = AchievementManager.Instance;
        canDmg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.player.GetComponent<PlayerEntity>().resetMeleeInputTimer >= 0.25f)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Count > 0)
            {
                gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Clear();
            }
        }

        if (gameManager.player.GetComponent<PlayerEntity>().resetSlashInputTimer >= 0.5f)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Count > 0)
            {
                gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Clear();
            }
        }
    }

}
