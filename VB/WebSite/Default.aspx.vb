Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxPivotGrid
Imports System.Drawing
Imports DevExpress.XtraPivotGrid.Data


Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		ASPxPivotGrid1.FieldValueTemplate = New FieldValueTemplate()
		ASPxPivotGrid1.CellTemplate = New CellTemplate()
	End Sub
	Private Class FieldValueTemplate
		Implements ITemplate
		Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
			Dim c As PivotGridFieldValueTemplateContainer = CType(container, PivotGridFieldValueTemplateContainer)
			Dim cell As PivotGridFieldValueHtmlCell = c.CreateFieldValue()
			Dim valueItem As PivotFieldValueItem = c.ValueItem
			Dim helperArgs As New PivotFieldValueEventArgs(valueItem)
			Dim fields() As PivotGridField = helperArgs.GetHigherLevelFields()
			Dim fieldValues As New List(Of Object)()
			For Each field As PivotGridField In fields
				Dim currentValue As Object = helperArgs.GetHigherLevelFieldValue(field)
				If currentValue IsNot Nothing Then
					fieldValues.Add(currentValue)
				End If
			Next field

			cell.Controls.AddAt(cell.Controls.IndexOf(cell.TextControl), New MyLink(c.Text, fieldValues))
			cell.Controls.Remove(cell.TextControl)
			c.Controls.Add(cell)
		End Sub
	End Class
	Private Class CellTemplate
		Implements ITemplate
		Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
			Dim c As PivotGridCellTemplateContainer = TryCast(container, PivotGridCellTemplateContainer)
			Dim fieldValues As New List(Of Object)()
			If c.ColumnField IsNot Nothing Then
				fieldValues.Add(c.GetFieldValue(c.ColumnField))
			End If
			If c.RowField IsNot Nothing Then
				fieldValues.Add(c.GetFieldValue(c.RowField))
			End If

			c.Controls.Add(New MyLink(c.Text,fieldValues))
		End Sub
	End Class

	Public Class MyLink
		Inherits HyperLink
		Public Sub New(ByVal text As String, ByVal values As List(Of Object))
			MyBase.New()
			Me.Text = text
			NavigateUrl = "#"

			Dim valuesString As String = String.Empty
			For Each value As Object In values
				valuesString = valuesString & " | " & value.ToString()
			Next value
			If valuesString.Length > 3 Then
				valuesString = valuesString.Substring(2)
			End If
			Attributes("onclick") = "alert('" & valuesString & "')"
			ToolTip = valuesString
		End Sub
	End Class

End Class
