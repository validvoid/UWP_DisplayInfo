namespace UWP_DisplayInfo
{
    public class DisplayInfoModel
    {
        public string MonitorDeviceId { get; set; }

        public string DisplayName { get; set; }

        public string ConnectionKind { get; set; }

        public string PhysicalConnector { get; set; }

        public string UsageKind { get; set; }

        public bool IsDolbyVisionSupportedInHdrMode { get; set; }

        public int ColorDepth { get; set; }

        public string ColorSpace { get; set; }

        public string PixelEncoding { get; set; }

        public string SourcePixelFormat { get; set; }

        public string RefreshRate { get; set; }

        public string Resolution { get; set; }

        public string Rotation { get; set; }

        public string Scaling { get; set; }

        public string Status { get; set; }
    }
}
