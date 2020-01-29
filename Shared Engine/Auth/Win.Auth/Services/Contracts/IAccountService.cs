using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Models = Engine.Model;

namespace Engine.Auth
{
    public interface IAccountService
    {
        Task Login();
        void AdInfo();
        Task GetUsers();

    }
}