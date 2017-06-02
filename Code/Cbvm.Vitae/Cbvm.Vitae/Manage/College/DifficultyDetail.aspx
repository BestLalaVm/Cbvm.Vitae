<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="DifficultyDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.College.DifficultyDetail" %>

<%@ Register Src="~/Manage/College/UserControl/_AssessmentDetail.ascx" TagName="assessmentDetail" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
    </style>
    <script type="text/javascript">
        function TabSelectedEx(sender, args) {
            if (args.get_tab().get_value() == "2") {
                $(".action").hide();
            } else {
                $(".action").show();
            }
        }
        $(window).load(function () {
            parent.ResetSize();
        });
        $(function () {
            $("input.edit-text").jqxInput({ height: 25 });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <uc:assessmentDetail id="uc_assessment_" runat="server" SourceAssessType="DifficultyIdentified"  RedirectListPage="DifficultyManage.aspx" Caption="困难认定明细"/>
</asp:Content>
