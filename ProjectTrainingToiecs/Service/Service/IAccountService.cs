using ProjectTrainingToiecs.Service.Service.Model;

namespace ProjectTrainingToiecs.Service.Service
{
    public interface IAccountService
    {
        string CheckLogin(AccountModel model);
        string DeCodePassword(string password);
        string SendEmailDigitNumber(AccountModel model);
        //bool UpdateUserActive(int userId);
    }
}
