using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AlkemyWallet.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET
        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            var accountsDTO = new List<AccountDTO>();

            foreach (var account in accounts)
            {
                accountsDTO.Add(AccountMapper.AccountToAccountDTO(account));
            }

            return accountsDTO;

        }

        // GET
        public async Task<AccountDTO> GetByIdAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            var accountDTO = new AccountDTO();

            accountDTO = AccountMapper.AccountToAccountDTO(account);
            return accountDTO;
        }

        public async Task<AccountUpdateDTO> GetByIdUpdateAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            var accountDTO = _mapper.Map<AccountUpdateDTO>(account);

            return accountDTO;
        }

        public async Task<AccountUpdateDTO> UpdateAsync(int id, AccountUpdateDTO accountDTO)
        {
            var accountToUpdate = _mapper.Map<Account>(accountDTO);
            accountToUpdate.Id = id;
            try
            {
                var accountUpdate = _unitOfWork.AccountRepository.UpdateAsync(accountToUpdate);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return accountDTO;
        }

        public async Task<AccountUpdateDTO> UpdatePatchAsync(AccountUpdateDTO exitsAccount, JsonPatchDocument<AccountUpdateDTO> pathDocument)
        {
            //_unitOfWork.AccountRepository.UpdatePatchAsync(exitsAccount, pathDocument);
            
            pathDocument.ApplyTo(exitsAccount);
            // verificar
            var accountToUpdate = _mapper.Map<Account>(exitsAccount);
            var accountUpdate = _unitOfWork.AccountRepository.UpdateAsync(accountToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return exitsAccount;
        }
    }
}
