using UnityEngine;


namespace Judge
{
	public enum CharacterType
	{
		Judge = 0,
		Prosecutor,
		Lawyer,
		Ethicist,
		Scientist,
	}

	public enum GameState
	{
		None = 0,
		CaseBriefing,
		Hearing,
		Verdict,
	}
}