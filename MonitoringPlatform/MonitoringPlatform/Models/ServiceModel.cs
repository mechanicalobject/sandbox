namespace MonitoringPlatform.Models
{
    public class ServiceModel
    {
        public string ServiceName { get; set; }
        public WindowsServiceStatus Status { get; set; }
    }
}
