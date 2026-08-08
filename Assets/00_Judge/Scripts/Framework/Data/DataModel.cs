using System.Collections.Generic;
using Newtonsoft.Json;

namespace Judge
{
    public class CaseData
    {
        [JsonProperty("caseId")] public string CaseID { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("summary")] public string Summary { get; set; }
        [JsonProperty("judgmentTarget")] public string JudgmentTarget { get; set; }
        [JsonProperty("evidence")] public CaseEvidenceData Evidence { get; set; }
        [JsonProperty("confirmedFacts")] public List<string> ConfirmedFacts { get; set; }
        [JsonProperty("indeterminableFacts")] public List<string> IndeterminableFacts { get; set; }
        [JsonProperty("availableTags")] public List<TagData> AvailableTags { get; set; }
        [JsonProperty("referenceReasoning")] public ReferenceReasoningData ReferenceReasoning { get; set; }
    }

    public class CaseEvidenceData
    {
        [JsonProperty("id")] public string ID { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
    }

    public class TagData
    {
        [JsonProperty("id")] public string ID { get; set; }
        [JsonProperty("label")] public string Label { get; set; }
    }

    public class ReferenceReasoningData
    {
        [JsonProperty("validGuiltyPaths")] public List<string> ValidGuiltyPaths { get; set; }
        [JsonProperty("validNotGuiltyPaths")] public List<string> ValidNotGuiltyPaths { get; set; }
    }

    public class EvidenceData
    {
        [JsonProperty("caseId")] public string CaseID { get; set; }
        [JsonProperty("evidenceStatements")] public Dictionary<string, string> EvidenceStatements { get; set; }
    }

    public class UserInputData
    {
        [JsonProperty("caseId")] public string CaseID { get; set; }
        [JsonProperty("verdict")] public string Verdict { get; set; }
        [JsonProperty("selectedTagIds")] public List<string> SelectedTagIDs { get; set; }
        [JsonProperty("finalStatement")] public string FinalStatement { get; set; }
    }

    public class ResultData
    {
        [JsonProperty("characterEvaluations")] public Dictionary<string, CharacterEvaluationData> CharacterEvaluations { get; set; }
        [JsonProperty("reasoningAnalysis")] public ReasoningAnalysisData ReasoningAnalysis { get; set; }
        [JsonProperty("criticalErrors")] public List<string> CriticalErrors { get; set; }
        [JsonProperty("overallComment")] public string OverallComment { get; set; }
    }

    public class CharacterEvaluationData
    {
        [JsonProperty("score")] public int Score { get; set; }
        [JsonProperty("reaction")] public string Reaction { get; set; }
    }

    public class ReasoningAnalysisData
    {
        [JsonProperty("usedTagIds")] public List<string> UsedTagIDs { get; set; }
        [JsonProperty("unusedTagIds")] public List<string> UnusedTagIDs { get; set; }
        [JsonProperty("verdictConsistent")] public bool IsVerdictConsistent { get; set; }
        [JsonProperty("containsUnsupportedClaim")] public bool ContainsUnsupportedClaim { get; set; }
    }
}
