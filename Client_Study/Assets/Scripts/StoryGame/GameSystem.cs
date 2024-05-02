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
                //오브젝트 이름으로 Text 컴포넌트를 찾기
                GameObject textObject = GameObject.Find("StroyTextUI");
                if (textObject != null)
                {
                    TMP_Text textComponent = textObject.GetComponent<TMP_Text>();
                    if (textComponent != null)
                    {
                        //Text Componet 할당
                        gameSystem.textComponent = textComponent;
                    }
                }
            }
        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem Instance;      //Scene 내부에서만 존재

        //public float delay = 0.1f;              //각 글자가 나타나는 시간
        //private string currentText = "";        //표시된 텍스트
        public TMP_Text textComponent;          //TextMeshPro 컴포넌트

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
        
        public void ApplyChoice(StoryModel.Result result)   // 스토리 선택시 결과
        {
            switch(result.resultType)
            {
                case StoryModel.Result.EResultType.ChangeHp:
                    // GameUI.Instance.UpdateHpUI();    // 나중에 추가
                    ChangeStats(result);
                    break;

                case StoryModel.Result.EResultType.GoToNextStory:
                    currentStoryIndex = result.value;   // 다음 이동 스토리 번호를 받아와서 실행
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

        public void ChangeStats(StoryModel.Result result)   // 상태 변경 함수
        {
            // 기본 스텟
            if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
            if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;

            // 현재 상태
            if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
            if (result.stats.currentSpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
            if (result.stats.currentXpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;

            // 능력치 상태
            if (result.stats.strength > 0) stats.strength += result.stats.strength;
            if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
            if (result.stats.consitiution > 0) stats.consitiution += result.stats.consitiution;
            if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
            if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
            if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;

        }


        StoryModel RandomStory()    // 랜덤 스토리 리턴함수
        {
            StoryModel tempStoryModels = null;

            List<StoryModel> StoryModelList = new List<StoryModel>();

            for(int index =0; index < storyModels.Length; index++)  // MAIN 스토리들만 가져와서 List에 넣는다.
            {
                if (storyModels[index].storyType == StoryModel.ESTORYTYPE.MAIN) 
                {
                    StoryModelList.Add(storyModels[index]);
                }
            }

            tempStoryModels = StoryModelList[Random.Range(0, StoryModelList.Count)];    // MAIN들만 있는 리스트에서 랜덤으로 스토리 진행
            currentStoryIndex = tempStoryModels.storyNumber;
            print("currentStoryIndex : " + currentStoryIndex);

            return tempStoryModels;
        }

        StoryModel FindStoryModel(int number) // 스토리 번호로 찾아주는 함수
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
            //Resources 폴더 아래 모든 StoryModel 불러 오기
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