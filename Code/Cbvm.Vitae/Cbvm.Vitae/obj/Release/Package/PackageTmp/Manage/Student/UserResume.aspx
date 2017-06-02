<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="UserResume.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.UserResume" %>

<%@ Register Src="~/UserControl/StudentResumeControl.ascx" TagName="StudentResumeControl"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
    .body-content .main table.wrap-container .description
    {
       max-width:100% !important;
    }
   </style>
    <script type="text/javascript">
        $(window).load(function () {
            setTimeout(function () {
                window.parent.ResetSize();
            }, 2000);
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <uc:StudentResumeControl ID="StudentResumeControl" runat="server">
    </uc:StudentResumeControl>
</asp:Content>
