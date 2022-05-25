using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickState : MonoBehaviour, IPlayerBaseState
{
    //private GameManager gameManager;

    private PlayerEntity playerEntityInstance;

    public PlayerPickState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }

    private void Awake()
    {
        //playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>();
    }

    #region Pick
    //Picking up
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.GetComponent<Drops>())
            {
                //isCollingWithItem = true;
                if (playerEntityInstance.IsPicking)
                {
                    Debug.Log("IsPicking");
                    playerEntityInstance.HasPickedItem = true;
                    playerEntityInstance.PickableText.GetComponent<TMPro.TextMeshProUGUI>().text = "Picked";
                    //other.gameObject.SetActive(false);
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void PickingItem()
    {
        if (playerEntityInstance.HasPickedItem)
        {
            playerEntityInstance.Speed = 0f;
            playerEntityInstance.Animator.SetBool("Pick", true);
            playerEntityInstance.Sword.SetActive(false);
            playerEntityInstance.TimeToWait += Time.deltaTime;
            //Debug.Log(timeToWait);
            if (playerEntityInstance.TimeToWait >= 2.2f)
            {
                playerEntityInstance.Animator.SetBool("Pick", false);
                playerEntityInstance.Sword.SetActive(true);
                playerEntityInstance.Speed = 5f;
                playerEntityInstance.TimeToWait = 0f;
                playerEntityInstance.HasPickedItem = false;
                playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
            }
        }
    }
    #endregion

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void ExitState()
    {
        return;
    }

    public void OnUpdate()
    {
        PickingItem();
    }

}
