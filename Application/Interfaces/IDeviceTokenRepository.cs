using System.Threading;
using System.Threading.Tasks;

using ChatApp.Domain.Entities;


namespace ChatApp.Application.Interfaces
{
    public interface IDeviceTokenRepository
    {
        Task UpsertAsync(DeviceToken token, CancellationToken ct = default);
    }
}