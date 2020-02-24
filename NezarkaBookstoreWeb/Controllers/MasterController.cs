using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	// Supported requests (where x is a valid customer Id, and 5 is a valid book Id):
	// GET x /Books
	// GET x /Books/Detail/5
	// GET x /ShoppingCart	
	// GET x /ShoppingCart/Add/5
	// GET x /ShoppingCard/Remove/5

	class MasterController {
		private const string CommonRequestPrefix = "http://www.nezarka.net/";

		private ModelStore store;
		private IController booksController;
		private IController shoppingCartController;
		private IMasterViewFactory masterViewFactory;
		private IMenuViewFactory menuViewFactory;

		public MasterController(
			ModelStore store,
			IController booksController, IController shoppingCartController,
			IMasterViewFactory masterViewFactory,
			IMenuViewFactory menuViewFactory
		) {
			this.store = store;
			this.booksController = booksController;
			this.shoppingCartController = shoppingCartController;
			this.masterViewFactory = masterViewFactory;
			this.menuViewFactory = menuViewFactory;
		}

		public void ExecuteRequest(string request, TextWriter responseWriter) {
			IView view = InvalidRequestView.Instance;

			try {
				string[] tokens = request.Split(' ');

				if (tokens[0] == "GET") {
					var customer = store.GetCustomer(int.Parse(tokens[1]));

					if (customer != null && tokens[2].StartsWith(CommonRequestPrefix)) {
						string[] verbs = tokens[2].Substring(CommonRequestPrefix.Length).Split('/');

						if (verbs.Length <= 3) {
							string action = verbs.Length >= 2 ? verbs[1] : null;
							int id = verbs.Length >= 3 ? int.Parse(verbs[2]) : 0;

							switch (verbs[0]) {
								case "Books":
									view = booksController.HandleRequest(customer, action, id);
									break;

								case "ShoppingCart":
									view = shoppingCartController.HandleRequest(customer, action, id);
									break;

								default:
									view = null;
									break;
							}

							if (view == null) {
								view = InvalidRequestView.Instance;
							} else {
								view = masterViewFactory.CreateView(
									menuViewFactory.CreateView(customer),
									view
								);
							}
						}
					}
				}
			} catch (Exception ex) {
				if (ex is IndexOutOfRangeException || ex is FormatException || ex is ArgumentOutOfRangeException) {
					// Do nothing <- InvalidRequestView.Instance view is fine
				} else {
					throw;
				}
			}

			view.RenderTo(responseWriter);
		}

	}

}