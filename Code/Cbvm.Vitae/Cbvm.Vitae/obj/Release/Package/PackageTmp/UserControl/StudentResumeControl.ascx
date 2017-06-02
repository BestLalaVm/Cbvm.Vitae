<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentResumeControl.ascx.cs"
    Inherits="Cbvm.Vitae.UserControl.StudentResumeControl" %>
<%@ Register Src="~/Manage/UserControl/GradeControl.ascx" TagName="GradeControl"
    TagPrefix="uc" %>
<script type="text/javascript">
    $(function () {
        $(".resume .print-container span").click(function () {
            window.print();
        })
    });
</script>
<div class="func-block resume">
    <div class="caption">
        <h3>
            个人简历预览</h3>
        <div class="print-container">
            <a><span>打印简历</span></a><img src="/Image/print.png" alt="打印" />
        </div>
    </div>
    <div class="block">
        <table border="1">
            <tr>
                <th>
                    中文名称:
                </th>
                <td>
                    <asp:Literal ID="ltl_NameZh_" runat="server"></asp:Literal>
                </td>
                <th style="width:80px;">
                    英文名称:
                </th>
                <td style="width:170px;">
                    <asp:Literal ID="ltl_NameEn_" runat="server"></asp:Literal>
                </td>
                <th>
                    出生日期:
                </th>
                <td>
                    <asp:Literal ID="ltl_Birthday_" runat="server"></asp:Literal>
                </td>
                <td rowspan="8" style="width: 200px;">
                    <div class="user-image">
                        <asp:Image ID="imgPhoto" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <th>
                    身份证:
                </th>
                <td>
                    <asp:Literal ID="ltl_IDentityNum_" runat="server"></asp:Literal>
                </td>
                <th>
                    所属系别:
                </th>
                <td>
                    <asp:Literal ID="ltl_DepartName_" runat="server"></asp:Literal>
                </td>
                <th>
                    所属专业:
                </th>
                <td>
                    <asp:Literal ID="ltl_MarjorName_" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    性别:
                </th>
                <td>
                    <asp:Literal ID="ltl_Sex_" runat="server"></asp:Literal>
                </td>
                <th>
                    婚姻状况:
                </th>
                <td>
                    <asp:Literal ID="ltl_Marriage_" runat="server"></asp:Literal>
                </td>
                <th>
                    身高:
                </th>
                <td>
                    <asp:Literal ID="ltl_Tall_" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    政治面貌:
                </th>
                <td>
                    <asp:Literal ID="ltl_Politics_" runat="server"></asp:Literal>
                </td>
                <th>
                    籍贯:
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltl_NativePlace_" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th class="summary" colspan="6">
                    关于我
                </th>
            </tr>
            <tr>
                <th>
                    我的兴趣:
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltl_Interested_" runat="server"></asp:Literal>
                </td>
                 <th style="width:110px;">
                    喜欢的电影:
                </th>
                <td>
                    <asp:Literal ID="ltl_Movie_" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    喜欢的活动:
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltl_Activity_" runat="server"></asp:Literal>
                </td>
                 <th>
                    喜欢的节目:
                </th>
                <td colspan="6">
                    <asp:Literal ID="ltl_Program_" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    喜欢的音乐:
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltl_Music_" runat="server"></asp:Literal>
                </td>
                 <th>
                    喜欢的书:
                </th>
                <td colspan="6">
                    <asp:Literal ID="ltl_Book_" runat="server"></asp:Literal>
                </td>
            </tr>
   
            <tr>
                <th class="summary" colspan="7">
                    掌握技能
                </th>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Repeater ID="rptProfessional" runat="server">
                        <ItemTemplate>
                            <table class="item-block" border="1">
                                <tr>
                                    <th style="width: 100px;">
                                        获奖时间:
                                    </th>
                                    <td style="width: 150px;">
                                        <%#Eval("ObtainTime")%>
                                    </td>
                                    <th style="width: 100px;">
                                        证书名称:
                                    </th>
                                    <td>
                                        <%#Eval("Name")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        附件:
                                    </th>
                                    <td colspan="3">
                                        <asp:Repeater ID="rptProfessionalAttachment" runat="server" DataSource='<%#Eval("AttachmentList") %>'>
                                            <ItemTemplate>
                                                <div class="image-block">
                                                    <a href='<%#Eval("Path") %>' target="_blank">
                                                        <div class="image-item">
                                                            <img src='<%#Eval("ThumbPath")%>' title='<%#Eval("Description") %>' />
                                                        </div>
                                                    </a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="summary" colspan="7">
                    参与项目
                </th>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Repeater ID="rptProjet" runat="server">
                        <ItemTemplate>
                            <table class="item-block" border="1">
                                <tr>
                                    <th style="width: 100px;">
                                        项目名称:
                                    </th>
                                    <td colspan="5">
                                        <%#Eval("Name") %>
                                    </td>
                                </tr>
                                <tr style="width: 100px;">
                                    <th>
                                        职责:
                                    </th>
                                    <td colspan="3" style="width: 200px;">
                                        <%#Eval("Position")%>
                                    </td>
                                    <th style="width: 100px;">
                                        项目时间:
                                    </th>
                                    <td style="width: 270px;">
                                        从
                                        <%#Eval("BeginTime")%>
                                        到
                                        <%#Eval("EndTime")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        实用性:
                                    </th>
                                    <td colspan="5">
                                        <div class="grade-item">
                                            <uc:GradeControl ID="gdcUsableLevel" runat="server" ShowTitle="false" Enabled="false" RadRate='<%#Eval("UsableLevel") %>' >
                                            </uc:GradeControl>
                                        </div>
                                        <div class="grade-caption">
                                            技术性:</div>
                                        <div class="grade-item">
                                            <uc:GradeControl ID="gdcSkillLevel" runat="server" ShowTitle="false" Enabled="false" RadRate='<%#Eval("SkillLevel") %>'>
                                            </uc:GradeControl>
                                        </div>
                                        <div class="grade-caption">
                                            创意性:</div>
                                        <div class="grade-item">
                                            <uc:GradeControl ID="gdcDreativeLevel" runat="server" ShowTitle="false" Enabled="false" RadRate='<%#Eval("DreativeLevel") %>'>
                                            </uc:GradeControl>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 100px;">
                                        职责描述:
                                    </th>
                                    <td colspan="2">
                                        <div class="description">
                                            <%#Eval("PositionDescrition")%>
                                        </div>
                                    </td>
                                    <th>
                                        审核评语:
                                    </th>
                                    <td colspan="2">
                                        <div class="description">
                                            <%#Eval("EvaluateFromTeacher")%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">
                                        附件:
                                    </th>
                                    <td colspan="5">
                                        <asp:Repeater ID="rptProjetAttachment" runat="server" DataSource='<%#Eval("AttachmentList") %>'>
                                            <ItemTemplate>
                                                <div class="image-block">
                                                    <a href='<%#Eval("Path") %>' target="_blank">
                                                        <div class="image-item">
                                                            <img src='<%#Eval("ThumbPath")%>' title='<%#Eval("Description") %>' />
                                                        </div>
                                                    </a>
                                                    <div class="image-label">
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="clear">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
            <th class="summary" colspan="7">
                    参与活动
                </th>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Repeater ID="rptActivity" runat="server">
                        <ItemTemplate>
                            <table class="item-block" border="1">
                                <tr>
                                    <th>
                                        主题:
                                    </th>
                                    <td colspan="3">
                                        <%#Eval("Title")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        地点:
                                    </th>
                                    <td style="width: 400px;">
                                        <%#Eval("Address")%>
                                    </td>
                                    <th>
                                        活动时间:
                                    </th>
                                    <td style="width: 270px">
                                        从
                                        <%#Eval("BeginTime")%>
                                        到
                                        <%#Eval("EndTime")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <th class="summary" colspan="7">
                    实习实践
                </th>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Repeater ID="rptExercitation" runat="server">
                        <ItemTemplate>
                            <table class="item-block" border="1">
                                <tr>
                                    <th>
                                        实习单位:
                                    </th>
                                    <td colspan="3">
                                        <%#Eval("Title")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        单位地点:
                                    </th>
                                    <td style="width: 400px;">
                                        <%#Eval("Address")%>
                                    </td>
                                    <th>
                                        实习时间:
                                    </th>
                                    <td style="width: 270px">
                                        从
                                        <%#Eval("BeginTime")%>
                                        到
                                        <%#Eval("EndTime")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="summary" colspan="7">
                    个人描述
                </th>
            </tr>
            <tr>
                <td colspan="7">
                    <div class="description">
                      <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
