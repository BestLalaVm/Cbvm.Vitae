<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="RecruitFlow.aspx.cs" 
    Inherits="Cbvm.Vitae.Manage.Enterprise.RecruitFlow"  Theme="EnterpriseManage"%>

<%@ Register Src="~/Manage/Enterprise/UserControl/RecruitFlowControl.ascx" TagName="RecruitFlowControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .flow-container {
            padding-top: 20px;
            margin-right: auto;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            background-color: rgb(252, 231, 230);
            height: 406px;
            overflow: auto;
        }
            .flow-container .flow-wrap {
                width: 1200px;
            }
            .flow-container .flow-dockzone {
                min-width: 120px !important;
                min-height: 720px !important;
                display: inline-block;
                margin-left: 5px;
                width: 165px;
            }

                .flow-container .flow-dockzone .flow-caption {
                    text-align: center;
                    margin-bottom: 5px;
                    min-width: 120px !important;
                }

                .flow-container .flow-dockzone .RadDockZone.rdVertical {
                    min-width: 150px !important;
                    min-height: 700px !important;
                    -webkit-border-radius: 3px;
                    -moz-border-radius: 3px;
                    border: 2px solid #ddd !important;
                    border-radius: 10px;
                    background-color: #fff;
                }

                    .flow-container .flow-dockzone .RadDockZone.rdVertical .RadDock {
                        margin-top: 2px;
                    }

                    .flow-container .flow-dockzone .RadDockZone.rdVertical .recruit-cell {
                        margin-top: 10px;
                    }

                        .flow-container .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-info {
                            float: left;
                            width: 70px;
                        }
                        .flow-container .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-addition {
                            width: 50px;
                            height: 50px;
                            float: right;
                            display: inline-table;
                            text-align: center;
                        }
                        .flow-container .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-image {
                            padding-bottom: 2px;
                        }
                            .flow-container .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-image img {
                                max-width: 50px;
                                max-height: 50px;
                                margin-top: 8px;
                            }

        .body-content .main .wrap-container .wrap-right .action {
            border-bottom: 1px solid #c1c1c1;
            margin-top: 0px !important;
            background-color: rgb(249, 243, 243);
            margin-bottom: 0px !important;
            padding-bottom: 2px !important;
        }
        .body-content .main {
            border: 1px solid #ddd;
            border-right-width: 0px;
        }
            .body-content .main .wrap-container .wrap-right .caption {
                display: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:RecruitFlowControl ID="rctFlowControl" runat="server"></uc:RecruitFlowControl>
</asp:Content>
