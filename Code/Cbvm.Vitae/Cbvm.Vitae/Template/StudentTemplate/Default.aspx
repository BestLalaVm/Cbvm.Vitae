<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.Default" %>

<%@ Register Src="~/Template/StudentTemplate/UserControlV2/NewestTopProjectWidget.ascx" TagName="NwTopProject" TagPrefix="uc1" %>
<%@ Register Src="~/Template/StudentTemplate/UserControlV2/NewestTopProfessionalWidget.ascx" TagName="NwTopProfessional" TagPrefix="uc1" %>
<%@ Register Src="~/Template/StudentTemplate/UserControlV2/NewestTopActivityWidget.ascx" TagName="NwTopActivity" TagPrefix="uc1" %>
<%@ Register Src="~/Template/StudentTemplate/UserControlV2/NewestTopExercitationWidget.ascx" TagName="NwTopExercitation" TagPrefix="uc1" %>
<%@ Register Src="~/Template/StudentTemplate/UserControlV2/NewestTopDailyEssayWidget.ascx" TagName="NwTopDailyEssay" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="/Scripts/Slides/slides.css" rel="Stylesheet" />
    <script type="text/javascript" src="/Scripts/Slides/slides.jquery.js"></script>
    <style type="text/css" media="screen">
        .slides_container {
            width: 570px;
            height: 270px;
        }

            .slides_container div {
                width: 570px;
                height: 270px;
                display: block;
            }

        #slides .next, #slides .prev {
            position: absolute;
            top: 280px;
            width: 24px;
            height: 43px;
            display: block;
            z-index: 101;
        }

        #slides .next {
            right: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="contentMain1" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
       <uc1:NwTopProject id="nwpre" runat="server"></uc1:NwTopProject>
    </div>
    <div class="grxmListContent">
        <uc1:NwTopProfessional id="nwPro" runat="server"></uc1:NwTopProfessional>
    </div>
    <div class="grxmListContent">
        <uc1:NwTopActivity id="NwTopAct" runat="server"></uc1:NwTopActivity>
    </div>
    <div class="grxmListContent">
        <uc1:NwTopExercitation id="NwTopExer" runat="server"></uc1:NwTopExercitation>
    </div>
        <div class="grxmListContent">
        <uc1:NwTopDailyEssay id="NwTopDail" runat="server"></uc1:NwTopDailyEssay>
    </div>
</asp:Content>
