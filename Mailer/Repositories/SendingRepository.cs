using Mailer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mailer.Repositories
{
    public class SendingRepository: ISendingRepository
    {
        private readonly SendingContext _context;
        public SendingRepository(SendingContext context)
        {
            _context = context;
        }
        public async Task<SendStatus> Create(SendStatus sendStatus)
        {
            _context.Sendings.Add(sendStatus);
            await _context.SaveChangesAsync();
            return sendStatus;
        }
        public async Task Delete(int id)
        {
            var sendingToDelete = await _context.Sendings.FindAsync(id);
            _context.Sendings.Remove(sendingToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SendStatus>> Get()
        {
            return await _context.Sendings.ToListAsync();
        }
        public async Task<SendStatus> Get(int id)
        {
            return await _context.Sendings.FindAsync(id);
        }
        public async Task Update(SendStatus sendStatus)
        {
            _context.Entry(sendStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
