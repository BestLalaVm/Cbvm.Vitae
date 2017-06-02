<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" 
CodeBehind="UserIntroduce.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.UserIntroduce"  Theme="StudentManage"%>
<%@ Register Src="~/Manage/Student/UserControl/UserIntroduceInfo.ascx" TagName="UserIntroduceInfo" TagPrefix="uc" %>
<asp:Content ID="content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <uc:UserIntroduceInfo ID="UserIntroduceInfo" runat="server"></uc:UserIntroduceInfo>
</asp:Content>

<asp:Content runat="server"  ContentPlaceHolderID="head">
    <style type="text/css">
        input[type='submit'], input[type='button'] {
            margin-right: 10px !important;
        }
        .body-content .main .wrap-container .wrap-right .user-introduce .action {
            padding-bottom: 20px !important;
            padding-right: 100px !important;
        }
        .body-content {
            min-height: 430px !important;
        }
            .body-content .main > .wrap-container .wrap-right .container {
                padding-top: 30px !important;
                padding-left: 60px !important;
            }
    </style>
</asp:Content>