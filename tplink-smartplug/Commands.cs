namespace tplink_smartplug
{
    public static class Commands
    {
        /// <summary>
        /// Command to turn device on
        /// </summary>
        public static readonly string On = "{\"system\":{\"set_relay_state\":{\"state\":1}}}";
        /// <summary>
        /// Command to turn device off
        /// </summary>
        public static readonly string Off = "{\"system\":{\"set_relay_state\":{\"state\":0}}}";
        /// <summary>
        /// Command to get system info from device (Software version, Hardware version, Model, MAC, device ID etc.)
        /// </summary>
        public static readonly string Info = "{\"system\":{\"get_sysinfo\":{}}}";
        /// <summary>
        /// Command to get cloud info from device (Username, Server, Connection status)
        /// </summary>
        public static readonly string CloudInfo = "{\"cnCloud\":{\"get_info\":{}}}";
        /// <summary>
        /// Command to get device to scan for available Access Points
        /// </summary>
        public static readonly string WlanScan = "{\"netif\":{\"get_scaninfo\":{\"refresh\":0}}}";
        /// <summary>
        /// Command to get time from device
        /// </summary>
        public static readonly string Time = "{\"time\":{\"get_time\":{}}}";
        /// <summary>
        /// Command to get timezone from device
        /// </summary>
        public static readonly string Timezone = "{\"time\":{\"get_timezone\":{}}}";
        /// <summary>
        /// Command to get schedule rule list from device
        /// </summary>
        public static readonly string Schedules = "{\"schedule\":{\"get_rules\":{}}}";
        /// <summary>
        /// Command to get countdown rule from device
        /// </summary>
        public static readonly string Countdown = "{\"count_down\":{\"get_rules\":{}}}";
        /// <summary>
        /// Command to get anti theft rules from device
        /// </summary>
        public static readonly string Antitheft = "{\"anti_theft\":{\"get_rules\":{}}}";
        /// <summary>
        /// Command to reboot the device
        /// </summary>
        public static readonly string Reboot = "{\"system\":{\"reboot\":{\"delay\":1}}}";
    }
}