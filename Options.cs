namespace ASCIIMod.CustomOPtions
{
    /// <summary>
    /// Thin layer over the mod's game options to determine what settings should be enabled or disabled
    /// when starting a new game.
    /// </summary>
    public class ASCIIOptions
    {
        public static bool ASCIIDmgFloatText => GetOption("OptionRealHomosapienNoDmgFloatText").EqualsNoCase("Yes");


        private static string GetOption(string ID, string Default = "") => XRL.UI.Options.GetOption(ID, Default: Default);
    }
    
}
