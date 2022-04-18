using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int Variable", menuName = "Lobby Tools/ Scriptable Variables/ Int Variable")]
public class IntVariable : BaseScriptableVariable
{
    [SerializeField] int value = 0;

    public Action<int> onValueChange;

    public void AddValue(int newValue)
    {
        value += newValue;

        onValueChange?.Invoke(value);
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        onValueChange?.Invoke(value);
    }

    public int GetValue()
    {
        return value;
    }

    public override void SaveData()
    {
        IntVariableStruct temp = new IntVariableStruct { value = value };
        SaveData<IntVariableStruct>(temp);
    }

    public override void LoadData()
    {
        value = LoadData<IntVariableStruct>().value;
    }

    public override void EraseSaveFile()
    {
        base.EraseSaveFile();

        value = 0;
    }
}

struct IntVariableStruct
{
    public int value;
}
