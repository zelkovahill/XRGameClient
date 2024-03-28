using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharaterManager : MonoBehaviour
{
    public bool isMising;

    // ExCharacter , Ex CharacterFast , ExCharacterDifferent
    public List<ExCharacter> excharacters = new List<ExCharacter>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMising) 
        {
                isMising = true;
                for (int i = 0; i < excharacters.Count; i++)
                {
                excharacters[i].DestroyChatacter();
                }
        }
    }
}
