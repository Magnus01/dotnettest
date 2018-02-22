using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager
{
    public class UsersDataStore
    {
        public static UsersDataStore Current { get; } = new UsersDataStore(); 
        public List<UserDto> Users { get; set; }

        public UsersDataStore()
        {
            Users = new List<UserDto>()
            {
                new UserDto()
                {
                    Id = "1",
                    Name = "New York User",
                    Description = "The one with the big park"
                },
                new UserDto()
                {
                    Id = "2",
                    Name = "New York User",
                    Description = "The one with the big park"
                }

            };
        }      
    }
}
