﻿using AntDesign;
using BlazorWeb.Shared.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Project.AppCore.Auth;
using Project.AppCore.Services;
using Project.AppCore.Store;
using Project.Models.Forms;
using System.Web;

namespace BlazorWebAdmin.Shared
{
    public partial class Login
    {
        private LoginFormModel model = new LoginFormModel();
        [Inject] public UserStore UserStore { get; set; }
        [Inject] public RouterStore RouterStore { get; set; }
        [Inject] public MessageService MessageSrv { get; set; }
        [Inject] public ILoginService LoginSrv { get; set; }
        [Inject] public AuthenticationStateProvider Auth { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [CascadingParameter] RootLayout Root { get; set; }
        public bool Loading { get; set; } = false;
        public string? Redirect { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var full = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            Redirect = HttpUtility.ParseQueryString(full.Query).Get(nameof(Redirect));
            Root.OnKeyDown += OnPressEnter;
        }

        private void OnPressEnter(KeyboardEventArgs e)
        {
            Console.WriteLine("OnPressEnter: " + e.Key);
            if (e.Key == "Enter")
                _ = HandleLogin();
        }

        private async Task HandleLogin()
        {
            Loading = true;
            await Task.Yield();
            var result = await LoginSrv.LoginAsync(model.UserName, model.Password);
            if (result.Success)
            {
                UserStore.SetUser(result.Payload);
                await ((CustomAuthenticationStateProvider)Auth).IdentifyUser(result.Payload);
                await RouterStore.InitRoutersAsync(result.Payload);
                _ = MessageSrv.Info("登录成功");
                Root.OnKeyDown -= OnPressEnter;
                if (string.IsNullOrEmpty(Redirect))
                    NavigationManager.NavigateTo("/");
                else
                    NavigationManager.NavigateTo(Redirect);
            }
            else
            {
                _ = MessageSrv.Info(result.Message);
            }
            Loading = false;
        }
    }
}
