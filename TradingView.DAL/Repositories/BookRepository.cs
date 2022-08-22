﻿using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TradingView.DAL.Contracts;

namespace TradingView.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BookRepository(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();
}
