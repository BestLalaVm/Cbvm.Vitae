﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="DifficultyDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.DifficultyDetail" %>

<%@ Register Src="~/Manage/UserControl/_AssessmentReadonlyDetail.ascx" TagName="assessmentDetail" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .container table th {
            width: 80px;
        }

        .body-content .main table.wrap-container .wrap-right .caption {
            margin-top: 10px;
        }

        .body-content .main table.wrap-container .wrap-right .container .description {
            width: 775px;
            max-width: 100%;
        }

        .body-content .main table.wrap-container tr td.wrap-right .detail .func-block .tab-block.detail {
            padding: 10px 15px !important;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <uc:assessmentDetail id="uc_assessment_" runat="server"  SourceAssessType="DifficultyIdentified"  RedirectListPage="DifficultyManage.aspx" Caption="困难认定明细"/>
</asp:Content>
