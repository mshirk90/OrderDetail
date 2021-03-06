﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Invoice_Report._Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:TextBox ID="txtOrderId" runat="server" placeholder="Enter Order Id"></asp:TextBox>
    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click" />
    <rsweb:ReportViewer ID="rvInvoice" runat="server"></rsweb:ReportViewer>

</asp:Content>
