<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs"  MasterPageFile="/umbraco/masterpages/umbracoPage.Master" ValidateRequest="false" Inherits="TaskScheduler.Settings" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:UmbracoPanel ID="UmbracoPanel" runat="server">
        <cc1:Pane ID="Pane1" runat="server">
         <cc1:PropertyPanel ID="EmailFromAdressPropertyPanel" runat="server" Text="From address"><asp:TextBox ID="EmailFromAdressTextBox" runat="server" Width="300" CssClass="guiInputText" /> </cc1:PropertyPanel>
         <cc1:PropertyPanel ID="EmaillSubjectPropertyPanel" runat="server" Text="Email Subject"><asp:TextBox ID="EmaillSubjectTextbox" runat="server" Width="300" CssClass="guiInputText" /> </cc1:PropertyPanel>
         <cc1:PropertyPanel ID="EmailBodyPropertyPanel" runat="server" Text="Email Template">&nbsp;</cc1:PropertyPanel>
         <cc1:PropertyPanel ID="PropertyPanel1" runat="server" Text=""><asp:TextBox ID="EmailBodyTextBox" Width="800" Height="500" runat="server"/></cc1:PropertyPanel>
        </cc1:Pane>
    </cc1:UmbracoPanel>
</asp:Content>
