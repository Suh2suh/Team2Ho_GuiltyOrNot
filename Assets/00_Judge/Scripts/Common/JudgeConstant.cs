using UnityEngine;


namespace Judge
{
	public enum CharacterType
	{
		Judge,
		Prosecutor,
		Lawyer,
		Ethicist,
		Scientist,
	}

	public enum GameState
	{
		CaseBriefing,
		Hearing,
		Verdict,
	}
}