<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="ViewJobDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.ViewJobDetail" Theme="StudentManage" %>

<%@ Register Src="~/UserControl/JobRequestDetail.ascx" TagName="JobRequestDetail" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .enterprise-detail .separate {
            background-color: rgb(252, 231, 230);
            height: 4px;
        }

        .enterprise-detail .enterprise-basic {
            margin-bottom: 20px;
            padding: 10px;
        }

            .enterprise-detail .enterprise-basic th, .enterprise-detail .enterprise-postList th {
                text-align: right;
            }

        .enterprise-detail table tr th {
            padding-top: 2px;
            padding-bottom: 2px;
        }

        .enterprise-detail th {
            width: 100px;
            font-weight: normal;
        }

        .enterprise-detail .enterprise-postList .post-item {
            margin-top: 10px;
            padding: 10px;
        }

                .enterprise-detail .enterprise-postList .post-item table thead .enterprise-head {
                    background-color: rgb(249, 243, 243);
                    border-top-right-radius: 15px;
                    border-top-left-radius: 15px;
                }
                .enterprise-detail .enterprise-postList .post-item table thead .enterprise-head .caption {
                    border-bottom-width: 0px;
                }
                .enterprise-detail .enterprise-postList .post-item table thead .enterprise-head .caption label {
                    margin-left: 5px;
                }

            .enterprise-detail .enterprise-postList .post-item > table tr > td {
                width: 650px;
            }

        .item-left {
            float: left;
        }

        .item-right {
            float: right;
            min-width: 200px;
        }

        .enterprise-head {
            padding: 5px;
        }

        .enterprise-detail .enterprise-postList .block {
            margin-top: 20px;
        }

        .enterprise-detail .enterprise-postList .post-item .block .caption {
            font-weight: bold;
        }

        .enterprise-detail .enterprise-postList .block .container {
            border: 1px solid #C1C1C1;
            padding: 10px;
        }

        .main .body .widget .container {
            margin: 10px;
        }

        .body-content .main table.wrap-container .enterprise-detail .enterprise-postList .block .description {
            min-height: 100px;
            padding: 10px;
            padding-left: 30px;
            padding-right: 30px;
        }
        .body-content .main table.wrap-container .enterprise-detail .description {
            background-color: #fff;
            border-width: 0px;
        }

        .body-content .main > table.wrap-container .wrap-right .caption {
            background-color: #fff !important;
            border-bottom: 0px solid #DDD !important;
        }
        .body-content .main .wrap-container .description {
            max-width: 896px !Important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:JobRequestDetail ID="jqDetail" runat="server"></uc:JobRequestDetail>
</asp:Content>
