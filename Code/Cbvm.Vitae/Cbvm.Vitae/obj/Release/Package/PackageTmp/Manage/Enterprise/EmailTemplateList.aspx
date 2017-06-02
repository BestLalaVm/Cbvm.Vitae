<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="EmailTemplateList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Enterprise.EmailTemplateList" Theme="EnterpriseManage" %>
<%@ Import Namespace="Presentation" %>
<%@ Import Namespace="Presentation.Enum" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .email-description {
            border: 1px solid #ddd;
            height: 80px;
        }

        .list-container .rgEditForm .action {
            margin-right: 75px;
        }

        .helptip {
            border: 1px solid #ddd;
        }

            .helptip .caption {
                border-bottom: 1px solid #ddd;
                background-color: whiteSmoke;
            }

            .helptip .container {
                padding: 5px;
            }

        .data-detail {
            width: 850px;
            margin-left: auto;
            margin-right: auto;
        }

        .body-content .main .wrap-container .wrap-right .rgEditForm .action {
            margin-right: 30px !important;
        }

        .body-content .wrap-right .list .rgEditForm .data-detail table th {
            width: 50px;
            padding-right: 10px;
        }

        .body-content .wrap-right .list .rgEditForm .data-detail {
            width: 795px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="list">
        <div class="list-container">
            <telerik:radgrid runat="server" id="grdEmailTemplate" autogeneratecolumns="False" onupdatecommand="grdEmailTemplate_ItemCommand"
                onneeddatasource="grdEmailTemplate_NeedDataSource" onitemdatabound="grdEmailTemplate_ItemDataBound">
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                            <HeaderStyle HorizontalAlign="Left" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="邮件类型">
                            <HeaderStyle HorizontalAlign="Left" Width="200px" />
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            <ItemTemplate>
                               <%#EnumHelper.GetEnumDescription(((EnterpriseEmailType)DataBinder.Eval(Container.DataItem,"EmailType"))) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Subject" HeaderText="主题">
                            <HeaderStyle HorizontalAlign="Left" Width="500px" />
                            <ItemStyle HorizontalAlign="Left" Width="500px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkDelete" runat="server" CommandName="edit" Text="编辑"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings EditFormType="Template">
                        <FormTemplate>
                            <div class="data-detail">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <div class="helptip">
                                                <div class="caption">
                                                    <h4>本模板关键字</h4>
                                                </div>
                                                <div class="container">
                                                    <asp:Literal runat="server" ID="ltlHelpTip"></asp:Literal>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>主题:</th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSubject" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Cc:</th>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCc" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th valign="top">内容:</th>
                                        <td>
                                            <uc:EditorControl ID="edtControl" runat="server" EditorWidth="725"></uc:EditorControl>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="action">
                                <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName="Update" />
                                <asp:Button ID="btnCancel" Text="取消" runat="server" CausesValidation="False" CommandName="Cancel" />
                            </div>
                        </FormTemplate>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:radgrid>
        </div>
    </div>
</asp:Content>
