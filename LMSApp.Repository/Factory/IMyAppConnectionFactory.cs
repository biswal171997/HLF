using System.Data;

namespace LMSApp.Repository.Factory
{
     public interface IMyAppConnectionFactory
    {
        /// <summary>
        /// Gets the get connection.
        /// </summary>
        /// <value>The get connection.</value>
        IDbConnection GetConnection { get; }
    }
}
