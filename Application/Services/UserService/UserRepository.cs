using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public class UserRepository :IUSerRepo
    {
        private readonly IRepository<User> repository;

        public UserRepository(IRepository<User> _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken)
            ?? throw new NotFoundException($"No user was found");
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"the user with id{id} was not found");
        }

        public async Task CreateUser(User user)
        {
            await repository.AddAsync(user);
        }

        public async Task UpdateUser(Guid id,User user)
        {
            var userToUpdate = await repository.GetByIdAsync(id)
           ?? throw new NotFoundException($"the user with id{id} was not found");

            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;
           
           await repository.UpdateAsync(userToUpdate);
        }
        public async Task DeleteUser(Guid id)
        {
            var userToDelete = await repository.GetByIdAsync(id)
           ?? throw new NotFoundException($"the user with id{id} was not found");

            await repository.DeleteAsync(userToDelete);
        }
    }
}
