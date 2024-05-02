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

    [TextArea(10, 10)]  // 인스펙터 text 영역 설정
    public string storyText;
    public Option[] options;

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonValue;

        public EventCheck eventCheck;
    }

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

        public Result[] successResult;  // 선택지에 대한 성공 값 반영
        public Result[] failedResult;   // 선택지에 대한 실패 값 반영
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
