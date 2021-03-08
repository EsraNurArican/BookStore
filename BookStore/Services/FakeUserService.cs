using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class FakeUserService : IUserService
    {
        //fake list
        private List<User> users = new List<User> {

            new User{UserName="Esra", Name="Esra", Password="1234",Role=new Role{Name="User"} },
            new User{UserName="Tugce", Name="Tugcenur", Password="123",Role=new Role{Name="Admin" } },
            new User{UserName="Fatih", Name="M.Fatih",Password="111",Role=new Role{Name="Editor"}}
        };
        public User ValidUser(string username, string password)
        {
            return users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}
