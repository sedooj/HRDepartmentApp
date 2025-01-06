namespace CourseWork_2.Presentation.Util;

public static class EntryUtil
{
    private static readonly Color OkColor = Colors.White;
    private static readonly Color ErrorColor = Colors.Red;

    public static void ChangeEntryColor(Entry entry, bool isValid)
    {
        entry.TextColor = isValid ? OkColor : ErrorColor;
    }
    
    public static Color GetInvertedColor(Color? bgColor)
    {
        bgColor ??= Color.FromRgba(0f, 0f, 0f, 1f);
        return Color.FromRgba(1f - bgColor.Red, 1f - bgColor.Green, 1f - bgColor.Blue, 1f);
    }

}