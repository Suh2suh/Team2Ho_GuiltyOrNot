using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Judge
{
    public class DataManager : SingletonBase<DataManager>
    {
        private const string k_caseDataPath = "Data/caseDataJson";
        private const string k_evidenceDataPath = "Data/evidenceDataJson";
        private const string k_resultDataPath = "Data/resultJson";
        private const string k_userInputDataPath = "Data/userInputJson";

        public CaseData CaseData { get; private set; }
        public EvidenceData EvidenceData { get; private set; }
        public ResultData ResultData { get; private set; }
        public UserInputData UserInputData { get; private set; }

        protected override bool PersistAcrossScenes => true;

        public override void Initialize()
        {
            CaseData = LoadData<CaseData>(k_caseDataPath);
            EvidenceData = LoadData<EvidenceData>(k_evidenceDataPath);
            ResultData = LoadData<ResultData>(k_resultDataPath);
            UserInputData = LoadData<UserInputData>(k_userInputDataPath);
        }

        public string GetEvidenceStatement(CharacterType characterType)
        {
            string key = characterType.ToString().ToLowerInvariant();

            if (EvidenceData?.EvidenceStatements != null &&
                EvidenceData.EvidenceStatements.TryGetValue(key, out string statement))
            {
                return statement;
            }

            return string.Empty;
        }

        public CharacterEvaluationData GetCharacterEvaluation(CharacterType characterType)
        {
            string key = characterType.ToString().ToLowerInvariant();

            if (ResultData?.CharacterEvaluations != null &&
                ResultData.CharacterEvaluations.TryGetValue(key, out CharacterEvaluationData evaluation))
            {
                return evaluation;
            }

            return null;
        }

        public void UpdateUserInput(string caseID, string verdict, IEnumerable<string> selectedTagIDs, string finalStatement)
        {
            UserInputData = new UserInputData
            {
                CaseID = caseID,
                Verdict = verdict,
                SelectedTagIDs = new List<string>(selectedTagIDs),
                FinalStatement = finalStatement,
            };
        }

        public void SetResultData(ResultData resultData)
        {
            ResultData = resultData;
        }

        private T LoadData<T>(string resourcePath) where T : class
        {
            TextAsset jsonAsset = Resources.Load<TextAsset>(resourcePath);

            if (jsonAsset == null)
            {
                Debug.LogError($"JSON resource was not found. Path: Resources/{resourcePath}.json");
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(jsonAsset.text);
            }
            catch (JsonException exception)
            {
                Debug.LogError($"Failed to parse JSON. Path: Resources/{resourcePath}.json\n{exception}");
                return null;
            }
        }
    }
}
