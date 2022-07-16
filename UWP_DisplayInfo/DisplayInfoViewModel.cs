using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Devices.Display;
using Windows.Devices.Display.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;

namespace UWP_DisplayInfo
{
    public class DisplayInfoViewModel : ObservableRecipient
    {
        public ObservableCollection<DisplayInfoModel> DisplayInfoItems { get; } = new ObservableCollection<DisplayInfoModel>();

        private DisplayInfoModel selectedInfoModel;

        public DisplayInfoModel SelectedInfoModel
        {
            get => selectedInfoModel;
            set => SetProperty(ref selectedInfoModel, value, true);
        }

        private DisplayInfoModel appAssociatedInfoModel;

        public DisplayInfoModel AppAssociatedInfoModel
        {
            get => appAssociatedInfoModel;
            set => SetProperty(ref appAssociatedInfoModel, value, true);
        }

        public void LoadDisplayInfo()
        {
            DisplayManager displayManager = DisplayManager.Create(DisplayManagerOptions.None);

            var currentTargets = displayManager.GetCurrentTargets();
            var displayManagerResult = displayManager.TryReadCurrentStateForAllTargets();

            foreach (var target in currentTargets)
            {
                // Only list connected targets
                if (target.IsConnected)
                {
                    DisplayInfoModel model = new DisplayInfoModel();

                    // To get the monitor of a target, use one of the following methods:
                    // DisplayMonitor displayMonitor = await DisplayMonitor.FromInterfaceIdAsync(target.DeviceInterfacePath);
                    DisplayMonitor displayMonitor = target.TryGetMonitor();

                    // Compose a fallback name for monitor with a empty display name
                    if (string.IsNullOrWhiteSpace(displayMonitor?.DisplayName))
                    {
                        model.DisplayName = $"{displayMonitor?.ConnectionKind.ToString()} Monitor";
                    }
                    else
                    {
                        model.DisplayName = displayMonitor?.DisplayName;
                    }
                    
                    model.MonitorDeviceId = displayMonitor?.DeviceId;
                    model.ConnectionKind = displayMonitor?.ConnectionKind.ToString();
                    model.PhysicalConnector = displayMonitor?.PhysicalConnector.ToString();
                    model.UsageKind = displayMonitor?.UsageKind.ToString();
                    model.IsDolbyVisionSupportedInHdrMode = displayMonitor?.IsDolbyVisionSupportedInHdrMode ?? false;

                    if (displayManagerResult.ErrorCode is DisplayManagerResult.Success)
                    {
                        var targetPath = displayManagerResult.State.GetPathForTarget(target);
                        uint presentationRate = targetPath.PresentationRate.HasValue
                            ? (targetPath.PresentationRate.Value.VerticalSyncRate.Numerator / targetPath.PresentationRate.Value.VerticalSyncRate.Denominator)
                            : 0;
                        model.RefreshRate = $"{presentationRate} Hz";
                        model.SourcePixelFormat = targetPath.SourcePixelFormat.ToString();
                        model.ColorDepth = targetPath.WireFormat.BitsPerChannel;
                        model.ColorSpace = targetPath.WireFormat.ColorSpace.ToString();
                        model.PixelEncoding = targetPath.WireFormat.PixelEncoding.ToString();
                        model.Rotation = targetPath.Rotation.ToString();
                        model.Scaling = targetPath.Scaling.ToString();
                        model.Status = targetPath.Status.ToString();
                        model.Resolution = targetPath.TargetResolution.HasValue
                            ? $"{targetPath.TargetResolution.Value.Width} × {targetPath.TargetResolution.Value.Height}"
                            : "N/A";
                    }

                    DisplayInfoItems.Add(model);
                }
            }

            if (DisplayInfoItems.Any())
            {
                SelectedInfoModel = DisplayInfoItems.First();

                var displayRegions = ApplicationView.GetForCurrentView().WindowingEnvironment.GetDisplayRegions();

                var firstDisplayRegion = displayRegions.First();

                AppAssociatedInfoModel = DisplayInfoItems.First(x => string.Equals(x.MonitorDeviceId, firstDisplayRegion.DisplayMonitorDeviceId));
            }
        }
    }
}
