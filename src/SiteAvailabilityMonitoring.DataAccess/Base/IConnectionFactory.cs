using System.Data.Common;

namespace SiteAvailabilityMonitoring.DataAccess.Base;

public interface IConnectionFactory
{
    DbConnection CreateConnection();
}