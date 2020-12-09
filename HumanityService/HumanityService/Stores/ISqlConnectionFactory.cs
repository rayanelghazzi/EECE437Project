using System.Data;

namespace HumanityService.Stores
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}