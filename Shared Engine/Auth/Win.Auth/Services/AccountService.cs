using Engine.Http;
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
    public class AccountService : IAccountService
    {
        protected string CurrentUserName { get; set; }
        private readonly IAuthClient _httpClient;
        private string UserName;
        private Hashtable AdGroups = new Hashtable();
        private Models.UserModel UserInfo = null;
        private string RoleName = "";
        private int RoleId = 0;
        private readonly IHttpContextAccessor _contextAccessor;
        private List<Models.UserModel> Users;

        public AccountService(IAuthClient httpClient, IHttpContextAccessor contextAccessor)
        {
            //try
            //{
            //throw new Exception("See first");
            _contextAccessor = contextAccessor;
            _httpClient = httpClient;
            //this.Login().Wait();

            //}
            //catch 
            //{
            //throw;
            //}
        }

        public async Task Login()
        {
            try
            {
                AdInfo();
                bool bolAllowUser = false;

                //_strRoleName = "Administrator";
                //Session["RoleName"] = _strRoleName;
                //Session["RoleValue"] = _intRoleValue;
                //bolAllowUser = true;

                //if (_contextAccessor.HttpContext.Session.GetString("AllowUser") == null)
                //{
                //    _contextAccessor.HttpContext.Session.SetString("AllowUser", "true");
                //    await GetUsers();
                //}
                if (this.Users == null)
                {
                    await GetUsers();
                }
                if (_contextAccessor.HttpContext.Session.GetString("RoleName") != null)
                {
                    RoleName = _contextAccessor.HttpContext.Session.GetString("RoleName").ToString();
                    RoleId = _contextAccessor.HttpContext.Session.GetInt32("RoleId") ?? 0;
                }
                //if (this.UserName.Equals("APAC\\GGUILLE"))
                //{
                //    RoleName = "Administrator";
                //    _contextAccessor.HttpContext.Session.SetString("RoleName", RoleName);
                //    _contextAccessor.HttpContext.Session.SetInt32("RoleId", RoleId);
                //}
                if ((UserInfo != null) || (_contextAccessor.HttpContext.Session.GetString("AllowUser") != null))
                {
                    bolAllowUser = true;
                    if (_contextAccessor.HttpContext.Session.GetString("AllowUser") == null) _contextAccessor.HttpContext.Session.SetString("AllowUser", "true");
                    if (UserInfo != null)
                    {
                        _contextAccessor.HttpContext.Session.SetString("RoleName", UserInfo.Roles.Where(x => x.MainRole).FirstOrDefault().Name);
                        _contextAccessor.HttpContext.Session.SetInt32("RoleId", UserInfo.Roles.Where(x => x.MainRole).FirstOrDefault().Id);
                        CurrentUserName = UserInfo.UserName;
                        RoleName = _contextAccessor.HttpContext.Session.GetString("RoleName").ToString();
                        RoleId = _contextAccessor.HttpContext.Session.GetInt32("RoleId") ?? 0;
                    }
                }

                if (!bolAllowUser)
                {
                    //RedirectResult('')
                    //throw new UnauthorizedAccessException();
                }

                //return RedirectToAction("Index", "Host");

            }
            catch (Exception ex)
            {
                throw;
                //return RedirectToAction("Index", "Host");
            }
        }

        public void AdInfo()
        {

            //if (string.IsNullOrEmpty(UserName)) UserName = User.Identity.Name.ToString().ToUpper();
            //if (string.IsNullOrEmpty(UserName)) UserName = WindowsIdentity.GetCurrent().Name.ToString().ToUpper();

            //if (string.IsNullOrEmpty(UserName)) throw new Exception("Cannot authenticate user");
            try
            {
                //status 500 on error
                UserName = _contextAccessor.HttpContext.User.Identity.Name.ToString().ToUpper();

                if (_contextAccessor.HttpContext.Session.GetString("UserName") == null)
                {
                    _contextAccessor.HttpContext.Session.SetString("UserName", this.UserName);
                }
                //string userDomain = Environment.UserDomainName;
                //UserPrincipal user = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), IdentityType.SamAccountName, UserName);
                //PrincipalSearchResult<Principal> principalSearch = user.GetGroups();
                //foreach (GroupPrincipal group in principalSearch)
                //{
                //    string groupName = group.Name;
                //    groupName = userDomain + @"\" + groupName;
                //    AdGroups.Add(groupName, groupName);
                //    break;
                //}
            }
            catch
            {
                throw new Exception("Cannot Fetch User Identity");
            }
        }
        public async Task GetUsers()
        {
            try
            {
                var response = await _httpClient.GetUsers();
                var content = await response.Content.ReadAsAsync<Models.ResponseModelBase<List<Models.UserModel>>>();
                this.Users = content.Data;
                if (content.Data.Count > 0)
                {
                    //string strFilter = "UserName='" + this.UserName.Replace("'", "''") + "'";
                    if (content.Data.Any(x => x.UserName == this.UserName))
                    {
                        this.UserInfo = content.Data.Find(x => x.UserName == this.UserName);
                    }
                }
                //if user not found, check by group
                if (this.UserInfo == null)
                {
                    //foreach (var item in content.Data)
                    //{
                    //    string name = item.UserName;
                    //    if (AdGroups.ContainsKey(name))
                    //    {
                    //        this.UserInfo = item;
                    //        break;
                    //    }

                    //}

                    //allow anyone to access
                    //throw new Exception("User not added in database");
                }

                //if (UserInfo == null) { throw new Exception("User not found in authorized users"); }
            }
            catch (Exception)
            {
                throw;
            }


        }

    }
}