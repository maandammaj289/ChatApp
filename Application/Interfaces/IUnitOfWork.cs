using System.Threading;
using System.Threading.Tasks;


namespace ChatApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}