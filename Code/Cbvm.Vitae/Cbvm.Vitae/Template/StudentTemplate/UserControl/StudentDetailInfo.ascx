<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentDetailInfo.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.StudentDetailInfo" %>
<div class="user-detail widget">
    <div class="bar">
        <div class="caption">个人详细信息</div>
    </div>
    <div class="container">
        <div class="content-block student-others">
            <div class="row-caption">我的兴趣</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlInterested"></asp:Literal>
            </div>
            <div class="row-caption">喜欢的活动</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlActivity"></asp:Literal>
            </div>
            <div class="row-caption">喜欢的音乐</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlMusic"></asp:Literal>
            </div>
            <div class="row-caption">喜欢的电影</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlMovie"></asp:Literal>
            </div>
            <div class="row-caption">喜欢的节目</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlProgram"></asp:Literal>
            </div>
            <div class="row-caption">喜欢的书</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlBook"></asp:Literal>
            </div>
            <div class="row-caption">关于我</div>
            <div class="row-content">
                <asp:Literal runat="server" ID="ltlAboutMe"></asp:Literal>
            </div>
        </div>
        <asp:PlaceHolder runat="server" ID="cntJobExpect">
            <div class="content-block">
                <div class="row-caption">求职意向</div>
                <div class="row-content">
                    <table>
                        <tr>
                            <th>工作地点:</th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobAddress"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>期望待遇:</th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobSalary"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>期望岗位:</th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobContent"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>其他要求:</th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobRequired"></asp:Literal></td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
