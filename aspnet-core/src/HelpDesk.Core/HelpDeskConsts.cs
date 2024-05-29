using HelpDesk.Debugging;

namespace HelpDesk
{
    public class HelpDeskConsts
    {
        public const string LocalizationSourceName = "HelpDesk";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "35b8879a33bf4124b43ec88cc4f3725a";
    }
}
