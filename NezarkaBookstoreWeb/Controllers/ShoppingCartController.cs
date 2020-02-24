using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	class ShoppingCartController : IController {
		private enum ActionId { ListAll, Add, Remove };
		private static Dictionary<string, ActionId> actionMap = new Dictionary<string, ActionId>();

		static ShoppingCartController() {
			actionMap.Add("", ActionId.ListAll);
			actionMap.Add("Add", ActionId.Add);
			actionMap.Add("Remove", ActionId.Remove);
		}

		private ModelStore store;
		private IShoppingCartViewFactory shoppingCartViewFactory;

		public ShoppingCartController(ModelStore store, IShoppingCartViewFactory shoppingCartViewFactory) {
			this.store = store;
			this.shoppingCartViewFactory = shoppingCartViewFactory;
		}

		public IView HandleRequest(Customer customer, string action, int id) {
			ActionId actionId;

			if (action == null) {
				action = "";
			}

			if (!actionMap.TryGetValue(action, out actionId)) {
				return null;
			}

			if (actionId != ActionId.ListAll) {
				var book = store.GetBook(id);
				if (book == null) {
					return null;
				}

				if (actionId == ActionId.Add) {
					customer.ShoppingCart.Add(book);
				} else if (actionId == ActionId.Remove) {
					if (!customer.ShoppingCart.Remove(book)) {
						return null;
					}
				}
			}

			return shoppingCartViewFactory.CreateView(customer.ShoppingCart, store);
		}
	}

}