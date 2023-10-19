using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        /// <param name="entityName">Name / type of the <see cref="Entity"/>.</param>
        /// <param name="id">The identifier of the duplicate key.</param>
        public EntityNotFoundException(string entityName, object id) : base($"'{entityName}' with 'Id':'{id}' was not found.")
        {
        }
    }
}
