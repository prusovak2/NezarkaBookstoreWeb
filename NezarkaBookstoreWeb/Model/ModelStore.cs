﻿using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JumpingPlatformGame")]

namespace NezarkaBookstoreWeb
{
	
	 class ModelStore {
		private List<Book> books = new List<Book>();
		private List<Customer> customers = new List<Customer>();

		public IList<Book> GetBooks() {
			return books;
		}

		public Book GetBook(int id) {
			return books.Find(b => b.Id == id);
		}

		public Customer GetCustomer(int id) {
			return customers.Find(c => c.Id == id);
		}

		public List<Customer> GetCustomersList()
		{
			return this.customers;
		}

		public static ModelStore LoadFrom(TextReader reader) {
			var store = new ModelStore();

			try {
				if (reader.ReadLine() != "DATA-BEGIN") {
					return null;
				}
				while (true) {
					string line = reader.ReadLine();
					if (line == null) {
						return null;
					} else if (line == "DATA-END") {
						break;
					}

					string[] tokens = line.Split(';');
					switch (tokens[0]) {
						case "BOOK":
							store.books.Add(new Book {
								Id = int.Parse(tokens[1]), Title = tokens[2], Author = tokens[3], Price = decimal.Parse(tokens[4])
							});
							break;
						case "CUSTOMER": {
								var customer = new Customer {
									Id = int.Parse(tokens[1]), FirstName = tokens[2], LastName = tokens[3], DateJoined = null
								};
								if (tokens.Length >= 6) {
									customer.DateJoined = new DateTime(int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]));
								}
								store.customers.Add(customer);
								break;
							}
						case "CART-ITEM": {
								var customer = store.GetCustomer(int.Parse(tokens[1]));
								if (customer == null) {
									return null;
								}
								customer.ShoppingCart.Items.Add(new ShoppingCartItem {
									BookId = int.Parse(tokens[2]), Count = int.Parse(tokens[3])
								});
								break;
							}
						default:
							return null;
					}
				}
			} catch (Exception ex) {
				if (ex is FormatException || ex is IndexOutOfRangeException) {
					return null;
				}
				throw;
			}

			return store;
		}
	}

}