<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentReadonlyControl.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.UserControl.AttachmentReadonlyControl" %>
<div class="attachment">
    <div class="list">
        <telerik:RadGrid ID="radAttachment" runat="server" AutoGenerateColumns="false" OnDeleteCommand="radGrid_DeleteCommand"
            AllowCustomPaging="false" AllowPaging="false">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                        <HeaderStyle Width="60px" HorizontalAlign="Left" />
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FileLabel" HeaderText="描述">
                        <HeaderStyle Width="300px" HorizontalAlign="Left" />
                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>
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
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</div>
