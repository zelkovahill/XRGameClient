using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public NPCManager npcManager;
     public GameStateManager gameStateManager;
    private CharacterController characterController;
    private Vector3 moveDirection;      // 이동방향

    

    public float range = 2.5f;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float horzontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.TransformDirection(new Vector3(horzontalInput, 0, verticalInput));
        moveDirection = move * moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.E))
        {
            
            // 오버랩된 NPC 오브젝트를 가져온다 (TAG 사용)
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);

            foreach(Collider collider in colliders)
            {
                if (collider.CompareTag("NPC"))
                {


                    // NPC 오브젝트에서 다이얼로그 데이터 가져오기

                    Entity_Dialog.Param npcParam =
                        npcManager.GetParamData(collider.GetComponent<NPCActor>().npcNumber, gameStateManager.gameState);

                    if (npcParam != null)
                    {
                        // 대화 실행
                        Debug.Log($"Dialog : {npcParam.Dialog}");

                        // 작업 수행
                        if (npcParam.changeState > 0)
                        {
                            gameStateManager.gameState = npcParam.changeState;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("해당하는 데이터가 없습니다. ");
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);   // 빨간색 원으로 거리 확인

    }
}
