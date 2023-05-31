
using System.ComponentModel;

namespace ConferenceReservationSystem.Enumerations
{
    public enum PersonGender 
    {
        [Description("مرد")]
        Male = 0,
        [Description("زن")]
        Female
    }

    public enum ConferenceStatus 
    {
        [Description("فعال")]
        Enabled = 0,
        [Description("غیر فعال")]
        Disabled
    }
}
