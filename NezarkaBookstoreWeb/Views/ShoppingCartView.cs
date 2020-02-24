using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	interface IShoppingCartViewFactory {
		IView CreateView(ShoppingCart cart, ModelStore store);
	}

	class ShoppingCartViewFactory : IShoppingCartViewFactory {
		public IView CreateView(ShoppingCart cart, ModelStore store) {
			return new ShoppingCartView(cart, store);
		}

		private class ShoppingCartView : IView {
			public ShoppingCart cart { get; private set; }
			public ModelStore store { get; private set; }

			public ShoppingCartView(ShoppingCart cart, ModelStore store) {
				this.cart = cart;
				this.store = store;
			}

			public void RenderTo(TextWriter w) {
				if (cart.Items.Count == 0) {
					w.WriteLine("	Your shopping cart is EMPTY.");
				} else {
					w.WriteLine("	Your shopping cart:");
					w.WriteLine("	<table>");
					w.WriteLine("		<tr>");
					w.WriteLine("			<th>Title</th>");
					w.WriteLine("			<th>Count</th>");
					w.WriteLine("			<th>Price</th>");
					w.WriteLine("			<th>Actions</th>");
					w.WriteLine("		</tr>");

					decimal totalPrice = 0;
					foreach (var item in cart.Items) {
						var book = store.GetBook(item.BookId);
						var itemPrice = book.Price * item.Count;
						totalPrice += itemPrice;

						w.WriteLine("		<tr>");
						w.WriteLine("			<td><a href=\"/Books/Detail/" + item.BookId + "\">" + book.Title + "</a></td>");
						w.WriteLine("			<td>" + item.Count + "</td>");
						if (item.Count == 1) {
							w.WriteLine("			<td>" + book.Price + " EUR</td>");
						} else {
							w.WriteLine("			<td>" + item.Count + " * " + book.Price + " = " + itemPrice + " EUR</td>");
						}
						w.WriteLine("			<td>&lt;<a href=\"/ShoppingCart/Remove/" + item.BookId + "\">Remove</a>&gt;</td>");
						w.WriteLine("		</tr>");
					}

					w.WriteLine("	</table>");
					w.WriteLine("	Total price of all items: " + totalPrice + " EUR");
				}
			}
		}
	}

}