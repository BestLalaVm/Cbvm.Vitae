<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="ChildPunishDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Family.Children.ChildPunishDetail" %>
<%@ Register Src="~/Manage/UserControl/_AssessmentReadonlyDetail.ascx" TagName="assessmentDetail" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <uc:assessmentDetail id="uc_assessment_" runat="server"  SourceAssessType="Punishment"  RedirectListPage="PunishManage.aspx" Caption="惩处明细"/>
</asp:Content>
