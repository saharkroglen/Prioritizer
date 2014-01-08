<%@ Page Language="C#" MasterPageFile="~/web/SiteStruct.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PrioritizerService.Login" %>


  
<asp:Content ID="Login" runat="server" ContentPlaceHolderID="cphSiteContent">
    
    <div>  
        
        <%--<asp:LoginName ID="LoginName2" runat="server" FormatString="Hi {0}!" Font-Size="XX-Large" ForeColor="DeepPink" />  --%>
        <%--<asp:LoginStatus ID="LoginStatus1" runat="server" />  --%>
      
        
        <asp:Login ID="Login2" runat="server" onauthenticate="Login2_Authenticate">
                    <LayoutTemplate>
                    <fieldset>
                        <legend>Login</legend>
                        <table >
                            <tr>
                                <td align=center>
                                    <asp:Label ID="UserNameLabel" runat="server" CssClass="fieldsetLabel">User Name:</asp:Label>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required."
                                        ToolTip="User Name is required." CssClass="BoldRed" Text="*" />
                                </td>
                            </tr>
                            <tr>
                                <td align=center>
                                    <asp:Label ID="PasswordLabel" runat="server" CssClass="fieldsetLabel">Password:</asp:Label>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." Text="*"
                                        CssClass="BoldRed" />
                                </td>
                            </tr>
                            <tr>
                                <td align=center>
                                    <asp:Label ID="CompanyLabel" runat="server" CssClass="fieldsetLabel">Company:</asp:Label>
                                </td>
                                <td>&nbsp;
                                    <asp:TextBox ID="Company" runat="server" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="companyRequired" runat="server" ControlToValidate="Company"
                                        ErrorMessage="Company is required." ToolTip="Company is required." Text="*" CssClass="BoldRed" />
                                </td>
                            </tr>
                            <tr>
                                <td align=center>
                                    <asp:Label ID="lblRemMe" runat="server" Text="Remember Me:" CssClass="fieldsetLabel"></asp:Label>
                                </td>
                                <td>&nbsp;
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="" /> &nbsp;<asp:LinkButton ID="LoginButton" CommandName="Login" runat="server" CssClass="lnkBtn"
                                        Text="Login" />
                                </td>
                            </tr>
                            
                        </table>
                        <p class="BoldRed"><asp:Literal ID="FailureText" runat="server" EnableViewState="False" /></p>
                    </fieldset>
                </LayoutTemplate>    
      
        </asp:Login>
        
        <br />
        <br />
    </div>  
   </asp:Content>