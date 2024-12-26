using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace CourseWork_2.Util;

public static class EntryUtil
{
    private static readonly Color OkColor = Colors.White;
    private static readonly Color ErrorColor = Colors.Red;

    public static void ChangeEntryColor(Entry entry, bool isValid)
    {
        entry.TextColor = isValid ? OkColor : ErrorColor;
    }
}