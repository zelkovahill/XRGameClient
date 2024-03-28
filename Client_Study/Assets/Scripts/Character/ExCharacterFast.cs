using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Entity_monster;

public class ExCharacterFast : ExCharacter
{
   protected override void Move()
    {
        transform.Translate(Vector3.down * speed * 2 * Time.deltaTime);
    }
}
