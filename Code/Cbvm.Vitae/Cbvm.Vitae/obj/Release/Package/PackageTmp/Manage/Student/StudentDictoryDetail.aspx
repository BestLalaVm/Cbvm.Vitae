<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentDictoryDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.StudentDictoryDetail" Theme="StudentManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .dictory-detail {
            width: 450px;
            margin: auto;
        }

            .body-content .main .wrap-container .wrap-right .dictory-detail th {
                vertical-align: text-top;
            }
        .body-content .main .wrap-container .wrap-right .dictory-detail .action{
            padding-right:30px;
        }
    </style>
    <script type="text/javascript">
        function initValidation() {
            $("#<%=txtName.ClientID%>").rules("add", {
                required: true,
                maxlength: 50,
                messages: {
                    required: jQuery.format("<%=Resources.ValidateResourceMsg.Required %>"),
                    maxlength: jQuery.format("<%=Resources.ValidateResourceMsg.Maxlength%>")
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="dictory-detail">
        <div class="container">
            <table>
                <tr>
                    <th>相册名称:</th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="300px" CssClass="edit-text" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>相册描述:</th>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" EmptyMessage="说说该相册的故事" Width="300px" Height="60px" CssClass="multi-edit-text" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>可见用户:</th>
                    <td>
                        <asp:CheckBoxList runat="server" ID="chk_OpenType_" RepeatDirection="Horizontal" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="action">
                            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
