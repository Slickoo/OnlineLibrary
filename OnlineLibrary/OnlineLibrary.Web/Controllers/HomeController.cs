﻿using OnlineLibrary.Web.Infrastructure.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineLibrary.Web.Models;
using System.Collections.Generic;

namespace OnlineLibrary.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!IsUserNameSessionVariableSet())
            {
                InitializeUserNameSessionVariable();
            }

            // Obtain list of books from the database.
            var books = DbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.SubCategories)
                .Include("SubCategories.Category")
                .ToList();
            // Create list of view model objects.
            //CR: you can do this in the above call.
            var booksList = new List<BookViewModel>();
            foreach (var book in books)
            {
                booksList.Add(new BookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublishDate = book.PublishDate,
                    FrontCover = book.FrontCover,
                    //CR: always use string.Empty instead of ""
                    Authors = book.Authors.Select(a =>
                        string.Join(" ", a.FirstName, (a.MiddleName ?? ""), a.LastName)),
                    Categories = book.SubCategories.Select(sc => new CategoryViewModel
                    {
                        Category = sc.Category.Name,
                        SubCategory = sc.Name
                    }),
                    Description = book.Description
                });
            }
            return View(booksList);
        }
    }
}