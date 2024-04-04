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

        public float delay = 0.1f;              //�� ���ڰ� ��Ÿ���� �ð�
        private string currentText = "";        //ǥ�õ� �ؽ�Ʈ
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


        public GAMESTATE state;

        public StoryTableObject[] storyModels;
        public StoryTableObject currentModels;

        private void Start()
        {
            currentModels = FindStoryModel(1);
            StartCoroutine(ShowText());
        }

        StoryTableObject FindStoryModel(int number)
        {
            StoryTableObject tempStoryModels = null;
            for (int i = 0; i < storyModels.Length; i++)
            {
                if (storyModels[i].storyNumber == number)
                {
                    tempStoryModels = storyModels[i];
                    break;
                }
            }
            return tempStoryModels;
        }

        IEnumerator ShowText()
        {
            for (int i = 0; i <= currentModels.storyText.Length; i++)
            {
                currentText = currentModels.storyText.Substring(0, i);
                textComponent.text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(delay);
        }



#if UNITY_EDITOR
        [ContextMenu("Reset Story Models")]
        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryTableObject>("");
            //Resources ���� �Ʒ� ��� StoryModel �ҷ� ����
        }
#endif
    }

}