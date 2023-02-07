namespace Service.Contracts;

public interface IServiceManager
{
    IAccountsService Accounts { get; }
}