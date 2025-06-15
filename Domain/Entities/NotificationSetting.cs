namespace Domain.Entities
{
    public class NotificationSetting
    {
        public bool EnableNotifications { get; set; }
        public bool NotifyHighPressure { get; set; }
        public bool NotifyHighRainProbability { get; set; }
    }
}