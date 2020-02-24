using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	class CommonHeaderView : IView {
		public readonly static CommonHeaderView Instance = new CommonHeaderView();

		private CommonHeaderView() { }

		public void RenderTo(TextWriter w) {
			w.WriteLine("<!DOCTYPE html>");
			w.WriteLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
			w.WriteLine("<head>");
			w.WriteLine("	<meta charset=\"utf-8\" />");
			w.WriteLine("	<title>Nezarka.net: Online Shopping for Books</title>");
			w.WriteLine("</head>");
			w.WriteLine("<body>");
		}
	}

	class CommonFooterView : IView {
		public readonly static CommonFooterView Instance = new CommonFooterView();

		private CommonFooterView() { }

		public void RenderTo(TextWriter w) {
			w.WriteLine("</body>");
			w.WriteLine("</html>");
		}
	}

	class InvalidRequestView : IView {
		public readonly static InvalidRequestView Instance = new InvalidRequestView();

		private InvalidRequestView() { }

		public void RenderTo(TextWriter w) {
			CommonHeaderView.Instance.RenderTo(w);
			w.WriteLine("<p>Invalid request.</p>");
			CommonFooterView.Instance.RenderTo(w);
		}
	}

}