using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mailer.Repositories
{
    public interface ISendingRepository
    {
        public Task<SendStatus> Create(SendStatus sendStatus);
        public Task Delete(int id);
        public Task<IEnumerable<SendStatus>> Get();
        public Task<SendStatus> Get(int id);
        public Task Update(SendStatus sendStatus);
    }
}
