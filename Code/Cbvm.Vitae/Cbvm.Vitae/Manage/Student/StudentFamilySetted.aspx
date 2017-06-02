<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="StudentFamilySetted.aspx.cs" 
    Inherits="Cbvm.Vitae.Manage.Student.StudentFamilySetted" Theme="StudentManage" %>

<%@ Register Src="~/Manage/Student/UserControl/UserFamilySetted.ascx" TagPrefix="uc" TagName="UserFamilySetted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        label {
            line-height: 25px;
        }
        .user-introduce .container {
            border: 1px solid #ddd;
            width: 400px;
            margin-left: 200px;
            margin-top: 20px;
        }
            .user-introduce .container > table {
                margin-top: 20px;
                margin-left: 70px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:UserFamilySetted ID="ucFamilySetted" runat="server"></uc:UserFamilySetted>
</asp:Content>
