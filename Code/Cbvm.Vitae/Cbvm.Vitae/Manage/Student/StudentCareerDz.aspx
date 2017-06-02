<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentCareerDz.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentCareerDz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block user-introduce">
        <div class="block">
            <div class="caption">
                <h3>个人工作定制
                </h3>
            </div>
            <div class="container">
                <table>
                    <tr>
                        <th></th>
                        <td><div style="color:red">以分号来分割各关键字:比如:学生想找外贸或者报关员, 则输入: 外贸;报关员</div></td>
                    </tr>
                    <tr>
                        <th>关键字:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Keyword_" runat="server" Width="530px" CssClass="edit-text" MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>工作性质:
                        </th>
                        <td>
                            <asp:ListBox ID="lstNarure" runat="server" Width="220px" SelectionMode="Multiple"></asp:ListBox>
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
                    <tr>
                        <th>工作地点:
                        </th>
                        <td>
                            <asp:Repeater ID="rptWorkPlace" runat="server" OnItemDataBound="rptWorkPlace_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="workplace-list">
                                        <thead>
                                            <th style="width:120px;text-align:left;">省份</th>
                                            <th style="width:120px;text-align:left;">城市</th>
                                            <th>
                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="新增" /></th>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width:120px;">
                                            <asp:HiddenField ID="hdfValue" runat="server" />
                                            <asp:DropDownList ID="drp_Province_" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="drp_Province__SelectedIndexChanged"></asp:DropDownList></td>
                                        <td style="width:120px;">
                                            <asp:DropDownList ID="drp_City_" runat="server" Width="120px" OnSelectedIndexChanged="drp_City__SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                        <td>
                                            <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="block action">
            <asp:Button ID="btnSave1" runat="server" OnClick="btnSave_Click" Text="保存" UseSubmitBehavior="true" />
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("input.edit-text").jqxInput({ height: 25, width: 530 });
            $(".multi-edit-text").jqxInput();
        })
    </script>

</asp:Content>
