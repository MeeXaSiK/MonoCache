namespace NTC.Global.Cache
{
    public static class ColoredText
    {
        public const string WhiteColor = "FFFFFF";
        public const string BlueColor = "00FFF7";
        public const string OrangeColor = "F4CA16";
        public const string RedColor = "E22121";
        
        public static string GetColoredText(string color, string text)
        {
            if (color.IndexOf('#') == -1)
                color = '#' + color;

            return $"<color={color}>{text}</color>";
        }
    }
}