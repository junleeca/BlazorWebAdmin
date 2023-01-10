﻿using LogAopCodeGenerator;
using Project.AppCore.Aop;
using Project.Models;
using Project.Models.Permissions;

namespace Project.AppCore.Services
{
    [Aspectable(AspectHandleType = typeof(LogAop))]
    public partial interface ILoginService
    {
        [LogInfo(Action = "用户登录", Module = "登录模块")]
        Task<IQueryResult<UserInfo>> LoginAsync(string username, string password);
        [LogInfo(Action = "用户登录[自动]", Module = "登录模块")]
        Task<IQueryResult<bool>> UpdateLastLoginTimeAsync(string username);
        Task<bool> CheckUser(UserInfo info);
        [LogInfo(Action = "用户登出", Module = "登录模块")]
        Task<IQueryResult<bool>> LogoutAsync();
    }
}
