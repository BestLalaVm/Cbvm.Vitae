<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="EnterpriseRecruitBatchDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Enterprise.EnterpriseRecruitBatchDetail" Theme="EnterpriseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .flow-dockzone {
            float: left;
            width: 420px;
            margin-left: 5px;
            margin-top: 5px;
        }

            .flow-dockzone .RadDockZone.rdVertical {
                min-width: 150px !important;
                min-height: 700px !important;
                -webkit-border-radius: 3px;
                -moz-border-radius: 3px;
                border: 2px solid #ddd !important;
                border-radius: 10px;
                background-color: #fff;
            }

                .flow-dockzone .RadDockZone.rdVertical .RadDock {
                    margin-top: 2px;
                    vertical-align: top;
                }

                .flow-dockzone .RadDockZone.rdVertical .recruit-cell {
                    margin-top: 10px;
                }

                    .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-info {
                        float: left;
                        width: 70px;
                    }

                    .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-image {
                        width: 50px;
                        height: 50px;
                        float: right;
                        display: inline-table;
                        text-align: center;
                    }

                        .flow-dockzone .RadDockZone.rdVertical .recruit-cell .recruit-image img {
                            max-width: 50px;
                            max-height: 50px;
                            margin-top: 8px;
                        }

        .func-block .action {
            text-align: left !important;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #ddd;
            padding: 5px 5px 5px 20px;
            border-top-right-radius: 15px;
            border-top-left-radius: 15px;
        }

        .flow-dockzone .RadDockZone.rdVertical .RadDock {
            width: 200px !important;
            display: inline-block;
            margin-left: 2px;
        }
        .body-content .main .detail{
            background-color: rgb(252, 231, 230);
        }
        .body-content .main .wrap-container .wrap-right .action {
            background-color: rgb(249, 243, 243) !important;
        }

        .body-content .main .wrap-container .wrap-right .action {
            margin-top: 0px !important;
        }

        .body-content .main > .wrap-container .wrap-right .container {
            border-bottom: 1px solid rgb(252, 231, 230);
            border-bottom-left-radius: 10px;
            border-bottom-right-radius: 10px;
            background-color: #fff;
        }
        input[type='button'] {
            height: 25px !important;
        }
    </style>
    <script type="text/javascript">
        function goBack() {
            window.location.href = "EnterpriseRecruitBatchList.aspx";
        }

        function DragEnd(e, arg) {

        }

        $(function () {
            $("input.edit-text").jqxInput({ height: 22 });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="func-block">
        <div class="action">
            <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" />
            <input type="button" value="返回" onclick="goBack()" />
        </div>
        <div class="detail">
            <div class="container">
                <table>
                    <tr>
                        <th>批次名称:</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle" Width="220px" CssClass="edit-text" MaxLength="300"></asp:TextBox>
                        </td>
                        <th>批次描述:</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtDescription" Width="400px" CssClass="edit-text" MaxLength="2000"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ID="rqvTitle" ControlToValidate="txtTitle" Display="Dynamic" ErrorMessage="<span class='required'>批次名称不能为空</span>"></asp:RequiredFieldValidator>
                        </td>
                        <th></th>
                        <td></td>
                    </tr>
                </table>
            </div>
            <div class="list">
                <telerik:RadDockLayout runat="server" ID="radDockLayoutContainer">
                    <div class="flow-dockzone">
                        <legend>
                            <h3>应聘人列表</h3>
                        </legend>
                        <telerik:RadDockZone runat="server" ID="radOriginalRequestDockZone" UniqueName="Original" Orientation="Vertical" EnableViewState="False" />
                    </div>
                    <div class="flow-dockzone last-dockzone">
                        <legend>
                            <h3>本批次应聘人列表</h3>
                        </legend>
                        <telerik:RadDockZone runat="server" ID="radBatchRequestDockZone" UniqueName="Batch" Orientation="Vertical" EnableViewState="False" />
                    </div>
                    <asp:Panel runat="server" ID="pnl"></asp:Panel>
                    <br class="clear" />
                </telerik:RadDockLayout>
            </div>
        </div>

    </div>
</asp:Content>
