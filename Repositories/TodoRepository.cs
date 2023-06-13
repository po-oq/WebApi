using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class TodoRepository : IRepository<TodoItem>
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task<TodoItem> GetOne(long id)
        {
            //return await _context.Set<TodoItem>().FindAsync(id);
            return await _context.Set<TodoItem>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.Set<TodoItem>().ToListAsync();  // System.InvalidOperationException: The source 'IQueryable' doesn't implement 'IAsyncEnumerable<WebApi.Models.TodoItem>'. Only sources that implement 'IAsyncEnumerable' can be used for Entity Framework asynchronous operations.
        }
        public async Task<TodoItem> Add(TodoItem item)
        {
            // TodoItemがnullならエラー
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            // IsCompleteがfalseならエラー
            if (!item.IsComplete)
            {
                throw new ArgumentException("IsCompleteがfalseです。", nameof(item));
            }
            _context.Set<TodoItem>().Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TodoItem> Update(TodoItem item)
        {
            // itemがTodoItemに存在しなければエラー
            if (!_context.Set<TodoItem>().Any(x => x.Id == item.Id))
            {
                throw new ArgumentException("itemがTodoItemに存在しません。", nameof(item));
            }
            // IsCompleteがfalseならエラー
            if (!item.IsComplete)
            {
                throw new ArgumentException("IsCompleteがfalseです。", nameof(item));
            }
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TodoItem> Delete(long id)
        {
            // itemがTodoItemに存在しなければエラー
            if (!_context.Set<TodoItem>().Any(x => x.Id == id))
            {
                throw new ArgumentException("idがTodoItemに存在しません。", nameof(id));
            }
            var item = await GetOne(id);
            if (item == null)
            {
                throw new ArgumentException("itemがTodoItemに存在しません。", nameof(item));
            }
            // IsCompleteがfalseならエラー
            if (!item.IsComplete)
            {
                throw new ArgumentException("IsCompleteがfalseです。", nameof(item));
            }
            _context.Set<TodoItem>().Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
