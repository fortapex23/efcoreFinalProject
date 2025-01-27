using System.Collections.Generic;
using Library_Management_EF_Business.Interfaces;
using Library_Management_EF_Core.Models;
using Library_Management_EF_Core.Repositories;
using Library_Management_EF_Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_EF_Business.Implementations
{
    public class BookService : IBookService
    {
        private IBookRepository _repository;
        private ILoanItemRepository _loanItemRepository;
        public BookService()
        {
            _repository = new BookRepository();
            _loanItemRepository = new LoanItemRepository();
        }

        public async Task CreateBookAsync(Book book)
        {
            await _repository.Insert(book);
            await _repository.CommitAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var wantedbook = await _repository.GetAsync(id);
            if (wantedbook == null)
            {
                throw new NullReferenceException();
            }
            _repository.Delete(wantedbook);
            await _repository.CommitAsync();
        }

        public async Task<List<Book>> FilterBooksByTitle(string title)
        {
            var book = await _repository.GetAll().Where(x => x.Title.Contains(title)).ToListAsync();
            return book;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<Book> GetMostBorrowedBook()
        {
            var loanitem = await _loanItemRepository.GetAll().ToListAsync();
            var borrowerCount = new Dictionary<int, int>();

            int maxBorrows = 0;
            int mostborrowedbook = 0;

            foreach (var item in loanitem)
            {
                if (borrowerCount.ContainsKey(item.BookId))
                {
                    borrowerCount[item.BookId]++;
                }
                else
                {
                    borrowerCount[item.BookId] = 1;
                }
            }

            foreach (var item in borrowerCount)
            {
                if (item.Value > maxBorrows)
                {
                    maxBorrows = item.Value;
                    mostborrowedbook = item.Key;
                }
            }
            var mostBorrowedBook = await _repository.GetAsync(mostborrowedbook);

            return mostBorrowedBook;
        }

        public async Task UpdateBookAsync(Book book)
        {
            var data = await _repository.GetAsync(book.Id);
            if (data == null)
            {
                throw new NullReferenceException();
            }
            data.Title = book.Title;
            data.Desc = book.Desc;
            data.PublishedYear = book.PublishedYear;
            data.BorrowerId = book.BorrowerId;
            await _repository.CommitAsync();
        }

        public async Task<List<Book>> FilterBooksByAuthor(string authorname)
        {
            var books = await _repository.GetAll().Where(x => x.AuthorBooks.Any(x => x.Author.Name.Contains(authorname))).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetAllBooksWhereAsync()
        {
            return await _repository.GetAll().Where(x => x.BorrowerId != null).ToListAsync();
        }

        public async Task<List<Book>> GetAllBooksWhereNoBorrowerAsync()
        {
            return await _repository.GetAll().Where(x => x.BorrowerId == null).ToListAsync();
        }
    }
}
