using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	interface IBooksDetailViewFactory {
		IView CreateView(Book book);
	}

	class BooksDetailViewFactory : IBooksDetailViewFactory {
		public IView CreateView(Book book) {
			return new BooksDetailView(book);
		}

		private class BooksDetailView : IView {
			public Book book { get; private set; }

			public BooksDetailView(Book book) {
				this.book = book;
			}

			public void RenderTo(TextWriter w) {
				w.WriteLine("	Book details:");
				w.WriteLine("	<h2>" + book.Title + "</h2>");
				w.WriteLine("	<p style=\"margin-left: 20px\">");
				w.WriteLine("	Author: " + book.Author + "<br />");
				w.WriteLine("	Price: " + book.Price + " EUR<br />");
				w.WriteLine("	</p>");
				w.WriteLine("	<h3>&lt;<a href=\"/ShoppingCart/Add/" + book.Id + "\">Buy this book</a>&gt;</h3>");
			}
		}
	}

}