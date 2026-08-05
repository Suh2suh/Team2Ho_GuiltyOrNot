namespace Judge
{
    public enum UIList
    {
        // Panel Start
        Panel = UIType.Panel,
		TitleUI,
        CaseUI,
        EvidenceUI,
        JudgeUI,

		// Popup Start
		Popup = UIType.Popup,
        LoadingUI,
    }

    public enum UIType
    {
        Panel = 0,
        Popup = 500,
    }

    public static class UIListExtensions
    {
        public static UIType GetUIType(this UIList uiList)
        {
            return (int)uiList >= (int)UIType.Popup ? UIType.Popup : UIType.Panel;
        }
    }
}
