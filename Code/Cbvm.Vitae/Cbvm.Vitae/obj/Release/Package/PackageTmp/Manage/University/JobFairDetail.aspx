<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="JobFairDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.University.JobFairDetail" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-clear {
            width: 76px !important;
        }
    </style>
    <script type="text/javascript">
        function OpenEnterpriseList() {
            window.parent.ShowIframeForm("选择企业信息", "../Common/SearchEnterprisePage.aspx", "910px", "500px", {
                hideOverFlow: true,
                isShowFooter: true,
                closeCallBackHandler: function () {
                    $("#<%=btnRefreshSession.ClientID%>").trigger("click");
                    // window.location.href = window.location.href;
                }
            });
        }

        function OpenCollegeList() {
            window.parent.ShowIframeForm("选择学院信息", "../Common/SearchCollegePage.aspx", "910px", "500px", {
                hideOverFlow: true,
                isShowFooter: true,
                closeCallBackHandler: function () {
                    $("#<%=btnRefreshSession.ClientID%>").trigger("click");
                    // window.location.href = window.location.href;
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <div class="block tab-block detail">
            <table>
                <tr>
                    <th>
                        <span class="required">宣讲会名称</span>:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Name_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Name_"
                            Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>名称不能为空!</label></span>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th class="required">宣讲会时间从:
                    </th>
                    <td>
                        <div class="picker-item">
                            <telerik:RadDateTimePicker ID="dtp_BeginTime_" runat="server" Width="200px"></telerik:RadDateTimePicker>
                        </div>
      <%--                  <div class="picker-item picker-label">
                            <span class="required">到</span>
                        </div>--%>
<%--                        <div class="picker-item">
                            <telerik:RadDateTimePicker ID="dtp_EndTime_" runat="server"></telerik:RadDateTimePicker>
                        </div>--%>

                        <asp:RequiredFieldValidator ID="rqvBeginTime" runat="server" ControlToValidate="dtp_BeginTime_"
                            Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>宣讲会时间从不能为空!</label></span>"></asp:RequiredFieldValidator>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtp_EndTime_"
                            Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>结束招聘会时间从不能为空!</label></span>"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="vdtCompareData" ControlToValidate="dtp_BeginTime_" ControlToCompare="dtp_EndTime_"
                            Operator="LessThanEqual" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>招聘会开始时间不能大于结束时间!</label></span>"
                            runat="server" Display="Dynamic"></asp:CompareValidator>--%>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="required">宣讲会地址</span>:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Address_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvAddress" runat="server" ControlToValidate="txt_Address_"
                            Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>宣讲会地址!</label></span>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        <span>宣讲会内容</span>:
                    </th>
                    <td>
                        <uc:EditorControl Id="txt_Content_" runat="server" editorheight="400" editorwidth="720"></uc:EditorControl>
                    </td>
                </tr>
<%--                <tr>
                    <th class="multiLine"></th>
                    <td colspan="2">
                        <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="发布" />
                    </td>
                </tr>--%>
<%--                <tr class="enterprise-block">
                    <th valign="top">
                        <span>招聘会企业</span>:
                    </th>
                    <td>
                        <div>
                            <input type="button" value="添加企业" onclick="OpenEnterpriseList()" style="width: 90px; height: 25px;" />
                            <asp:Button ID="btnClearEnterprise" OnClick="btnClearEnterprise_Click" Text="清除企业" CssClass="btn-clear" runat="server" Width="200px" />
                        </div>
                        <div style="width: 720px;">
                            <telerik:RadGrid ID="grdEnterprise" runat="server" OnDeleteCommand="grdEnterprise_DeleteCommand"
                                AutoGenerateColumns="false" AllowCustomPaging="false" AllowPaging="false">
                                <MasterTableView DataKeyNames="Code" AllowCustomPaging="false" AllowPaging="false">
                                    <Columns>                                        
                                        <telerik:GridBoundColumn DataField="Code" HeaderText="编码">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Name" HeaderText="名称">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="delete" Text="删除" runat="server" OnClientClick='<%# "if(!BeforeDeletd(\""+DataBinder.Eval(Container,"DataItem.Code")+"\",\""+DataBinder.Eval(Container,"DataItem.Name")+"\"))return false;"%>' ID="linkDelete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="empty">
                                            没有记录
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>--%>
                <tr class="college-block">
                    <th valign="top">
                        <span>招聘会面向学院</span>:
                    </th>
                    <td>
                        <div>
                            <input type="button" value="添加学院" onclick="OpenCollegeList()" style="width: 90px; height: 25px;" />
                            <asp:Button ID="btnClearCollege" Text="清除学院" OnClick="btnClearCollege_Click" runat="server" CssClass="btn-clear" Width="200px" />
                        </div>
                        <div style="width: 720px;">
                            <telerik:RadGrid ID="grdCollege" runat="server" OnDeleteCommand="grdCollege_DeleteCommand"
                                AutoGenerateColumns="false" AllowCustomPaging="false" AllowPaging="false">
                                <MasterTableView DataKeyNames="Code" AllowCustomPaging="false" AllowPaging="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Code" HeaderText="编码">
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Name" HeaderText="名称">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton CommandName="delete" Text="删除" runat="server" OnClientClick='<%# "if(!BeforeDeletd(\""+DataBinder.Eval(Container,"DataItem.Code")+"\",\""+DataBinder.Eval(Container,"DataItem.Name")+"\"))return false;"%>' ID="linkDelete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="empty">
                                            没有记录
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>

                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="block action">
        <div style="display: none;">
            <asp:Button ID="btnRefreshSession" runat="server" OnClick="btnRefreshSession_Click" />
        </div>
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"></asp:Button>
    </div>
</asp:Content>
