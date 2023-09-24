using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Database;

namespace ZetaTradingTask.Application.Services
{
    public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly AppDbContext _context;

        private bool _disposed;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
