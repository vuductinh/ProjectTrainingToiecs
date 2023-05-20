using System.Net.NetworkInformation;

namespace ProjectTrainingToiecs.Service.Service
{
    public interface ICommonService
    {
        string GetLocalIPv4(NetworkInterfaceType _type);
        string GetBetween(string strSource, string strStart, string strEnd);
    }
}
