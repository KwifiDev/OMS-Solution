﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using OMS.UI.APIs.Services.Interfaces.Tables;
using OMS.UI.Models;

namespace OMS.UI.Services.UserSession
{
    public partial class UserSessionService : ObservableObject, IUserSessionService
    {
        private readonly IUserService _userService;
        //private readonly IMapper _mapper;

        [ObservableProperty]
        private bool _isLoggedIn;

        [ObservableProperty]
        private UserLoginModel? _currentUser;


        public UserSessionService(IUserService userService)
        {
            _userService = userService;
            //_mapper = mapper;
        }

        public void Login(UserLoginModel user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
        }

        public void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }

        public async Task UpdateModel()
        {
            var userLoginModel = await _userService.GetUserLoginByPersonIdAsync(CurrentUser!.PersonId);

            //var userLoginModel = _mapper.Map<UserLoginModel>(userLoginModel);

            CurrentUser = userLoginModel ?? CurrentUser;

            WeakReferenceMessenger.Default.Send(CurrentUser);
        }
    }
}
