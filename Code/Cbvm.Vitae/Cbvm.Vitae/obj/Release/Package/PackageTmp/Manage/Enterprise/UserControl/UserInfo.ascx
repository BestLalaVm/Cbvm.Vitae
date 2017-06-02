<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Cbvm.Vitae.Manage.Enterprise.UserControl.UserInfo" %>
<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/UploadControl.ascx" TagName="UploadControl"
    TagPrefix="uc" %>

<div class="func-block">
    <div class="block">
        <div class="caption">
            <h3>
                <%=GetLocalResourceObject("BaseUserInfo")%>
            </h3>
        </div>
        <div class="container">
            <div class="user-left">
                <table>
                    <tr>
                        <th>编号:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Code_" runat="server" Enabled="false" ReadOnly="True" CssClass="edit-text disabled"></asp:TextBox>
                        </td>
                        <td rowspan="7" style="padding-left: 125px;" colspan="3">
                            <div class="user-right upload-image">
                                <div class="image-container">
                                    <asp:Image ID="imgSource" runat="server" />
                                </div>
                                <div>
                                    <uc:UploadControl ID="upLoadControl" runat="server" MaxWidth="210" MaxHeight="200">
                                    </uc:UploadControl>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th>单位名称:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Name_" runat="server" Enabled="false" CssClass="edit-text" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="vertical-align:text-top;">所在地区:
                        </th>
                        <td>
      <%--                      <asp:DropDownList ID="drp_CdRegionCode_" runat="server" Width="300px">
                            </asp:DropDownList>--%>
                            <div style="margin-bottom:5px;"><asp:DropDownList ID="drp_ProvinceName_" runat="server" Width="200px" OnSelectedIndexChanged="drp_ProvinceName__SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                             <div style="margin-bottom:5px;"><asp:DropDownList ID="drp_CityName_" runat="server" Width="200px" Enabled="false" OnSelectedIndexChanged="drp_CityName__SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
                        <div style="margin-bottom:5px;"><asp:DropDownList ID="drp_DistrictName_" runat="server" Width="200px" Enabled="false"></asp:DropDownList></div>
                        
                        </td>
                    </tr>
                    <tr>
                        <th>单位类型:
                        </th>
                        <td>
                            <asp:DropDownList ID="drp_EnterpriseTypeCode_" runat="server" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>行业类别:
                        </th>
                        <td>
                            <asp:DropDownList ID="drp_CdIndustryCode_" runat="server" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>单位规模:
                        </th>
                        <td>
                            <asp:DropDownList ID="drp_ScopeCode_" runat="server" Width="300px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>法人代表:
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Corporation_" runat="server" Width="516px" CssClass="edit-text" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>营业执照扫描件:
                        </th>
                        <td>
                            
                            <div class="licenseNo-image-file upload-image">
                                <div class="image-container">
                                    <asp:Image ID="imgLicenseNoImageSource" runat="server" />
                                    <asp:HyperLink ID="lnkLicenseNoImageSource" runat="server" Visible="false" Text="下载营业执照" Target="_blank"></asp:HyperLink>
                                </div>
                                <div>
                                    <uc:UploadControl ID="upLicenseNoImageLoadControl" runat="server" MaxWidth="450" MaxHeight="600">
                                    </uc:UploadControl>
                                </div>
                            </div>
                        </td>
                       
                    </tr>
                    <tr>
                        <th>组织机构代码证扫描件:</th>
                          <td>
                            <div class="organization-code-image-file upload-image">
                                <div class="image-container">
                                    <asp:Image ID="imgOrganizationCodeImageSource" runat="server" />
                                    <asp:HyperLink ID="lnkOrganizationCodeImageSource" runat="server" Visible="false" Text="下载组织机构代码" Target="_blank"></asp:HyperLink>
                                </div>
                                <div>
                                    <uc:UploadControl ID="upOrganizationCodeImageLoadControl" runat="server" MaxWidth="450" MaxHeight="600">
                                    </uc:UploadControl>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="user-right">
            </div>
        </div>
    </div>
    <div class="block">
        <div class="caption">
            <h3>
                <%=GetLocalResourceObject("BaseCompanyInfo")%>
            </h3>
        </div>
        <div class="container">
            <table>
                <tr class="">
                    <th>用户名:
                    </th>
                    <td style="width: 160px;">
                        <asp:TextBox ID="txt_UserName_" runat="server" Enabled="false" ReadOnly="True" CssClass="edit-text disabled" MaxLength="20"></asp:TextBox>
                    </td>
                    <th>Email:
                    </th>
                    <td class="email-block">
                        <asp:TextBox ID="txt_Email_" runat="server" Width="232px" CssClass="edit-text" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>联系人:
                    </th>
                    <td>
                        <asp:TextBox ID="txt_ContactName_" runat="server" CssClass="edit-text" MaxLength="30"></asp:TextBox>
                    </td>
                    <th>联系电话:
                    </th>
                    <td class="tel-block">
                        <asp:TextBox ID="txt_ContactPhone_" runat="server" Width="232px" CssClass="edit-text" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>公司网站:
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_WebSite_" runat="server" Width="516px" CssClass="edit-text" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>所在地区:
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Address_" runat="server" Width="516px" CssClass="multi-edit-text" MaxLength="500"></asp:TextBox>                       
                    </td>
                </tr>
                <tr>
                    <th>审核状态:
                    </th>
                    <td>
                        <asp:Label ID="lbl_VerifyStatus_" runat="server"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="公布企业信息" />
                    </td>
                </tr>
                <tr>
                    <th class="multiLine">公司简介:
                    </th>
                    <td colspan="3">
                        <uc:EditorControl ID="editDescription" runat="server" EditorHeight="390" EditorWidth="700" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="block action">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
    </div>
</div>
<script type="text/javascript">
    $(function () {
        setTimeout(function () {
            window.parent.ResetSize();
        }, 1000);
    })
</script>

<script type="text/javascript">
    $(function () {
        $("input.edit-text").jqxInput({ height: 25, width: 298 });
        $(".multi-edit-text").jqxInput({ height: 25, width: 700 });

        $(".email-block input.edit-text,.tel-block input.edit-text").jqxInput({ height: 25, width: 275 });

        $(".upload-edit-text").jqxInput({ height: 25, width: 120 });

        $(".disabled").jqxInput({ disabled: true });
    });
    function initValidation() {
        $("#<%=txt_Email_.ClientID%>").rules("add", {
            required: true,
            email: true,
            messages: {
                required: "<%=Resources.ValidateResourceMsg.Required%>",
                email: "<%=Resources.ValidateResourceMsg.Email%>"
            }
        });

        $("#<%=txt_ContactPhone_.ClientID%>").rules("add", {
            customPhone: true,
            messages: {
                customPhone: "<%=Resources.ValidateResourceMsg.Telephone%>"
            }
        });
    }
</script>
