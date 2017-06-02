<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="EnterpriseReqiredDz.aspx.cs" Inherits="Cbvm.Vitae.Manage.Enterprise.EnterpriseReqiredDz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block user-introduce">
        <div class="block">
            <div class="caption">
                <h3>人才定制
                </h3>
            </div>
            <div class="container">
                <table>
                    <tr>
                        <th></th>
                        <td>
                            <div style="color: red">以分号来分割各关键字:比如:企业想找英语专业或日语专业的学生, 则输入: 英语;日语</div>
                        </td>
                    </tr>
                    <tr>
                        <th>关键字:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Keyword_" runat="server" Width="530px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>年级(入学年份):
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Grade_" runat="server" Width="530px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>性别要求:
                        </th>
                        <td>
                            <asp:DropDownList ID="drp_Sex_" runat="server" Width="160px">
                                <asp:ListItem Text="不限" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="男" Value="1"></asp:ListItem>
                                <asp:ListItem Text="女" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>定制推送频率:
                        </th>
                        <td>
                            <asp:DropDownList ID="drp_Frquence_" runat="server" Width="220px">
                                <asp:ListItem Text="3天一次"></asp:ListItem>
                                <asp:ListItem Text="一周一次"></asp:ListItem>
                                <asp:ListItem Text="半个月一次"></asp:ListItem>
                                <asp:ListItem Text="一个月一次"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="block action">
            <asp:Button ID="btnSave1" runat="server" OnClick="btnSave1_Click" Text="保存" UseSubmitBehavior="true" />
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("input.edit-text").jqxInput({ height: 25, width: 530 });
            $(".multi-edit-text").jqxInput();
        })
    </script>

</asp:Content>

