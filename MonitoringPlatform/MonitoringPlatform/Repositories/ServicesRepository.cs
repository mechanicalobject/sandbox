using System.Collections.Generic;
using System.ServiceProcess;
using MonitoringPlatform.Models;

namespace MonitoringPlatform.Repositories
{
    public interface IServicesRepository
    {
        IList<ServiceModel> GetServices();
    }

    public class ServicesRepository : IServicesRepository
    {

        public IList<ServiceModel> GetServices()
        {
            var windowsServices = ServiceController.GetServices();

            IList<ServiceModel> serviceModels = new List<ServiceModel>(windowsServices.Length);

            foreach (ServiceController serviceController in windowsServices)
            {
                var serviceModel = new ServiceModel();
                serviceModel.ServiceName = serviceController.ServiceName;

                switch (serviceController.Status)
                {
                    case ServiceControllerStatus.ContinuePending:
                    case ServiceControllerStatus.PausePending:
                    case ServiceControllerStatus.StartPending:
                    case ServiceControllerStatus.StopPending:
                        serviceModel.Status = WindowsServiceStatus.Intermediate;
                        break;
                    case ServiceControllerStatus.Paused:
                        serviceModel.Status = WindowsServiceStatus.Paused;
                        break;
                    case ServiceControllerStatus.Running:
                        serviceModel.Status = WindowsServiceStatus.Running;
                        break;
                    case ServiceControllerStatus.Stopped:
                        break;
                }

                serviceModels.Add(serviceModel);
            }

            return serviceModels;
        }
    }

}
