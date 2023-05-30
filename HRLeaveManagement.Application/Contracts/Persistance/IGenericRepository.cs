﻿namespace HRLeaveManagement.Application.Contracts.Persistance;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAsync();

    Task<T> GetByIdAsync(int id);

    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> DeleteAsync(T entity);
}