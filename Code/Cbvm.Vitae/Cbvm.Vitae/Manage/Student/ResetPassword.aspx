<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Login.Master" AutoEventWireup="true"
    CodeBehind="ResetPassword.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.ResetPassword"  Theme="Login"%>
<%@ Register Src="~/Manage/UserControl/ResetPasswordControl.ascx" TagName="ResetPasswordControl" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <uc:ResetPasswordControl ID="resetPassword" runat="server" UserType="Student"></uc:ResetPasswordControl>
</asp:Content>
