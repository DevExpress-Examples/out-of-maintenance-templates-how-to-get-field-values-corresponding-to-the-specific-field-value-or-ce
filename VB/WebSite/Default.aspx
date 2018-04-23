<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>

		<dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" 
			DataSourceID="AccessDataSource1">
			<Fields>
				<dx:PivotGridField ID="fieldProductName" AreaIndex="1" 
					FieldName="ProductName" Area="RowArea">
				</dx:PivotGridField>
				<dx:PivotGridField ID="fieldCompanyName" Area="RowArea" AreaIndex="0" 
					FieldName="CompanyName">
				</dx:PivotGridField>
				<dx:PivotGridField ID="fieldOrderDate" AreaIndex="0" 
					FieldName="OrderDate" 
					UnboundFieldName="fieldOrderDate" Area="ColumnArea" 
					GroupInterval="DateYear">
				</dx:PivotGridField>
				<dx:PivotGridField ID="fieldProductAmount" Area="DataArea" AreaIndex="0" 
					FieldName="ProductAmount">
				</dx:PivotGridField>
			</Fields>
		</dx:ASPxPivotGrid>
		<asp:AccessDataSource ID="AccessDataSource1" runat="server" 
			DataFile="~/App_Data/nwind.mdb" 

			SelectCommand="SELECT [ProductName], [CompanyName], [OrderDate], [ProductAmount] FROM [CustomerReports]">
		</asp:AccessDataSource>

	</div>
	</form>
</body>
</html>