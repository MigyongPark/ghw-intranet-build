<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleTask.aspx.cs" Inherits="TaskScheduler.ScheduleTask" MasterPageFile="/umbraco/masterpages/umbracoPage.Master"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:UmbracoPanel ID="ScheduledTaskPanel" runat="server">
        <asp:ValidationSummary ID="ScheduleValSum" runat="server" />
        <asp:HiddenField ID="importIDHiddenField" runat="server" />
        
        <cc1:Pane ID="TaskNamePane" runat="server">
            <cc1:PropertyPanel ID="TaskNamePanel" runat="server" Text="Scheduled task name">
              <asp:TextBox ID="TaskNameTextBox" runat="server" MaxLength="50" CssClass="umbEditorTextField" /><asp:RequiredFieldValidator ID="TaskNameValidator" runat="server"  Text="*" ErrorMessage="Enter a scheduled task name" ControlToValidate="TaskNameTextBox" />
            </cc1:PropertyPanel>
        </cc1:Pane>

        <cc1:Pane ID="UrlNamePane" runat="server">
            <cc1:PropertyPanel ID="UrlNamePanel" runat="server" Text="Schedule url">
              <asp:TextBox ID="UrlNameTextBox" runat="server" MaxLength="500" CssClass="umbEditorTextField" /><asp:RequiredFieldValidator ID="UrlNameValidator" runat="server"  Text="*" ControlToValidate="UrlNameTextBox" />
            </cc1:PropertyPanel>
            <cc1:PropertyPanel ID="IncludeOuputPanel" runat="server" Text="Use Url output in report">
              <asp:checkbox ID="IncludeOuput" runat="Server" Text="Yes" />
            </cc1:PropertyPanel>
        </cc1:Pane>
        
         <cc1:Pane ID="NotifyPane" runat="server">
            <cc1:PropertyPanel ID="NotifyPanel" runat="server" Text="Notify emailaddress">
              <asp:TextBox ID="NotifyTextbox" runat="server" MaxLength="50" CssClass="umbEditorTextField" /><asp:RegularExpressionValidator ID="NotifyAdressValidator" runat="server" ControlToValidate="NotifyTextbox" Text="*"  ErrorMessage="Notify emailadress is invalid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </cc1:PropertyPanel>
        </cc1:Pane>
       
        <cc1:Pane ID="ExecuteEveryPane" runat="server">
            <cc1:PropertyPanel ID="ExecuteEveryPanel" runat="server" Text="Execute every">
                <asp:RadioButtonList ID="ExecuteEveryRadioList" runat="server" AutoPostBack="true" CausesValidation="false">
                </asp:RadioButtonList>
            </cc1:PropertyPanel>
        </cc1:Pane>
        
        <cc1:Pane ID="DaysPane" runat="server">
            <cc1:PropertyPanel ID="DaysProperties" runat="server" Text="Days">
                <asp:checkboxList ID="DaysCheckboxList" runat="server">
                </asp:checkboxList><asp:CustomValidator ID="DaysValidator" runat="server"  Text="*" ErrorMessage="Select at least one day" OnServerValidate="DaysValidator_Validate" />
            </cc1:PropertyPanel>
            
        </cc1:Pane>
        
        <cc1:Pane ID="TimePane" runat="server">
            
            <cc1:PropertyPanel ID="TimePanel" runat="server" Text="Time">
            <asp:DropDownList ID="HourDropdown" runat="server" /> : <asp:DropDownList ID="MinuteDropdown" runat="server" />
            </cc1:PropertyPanel>
            
        </cc1:Pane>
    
    </cc1:UmbracoPanel>
</asp:Content>