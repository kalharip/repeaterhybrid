using System;
using System.Web.UI;
using System.Web.UI.WebControls;

// create a new item template
class ReadTemplate : ITemplate
{
    ListItemType templateType;

    public ReadTemplate(ListItemType type)
    {
        templateType = type;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        // new literal instance
        Literal lc = new Literal();
        LinkButton btn = new LinkButton();
        switch (templateType)
        {
            case ListItemType.Header:
                // create literal with table tags
                lc.Text = "<table border='1'><thead><tr><th>Id</th><th>Name</th><th>Price</th></tr></thead><tbody>";
                break;
            case ListItemType.Item:
                lc.Text = "<tr>";
                // on databinding call the template control method
                lc.DataBinding += new EventHandler(TemplateControl_DataBinding);
                break;
            case ListItemType.Footer:
                lc.Text = "</tbody></table>";
                break;
        }

        container.Controls.Add(lc);
    }

    private void TemplateControl_DataBinding(object sender, System.EventArgs e)
    {     
        // new literal for id
        Literal id = (Literal)sender;
        RepeaterItem container = (RepeaterItem)id.NamingContainer;
        // eval the id
        id.Text += "<td>" + DataBinder.Eval(container.DataItem, "id") + "</td>";

        // new literal for name
        Literal name = (Literal)sender;
        name.Text += "<td>" + DataBinder.Eval(container.DataItem, "name") + "</td>";

        // new literal for price
        Literal price = (Literal)sender;
        price.Text += "<td>" + DataBinder.Eval(container.DataItem, "price") + "</td>";
    }
}

namespace repeaterControlClass
{
    public class repeaterControl : CompositeControl
    {
        // new linq instance
        linqClass objLinq = new linqClass();
        
        private Repeater _rpt;

        protected override void CreateChildControls()
        {
            // define the repeater
            _rpt = new Repeater();
            _rpt.ID = "rpt";

            // bind the template class to the repeater
            _rpt.HeaderTemplate = new ReadTemplate(ListItemType.Header);
            _rpt.ItemTemplate = new ReadTemplate(ListItemType.Item);
            _rpt.FooterTemplate = new ReadTemplate(ListItemType.Footer);

            // data bind the repeater
            _rpt.DataSource = objLinq.getAllProducts();
            _rpt.DataBind();

            // define the item template
            this.Controls.Add(_rpt);
        }
    }
}

