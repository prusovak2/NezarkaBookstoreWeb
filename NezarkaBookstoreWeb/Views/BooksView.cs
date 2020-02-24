using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	interface IBooksViewFactory {
		IView CreateView(ModelStore store);
	}

	class BooksViewFactory : IBooksViewFactory {
		public IView CreateView(ModelStore store) {
			return new BooksView(store);
		}

		private class BooksView : IView {
			public const int BooksPerRow = 3;

			public ModelStore store { get; private set; }

			public BooksView(ModelStore store) {
				this.store = store;
			}

			public void RenderTo(TextWriter w) {
				w.WriteLine("	Our books for you:");
				w.WriteLine("	<table>");

				var books = store.GetBooks();
				int bookIdx = 0;
				while (bookIdx < books.Count) {
					w.WriteLine("		<tr>");

					for (int i = 0; i < BooksPerRow && bookIdx < books.Count; i++, bookIdx++) {
						var book = books[bookIdx];
						w.WriteLine("			<td style=\"padding: 10px;\">");
						w.WriteLine("				<a href=\"/Books/Detail/" + book.Id + "\">" + book.Title + "</a><br />");
						w.WriteLine("				Author: " + book.Author + "<br />");
						w.WriteLine("				Price: " + book.Price + " EUR &lt;<a href=\"/ShoppingCart/Add/" + book.Id + "\">Buy</a>&gt;");
						w.WriteLine("			</td>");
					}

					w.WriteLine("		</tr>");
				}

				w.WriteLine("	</table>");
			}
		}
	}

}