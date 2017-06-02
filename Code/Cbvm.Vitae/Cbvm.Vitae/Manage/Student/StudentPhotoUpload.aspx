<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="StudentPhotoUpload.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentPhotoUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body.master {
            margin:0px;
        }
    </style>
     <script type="text/javascript">
         var index=1;
         var FileItem = function (nameContainer) {
             var controlFile = null;
             var controlNote = null;
             var controlRemoved = null;
             var Self = this;
             var SelfWrapContainer = $("<tr class='file-item'></tr>");

             var init = function () {
                 Self.NameContainer = nameContainer;
                 index++;
                 controlFile = $("<td class='file'><input type='file' name='file"+index+"' required='required'/></td>");
                 controlNote = $("<td class='text'><input type='text' name='text"+index+"_file"+index+"' required='required'/></td>");
                 controlRemoved = $("<td class='remove'><input type='button' value='删除' style='height:25px;'/></td>");

                 controlRemoved.find("input").click(removeFunc);
                 SelfWrapContainer.append(controlFile);
                 SelfWrapContainer.append(controlNote);
                 SelfWrapContainer.append(controlRemoved);

                 Self.NameContainer.append(SelfWrapContainer);

                 controlRemoved.find("input").jqxButton();
             }

             var removeFunc = function () {
                 SelfWrapContainer.remove();
                 window.parent.ResetSize();
             }

             init();
         }

         function CreateFile() {
             if ($(".files-list .file-item").length < 15) {
                 var fileItem = new FileItem($(".upload-container .container .files-list tbody"));
             }
             window.parent.ResetSize();
         }
         function GoBack(dctId) {
             window.location.href = "StudentPhotoManage.aspx?dctId=" + dctId;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="upload-container batch-upload">
        <div class="action">
            <input type="button" value="新增" onclick="CreateFile()" />
            <input type="submit" value="开始上传" style="width: 86px;" />
            <input type="button" value="返回相册" onclick="GoBack('<%=CurrentDictoryId %>')" style="width: 86px;" />
        </div>
        <div class="container">
            <table class="files-list" style="width: 100%;">
                <tbody>
                    <tr class="file-item">
                        <td class='file'>
                            <input type="file" name="file1" required="required" /></td>
                        <td class='text'>
                            <input type="text" name="text1_file1" required='required' /></td>
                        <td class='remove'></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
