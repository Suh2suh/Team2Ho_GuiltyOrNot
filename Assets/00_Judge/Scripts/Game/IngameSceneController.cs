using Judge;
using REIW;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class IngameSceneController : SingletonBase<IngameSceneController>
{
	[Header("Characters")]
	[SerializeField] List<AssistantJudge> _assistantJudges = new();
	[SerializeField] PlayerJudge _playerJudge;
	private Dictionary<CharacterType, AssistantJudge> _assistantJudgeDic = new();


	protected override void Awake()
	{
		base.Awake();

		_assistantJudgeDic.Clear();
		foreach (var assistantJudge in _assistantJudges)
		{
			_assistantJudgeDic.Add(assistantJudge.CharacterType, assistantJudge);
		}
	}

	public AssistantJudge GetAssistantJudge(CharacterType assistantJudgeType)
	{
		if (_assistantJudgeDic.ContainsKey(assistantJudgeType))
		{
			return _assistantJudgeDic[assistantJudgeType];
		}
		return null;
	}

	public PlayerJudge GetPlayerJudge() => _playerJudge;
}
