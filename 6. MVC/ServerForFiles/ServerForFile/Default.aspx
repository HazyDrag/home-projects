<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ServerForFile.Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>FileServerWF</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
      <style type="text/css">
          #Form1 {
              height: 0px;
          }
          .auto-style1 {
              width: 202px;
          }
          .auto-style2 {
              width: 204px;
          }
          #Submit1 {
              height: 40px;
              width: 176px;
          }
          #Table1 {
              height: 133px;
              width: 235px;
          }
      </style>
  </HEAD>
  <body MS_POSITIONING="GridLayout">
<form id="Form1" method="post" enctype="multipart/form-data" runat="server">

    
    <Table ID="Table1" runat="server">
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Загрузка файла на сервер:" Font-Size="15pt"></asp:Label>
        <INPUT type=file id=File1 name=File1 runat="server" >
        <input type="submit" id="Submit1" value="Upload File" runat="server" NAME="Submit1">
                </td>
        </tr>
    </Table>
    <br>
&nbsp;<div style="height: 145px">
                    

    <table style="border-style: solid; border-width: thin; width: 808px; height: 340px; vertical-align: top;" align="left">
        <tr>
            <td valign="top" class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Фотографии:" Font-Size="15pt"></asp:Label>
     <asp:Repeater ID="Repeater1" runat="server" OnPreRender="Repeater1_PreRender">
                        <ItemTemplate> 
    <table style="border-style: solid; border-width: thin; height: 100px; width: 300px" bgcolor="#CCCCCC">
        <tr>
            <td>                
                <img alt="" src="/Files/system/i.png" />
                </td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# "~/Files/Images/" + Eval("Name") %>' /> 
            </td>
        </tr>
        </table>
                            </ItemTemplate>
         </asp:Repeater>

                </td>
            <td valign="top" class="auto-style1">
                    
                <asp:Label ID="Label2" runat="server" Text="Документы:" Font-Size="15pt"></asp:Label>
                    
     <asp:Repeater ID="Repeater2" runat="server" OnPreRender="Repeater2_PreRender">
                        <ItemTemplate> 
    <table style="border-style: solid; border-width: thin; height: 100px; width: 300px" bgcolor="#CCCCCC">
        <tr>

            <td>                
                <img alt="" src="/Files/system/d.png" />
                   </td><td>
                <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# "~/Files/Documents/" + Eval("Name") %>' /> 
            </td>
        </tr>
        </table>
                            </ItemTemplate>
         </asp:Repeater>
                </td>

            <td valign="top">
                <asp:Label ID="Label3" runat="server" Text="Архивы:" Font-Size="15pt"></asp:Label>
     <asp:Repeater ID="Repeater3" runat="server" OnPreRender="Repeater3_PreRender">
                        <ItemTemplate> 
    <table style=" border-style: solid; border-width: thin; height: 100px; width: 300px" bgcolor="#CCCCCC">
        <tr>
            <td>                
                <img alt="" src="/Files/system/a.png" />
                   </td><td>
                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# "~/Files/Archives/" + Eval("Name") %>' /> 
            </td>
        </tr>
        </table>
                            </ItemTemplate>
         </asp:Repeater>
            </td>

            <td valign="top">
                <asp:Label ID="Label4" runat="server" Text="Другое:" Font-Size="15pt"></asp:Label>
     <asp:Repeater ID="Repeater4" runat="server" OnPreRender="Repeater4_PreRender">
                        <ItemTemplate> 
    <table style=" border-style: solid; border-width: thin; height: 100px; width: 300px" bgcolor="#CCCCCC">
        <tr>
            <td>                
                <img alt="" src="/Files/system/o.png" />
                    </td><td>
                <asp:HyperLink ID="HyperLink4" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%# "~/Files/Other/" + Eval("Name") %>' /> 
            </td>
        </tr>
        </table>
                            </ItemTemplate>
         </asp:Repeater>
            </td>
            </tr>
        </table>

  </div>
</form>		
  </body>
</HTML>