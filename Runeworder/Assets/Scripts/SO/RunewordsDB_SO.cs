using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunewordsDB", menuName = "Scriptables/Add Runewords Database", order = 4)]
public class RunewordsDB_SO : ScriptableObject
{
    public List<Runeword_SO> runewords;
}
