<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="ViewMessageBox.aspx.cs" Inherits="Cbvm.Vitae.Manage.MessageBox.ViewMessageBox" Theme="BaseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Css/MessageBoxStyleSheet.css" />
    <style type="text/css">
        .msg-container {
            padding: 10px 20px 20px 20px;
        }
            .msg-container .row {
                margin: 10px auto;
            }

                .msg-container .row.msg-to, .msg-container .row.msg-receiver {
                    border-bottom: 1px solid rgb(249, 243, 243);
                    padding-bottom: 5px;
                }

                .msg-container .row .label {
                    display: inline-block;
                    width: 60px;
                    text-align: right;
                    padding-right: 5px;
                }

                .msg-container .row .content {
                    display: inline-block;
                }

            .msg-container .msg-action {
                margin-top: 10px;
            }

        .auto-list li {
            display: inline;
        }

        .auto-list {
            list-style: none;
        }

            .auto-list span {
                display: inline-block;
            }

            .auto-list .index > span {
                width: 40px;
            }

            .auto-list .code > span {
                width: 120px;
            }

            .auto-list .userType > span {
                width: 80px;
            }

            .auto-list .name > span {
                width: 150px;
            }

        .rcbList > li .index span {
            padding-left: 8px;
        }

        .rcbList > li {
            border-bottom: 1px solid #ddd;
        }

            .rcbList > li :hover {
                background-color: rgb(226, 241, 207);
            }

        .msg-body {
            padding: 10px;
            background-color: rgb(249, 243, 243);
            border: 1px solid #ddd;
        }

            .msg-body .label {
                padding-top: 5px;
                padding-bottom: 5px;
            }

            .msg-body .content {
                margin-top: 5px;
                border: 2px solid #ddd;
                min-height: 400px;
                background-color: #fff;
                padding: 2px;
            }

        input[type='button'] {
            height: 25px !important;
            width: 40px !important;
        }
    </style>
    <script type="text/javascript">
        function goBack() {
            window.location.href = "../MessageBox/MessageBoxList.aspx";
        }

        function reply() {
            window.location.href = "../MessageBox/NewMessageBox.aspx?RefId=<%=MsgId %>";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radajaxpanel id="ajaxPanel" runat="server" loadingpanelid="ajaxLoadingPanel" clientevents-onresponseend="ResponseEnd">
        <div class="msg-container">
            <div class="action-bar">
                <asp:Button runat="server" ID="btnReply" OnClientClick="reply();return;" Text="回复"/>
                <input type="button" id="btnBack" value="返回" onclick="goBack()" />
            </div>
            <div class="msg-header">
                <div class="row msg-to">
                    <div class="label">
                        发件人:
                    </div>
                    <div class="content">
                        <asp:Literal runat="server" ID="ltlSender"></asp:Literal>
                    </div>
                </div>
                <div class="row msg-receiver">
                    <div class="label">
                        收件人:
                    </div>
                    <div class="content">
                        <asp:Literal runat="server" ID="ltlReceiver"></asp:Literal>
                    </div>
                </div>
                <div class="row msg-title">
                    <div class="label">
                        主题:
                    </div>
                    <div class="content">
                        <asp:Literal runat="server" ID="ltlSubject"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="msg-body">
                <div class="label">
                    内容:
                </div>
                <div class="content">
                    <asp:Literal runat="server" ID="ltlContent"></asp:Literal>
                </div>
            </div>
        </div>
    </telerik:radajaxpanel>
</asp:Content>
