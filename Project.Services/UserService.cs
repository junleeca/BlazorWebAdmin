﻿using MDbContext.ExpressionSql;
using MDbContext.Repository;
using Project.AppCore.Repositories;
using Project.AppCore.Services;
using Project.Models;
using Project.Models.Entities;
using Project.Models.Permissions;
using Project.Models.Request;

namespace Project.Services
{
    public partial class UserService : IUserService
    {
        private readonly IExpressionContext context;

        public UserService(IExpressionContext context)
        {
            this.context = context;
        }

        public async Task<IQueryResult> DeleteUserAsync(User user)
        {
            var trans = context.BeginTransaction();
            trans.Delete<User>().Where(u => u.UserId == user.UserId).AttachTransaction();
            trans.Delete<UserRole>().Where(ur => ur.UserId == user.UserId).AttachTransaction();
            var result = await trans.CommitTransactionAsync();
            return result.Result();
        }

        public async Task<IQueryCollectionResult<User>> GetUserListAsync(GenericRequest<User> req)
        {
            var list = await context.Repository<User>().GetListAsync(req.Expression, out var count, req.PageIndex, req.PageSize);
            return list.CollectionResult((int)count);
        }

        public async Task<IQueryResult> InsertUserAsync(User user)
        {
            var u = await context.Repository<User>().InsertAsync(user);
            return u.Result();
        }

        public async Task<IQueryResult> UpdateUserAsync(User user)
        {
            var flag = await context.Repository<User>().UpdateAsync(user, u => u.UserId == user.UserId);
            return flag.Result();
        }
    }
}
