<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Cbvm.Vitae.Manage.DepartAdmin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .action
        {
            padding-right: 125px;
        }
        .body-content {
            background-color: rgb(252, 231, 230);
        }

            .body-content .main .wrap-container .wrap-right {
                float: left;
                               width: 865px;
                margin-left: 10px;
                border-top-right-radius: 15px;
                border-top-left-radius: 15px;
                border: 2px solid rgb(169, 170, 170);
            }

            .body-content .main > .wrap-container {
                border-radius: 3px;
                background-clip: padding-box;
                margin-top: 20px;
                margin-bottom: 10px;
            }

        .xmca-toplevel .body-content {
            width: 1142px;
            margin-left: auto;
            margin-right: auto;
            border: 1px solid rgb(193, 193, 193);
        }

        .body-content .main {
            width: 1120px;
            margin-left: auto;
            margin-right: auto;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="contentUserInfo">
      <ul>
    <li><asp:Literal ID="ltlUserName" runat="server"></asp:Literal>, <asp:Literal ID="ltlWelCome" runat="server"></asp:Literal> </li>
  </ul>  
</asp:Content>