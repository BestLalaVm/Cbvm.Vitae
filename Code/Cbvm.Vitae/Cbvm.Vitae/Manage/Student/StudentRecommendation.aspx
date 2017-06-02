<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentRecommendation.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.StudentRecommendation" Theme="StudentManage" %>

<%@ Register Src="~/Manage/UserControl/UploadControl.ascx" TagName="UploadControl"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">

    <div class="func-block user-recommend">
        <div class="block">
            <div class="caption">
                <h3>就业推荐表
                </h3>
            </div>
            <div class="container">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>姓名</th>
                        <td>
                            <asp:TextBox ID="txt_StudentName_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                        <th>性别</th>
                        <td>
                            <asp:TextBox ID="txt_Sex_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                        <th>出生日期</th>
                        <td colspan="3" style="border-right-width: 0px;">
                            <asp:TextBox ID="txt_Birthday_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                        <td rowspan="4" style="width: 250px; border-left-width: 1px;">
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
                        <th>生源地区</th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_FromArea_" runat="server" CssClass="customize-text" MaxLength="100"></asp:TextBox>
                        </td>
                        <th>身份证号码</th>
                        <td colspan="3">
                            <asp:TextBox ID="txt_Identity_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>学院</th>
                        <td>
                            <asp:TextBox ID="txt_CollegeName_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                        <th>专业</th>
                        <td colspan="2">
                            <asp:TextBox ID="txt_MarjorName_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                        <th>政治面貌</th>
                        <td colspan="2">
                            <asp:TextBox ID="txt_Politics_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>培养方式</th>
                        <td>
                            <asp:TextBox ID="txt_TrainingMode_" runat="server" CssClass="customize-text"  MaxLength="300"></asp:TextBox>
                        </td>
                        <th colspan="2">第二学历(学位)专业名称、层次</th>
                        <td colspan="4">
                            <asp:TextBox ID="txt_SecondDegree_" runat="server" CssClass="customize-text" MaxLength="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>健康状况</th>
                        <td>
                            <asp:TextBox ID="txt_Health_" runat="server" CssClass="customize-text" MaxLength="100"></asp:TextBox>
                        </td>
                        <th>身高</th>
                        <td>
                            <asp:TextBox ID="txt_Tall_" runat="server" CssClass="customize-text" Enabled="false"></asp:TextBox>
                        </td>
                        <th>外语水平</th>
                        <td colspan="2">
                            <asp:TextBox ID="txt_LanguageLevel_" runat="server" CssClass="customize-text" MaxLength="50"></asp:TextBox>
                        </td>
                        <th>计算机水平</th>
                        <td>
                            <asp:TextBox ID="txt_ComputerLevel_" runat="server" CssClass="customize-text" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th rowspan="2">联系方式</th>
                        <td colspan="8">
                            <span class="label" style="width: 50px;">手    机:</span><span><asp:TextBox ID="txt_Telephone_" runat="server" CssClass="customize-text" Width="200px" Enabled="false"></asp:TextBox></span>
                            <span class="label">家庭电话:</span><span><asp:TextBox ID="txt_HomePhone_" runat="server" CssClass="customize-text" Width="246px" MaxLength="30"></asp:TextBox></span>
                            <span class="label">E－mail:</span><span><asp:TextBox ID="txt_Email_" runat="server" CssClass="customize-text" Width="340px" Enabled="false"></asp:TextBox></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span class="label">家庭地址:</span><span><asp:TextBox ID="txt_HomeAddress_" runat="server" CssClass="customize-text home-address" Width="480px" MaxLength="100"></asp:TextBox></span>
                        </td>
                        <th>邮  编 :</th>
                        <td>
                            <asp:TextBox ID="txt_PostCode_" runat="server" CssClass="customize-text" Width="320px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>专    长</th>
                        <td colspan="6">
                            <asp:TextBox ID="txt_Specialty_" runat="server" CssClass="customize-text" MaxLength="200"></asp:TextBox>
                        </td>
                        <th>毕业年份</th>
                        <td>
                            <asp:TextBox ID="txt_GraduateYear_" runat="server" CssClass="customize-text" MaxLength="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <div>社会工作与担任职务:</div>
                            <div style="margin: 2px;">
                                <asp:TextBox TextMode="MultiLine" CssClass="customize-text" ID="txt_SocialWorkPosition_" runat="server" Width="1090px" Height="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <div>创新性实践经历:</div>
                            <div style="margin: 2px;">
                                <asp:TextBox TextMode="MultiLine" CssClass="customize-text" ID="txt_InnovativePracticeExperience_" runat="server" Width="1090px" Height="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <div>发表文章和从事科研项目:</div>
                            <div style="margin: 2px;">
                                <asp:TextBox TextMode="MultiLine" CssClass="customize-text" ID="txt_ScientificResearchProject_" runat="server" Width="1090px" Height="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <div>奖惩情况:</div>
                            <div style="margin: 2px;">
                                <asp:TextBox TextMode="MultiLine" CssClass="customize-text" ID="txt_PunishmentDesc_" runat="server" Width="1090px" Height="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <div>自我评价（品德、个性、业务能力、敬业精神、人际关系、适合工作等）:</div>
                            <div style="margin: 2px;">
                                <asp:TextBox TextMode="MultiLine" CssClass="customize-text" ID="txt_SelfEvaluation_" runat="server" Width="1090px" Height="150px"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
<%--                    <tr>
                        <th style="width:150px;">
                            <asp:CheckBox ID="chk_ToVerify_" runat="server" Text="申请审核" />
                        </th>
                        <td colspan="8">
                            <div><span style="width:120px;display:inline-block;padding-left:5px;">审核状态:</span><asp:TextBox ID="txt_VerifyStatus_" runat="server" CssClass="customize-text" Width="250px" Enabled="false" ></asp:TextBox>
                                <span style="width:120px;display:inline-block;padding-left:5px;">审核描述:</span><asp:TextBox ID="txt_VerifyComment_" runat="server" CssClass="customize-text" Width="400px" Enabled="false" ></asp:TextBox></div> 
                        </td>
                    </tr>--%>
                </table>
            </div>
            <div class="block action">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"/>
                <button type="button" onclick="generatePdf()" id="btnGeneratePdf" runat="server">生成PDF</button>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function generatePdf() {
            window.location.href = "/Manage/Student/GeneratePdf.aspx";
        }
    </script>
</asp:Content>
