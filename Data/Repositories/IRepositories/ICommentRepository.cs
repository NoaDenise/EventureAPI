﻿using EventureAPI.Models;

namespace EventureAPI.Data.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsByActitivityAsync(int actitivtyId);
        Task AddCommentAsync(Comment comment);
        Task EditCommentAsync(Comment comment);
        Task DeleteCommentAsync(int commentId);
        Task<Comment> GetCommentByIdAsync(int commentId);


    }
}
