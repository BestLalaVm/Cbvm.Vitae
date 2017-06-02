<%@ control language="C#" autoeventwireup="true" codebehind="StudentAttachmentControl.ascx.cs"
    inherits="Cbvm.Vitae.Manage.UserControl.StudentAttachmentControl" %>
<%@ register src="~/Manage/UserControl/UploadLabelControl.ascx" tagname="UploadLabelControl"
    tagprefix="uc" %>
<div class="attachment">
    <asp:PlaceHolder ID="phAction" runat="server">
        <div class="action">
            <uc:uploadlabelcontrol id="UploadLabelControl" runat="server">
            </uc:uploadlabelcontrol>
        </div>
    </asp:PlaceHolder>
    <div class="list">
        <telerik:radgrid id="radAttachment" runat="server" autogeneratecolumns="false" ondeletecommand="radGrid_DeleteCommand"
            allowcustompaging="false" allowpaging="false" onitemdatabound="radAttachment_ItemDataBound">
            <MasterTableView DataKeyNames="ID" AllowCustomPaging="False" AllowPaging="False">
                <Columns>
                    <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FileLabel" HeaderText="描述">
                                            <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>
                         <HeaderStyle Width="180px" HorizontalAlign="Left" />
                        <ItemStyle Width="180px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <div class="image-item">
                                <asp:LinkButton ID="link2Attachment" runat="server">
                                    <span>
                                        <asp:Image ID="imgAttachement" runat="server" ImageUrl='<%#Eval("ImagePath") %>' />
                                    </span>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete" Text="删除" OnClientClick="if(!DeleteAttachmentItem())return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:radgrid>
    </div>
</div>
<script type="text/javascript">
    function DeleteAttachmentItem() {
        if (typeof BeforeDeleteAttachmentItem == "function") {
            return BeforeDeleteAttachmentItem();
        }
        return true;
    }
</script>
