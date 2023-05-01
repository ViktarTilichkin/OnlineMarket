﻿using MySql.Data.MySqlClient;
using OnlineMarket.Models;
using OnlineMarket.Repository;

namespace OnlineMarket.Services
{
    public class UserService
    {
        private readonly UsersRepository m_Service;
        public UserService(UsersRepository rep)
        {
            m_Service = rep ?? throw new ArgumentNullException(nameof(rep));
        }
        public async Task<List<User>> GetAll()
        {
            return await m_Service.GetAll();
        }
        public async Task<User> GetById(int id)
        {
            return await m_Service.GetById(id);
        }
        public async Task<User> Create(User user)
        {
            return await m_Service.Create(user);
        }
        public async Task<User> Update(User user)
        {
            return await m_Service.Update(user);
        }
        public async Task<bool> Delete(int id)
        {
            return await m_Service.Delete(id);
        }
    }
}
