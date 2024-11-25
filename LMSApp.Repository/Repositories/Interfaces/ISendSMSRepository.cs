using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Repository.Repositories.Interfaces
{
	public interface ISendSMSRepository : IDisposable
    {
		 /// <summary>
        /// Gets the get connection.
        /// </summary>
        Task<string> Sendsms(string mobno, string content,string templateid);
    }
}
