using RedRift.Common.Model;

using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public interface IGameBusinessLogic
    {
        Task Play(ILobby lobby, int randomSeed);
    }
}