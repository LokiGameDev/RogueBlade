public static class CurrentSavePlay
{
    public static string currentSaveName
    {
        get; private set;
    }
    public static int saveIndex { get; private set; }

    public static void ModifyCurrentSaveDetails(string name, int index)
    {
        currentSaveName = name;
        saveIndex = index;
    }
}
