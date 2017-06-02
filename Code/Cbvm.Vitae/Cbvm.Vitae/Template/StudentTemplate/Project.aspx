<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master"
    AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.Project" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/ProjectDetailControl.ascx"
    TagName="ProjectDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/Slides/slides.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/Scripts/Slides/slides.jquery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <uc:ProjectDetailControl ID="projectjDetail" runat="server">
    </uc:ProjectDetailControl>
</asp:Content>