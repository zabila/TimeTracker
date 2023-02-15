using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAccountsService {
    IEnumerable<AccountDto> GetAllAccounts(bool trackChanges);
    AccountDto? GetAccount(Guid accountId, bool trackChanges);
    AccountDto CreateAccount(AccountForCreationDto account);
    void DeleteAccount(Guid accountId);
    void UpdateAccount(Guid accountId, AccountForUpdateDto accountForUpdateDto, bool trackChanges);


    Task<IEnumerable<AccountDto>> GetAllAccountsAsync(bool trackChanges);
    Task<AccountDto?> GetAccountAsync(Guid accountId, bool trackChanges);
    Task<AccountDto> CreateAccountAsync(AccountForCreationDto account);
    Task DeleteAccountAsync(Guid accountId);
    Task UpdateAccountAsync(Guid accountId, AccountForUpdateDto accountForUpdateDto, bool trackChanges);
}