using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPivotGrid;
using System.Drawing;
using DevExpress.XtraPivotGrid.Data;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ASPxPivotGrid1.FieldValueTemplate = new FieldValueTemplate();
        ASPxPivotGrid1.CellTemplate = new CellTemplate();
    }
    class FieldValueTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            PivotGridFieldValueTemplateContainer c = (PivotGridFieldValueTemplateContainer)container;
            PivotGridFieldValueHtmlCell cell = c.CreateFieldValue();
            PivotFieldValueItem valueItem = c.ValueItem;
            PivotFieldValueEventArgs helperArgs = new PivotFieldValueEventArgs(valueItem);
            PivotGridField[] fields = helperArgs.GetHigherLevelFields();
            List<object> fieldValues = new List<object>();
            foreach (PivotGridField field in fields)
            {
                object currentValue = helperArgs.GetHigherLevelFieldValue(field);
                if (currentValue != null)
                    fieldValues.Add(currentValue);
            }
            
            cell.Controls.AddAt(cell.Controls.IndexOf(cell.TextControl), new MyLink(c.Text, fieldValues));
            cell.Controls.Remove(cell.TextControl);
            c.Controls.Add(cell);
        }
    }
    class CellTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            PivotGridCellTemplateContainer c = container as PivotGridCellTemplateContainer;
            List<object> fieldValues = new List<object>();
            if (c.ColumnField != null)
                fieldValues.Add(c.GetFieldValue(c.ColumnField));
            if (c.RowField != null)
                fieldValues.Add(c.GetFieldValue(c.RowField));
            
            c.Controls.Add(new MyLink(c.Text,fieldValues));
        }
    }
    
    public class MyLink : HyperLink
    {
        public MyLink(string text, List<object> values) : base()
        {
            Text = text;
            NavigateUrl = "#";

            string valuesString = string.Empty;
            foreach (object value in values)
                valuesString = valuesString + " | " + value.ToString();
            if (valuesString.Length > 3)
                valuesString = valuesString.Substring(2);
            Attributes["onclick"] = "alert('" + valuesString + "')";
            ToolTip = valuesString;
        }
    }

}
