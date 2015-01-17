using System.Collections.Generic;
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
            // TODO
            return null;

        }
    }
}
