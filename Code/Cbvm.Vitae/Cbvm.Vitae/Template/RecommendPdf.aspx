<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecommendPdf.aspx.cs" Inherits="Cbvm.Vitae.Template.RecommendPdf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
         /*.user-recommend {
            padding-bottom: 30px;
        }*/

            .user-recommend table {
                border: 1px solid #ddd;
            }

                .user-recommend table .label {
                    width: 80px;
                    display: inline-block;
                }

            .user-recommend .customize-text {
                height: 55px;
                width: 100%;
                border: 0px;
                font-size: 20px;
            }

        .user-recommend table th {
            font-weight: normal;
            min-width: 80px;
            padding-right: 10px;
            text-align: center;
        }

        .user-recommend table th, .user-recommend table td {
            border: 1px solid #ddd;
            border-left-width: 0px;
            border-top-width: 0px;
            /*font-size:22px;*/
        }

            .user-recommend table td:last-child {
                border-right-width: 0px;
            }

        .user-recommend .user-right {
            float: none;
            margin: 5px;
            margin-left: 20px;
        }
.user-recommend .action input[type='submit'] {
    width:80px !important;
    height:30px !important;
    font-size:16px;
}

        .recommend-preview {
            width: 1980px;
            /*height: 3600px;*/
        }

            .recommend-preview.user-recommend table th, .recommend-preview.user-recommend table td {
                border-left-width: 1px;
                border-top-width: 1px;
                vertical-align: top;
            }

            .recommend-preview .page {
                position: relative;
            }

                .recommend-preview .page > table, .recommend-preview .page .table {
                    position: absolute;
                    top: 0px;
                    height: 1350px;
                    width: 940px;
                }

                .recommend-preview .page .table {
                    border: 1px solid #ddd;
                }

                .recommend-preview .page > table.table-right {
                    right: 10px;
                }
            .recommend-preview.user-recommend table th ,.recommend-preview.user-recommend table td{
                vertical-align:central;
                vertical-align:middle;
                padding-left:2px;
            }

            .recommend-preview.user-recommend table td:last-child {
                border-right-width: 1px;
            }
            .recommend-preview.user-recommend .customize-text {
                height:52px;
                width:98%;
                margin-top:2px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="recommend-preview user-recommend">
        <div class="page1 page">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 940px;" class="table-left">
                <tr>
                    <th style="width: 60px;">姓名</th>
                    <td style="width: 120px;">
                        <asp:TextBox ID="txt_StudentName_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th style="width: 50px; min-width: 40px;">性别</th>
                    <td style="width: 80px;">
                        <asp:TextBox ID="txt_Sex_" runat="server" CssClass="customize-text" Width="80px"></asp:TextBox>
                    </td>
                    <th>出生日期</th>
                    <td colspan="3" style="border-right-width: 0px; width: 180px;">
                        <asp:TextBox ID="txt_Birthday_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <td rowspan="4" style="width: 250px; border-left-width: 1px;">
                        <div class="user-right upload-image">
                            <div class="image-container" style="border-width: 0px;">
                                <asp:Image ID="imgSource" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>生源地区</th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_FromArea_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th style="width: 120px;">身份证号码</th>
                    <td colspan="3" style="width: 180px;">
                        <asp:TextBox ID="txt_Identity_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>学院</th>
                    <td style="width:260px;">
                        <asp:TextBox ID="txt_CollegeName_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th>专业</th>
                    <td colspan="2">
                        <asp:TextBox ID="txt_MarjorName_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th style="width: 60px;">政治面貌</th>
                    <td colspan="2" style="width: 100px;">
                        <asp:TextBox ID="txt_Politics_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>培养方式</th>
                    <td>
                        <asp:TextBox ID="txt_TrainingMode_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th colspan="2">第二学历(学位)专业名称、层次</th>
                    <td colspan="4">
                        <asp:TextBox ID="txt_SecondDegree_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>健康状况</th>
                    <td>
                        <asp:TextBox ID="txt_Health_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th>身高</th>
                    <td style="width: 80px;">
                        <asp:TextBox ID="txt_Tall_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th>外语水平</th>
                    <td colspan="2" style="width: 90px;">
                        <asp:TextBox ID="txt_LanguageLevel_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                    <th style="width: 80px;">计算机水平</th>
                    <td style="width: 100px;">
                        <asp:TextBox ID="txt_ComputerLevel_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th rowspan="2">联系方式</th>
                    <td colspan="8">
                        <span class="label" style="width: 50px;display:inline-block;">手    机:</span><span style="display:inline-block;"><asp:TextBox ID="txt_Telephone_" runat="server" CssClass="customize-text" Width="200px"></asp:TextBox></span>
                        <span class="label" style="display:inline-block;">家庭电话:</span><span style="display:inline-block;"><asp:TextBox ID="txt_HomePhone_" runat="server" CssClass="customize-text" Width="200px"></asp:TextBox></span>
                        <span class="label" style="display:inline-block;">E－mail:</span><span style="display:inline-block;"><asp:TextBox ID="txt_Email_" runat="server" CssClass="customize-text" Width="200px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <span class="label" style="display:inline-block;">家庭地址:</span><span style="display:inline-block;"><asp:TextBox ID="txt_HomeAddress_" runat="server" CssClass="customize-text home-address" Width="320px"></asp:TextBox></span>
                        <span class="label" style="display:inline-block;">邮  编 :</span><span style="display:inline-block;"><asp:TextBox ID="txt_PostCode_" runat="server" CssClass="customize-text" Width="200px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <th>专    长</th>
                    <td colspan="8">
                        <asp:TextBox ID="txt_Specialty_" runat="server" CssClass="customize-text"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" style="height: 280px;vertical-align:top; ">
                        <div>社会工作与担任职务:</div>
                        <div style="margin: 2px;margin-top:10px;max-width:910px; word-break:break-all;">
                            <asp:Literal ID="txt_SocialWorkPosition_" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" style="height: 280px;vertical-align:top;">
                        <div>创新性实践经历:</div>
                        <div style="margin: 2px;margin-top:10px;max-width:910px; word-break:break-all;">
                            <asp:Literal ID="txt_InnovativePracticeExperience_" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" style="height: 280px;vertical-align:top;">
                        <div>发表文章和从事科研项目:</div>
                        <div style="margin: 2px;margin-top:10px;max-width:910px; word-break:break-all;">
                            <asp:Literal ID="txt_ScientificResearchProject_" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 940px;" class="table-right">
                <tr>
                    <td colspan="9" style="height: 280px;vertical-align:top;">
                        <div>奖惩情况:</div>
                        <div style="margin: 2px;margin-top:10px;max-width:910px; word-break:break-all;">
                            <asp:Literal ID="txt_PunishmentDesc_" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" style="height: 280px;vertical-align:top;">
                        <div>自我评价（品德、个性、业务能力、敬业精神、人际关系、适合工作等）:</div>
                        <div style="margin: 2px;margin-top:10px; max-width:910px; word-break:break-all;">
                            <asp:Literal ID="txt_SelfEvaluation_" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="9" style="height: 468px; position: relative;vertical-align:top;">
                        <div>学院对毕业生的评语及就业推荐意见:</div>
                        <div style="margin: 2px;margin-top:10px;max-width:910px; word-break:break-all;">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </div>
                        <div style="position: absolute; bottom: 80px;">
                            <div class="part-left" style="display: inline-block; width: 300px; text-align: right;">
                                <label style="margin-right: 50px;">班主任签名:</label>
                                <label style="display: block; margin-top: 20px;">20&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</label>
                            </div>
                            <div class="part-right" style="display: inline-block; width: 300px; text-align: right; margin-left: 70px;">
                                <label>学院分管领导（签名、盖章）:</label>
                                <label style="display: block; margin-top: 20px;">20&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</label>
                            </div>
                        </div>

                    </td>
                </tr>

                <tr>
                    <td colspan="9" style="height: 280px; position: relative;">
                        <div>学校推荐意见:</div>
                        <div style="margin: 2px;margin-top:10px;">
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </div>
                        <div style="position: absolute; top: 80px; left: 400px;">
                            <strong style="font-size: 24px;">同&nbsp;&nbsp;&nbsp;&nbsp;意&nbsp;&nbsp;&nbsp;&nbsp;推&nbsp;&nbsp;&nbsp;&nbsp;荐</strong>
                        </div>
                        <div style="position: absolute; bottom: 80px; right: 300px;">
                            <label>盖   章</label>
                        </div>
                        <div style="position: absolute; bottom: 30px; right: 100px;">
                            <label style="display: block; margin-top: 20px;">20&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日</label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="page2 page">
            <div class="table-left table" style="top: 1400px; height: ">
                <div style="text-align:center;margin-top:20px;">毕业生成绩表粘贴处</div>
            </div>
            <div class="table-right table" style="top: 1400px; right: 10px;">
                <div style="text-align:center;margin-top:80px;margin-right:30px;">
                    <img src="/Image/university.png" alt="" /></div>
                <div style="text-align:center;font-size:35px;margin-top:60px;margin-left:130px;font-weight:bold;">
                     <asp:Literal ID="txt_GraduateYear_" runat="server"></asp:Literal> 届 毕 业 生 就 业 推 荐 表
                </div>
                <div class="info" style="position:absolute;text-align:center;font-size:32px;bottom:140px;left:250px;">
                    <div>
                        <div style="margin:5px;display:inline-block;">姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名 ：</div><span style="border-bottom:1px solid #ddd; width:250px;display:inline-block;"></span>
                    </div>
                    <div>
                        <div style="margin:5px;display:inline-block;">专&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;业 ：</div><span style="border-bottom:1px solid #ddd; width:250px;display:inline-block;"></span>
                    </div>
                    <div>
                        <div style="margin:5px;display:inline-block;">学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;历 ：</div><span style="border-bottom:1px solid #ddd; width:250px;display:inline-block;"></span>
                    </div>
                    <div>
                        <div style="margin:5px;display:inline-block;">毕业时间：</div><span style="border-bottom:1px solid #ddd; width:250px;display:inline-block;"></span>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
