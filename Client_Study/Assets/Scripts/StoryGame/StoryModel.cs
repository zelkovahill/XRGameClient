using STORYGAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewStory",menuName ="ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{

    public int storyNumber;
    public Texture2D MainImage;
    public bool storyDne;

    public enum ESTORYTYPE
    {
        MAIN,
        SUB,
        SERIAL
    }

    public ESTORYTYPE storyType;

    [TextArea(10, 10)]  // �ν����� text ���� ����
    public string storyText;
    public Option[] options;
    

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText;

        public EventCheck eventCheck;
    }

    [System.Serializable]
    public class EventCheck
    {
        public int checkValue;
        public enum EventType : int
        {
            NONE,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }

        public EventType type;

        public Result[] successResult;  // �������� ���� ���� �� �ݿ�
        public Result[] failedResult;   // �������� ���� ���� �� �ݿ�
    }


    [System.Serializable]
    public class Result
    {

        public enum EResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory,
            GoToEnding
        }


        public EResultType resultType;
        public int value;
        public Stats stats;
    }
}
