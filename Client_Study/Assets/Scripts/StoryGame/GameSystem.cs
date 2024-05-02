using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEngine.UI;           //UI
using TMPro;                    //TextMeshPro
using STORYGAME;

namespace STROYGAME
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]
    public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gameSystem = (GameSystem)target;

            if (GUILayout.Button("Reset Story Models"))
            {
                gameSystem.ResetStoryModels();
            }

            if (GUILayout.Button("Assing Text Component by Name"))
            {
                //������Ʈ �̸����� Text ������Ʈ�� ã��
                GameObject textObject = GameObject.Find("StroyTextUI");
                if (textObject != null)
                {
                    TMP_Text textComponent = textObject.GetComponent<TMP_Text>();
                    if (textComponent != null)
                    {
                        //Text Componet �Ҵ�
                        gameSystem.textComponent = textComponent;
                    }
                }
            }
        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem Instance;      //Scene ���ο����� ����

        //public float delay = 0.1f;              //�� ���ڰ� ��Ÿ���� �ð�
        //private string currentText = "";        //ǥ�õ� �ؽ�Ʈ
        public TMP_Text textComponent;          //TextMeshPro ������Ʈ

        private void Awake()
        {
            Instance = this;
        }

        public enum GAMESTATE
        {
            STORYSHOW,
            STORYEND,
            ENDMODE
        }


        public GAMESTATE currentState;
        public Stats stats;
        public StoryModel[] storyModels;
        public int currentStoryIndex = 0;

        public void ChangeState(GAMESTATE temp)
        {
            currentState = temp;

            if(currentState == GAMESTATE.STORYSHOW)
            {
                StoryShow(currentStoryIndex);
            }
        }

        public void StoryShow(int number)
        {
            StoryModel tempStoryModels = FindStoryModel(number);

            // StorySystem.Instance.currentStoryModel = tempstoryModels;
            // StorySystem.Instance.CoShowText();
        }
        
        public void ApplyChoice(StoryModel.Result result)   // ���丮 ���ý� ���
        {
            switch(result.resultType)
            {
                case StoryModel.Result.EResultType.ChangeHp:
                    // GameUI.Instance.UpdateHpUI();    // ���߿� �߰�
                    ChangeStats(result);
                    break;

                case StoryModel.Result.EResultType.GoToNextStory:
                    currentStoryIndex = result.value;   // ���� �̵� ���丮 ��ȣ�� �޾ƿͼ� ����
                    ChangeState(GAMESTATE.STORYSHOW);
                    ChangeStats(result);
                    break;

                case StoryModel.Result.EResultType.GoToRandomStory:
                    RandomStory();
                    ChangeState(GAMESTATE.STORYSHOW);
                    ChangeStats(result); 
                    break;

                default:
                    Debug.LogError("UnKnown effect Type");
                    break;
                 
            }

        }

        public void ChangeStats(StoryModel.Result result)   // ���� ���� �Լ�
        {
            // �⺻ ����
            if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
            if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;

            // ���� ����
            if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
            if (result.stats.currentSpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
            if (result.stats.currentXpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;

            // �ɷ�ġ ����
            if (result.stats.strength > 0) stats.strength += result.stats.strength;
            if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
            if (result.stats.consitiution > 0) stats.consitiution += result.stats.consitiution;
            if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
            if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
            if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;

        }


        StoryModel RandomStory()    // ���� ���丮 �����Լ�
        {
            StoryModel tempStoryModels = null;

            List<StoryModel> StoryModelList = new List<StoryModel>();

            for(int index =0; index < storyModels.Length; index++)  // MAIN ���丮�鸸 �����ͼ� List�� �ִ´�.
            {
                if (storyModels[index].storyType == StoryModel.ESTORYTYPE.MAIN) 
                {
                    StoryModelList.Add(storyModels[index]);
                }
            }

            tempStoryModels = StoryModelList[Random.Range(0, StoryModelList.Count)];    // MAIN�鸸 �ִ� ����Ʈ���� �������� ���丮 ����
            currentStoryIndex = tempStoryModels.storyNumber;
            print("currentStoryIndex : " + currentStoryIndex);

            return tempStoryModels;
        }

        StoryModel FindStoryModel(int number) // ���丮 ��ȣ�� ã���ִ� �Լ�
        {
            StoryModel tempStoryModels = null;

            for(int index =0; index < storyModels.Length; index++)
            {
                if (storyModels[index].storyNumber == number)
                {
                    tempStoryModels = storyModels[index];
                    break;
                }
            }
            return tempStoryModels;
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryModel>("");
            //Resources ���� �Ʒ� ��� StoryModel �ҷ� ����
        }
#endif


        // public StoryTableObject currentModels;

        //private void Start()
        //{
        //    currentModels = FindStoryModel(1);
        //    StartCoroutine(ShowText());
        //}

        //StoryTableObject FindStoryModel(int number)
        //{
        //    StoryTableObject tempStoryModels = null;
        //    for (int i = 0; i < storyModels.Length; i++)
        //    {
        //        if (storyModels[i].storyNumber == number)
        //        {
        //            tempStoryModels = storyModels[i];
        //            break;
        //        }
        //    }
        //    return tempStoryModels;
        //}

        //IEnumerator ShowText()
        //{
        //    for (int i = 0; i <= currentModels.storyText.Length; i++)
        //    {
        //        currentText = currentModels.storyText.Substring(0, i);
        //        textComponent.text = currentText;
        //        yield return new WaitForSeconds(delay);
        //    }
        //    yield return new WaitForSeconds(delay);
        //}




    }

}