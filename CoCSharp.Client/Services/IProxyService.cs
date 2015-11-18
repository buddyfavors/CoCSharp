using CoCSharp.Client.API.Events;

namespace CoCSharp.Client.Services
{
    public interface IProxyService
    {
        void Start();
        void OnAllianceInfo(object sender, AllianceInfoEventArgs e);
    }
}
