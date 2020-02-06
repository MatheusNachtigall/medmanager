using System;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI;

/// <summary>
/// Estes controles devem ser utilizados se e somente se for 
/// necessária a inclusão de tags html em textos dos controles.
/// A utilização de controles do framework .NET ao invés destes 
/// controles não permitirá a utilização de tags html em textos
/// uma vez que o módulo de AntiXSS está ativado para este site.
/// Exemplo:
/// ERRADO: <asp:Label>Texto em <b>negrito</b>.</asp:Label>
/// CORRETO: <xss:HtmlLabel>Texto em <b>negrito</b>.</xss:HtmlLabel>
/// </summary>
namespace Web.UI.WebControls
{
    public class HtmlLabel : Label
    {
    }

    public class HtmlLiteral : Literal
    {
    }

    public class HtmlHyperLink : HyperLink
    {
    }

    public class HtmlLinkButton : LinkButton
    {
    }

	public class RadioButtonListControlAdapter : WebControlAdapter
	{
		protected override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(Control.CssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, Control.CssClass);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ol);
		}

		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.Indent++;

			RadioButtonList ButtonList = Control as RadioButtonList;
			if (null != ButtonList)
			{
				int i = 0;
				foreach (ListItem li in ButtonList.Items)
				{
					string itemID = string.Format("{0}_{1}", ButtonList.ClientID, i++);

					#region Li
					writer.RenderBeginTag(HtmlTextWriterTag.Li);

					#region Input
					writer.AddAttribute(HtmlTextWriterAttribute.Id, itemID);
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
					writer.AddAttribute(HtmlTextWriterAttribute.Name, ButtonList.UniqueID);
					writer.AddAttribute(HtmlTextWriterAttribute.Value, li.Value);
					if (li.Enabled == false)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					}
					if (li.Selected)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Input);
					writer.RenderEndTag(); // </input>
					#endregion

					#region Label
					writer.AddAttribute("for", itemID);
					writer.RenderBeginTag(HtmlTextWriterTag.Label);
					writer.Write(li.Text);
					writer.RenderEndTag(); // </label>
					#endregion

					writer.RenderEndTag();  // </li>
					#endregion

					Page.ClientScript.RegisterForEventValidation(ButtonList.UniqueID, li.Value);
				}
			}

			writer.Indent--;
		}
	}

	public class CheckBoxListControlAdapter : WebControlAdapter
	{
		protected override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(Control.CssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, Control.CssClass);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ol);
		}

		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.Indent++;

			CheckBoxList ButtonList = Control as CheckBoxList;
			if (null != ButtonList)
			{
				int i = 0;
				foreach (ListItem li in ButtonList.Items)
				{
					string itemID = string.Format("{0}_{1}", ButtonList.ClientID, i++);

					#region Li
					writer.RenderBeginTag(HtmlTextWriterTag.Li);

					#region Input
					writer.AddAttribute(HtmlTextWriterAttribute.Id, itemID);
					writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
					writer.AddAttribute(HtmlTextWriterAttribute.Name, ButtonList.UniqueID);
					writer.AddAttribute(HtmlTextWriterAttribute.Value, li.Value);
					if (li.Enabled == false)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					}
					if (li.Selected)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Input);
					writer.RenderEndTag(); // </input>
					#endregion

					#region Label
					writer.AddAttribute("for", itemID);
					writer.RenderBeginTag(HtmlTextWriterTag.Label);
					writer.Write(li.Text);
					writer.RenderEndTag(); // </label>
					#endregion

					writer.RenderEndTag();  // </li>
					#endregion

					Page.ClientScript.RegisterForEventValidation(ButtonList.UniqueID, li.Value);
				}
			}

			writer.Indent--;
		}
	}

}