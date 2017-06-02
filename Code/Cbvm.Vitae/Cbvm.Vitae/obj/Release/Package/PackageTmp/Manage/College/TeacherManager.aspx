<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true"
    CodeBehind="TeacherManager.aspx.cs" Inherits="Cbvm.Vitae.Manage.College.TeacherManager" Theme="UniversityManage" %>

<asp:Content runat="server" ContentPlaceHolderID="contentHead">
    <style type="text/css">
        label {
            vertical-align: bottom !important;
            line-height: 26px !important;
        }

        .body-content .main .rgMasterTable table th {
            width: 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>姓名:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="200px" CssClass="edit-text"></asp:TextBox>
                </td>
                 <th>工号:
                </th>
                <td>
                    <asp:TextBox ID="prm_TeacherNum_" runat="server" Width="200px" CssClass="edit-text"></asp:TextBox>
                </td>
<%--                <th>职能组:</th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_TeacherGroupCode_" />
                </td>--%>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="left">
        <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="新增"></asp:Button>
    </div>
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdTeacher" runat="server" AutoGenerateColumns="false" OnItemDataBound="grdTeacher_ItemDataBound"
        OnPageIndexChanged="radGrid_PageIndexChanged" OnNeedDataSource="radGrid_NeedDataSource"
        OnDeleteCommand="radGrid_DeleteCommand" OnUpdateCommand="grdTeacher_UpdateCommand">
        <MasterTableView DataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" Text="编辑" CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton CommandName="delete" Text="删除" runat="server" OnClientClick='<%# "if(!BeforeDeletd(\""+DataBinder.Eval(Container,"DataItem.TeacherNum")+"\",\""+DataBinder.Eval(Container,"DataItem.NameZh")+"\"))return false;"%>' ID="linkDelete"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TeacherNum" HeaderText="工号">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NameZh" HeaderText="中文名称">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NameEn" HeaderText="英文名称">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Telephone" HeaderText="联系电话">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GroupName" HeaderText="职能组">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="School" HeaderText="毕业学校">
                    <HeaderStyle HorizontalAlign="Left" Width="180px" />
                    <ItemStyle HorizontalAlign="Left" Width="180px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EducationName" HeaderText="学历">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="启用">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsOnline" runat="server" Checked='<%#Eval("IsOnline") %>' Enabled="false" />
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
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <div class="data-detail">
                        <div class="caption">
                            <h3>教师明细</h3>
                        </div>
                        <div class="container">
                            <table>
                                <tr>
                                    <th><span class="required">工号:</span></th>
                                    <td>
                                        <asp:HiddenField ID="hdfKey" runat="server" Value='<%#Eval("Id") %>' />
                                        <asp:TextBox ID="txtTeacherNum" runat="server" Width="240px" MaxLength="30" CssClass="edit-text"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvcTeacherNum" runat="server" ControlToValidate="txtTeacherNum"
                                            Display="Dynamic" ErrorMessage="<img src='/Image/invalid.gif' title='工号不能为空!' />"></asp:RequiredFieldValidator>
                                    </td>
                                    <th>所属系别:
                                    </th>
  <%--                                  <td class="disabled">
                                        <asp:DropDownList ID="drp_DepartCode_" runat="server" Width="182px">
                                        </asp:DropDownList>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <th><span class="required">中文名称:</span></th>
                                    <td>
                                        <asp:TextBox ID="txtNameZh" runat="server" Width="240px" MaxLength="12" CssClass="edit-text"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvcNameZh" runat="server" ControlToValidate="txtNameZh"
                                            Display="Dynamic" ErrorMessage="<img src='/Image/invalid.gif' title='中文名称不能为空!' />"></asp:RequiredFieldValidator>
                                    </td>
                                    <th>英文名称:</th>
                                    <td>
                                        <asp:TextBox ID="txtNameEn" runat="server" Width="240px" MaxLength="40" CssClass="edit-text"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>性别:</th>
                                    <td>
                                        <asp:RadioButton ID="rdoSexMale" runat="server" Checked="true" GroupName="Sex" Text="男" />
                                        <asp:RadioButton ID="rdoSexfeMale" runat="server" GroupName="Sex" Text="女" />
                                    </td>
                                    <th>籍贯:</th>
                                    <td>
                                        <asp:TextBox ID="txtNativePlace" runat="server" Width="240px" MaxLength="30" CssClass="edit-text"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th><span class="required">毕业学校:</span></th>
                                    <td>
                                        <asp:TextBox ID="txtGraduateSchool" runat="server" Width="240px" MaxLength="40" CssClass="edit-text"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvcGraduateSchool" runat="server" ControlToValidate="txtGraduateSchool"
                                            Display="Dynamic" ErrorMessage="<img src='/Image/invalid.gif' title='毕业学校不能为空!' />"></asp:RequiredFieldValidator>
                                    </td>
                                    <th>学历:</th>
                                    <td>
                                        <asp:DropDownList ID="drp_EducationName_" runat="server" Width="244PX">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <th><span class="required">专业名称:</span></th>
                                    <td>
                                        <asp:TextBox ID="txtMarjorName" runat="server" Width="240px" MaxLength="50" CssClass="edit-text"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvcMarjorName" runat="server" ControlToValidate="txtMarjorName"
                                            Display="Dynamic" ErrorMessage="<img src='/Image/invalid.gif' title='专业名称不能为空!' />"></asp:RequiredFieldValidator>
                                    </td>
                                    <th rowspan="3" valign="top">职能组:</th>
                                    <td rowspan="3">
                                        <asp:ListBox runat="server" ID="lstTeacherGroupCode" Width="300px" SelectionMode="Multiple" />
                                        <asp:CheckBox ID="chkIsOnline" runat="server" Text="启用" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>工龄:
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWorkingYear" runat="server" Width="240px" MaxLength="10" CssClass="edit-text" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>联系电话:
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTelephone" runat="server" Width="240px" MaxLength="20" CssClass="edit-text"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="action">
                        <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="保存" />
                        <asp:Button ID="btnCancel" Text="取消" runat="server" CausesValidation="False" CommandName="Cancel" />
                    </div>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
