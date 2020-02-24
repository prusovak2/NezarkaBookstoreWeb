using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	interface IMenuViewFactory {
		IView CreateView(Customer customer);
	}

	class MenuViewFactory : IMenuViewFactory {
		public IView CreateView(Customer customer) {
			return new MenuView(customer);
		}

		private class MenuView : IView {
			public Customer customer { get; private set; }

			public MenuView(Customer customer) {
				this.customer = customer;
			}

			public void RenderTo(TextWriter w) {
				w.Write("	" + customer.FirstName + " (we love you since ");
				if (customer.DateJoined == null) {
					w.Write("always");
				} else {
					w.Write(customer.DateJoined.Value.ToShortDateString());
				}
				w.WriteLine("), here is your menu:");
				w.WriteLine("	<table>");
				w.WriteLine("		<tr>");
				w.WriteLine("			<td><a href=\"/Books\">Books</a></td>");
				w.WriteLine("			<td><a href=\"/ShoppingCart\">Cart (" + customer.ShoppingCart.Items.Count + ")</a></td>");
				w.WriteLine("		</tr>");
				w.WriteLine("	</table>");
			}
		}
	}

}