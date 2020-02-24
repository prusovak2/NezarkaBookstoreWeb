using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	class BooksController : IController {
		private ModelStore store;
		private IBooksViewFactory booksViewFactory;
		private IBooksDetailViewFactory booksDetailViewFactory;

		public BooksController(ModelStore store, IBooksViewFactory booksViewFactory, IBooksDetailViewFactory booksDetailViewFactory) {
			this.store = store;
			this.booksViewFactory = booksViewFactory;
			this.booksDetailViewFactory = booksDetailViewFactory;
		}

		public IView HandleRequest(Customer customer, string action, int id) {
			switch (action) {
				case null:
					return booksViewFactory.CreateView(store);

				case "Detail":
					var book = store.GetBook(id);
					if (book == null) {
						return null;
					} else {
						return booksDetailViewFactory.CreateView(book);
					}

				default:
					return null;
			}
		}
	}

}