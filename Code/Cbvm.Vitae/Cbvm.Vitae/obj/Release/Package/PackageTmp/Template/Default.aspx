<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Master.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Cbvm.Vitae.Template.Default" %>

<%@ Register Src="~/Template/UserControl/ArticleTop20ListWidget.ascx" TagName="ArticleTop20ListWidget"
    TagPrefix="uc" %>
<%@ Register Src="~/Template/UserControl/TopHotEnterpriseList.ascx" TagName="TopHotEnterpriseList"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <uc:ArticleTop20ListWidget ID="ArticleTop20ListWidget" runat="server">
    </uc:ArticleTop20ListWidget>
    <uc:TopHotEnterpriseList ID="htNotifyEnterprise" Caption="最受关注企业" EnterpriseHotType="TopNotified"
        runat="server" />
    <uc:TopHotEnterpriseList ID="htHotJobEnterprise" Caption="最热门职位企业" EnterpriseHotType="TopHotJob"
        runat="server" />
    <uc:TopHotEnterpriseList ID="htSalaryEnterprise" Caption="薪资最高企业" EnterpriseHotType="TopSalary"
        runat="server" />
    <uc:TopHotEnterpriseList ID="TopHotEnterpriseList1" Caption="最新企业招聘" EnterpriseHotType="TopNewest"
        runat="server" />
</asp:Content>
