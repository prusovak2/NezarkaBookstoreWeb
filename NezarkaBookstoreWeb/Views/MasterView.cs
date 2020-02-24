using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace NezarkaBookstoreWeb {

	interface IMasterViewFactory {
		IView CreateView(IView menuView, IView pageView);
	}

	class MasterViewFactory : IMasterViewFactory {
		public IView CreateView(IView menuView, IView pageView) {
			return new MasterView(menuView, pageView);
		}

		private class MasterView : IView {
			public IView menuView { get; private set; }
			public IView pageView { get; private set; }

			public MasterView(IView menuView, IView pageView) {
				this.menuView = menuView;
				this.pageView = pageView;
			}

			public void RenderTo(TextWriter w) {
				CommonHeaderView.Instance.RenderTo(w);

				w.WriteLine("	<style type=\"text/css\">");
				w.WriteLine("		table, th, td {");
				w.WriteLine("			border: 1px solid black;");
				w.WriteLine("			border-collapse: collapse;");
				w.WriteLine("		}");
				w.WriteLine("		table {");
				w.WriteLine("			margin-bottom: 10px;");
				w.WriteLine("		}");
				w.WriteLine("		pre {");
				w.WriteLine("			line-height: 70%;");
				w.WriteLine("		}");
				w.WriteLine("	</style>");
				w.WriteLine("	<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>");

				menuView.RenderTo(w);
				pageView.RenderTo(w);

				CommonFooterView.Instance.RenderTo(w);
			}
		}
	}

}