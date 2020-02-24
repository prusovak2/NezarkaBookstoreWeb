using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	//
	// Views
	//

	interface IView {
		void RenderTo(TextWriter writer);
	}

	//
	// Controllers
	//

	interface IController {
		IView HandleRequest(Customer customer, string action, int id);
	}

}