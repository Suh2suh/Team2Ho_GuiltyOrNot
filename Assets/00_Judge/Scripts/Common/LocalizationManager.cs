using Judge;
using UnityEngine;

public static class LocalizationManager
{
    public static string GetLocalizedCharacterType(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Judge         : return "판사";
            case CharacterType.Lawyer       : return "변호사";
            case CharacterType.Prosecutor : return "검사";
            case CharacterType.Scientist     : return "과학자";
            case CharacterType.Ethicist       : return "윤리학자";
            default: return string.Empty;
        }
    }
}
